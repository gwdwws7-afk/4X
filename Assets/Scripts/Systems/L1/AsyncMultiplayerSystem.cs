using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.L1
{
    public enum AsyncRoomState
    {
        WaitingForOpponent = 0,
        VashidTurn = 1,
        AureanTurn = 2,
        Completed = 3,
        Abandoned = 4
    }

    [Serializable]
    public class AsyncTurnPacket
    {
        public int TurnIndex = 1;
        public string ActingFactionId = GameIds.Faction.Vashid;
        public string[] ActionIds = Array.Empty<string>();
        public string PriorSnapshotHash;
        public long SubmittedUtcTicks;
    }

    [Serializable]
    public class AsyncRoomData
    {
        public string RoomId;
        public string HostPlayerId;
        public string GuestPlayerId;
        public AsyncRoomState RoomState;
        public string PendingFactionId;
        public int LastResolvedTurn;
        public long CreatedUtcTicks;
        public long UpdatedUtcTicks;
        public List<AsyncTurnPacket> History = new List<AsyncTurnPacket>();
    }

    [Serializable]
    internal class AsyncRoomStore
    {
        public List<AsyncRoomData> Rooms = new List<AsyncRoomData>();
    }

    public class AsyncMultiplayerSystem : GameSystem
    {
        private const string kMetaFolderName = "Meta";
        private const string kDefaultSaveFileName = "l1_async_rooms.json";

        [Header("L1 Storage")]
        public string SaveFileName = kDefaultSaveFileName;
        public bool AutoLoadOnInitialize = true;

        private readonly List<AsyncRoomData> _rooms = new List<AsyncRoomData>();
        private string _storePath;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);

            _storePath = ResolveStorePath();
            if (AutoLoadOnInitialize)
            {
                LoadStore();
            }

            Events.ActionLogAdded("L1", $"Async multiplayer initialized (rooms={_rooms.Count})", FeedbackSeverity.Info);
        }

        public AsyncRoomData CreateRoom(string hostPlayerId, string guestPlayerId = null, bool hostStarts = true)
        {
            string normalizedHost = string.IsNullOrWhiteSpace(hostPlayerId) ? "Host" : hostPlayerId.Trim();
            string normalizedGuest = string.IsNullOrWhiteSpace(guestPlayerId) ? string.Empty : guestPlayerId.Trim();

            var room = new AsyncRoomData
            {
                RoomId = $"room_{Guid.NewGuid():N}",
                HostPlayerId = normalizedHost,
                GuestPlayerId = normalizedGuest,
                RoomState = string.IsNullOrEmpty(normalizedGuest)
                    ? AsyncRoomState.WaitingForOpponent
                    : (hostStarts ? AsyncRoomState.VashidTurn : AsyncRoomState.AureanTurn),
                PendingFactionId = hostStarts ? GameIds.Faction.Vashid : GameIds.Faction.Aurean,
                LastResolvedTurn = 0,
                CreatedUtcTicks = DateTime.UtcNow.Ticks,
                UpdatedUtcTicks = DateTime.UtcNow.Ticks
            };

            _rooms.Add(room);
            PersistStore();

            Events.NotificationAdded(
                "L1",
                $"Async room created: {room.RoomId} ({room.RoomState})",
                FeedbackSeverity.Info);

            return CloneRoom(room);
        }

        public bool TryJoinRoom(string roomId, string guestPlayerId, out string error)
        {
            error = string.Empty;
            AsyncRoomData room = FindRoom(roomId);
            if (room == null)
            {
                error = "room_not_found";
                return false;
            }

            if (room.RoomState != AsyncRoomState.WaitingForOpponent)
            {
                error = "room_not_waiting";
                return false;
            }

            if (string.IsNullOrWhiteSpace(guestPlayerId))
            {
                error = "invalid_guest_player";
                return false;
            }

            room.GuestPlayerId = guestPlayerId.Trim();
            room.RoomState = room.PendingFactionId == GameIds.Faction.Aurean
                ? AsyncRoomState.AureanTurn
                : AsyncRoomState.VashidTurn;
            room.UpdatedUtcTicks = DateTime.UtcNow.Ticks;

            PersistStore();
            Events.NotificationAdded("L1", $"Opponent joined room {room.RoomId}", FeedbackSeverity.Info);
            return true;
        }

        public bool TrySubmitTurn(string roomId, AsyncTurnPacket packet, out string error)
        {
            error = string.Empty;
            AsyncRoomData room = FindRoom(roomId);
            if (room == null)
            {
                error = "room_not_found";
                return false;
            }

            if (room.RoomState == AsyncRoomState.Completed || room.RoomState == AsyncRoomState.Abandoned)
            {
                error = "room_closed";
                return false;
            }

            if (room.RoomState == AsyncRoomState.WaitingForOpponent)
            {
                error = "opponent_not_joined";
                return false;
            }

            if (packet == null)
            {
                error = "packet_null";
                return false;
            }

            packet.ActingFactionId = GameIds.ResolveFactionId(packet.ActingFactionId);
            if (!string.Equals(packet.ActingFactionId, room.PendingFactionId, StringComparison.Ordinal))
            {
                error = "unexpected_faction_turn";
                return false;
            }

            int expectedIndex = room.History.Count + 1;
            if (packet.TurnIndex <= 0)
            {
                packet.TurnIndex = expectedIndex;
            }

            if (packet.TurnIndex != expectedIndex)
            {
                error = "turn_index_mismatch";
                return false;
            }

            packet.SubmittedUtcTicks = DateTime.UtcNow.Ticks;
            room.History.Add(ClonePacket(packet));

            room.PendingFactionId = string.Equals(room.PendingFactionId, GameIds.Faction.Vashid, StringComparison.Ordinal)
                ? GameIds.Faction.Aurean
                : GameIds.Faction.Vashid;

            if (room.PendingFactionId == GameIds.Faction.Vashid)
            {
                room.LastResolvedTurn++;
            }

            if (room.LastResolvedTurn >= GameConfig.kMaxTurns)
            {
                room.RoomState = AsyncRoomState.Completed;
                Events.GameEnded("l1_async_match_completed");
                Events.GlobalAlertRaised(
                    $"Async room {room.RoomId} reached turn limit ({GameConfig.kMaxTurns}).",
                    FeedbackSeverity.Info);
            }
            else
            {
                room.RoomState = room.PendingFactionId == GameIds.Faction.Aurean
                    ? AsyncRoomState.AureanTurn
                    : AsyncRoomState.VashidTurn;
            }

            room.UpdatedUtcTicks = DateTime.UtcNow.Ticks;
            PersistStore();

            Events.ActionLogAdded(
                "L1",
                $"Async packet accepted: room={room.RoomId}, packet={packet.TurnIndex}, next={room.PendingFactionId}",
                FeedbackSeverity.Info);

            return true;
        }

        public bool TryAbandonRoom(string roomId, out string error)
        {
            error = string.Empty;
            AsyncRoomData room = FindRoom(roomId);
            if (room == null)
            {
                error = "room_not_found";
                return false;
            }

            if (room.RoomState == AsyncRoomState.Completed || room.RoomState == AsyncRoomState.Abandoned)
            {
                error = "room_closed";
                return false;
            }

            room.RoomState = AsyncRoomState.Abandoned;
            room.UpdatedUtcTicks = DateTime.UtcNow.Ticks;
            PersistStore();

            Events.AlertAdded("L1", $"Async room abandoned: {room.RoomId}", FeedbackSeverity.Warning);
            return true;
        }

        public AsyncRoomData[] GetAllRooms()
        {
            var snapshot = new AsyncRoomData[_rooms.Count];
            for (int i = 0; i < _rooms.Count; i++)
            {
                snapshot[i] = CloneRoom(_rooms[i]);
            }

            return snapshot;
        }

        public AsyncRoomData GetRoom(string roomId)
        {
            AsyncRoomData room = FindRoom(roomId);
            return room != null ? CloneRoom(room) : null;
        }

        public string GetStorePathForDebug()
        {
            return _storePath ?? ResolveStorePath();
        }

        public void ClearAllRoomsForTestOnly()
        {
            _rooms.Clear();
            PersistStore();
        }

        private AsyncRoomData FindRoom(string roomId)
        {
            if (string.IsNullOrWhiteSpace(roomId))
            {
                return null;
            }

            for (int i = 0; i < _rooms.Count; i++)
            {
                if (string.Equals(_rooms[i].RoomId, roomId.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return _rooms[i];
                }
            }

            return null;
        }

        private void LoadStore()
        {
            _rooms.Clear();

            string path = ResolveStorePath();
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                string json = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return;
                }

                var store = JsonUtility.FromJson<AsyncRoomStore>(json);
                if (store?.Rooms == null)
                {
                    return;
                }

                for (int i = 0; i < store.Rooms.Count; i++)
                {
                    AsyncRoomData room = store.Rooms[i];
                    if (room == null || string.IsNullOrWhiteSpace(room.RoomId))
                    {
                        continue;
                    }

                    if (room.History == null)
                    {
                        room.History = new List<AsyncTurnPacket>();
                    }

                    _rooms.Add(room);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[L1] Failed to load async room store: {ex.Message}");
            }
        }

        private void PersistStore()
        {
            string path = ResolveStorePath();
            EnsureStoreDirectory(path);

            try
            {
                var store = new AsyncRoomStore { Rooms = _rooms };
                string json = JsonUtility.ToJson(store, true);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[L1] Failed to persist async room store: {ex.Message}");
            }
        }

        private string ResolveStorePath()
        {
            string fileName = string.IsNullOrWhiteSpace(SaveFileName)
                ? kDefaultSaveFileName
                : SaveFileName.Trim();

            string directory = Path.Combine(Application.persistentDataPath, kMetaFolderName);
            return Path.Combine(directory, fileName);
        }

        private static void EnsureStoreDirectory(string fullPath)
        {
            string directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static AsyncTurnPacket ClonePacket(AsyncTurnPacket packet)
        {
            if (packet == null)
            {
                return null;
            }

            return new AsyncTurnPacket
            {
                TurnIndex = packet.TurnIndex,
                ActingFactionId = packet.ActingFactionId,
                ActionIds = packet.ActionIds != null ? (string[])packet.ActionIds.Clone() : Array.Empty<string>(),
                PriorSnapshotHash = packet.PriorSnapshotHash,
                SubmittedUtcTicks = packet.SubmittedUtcTicks
            };
        }

        private static AsyncRoomData CloneRoom(AsyncRoomData room)
        {
            if (room == null)
            {
                return null;
            }

            var clone = new AsyncRoomData
            {
                RoomId = room.RoomId,
                HostPlayerId = room.HostPlayerId,
                GuestPlayerId = room.GuestPlayerId,
                RoomState = room.RoomState,
                PendingFactionId = room.PendingFactionId,
                LastResolvedTurn = room.LastResolvedTurn,
                CreatedUtcTicks = room.CreatedUtcTicks,
                UpdatedUtcTicks = room.UpdatedUtcTicks,
                History = new List<AsyncTurnPacket>()
            };

            for (int i = 0; i < room.History.Count; i++)
            {
                clone.History.Add(ClonePacket(room.History[i]));
            }

            return clone;
        }
    }
}

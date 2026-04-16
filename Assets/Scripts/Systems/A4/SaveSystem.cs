using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using EventideAge.Core;
using EventideAge.Systems.D2;
using EventideAge.Systems.D3;
using EventideAge.Systems.D4;
using EventideAge.Systems.D6;
using EventideAge.Systems.J;

namespace EventideAge.Systems.A4
{
    public class SaveSystem : GameSystem
    {
        private const string SaveFolder = "Saves";
        private const string Extension = ".json";
        
        private List<GameSystem> _allSystems = new List<GameSystem>();
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            EnsureSaveDirectoryExists();
            AutoRegisterSystemsForSave();
        }
        
        public void RegisterSystemForSave(GameSystem system)
        {
            if (system != null && !_allSystems.Contains(system))
            {
                _allSystems.Add(system);
            }
        }

        private void AutoRegisterSystemsForSave()
        {
            var manager = GameManager.Instance;
            if (manager == null)
            {
                manager = UnityEngine.Object.FindObjectOfType<GameManager>();
            }

            if (manager == null || manager.Systems == null)
                return;

            foreach (var system in manager.Systems)
            {
                if (system == null || system == this)
                    continue;

                RegisterSystemForSave(system);
            }
        }
        
        private void EnsureSaveDirectoryExists()
        {
            string path = GetSaveDirectoryPath();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        
        private string GetSaveDirectoryPath()
        {
            return Path.Combine(Application.persistentDataPath, SaveFolder);
        }
        
        private string GetSaveFilePath(string saveName)
        {
            return Path.Combine(GetSaveDirectoryPath(), saveName + Extension);
        }
        
        public bool SaveGame(string saveName)
        {
            try
            {
                var saveData = new GameSaveData
                {
                    SaveVersion = 1,
                    Timestamp = DateTime.Now.Ticks,
                    GameStateJson = JsonUtility.ToJson(State, true),
                    Metadata = $"Turn {State.CurrentTurn}, Phase {State.CurrentPhaseIndex}"
                };
                
                CaptureSystemStates(saveData);
                
                string path = GetSaveFilePath(saveName);
                string json = JsonUtility.ToJson(saveData, true);
                File.WriteAllText(path, json);
                Debug.Log($"[SaveSystem] Game saved: {saveName} at {path}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Save failed: {e.Message}");
                return false;
            }
        }
        
        public bool LoadGame(string saveName)
        {
            try
            {
                string path = GetSaveFilePath(saveName);
                if (!File.Exists(path))
                {
                    Debug.LogWarning($"[SaveSystem] Save file not found: {saveName}");
                    return false;
                }
                
                string json = File.ReadAllText(path);
                GameSaveData saveData = new GameSaveData();
                JsonUtility.FromJsonOverwrite(json, saveData);
                
                if (saveData == null)
                {
                    json = File.ReadAllText(path);
                    JsonUtility.FromJsonOverwrite(json, State);
                    Debug.Log($"[SaveSystem] Loaded legacy save format: {saveName}");
                    return true;
                }
                
                if (!string.IsNullOrEmpty(saveData.GameStateJson))
                {
                    JsonUtility.FromJsonOverwrite(saveData.GameStateJson, State);
                }
                
                RestoreSystemStates(saveData);
                
                Debug.Log($"[SaveSystem] Game loaded: {saveName}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Load failed: {e.Message}");
                return false;
            }
        }
        
        private void CaptureSystemStates(GameSaveData saveData)
        {
            CaptureD2State(saveData);
            CaptureD3State(saveData);
            CaptureD4State(saveData);
            CaptureD6State(saveData);
            CaptureJState(saveData);
        }
        
        private void RestoreSystemStates(GameSaveData saveData)
        {
            RestoreD2State(saveData);
            RestoreD3State(saveData);
            RestoreD4State(saveData);
            RestoreD6State(saveData);
            RestoreJState(saveData);
        }
        
        private void CaptureD2State(GameSaveData saveData)
        {
            var d2 = GetSystem<MilitaryPoliticalLinkageSystem>();
            if (d2 == null) return;
            
            var data = new D2SaveData();
            
            var occupiedField = typeof(MilitaryPoliticalLinkageSystem).GetField("_occupiedNodesUnderDigestion", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (occupiedField != null)
            {
                data.OccupiedNodesUnderDigestion = occupiedField.GetValue(d2) as List<string> ?? new List<string>();
            }
            
            var turnsField = typeof(MilitaryPoliticalLinkageSystem).GetField("_digestionTurnsRemaining",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (turnsField != null)
            {
                data.DigestionTurnsRemaining = turnsField.GetValue(d2) as Dictionary<string, int> ?? new Dictionary<string, int>();
            }
            
            saveData.Systems.Add(new SystemSaveData
            {
                SystemTypeName = "MilitaryPoliticalLinkageSystem",
                JsonData = JsonUtility.ToJson(data)
            });
        }
        
        private void RestoreD2State(GameSaveData saveData)
        {
            var d2 = GetSystem<MilitaryPoliticalLinkageSystem>();
            if (d2 == null) return;
            
            var systemData = saveData.Systems.Find(s => s.SystemTypeName == "MilitaryPoliticalLinkageSystem");
            if (systemData == null || string.IsNullOrEmpty(systemData.JsonData)) return;
            
            D2SaveData data = new D2SaveData();
            JsonUtility.FromJsonOverwrite(systemData.JsonData, data);
            if (data == null) return;
            
            var occupiedField = typeof(MilitaryPoliticalLinkageSystem).GetField("_occupiedNodesUnderDigestion",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (occupiedField != null && data.OccupiedNodesUnderDigestion != null)
            {
                occupiedField.SetValue(d2, data.OccupiedNodesUnderDigestion);
            }
            
            var turnsField = typeof(MilitaryPoliticalLinkageSystem).GetField("_digestionTurnsRemaining",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (turnsField != null && data.DigestionTurnsRemaining != null)
            {
                turnsField.SetValue(d2, data.DigestionTurnsRemaining);
            }
        }
        
        private void CaptureD3State(GameSaveData saveData)
        {
            var d3 = GetSystem<ProxyCivilAffairsSystem>();
            if (d3 == null) return;
            
            var data = new D3SaveData();
            
            var regionsField = typeof(ProxyCivilAffairsSystem).GetField("_proxyRegions",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (regionsField != null)
            {
                var regions = regionsField.GetValue(d3) as Dictionary<string, ProxyControlRegion>;
                if (regions != null)
                {
                    foreach (var kvp in regions)
                    {
                        data.ProxyRegions.Add(new ProxyRegionSave
                        {
                            NodeId = kvp.Key,
                            ControllerFactionId = kvp.Value.ControllerFactionId,
                            Stability = kvp.Value.Stability,
                            PublicSupport = kvp.Value.PublicSupport,
                            GovernanceLevel = (int)kvp.Value.GovernanceLevel,
                            TurnsInCurrentState = kvp.Value.TurnsInCurrentState
                        });
                    }
                }
            }
            
            saveData.Systems.Add(new SystemSaveData
            {
                SystemTypeName = "ProxyCivilAffairsSystem",
                JsonData = JsonUtility.ToJson(data)
            });
        }
        
        private void RestoreD3State(GameSaveData saveData)
        {
            var d3 = GetSystem<ProxyCivilAffairsSystem>();
            if (d3 == null) return;
            
            var systemData = saveData.Systems.Find(s => s.SystemTypeName == "ProxyCivilAffairsSystem");
            if (systemData == null || string.IsNullOrEmpty(systemData.JsonData)) return;
            
            D3SaveData data = new D3SaveData();
            JsonUtility.FromJsonOverwrite(systemData.JsonData, data);
            if (data == null || data.ProxyRegions == null) return;
            
            var regionsField = typeof(ProxyCivilAffairsSystem).GetField("_proxyRegions",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (regionsField != null)
            {
                var regions = new Dictionary<string, ProxyControlRegion>();
                foreach (var regionSave in data.ProxyRegions)
                {
                    regions[regionSave.NodeId] = new ProxyControlRegion
                    {
                        NodeId = regionSave.NodeId,
                        ControllerFactionId = regionSave.ControllerFactionId,
                        Stability = regionSave.Stability,
                        PublicSupport = regionSave.PublicSupport,
                        GovernanceLevel = (GovernanceLevel)regionSave.GovernanceLevel,
                        TurnsInCurrentState = regionSave.TurnsInCurrentState
                    };
                }
                regionsField.SetValue(d3, regions);
            }
        }
        
        private void CaptureD4State(GameSaveData saveData)
        {
            var d4 = GetSystem<NuclearDeterrenceSystem>();
            if (d4 == null) return;
            
            var state = d4.GetState();
            var save = new D4SaveData
            {
                WarheadCount = state.WarheadCount,
                DisplayCooldown = state.DisplayCooldown,
                IsFullWarLockActive = state.IsFullWarLockActive,
                FullWarLockTurnsRemaining = state.FullWarLockTurnsRemaining,
                CapabilityLevel = (int)state.CapabilityLevel
            };
            
            saveData.Systems.Add(new SystemSaveData
            {
                SystemTypeName = "NuclearDeterrenceSystem",
                JsonData = JsonUtility.ToJson(save)
            });
        }
        
        private void RestoreD4State(GameSaveData saveData)
        {
            var d4 = GetSystem<NuclearDeterrenceSystem>();
            if (d4 == null) return;
            
            var systemData = saveData.Systems.Find(s => s.SystemTypeName == "NuclearDeterrenceSystem");
            if (systemData == null || string.IsNullOrEmpty(systemData.JsonData)) return;
            
            D4SaveData save = new D4SaveData();
            JsonUtility.FromJsonOverwrite(systemData.JsonData, save);
            if (save == null) return;
            
            var state = new NuclearDeterrenceState
            {
                WarheadCount = save.WarheadCount,
                DisplayCooldown = save.DisplayCooldown,
                IsFullWarLockActive = save.IsFullWarLockActive,
                FullWarLockTurnsRemaining = save.FullWarLockTurnsRemaining,
                CapabilityLevel = (NuclearCapabilityLevel)save.CapabilityLevel
            };
            d4.RestoreState(state);
        }
        
        private void CaptureD6State(GameSaveData saveData)
        {
            var d6 = GetSystem<MilitaryTechSystem>();
            if (d6 == null) return;
            
            var research = d6.GetCurrentResearch();
            var data = new D6SaveData
            {
                CompletedTechs = new List<string>(d6.GetCompletedTechs())
            };
            
            if (research != null)
            {
                data.CurrentResearchTechId = research.TechId;
                data.CurrentResearchProgress = research.Progress;
                data.CurrentResearchTurn = research.CurrentTurn;
                data.IsCurrentResearchPaused = research.IsPaused;
                data.CurrentResearchPauseTurns = research.PauseTurns;
            }
            
            saveData.Systems.Add(new SystemSaveData
            {
                SystemTypeName = "MilitaryTechSystem",
                JsonData = JsonUtility.ToJson(data)
            });
        }
        
        private void RestoreD6State(GameSaveData saveData)
        {
            var d6 = GetSystem<MilitaryTechSystem>();
            if (d6 == null) return;
            
            var systemData = saveData.Systems.Find(s => s.SystemTypeName == "MilitaryTechSystem");
            if (systemData == null || string.IsNullOrEmpty(systemData.JsonData)) return;
            
            D6SaveData data = new D6SaveData();
            JsonUtility.FromJsonOverwrite(systemData.JsonData, data);
            if (data == null) return;
            
            foreach (var techId in data.CompletedTechs)
            {
                d6.MarkTechCompleted(techId);
            }
        }
        
        private void CaptureJState(GameSaveData saveData)
        {
            var j = GetSystem<VictoryDefeatSystem>();
            if (j == null) return;
            
            var save = new JSaveData
            {
                GameEnded = j.IsGameEnded(),
                EndReason = j.GetEndReason(),
                TurnsUnderLowBlockade = j.GetTurnsUnderLowBlockade()
            };
            
            saveData.Systems.Add(new SystemSaveData
            {
                SystemTypeName = "VictoryDefeatSystem",
                JsonData = JsonUtility.ToJson(save)
            });
        }
        
        private void RestoreJState(GameSaveData saveData)
        {
            var j = GetSystem<VictoryDefeatSystem>();
            if (j == null) return;
            
            var systemData = saveData.Systems.Find(s => s.SystemTypeName == "VictoryDefeatSystem");
            if (systemData == null || string.IsNullOrEmpty(systemData.JsonData)) return;
            
            JSaveData save = new JSaveData();
            JsonUtility.FromJsonOverwrite(systemData.JsonData, save);
            if (save == null) return;
            
            if (save.GameEnded)
            {
                j.ForceEndGame(save.EndReason);
            }
        }
        
        private T GetSystem<T>() where T : GameSystem
        {
            foreach (var system in _allSystems)
            {
                if (system is T) return (T)system;
            }
            return null;
        }
        
        public string[] GetAllSaves()
        {
            try
            {
                string path = GetSaveDirectoryPath();
                if (!Directory.Exists(path))
                    return new string[0];
                
                string[] files = Directory.GetFiles(path, "*" + Extension);
                string[] names = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    names[i] = Path.GetFileNameWithoutExtension(files[i]);
                }
                return names;
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] GetAllSaves failed: {e.Message}");
                return new string[0];
            }
        }
        
        public bool DeleteSave(string saveName)
        {
            try
            {
                string path = GetSaveFilePath(saveName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Debug.Log($"[SaveSystem] Save deleted: {saveName}");
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Delete failed: {e.Message}");
                return false;
            }
        }
        
        public bool SaveExists(string saveName)
        {
            return File.Exists(GetSaveFilePath(saveName));
        }
        
        public DateTime? GetSaveTimestamp(string saveName)
        {
            try
            {
                string path = GetSaveFilePath(saveName);
                if (File.Exists(path))
                {
                    return File.GetLastWriteTime(path);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}

using UnityEngine;
using System;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.A4
{
    [Serializable]
    public class SystemSaveData
    {
        public string SystemTypeName;
        public string JsonData;
    }

    [Serializable]
    public class GameSaveData
    {
        public int SaveVersion = 1;
        public long Timestamp;
        public string GameStateJson;
        public List<SystemSaveData> Systems = new List<SystemSaveData>();
        public string Metadata;
    }

    [Serializable]
    public class D2SaveData
    {
        public List<string> OccupiedNodesUnderDigestion = new List<string>();
        public Dictionary<string, int> DigestionTurnsRemaining = new Dictionary<string, int>();
    }

    [Serializable]
    public class D3SaveData
    {
        public List<ProxyRegionSave> ProxyRegions = new List<ProxyRegionSave>();
    }

    [Serializable]
    public class ProxyRegionSave
    {
        public string NodeId;
        public string ControllerFactionId;
        public int Stability;
        public int PublicSupport;
        public int GovernanceLevel;
        public int TurnsInCurrentState;
    }

    [Serializable]
    public class D4SaveData
    {
        public int WarheadCount;
        public int DisplayCooldown;
        public bool IsFullWarLockActive;
        public int FullWarLockTurnsRemaining;
        public int CapabilityLevel;
    }

    [Serializable]
    public class D6SaveData
    {
        public string CurrentResearchTechId;
        public int CurrentResearchProgress;
        public int CurrentResearchTurn;
        public bool IsCurrentResearchPaused;
        public int CurrentResearchPauseTurns;
        public List<string> CompletedTechs = new List<string>();
    }

    [Serializable]
    public class JSaveData
    {
        public bool GameEnded;
        public string EndReason;
        public int CurrentBlockadeLevel;
        public int TurnsUnderLowBlockade;
        public int ActiveConflictTurns;
        public int EnemyKeyNodeLosses;
        public int BlockadePostponementCount;
        public bool IsLargeScaleConflictActive;
    }
}
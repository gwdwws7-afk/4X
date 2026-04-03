using UnityEngine;
using System;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.J
{
    public enum VictoryPathType
    {
        EnergyLiberation,
        MilitaryStalemate,
        AxisVictory,
        DiplomaticResolution
    }

    public enum DefeatType
    {
        MilitaryCollapse,
        EconomicCollapse,
        InternalDivision
    }

    public enum BlockadeLevel
    {
        None,
        Unilateral,
        Multilateral,
        Total
    }

    [Serializable]
    public class VictoryPath
    {
        public VictoryPathType Type;
        public string Name;
        public string Description;
        public float Progress;
        public bool IsAchieved;
        public List<SubComponentProgress> SubComponents = new List<SubComponentProgress>();
        public int EstimatedTurnsToVictory;
    }

    [Serializable]
    public class SubComponentProgress
    {
        public string Name;
        public float Progress;
        public string Status;
    }

    [Serializable]
    public class DefeatRisk
    {
        public DefeatType Type;
        public string WarningMessage;
        public bool IsImminent;
    }

    public class VictoryDefeatSystem : GameSystem
    {
        private List<VictoryPath> _victoryPaths = new List<VictoryPath>();
        private List<DefeatRisk> _defeatRisks = new List<DefeatRisk>();
        private bool _gameEnded = false;
        private string _endReason = "";
        private BlockadeLevel _currentBlockadeLevel = BlockadeLevel.Unilateral;
        private int _turnsUnderLowBlockade = 0;
        private int _activeConflictTurns = 0;
        private int _enemyKeyNodeLosses = 0;
        private int _blockadePostponementCount = 0;
        private bool _isLargeScaleConflictActive = false;

        [Header("Victory Thresholds")]
        public float VictoryThreshold = 80f;
        public int MaxGameTurns = 24;

        [Header("Defeat Thresholds")]
        public int MilitaryCollapseAshWillThreshold = 30;
        public int EconomicCollapseEnergyZeroTurns = 3;
        public int InternalDivisionSatisfactionThreshold = 15;

        [Header("Warning Thresholds")]
        public float VictoryHopeThreshold = 60f;
        public float VictoryImminentThreshold = 80f;
        public int DefeatRiskAshWillThreshold = 35;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);

            _victoryPaths.Add(new VictoryPath
            {
                Type = VictoryPathType.EnergyLiberation,
                Name = "能源解放",
                Description = "能源出口完全不依赖星币结算，封锁体系持续失效"
            });

            _victoryPaths.Add(new VictoryPath
            {
                Type = VictoryPathType.MilitaryStalemate,
                Name = "军事均势",
                Description = "大规模军事冲突中迫使敌人接受停火协议"
            });

            _victoryPaths.Add(new VictoryPath
            {
                Type = VictoryPathType.AxisVictory,
                Name = "抵抗轴心胜利",
                Description = "控制两河、叙利亚、黎巴嫩，迫使敌方接受两国方案"
            });

            _victoryPaths.Add(new VictoryPath
            {
                Type = VictoryPathType.DiplomaticResolution,
                Name = "外交解决",
                Description = "联合国通过有利于瓦希德的决议，黎明协定全面恢复"
            });

            Events.OnTurnEnded += HandleTurnEnded;
            Debug.Log("[VictoryDefeatSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        public override void OnTurnEnded(int turnNumber)
        {
            CheckVictoryDefeat();
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            CheckVictoryDefeat();
        }
        
        public void CheckVictoryDefeat()
        {
            if (_gameEnded) return;

            CheckDefeatConditions();
            if (_gameEnded) return;

            UpdateAllPathProgress();

            CheckVictoryConditions();
            if (_gameEnded) return;

            CheckTimeout();
        }

        private void CheckDefeatConditions()
        {
            if (CheckMilitaryCollapse())
            {
                TriggerEndgame("defeat", "military_collapse");
                return;
            }

            if (CheckEconomicCollapse())
            {
                TriggerEndgame("defeat", "economic_collapse");
                return;
            }

            if (CheckInternalDivision())
            {
                TriggerEndgame("defeat", "internal_division");
                return;
            }
        }

        private bool CheckMilitaryCollapse()
        {
            var ashWill = State.GetResource("AshWill");
            if (ashWill == null) return false;
            return ashWill.Amount < MilitaryCollapseAshWillThreshold;
        }

        private bool CheckEconomicCollapse()
        {
            var energy = State.GetResource("Energy");
            if (energy == null) return false;
            return energy.Amount <= 0;
        }

        private bool CheckInternalDivision()
        {
            foreach (var faction in State.Factions)
            {
                if (faction.Satisfaction < InternalDivisionSatisfactionThreshold)
                    return true;
            }
            return false;
        }

        private void CheckVictoryConditions()
        {
            int completedPaths = 0;
            VictoryPathType? firstNearComplete = null;

            foreach (var path in _victoryPaths)
            {
                if (path.Progress >= VictoryThreshold)
                    completedPaths++;
                else if (path.Progress >= VictoryHopeThreshold && firstNearComplete == null)
                    firstNearComplete = path.Type;
            }

            foreach (var path in _victoryPaths)
            {
                if (path.Progress >= VictoryThreshold)
                {
                    TriggerEndgame("victory", path.Type.ToString().ToLower());
                    return;
                }
            }

            if (completedPaths >= 2)
            {
                TriggerEndgame("victory", "combined");
            }
        }

        private void CheckTimeout()
        {
            if (State.CurrentTurn >= MaxGameTurns)
            {
                TriggerEndgame("timeout", "attrition");
            }
        }

        private void UpdateAllPathProgress()
        {
            UpdateEnergyLiberationProgress();
            UpdateMilitaryStalemateProgress();
            UpdateAxisVictoryProgress();
            UpdateDiplomaticResolutionProgress();
        }

        private void UpdateEnergyLiberationProgress()
        {
            var path = GetPath(VictoryPathType.EnergyLiberation);
            if (path == null) return;

            path.SubComponents.Clear();

            float shangMengProgress = CalculateShangMengProgress();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "商盟券渠道覆盖率",
                Progress = shangMengProgress,
                Status = shangMengProgress >= 80 ? "on_track" : "at_risk"
            });

            float northProgress = CalculateNorthChannelProgress();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "北境渠道覆盖率",
                Progress = northProgress,
                Status = northProgress >= 80 ? "on_track" : "at_risk"
            });

            float blockadeProgress = CalculateBlockadeFailureProgress();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "封锁持续失效",
                Progress = blockadeProgress,
                Status = blockadeProgress >= 80 ? "on_track" : "at_risk"
            });

            path.Progress = (shangMengProgress * 0.4f) + (northProgress * 0.3f) + (blockadeProgress * 0.3f);
        }

        private float CalculateShangMengProgress()
        {
            var socialValue = State.GetResource("SocialValue");
            if (socialValue == null) return 0f;
            return Mathf.Clamp01((socialValue.Amount / 50f) * 100f);
        }

        private float CalculateNorthChannelProgress()
        {
            var tradeToken = State.GetResource("TradeToken");
            if (tradeToken == null) return 0f;
            return Mathf.Clamp01((tradeToken.Amount / 30f) * 100f);
        }

        private float CalculateBlockadeFailureProgress()
        {
            float levelProgress = 0f;
            switch (_currentBlockadeLevel)
            {
                case BlockadeLevel.None: levelProgress = 100f; break;
                case BlockadeLevel.Unilateral: levelProgress = 50f; break;
                case BlockadeLevel.Multilateral: levelProgress = 20f; break;
                case BlockadeLevel.Total: levelProgress = 0f; break;
            }

            float timeProgress = Mathf.Clamp01((_turnsUnderLowBlockade / 24f) * 100f);
            return Mathf.Max(levelProgress, timeProgress);
        }

        private BlockadeLevel GetCurrentBlockadeLevelFromState()
        {
            return _currentBlockadeLevel;
        }

        public void SetBlockadeLevel(BlockadeLevel level)
        {
            if (level <= BlockadeLevel.Unilateral)
            {
                _turnsUnderLowBlockade++;
            }
            else
            {
                _turnsUnderLowBlockade = 0;
            }
            _currentBlockadeLevel = level;
        }

        public int GetTurnsUnderLowBlockade()
        {
            return _turnsUnderLowBlockade;
        }

        private void UpdateMilitaryStalemateProgress()
        {
            var path = GetPath(VictoryPathType.MilitaryStalemate);
            if (path == null) return;

            path.SubComponents.Clear();

            float warDevelopment = CalculateWarDevelopment();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "战争发展",
                Progress = warDevelopment,
                Status = warDevelopment >= 50 ? "on_track" : "at_risk"
            });

            float enemyLosses = CalculateEnemyLosses();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "敌方损失",
                Progress = enemyLosses,
                Status = "at_risk"
            });

            float ceasefirePossibility = CalculateCeasefirePossibility();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "停火协议可能性",
                Progress = ceasefirePossibility,
                Status = ceasefirePossibility >= 40 ? "on_track" : "at_risk"
            });

            path.Progress = (warDevelopment * 0.4f) + (enemyLosses * 0.3f) + (ceasefirePossibility * 0.3f);
        }

        private float CalculateWarDevelopment()
        {
            if (_isLargeScaleConflictActive)
            {
                return Mathf.Clamp01(50f + (_activeConflictTurns * 5f));
            }
            return 0f;
        }

        private float CalculateEnemyLosses()
        {
            float losses = 0f;
            losses += _enemyKeyNodeLosses * 30f;
            if (_blockadePostponementCount > 3)
                losses += 20f;
            return Mathf.Clamp01(losses);
        }

        public void SetLargeScaleConflictActive(bool active)
        {
            if (active && !_isLargeScaleConflictActive)
            {
                _isLargeScaleConflictActive = true;
                _activeConflictTurns = 1;
            }
            else if (active)
            {
                _activeConflictTurns++;
            }
            else
            {
                _isLargeScaleConflictActive = false;
            }
        }

        public void RecordEnemyKeyNodeLoss()
        {
            _enemyKeyNodeLosses++;
        }

        public void RecordBlockadePostponement()
        {
            _blockadePostponementCount++;
        }

        private float CalculateCeasefirePossibility()
        {
            var ashWill = State.GetResource("AshWill");
            if (ashWill == null) return 0f;

            float possibility = 0f;
            if (ashWill.Amount > 50) possibility += 20f;

            return Mathf.Clamp01(possibility);
        }

        private void UpdateAxisVictoryProgress()
        {
            var path = GetPath(VictoryPathType.AxisVictory);
            if (path == null) return;

            path.SubComponents.Clear();

            float tigrisControl = CalculateTigrisControl();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "两河控制",
                Progress = tigrisControl,
                Status = tigrisControl >= 80 ? "on_track" : "at_risk"
            });

            float syriaControl = CalculateSyriaControl();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "叙利亚控制",
                Progress = syriaControl,
                Status = syriaControl >= 100 ? "on_track" : "blocked"
            });

            float lebanonControl = CalculateLebanonControl();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "黎巴嫩控制",
                Progress = lebanonControl,
                Status = lebanonControl >= 100 ? "on_track" : "at_risk"
            });

            float holyFireRelation = CalculateHolyFireRelation();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "圣火序关系",
                Progress = holyFireRelation,
                Status = holyFireRelation >= 50 ? "on_track" : "at_risk"
            });

            path.Progress = (tigrisControl * 0.3f) + (syriaControl * 0.3f) + (lebanonControl * 0.2f) + (holyFireRelation * 0.2f);
        }

        private float CalculateTigrisControl()
        {
            var tigrisNode = State.GetNode("Tigris");
            if (tigrisNode == null) return 0f;
            return IsUnderResistanceAxis(tigrisNode.ControllingFactionId) ? 100f : 0f;
        }

        private float CalculateSyriaControl()
        {
            var damascus = State.GetNode("Damascus");
            if (damascus == null) return 0f;
            return IsUnderResistanceAxis(damascus.ControllingFactionId) ? 100f : 0f;
        }

        private float CalculateLebanonControl()
        {
            var beirut = State.GetNode("Beirut");
            if (beirut == null) return 0f;
            return IsUnderResistanceAxis(beirut.ControllingFactionId) ? 100f : 0f;
        }

        private float CalculateHolyFireRelation()
        {
            var holyFire = State.GetFaction("HolyFire");
            if (holyFire == null) return 0f;
            return Mathf.Clamp01(((holyFire.RelationshipWithPlayer + 80f) / 160f) * 100f);
        }

        private bool IsUnderResistanceAxis(string factionId)
        {
            if (string.IsNullOrEmpty(factionId)) return false;
            return factionId == "Vahid" || factionId == "AshCloud" || factionId == "ResistanceAxis";
        }

        private void UpdateDiplomaticResolutionProgress()
        {
            var path = GetPath(VictoryPathType.DiplomaticResolution);
            if (path == null) return;

            path.SubComponents.Clear();

            float tributeOrderProgress = CalculateTributeOrderProgress();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "朝贡序达标",
                Progress = tributeOrderProgress,
                Status = tributeOrderProgress >= 80 ? "on_track" : "at_risk"
            });

            float goldLeaderRelation = CalculateGoldLeaderRelation();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "黄金领关系",
                Progress = goldLeaderRelation,
                Status = goldLeaderRelation >= 50 ? "on_track" : "at_risk"
            });

            float blockadeRelief = CalculateBlockadeRelief();
            path.SubComponents.Add(new SubComponentProgress
            {
                Name = "封锁解除",
                Progress = blockadeRelief,
                Status = blockadeRelief >= 80 ? "on_track" : "at_risk"
            });

            path.Progress = (tributeOrderProgress * 0.3f) + (goldLeaderRelation * 0.4f) + (blockadeRelief * 0.3f);
        }

        private float CalculateTributeOrderProgress()
        {
            var socialValue = State.GetResource("SocialValue");
            if (socialValue == null) return 0f;
            return Mathf.Clamp01((socialValue.Amount / 70f) * 100f);
        }

        private float CalculateGoldLeaderRelation()
        {
            var goldLeader = State.GetFaction("GoldLeader");
            if (goldLeader == null) return 0f;
            return Mathf.Clamp01(((goldLeader.RelationshipWithPlayer + 80f) / 120f) * 100f);
        }

        private float CalculateBlockadeRelief()
        {
            var level = GetCurrentBlockadeLevelFromState();
            switch (level)
            {
                case BlockadeLevel.None: return 100f;
                case BlockadeLevel.Unilateral: return 70f;
                case BlockadeLevel.Multilateral: return 30f;
                case BlockadeLevel.Total: return 0f;
                default: return 0f;
            }
        }

        private VictoryPath GetPath(VictoryPathType type)
        {
            return _victoryPaths.Find(p => p.Type == type);
        }

        public VictoryPath[] GetAllVictoryPaths()
        {
            return _victoryPaths.ToArray();
        }

        public VictoryPath GetClosestPath()
        {
            VictoryPath closest = null;
            float maxProgress = 0f;
            foreach (var path in _victoryPaths)
            {
                if (path.Progress > maxProgress)
                {
                    maxProgress = path.Progress;
                    closest = path;
                }
            }
            return closest;
        }

        public DefeatRisk[] GetCurrentDefeatRisks()
        {
            _defeatRisks.Clear();

            var ashWill = State.GetResource("AshWill");
            if (ashWill != null && ashWill.Amount < DefeatRiskAshWillThreshold)
            {
                _defeatRisks.Add(new DefeatRisk
                {
                    Type = DefeatType.MilitaryCollapse,
                    WarningMessage = $"军事崩溃风险：社稷值过低 ({ashWill.Amount})",
                    IsImminent = ashWill.Amount < MilitaryCollapseAshWillThreshold
                });
            }

            var energy = State.GetResource("Energy");
            if (energy != null && energy.Amount <= 0)
            {
                _defeatRisks.Add(new DefeatRisk
                {
                    Type = DefeatType.EconomicCollapse,
                    WarningMessage = "经济崩溃风险：能源收入归零",
                    IsImminent = false
                });
            }

            return _defeatRisks.ToArray();
        }

        private void TriggerEndgame(string type, string reason)
        {
            _gameEnded = true;
            _endReason = reason;
            Debug.Log($"[VictoryDefeat] Endgame triggered: {type} - {reason}");
            Events.GameEnded(reason);
        }

        public bool IsGameEnded()
        {
            return _gameEnded;
        }

        public string GetEndReason()
        {
            return _endReason;
        }
        
        public void ForceEndGame(string reason)
        {
            _gameEnded = true;
            _endReason = reason;
        }

        public void Reset()
        {
            _gameEnded = false;
            _endReason = "";
            _currentBlockadeLevel = BlockadeLevel.Unilateral;
            _turnsUnderLowBlockade = 0;
            _activeConflictTurns = 0;
            _enemyKeyNodeLosses = 0;
            _blockadePostponementCount = 0;
            _isLargeScaleConflictActive = false;

            foreach (var path in _victoryPaths)
            {
                path.Progress = 0f;
                path.IsAchieved = false;
                path.SubComponents.Clear();
            }

            _defeatRisks.Clear();
        }
    }
}
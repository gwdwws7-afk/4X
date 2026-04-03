using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using EventideAge.Core;
using EventideAge.Systems.D1;

namespace EventideAge.Systems.D6
{
    [Serializable]
    public class TechDefinition
    {
        public string TechId;
        public string TechName;
        public string Description;
        public string[] Prerequisites;
        public int GoldLeafCost;
        public int ResearchTurns;
        public string[] Unlocks;
        public string EffectTag;
    }

    [Serializable]
    public class TechResearch
    {
        public string TechId;
        public int Progress;
        public int CurrentTurn;
        public bool IsPaused;
        public int PauseTurns;
    }

    public class MilitaryTechSystem : GameSystem
    {
        private TechResearch _currentResearch;
        private HashSet<string> _completedTechs = new HashSet<string>();
        private List<TechDefinition> _techTree = new List<TechDefinition>();

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            InitializeTechTree();
            Events.OnTurnEnded += HandleTurnEnded;
            Debug.Log("[MilitaryTechSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        public override void OnTurnEnded(int turnNumber)
        {
            ProcessResearchTurn();
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            ProcessResearchTurn();
        }
        
        private void InitializeTechTree()
        {
            _techTree.Add(new TechDefinition
            {
                TechId = "DefensiveEnhancementI",
                TechName = "防御强化 I",
                Description = "不对称防御成功率+5%",
                Prerequisites = new string[0],
                GoldLeafCost = 50,
                ResearchTurns = 3,
                EffectTag = "asymmetric_defense_success+5"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "DefensiveEnhancementII",
                TechName = "防御强化 II",
                Description = "不对称防御成功率+5%",
                Prerequisites = new[] { "DefensiveEnhancementI" },
                GoldLeafCost = 80,
                ResearchTurns = 5,
                EffectTag = "asymmetric_defense_success+5"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "DefensiveEnhancementIII",
                TechName = "防御强化 III",
                Description = "不对称防御成功率+5%，地形加成+10%",
                Prerequisites = new[] { "DefensiveEnhancementII" },
                GoldLeafCost = 120,
                ResearchTurns = 7,
                EffectTag = "asymmetric_defense_success+5,terrain_bonus+10"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "OffensiveEnhancementI",
                TechName = "攻击强化 I",
                Description = "代理人行动成功率+5%",
                Prerequisites = new string[0],
                GoldLeafCost = 50,
                ResearchTurns = 3,
                EffectTag = "proxy_action_success+5"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "OffensiveEnhancementII",
                TechName = "攻击强化 II",
                Description = "代理人行动成功率+5%",
                Prerequisites = new[] { "OffensiveEnhancementI" },
                GoldLeafCost = 80,
                ResearchTurns = 5,
                EffectTag = "proxy_action_success+5"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "OffensiveEnhancementIII",
                TechName = "攻击强化 III",
                Description = "代理人行动成功率+5%，特种部队+10%",
                Prerequisites = new[] { "OffensiveEnhancementII" },
                GoldLeafCost = 120,
                ResearchTurns = 7,
                EffectTag = "proxy_action_success+5,special_forces+10"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "AsymmetricTacticsI",
                TechName = "不对称战术 I",
                Description = "咽喉威胁成功率+10%",
                Prerequisites = new string[0],
                GoldLeafCost = 60,
                ResearchTurns = 4,
                EffectTag = "chokepoint_threat_success+10"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "AsymmetricTacticsII",
                TechName = "不对称战术 II",
                Description = "咽喉威胁成功率+10%，封锁升级风险-10%",
                Prerequisites = new[] { "AsymmetricTacticsI" },
                GoldLeafCost = 100,
                ResearchTurns = 6,
                EffectTag = "chokepoint_threat_success+10,blockade_risk-10"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "NorthernWeaponsI",
                TechName = "北境武器 I",
                Description = "解锁北境L1装备，攻击+10%",
                Prerequisites = new string[0],
                GoldLeafCost = 70,
                ResearchTurns = 4,
                EffectTag = "unlock_northern_l1,attack+10"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "NorthernWeaponsII",
                TechName = "北境武器 II",
                Description = "解锁北境L2装备，攻击+15%",
                Prerequisites = new[] { "NorthernWeaponsI" },
                GoldLeafCost = 110,
                ResearchTurns = 6,
                EffectTag = "unlock_northern_l2,attack+15"
            });

            _techTree.Add(new TechDefinition
            {
                TechId = "NorthernWeaponsIII",
                TechName = "北境武器 III",
                Description = "解锁北境L3装备，攻击+20%，防御+10%",
                Prerequisites = new[] { "NorthernWeaponsII" },
                GoldLeafCost = 150,
                ResearchTurns = 8,
                EffectTag = "unlock_northern_l3,attack+20,defense+10"
            });
        }

        public TechDefinition[] GetAvailableTechs()
        {
            List<TechDefinition> available = new List<TechDefinition>();
            foreach (var tech in _techTree)
            {
                if (_completedTechs.Contains(tech.TechId))
                    continue;

                if (! MeetsPrerequisites(tech))
                    continue;

                if (_currentResearch != null && _currentResearch.TechId == tech.TechId)
                    continue;

                available.Add(tech);
            }
            return available.ToArray();
        }

        public bool StartResearch(string techId)
        {
            if (_currentResearch != null && !string.IsNullOrEmpty(_currentResearch.TechId))
                return false;

            var tech = GetTechDefinition(techId);
            if (tech == null)
                return false;

            if (_completedTechs.Contains(techId))
                return false;

            if (!MeetsPrerequisites(tech))
                return false;

            var goldLeaf = State.GetResource("GoldLeaf");
            if (goldLeaf == null || goldLeaf.Amount < tech.GoldLeafCost)
                return false;

            int oldAmount = goldLeaf.Amount;
            goldLeaf.Amount -= tech.GoldLeafCost;
            Events.ResourceChanged("GoldLeaf", oldAmount, goldLeaf.Amount);

            _currentResearch = new TechResearch
            {
                TechId = techId,
                Progress = 0,
                CurrentTurn = 0,
                IsPaused = false,
                PauseTurns = 0
            };

            Debug.Log($"[D6] Started research: {tech.TechName}");
            return true;
        }

        public void CancelResearch()
        {
            _currentResearch = null;
            Debug.Log("[D6] Research cancelled - no refund");
        }

        public TechResearch GetCurrentResearch()
        {
            return _currentResearch;
        }

        public float GetResearchProgress()
        {
            if (_currentResearch == null)
                return 0f;

            var tech = GetTechDefinition(_currentResearch.TechId);
            if (tech == null)
                return 0f;

            return (float)_currentResearch.Progress / tech.ResearchTurns;
        }
        
        public bool CanStartResearch(string techId)
        {
            if (_currentResearch != null && !string.IsNullOrEmpty(_currentResearch.TechId))
                return false;
            
            var tech = GetTechDefinition(techId);
            if (tech == null)
                return false;
            
            if (_completedTechs.Contains(techId))
                return false;
            
            if (!MeetsPrerequisites(tech))
                return false;
            
            var goldLeaf = State.GetResource("GoldLeaf");
            if (goldLeaf == null || goldLeaf.Amount < tech.GoldLeafCost)
                return false;
            
            return true;
        }
        
        public void ProcessResearchTurn()
        {
            if (_currentResearch == null || _currentResearch.IsPaused)
                return;

            var tech = GetTechDefinition(_currentResearch.TechId);
            if (tech == null)
                return;

            _currentResearch.Progress++;
            _currentResearch.CurrentTurn++;

            if (_currentResearch.Progress >= tech.ResearchTurns)
            {
                CompleteResearch(_currentResearch.TechId);
            }
        }

        private void CompleteResearch(string techId)
        {
            _completedTechs.Add(techId);
            Debug.Log($"[D6] Research completed: {techId}");

            var tech = GetTechDefinition(techId);
            if (tech != null && tech.Unlocks != null)
            {
                foreach (var unlock in tech.Unlocks)
                {
                    Debug.Log($"[D6] Unlocked: {unlock}");
                }
            }

            _currentResearch = null;
        }

        public bool HasCompleted(string techId)
        {
            return _completedTechs.Contains(techId);
        }
        
        public string[] GetCompletedTechs()
        {
            return _completedTechs.ToArray();
        }
        
        public void MarkTechCompleted(string techId)
        {
            if (!_completedTechs.Contains(techId))
            {
                _completedTechs.Add(techId);
            }
        }

        public TechBonus[] GetTechBonuses()
        {
            List<TechBonus> bonuses = new List<TechBonus>();
            foreach (var techId in _completedTechs)
            {
                var tech = GetTechDefinition(techId);
                if (tech != null)
                {
                    bonuses.Add(ParseTechBonus(tech));
                }
            }
            return bonuses.ToArray();
        }

        public float GetMilitaryActionBonus(MilitaryActionType actionType)
        {
            float bonus = 0f;
            foreach (var techId in _completedTechs)
            {
                var tech = GetTechDefinition(techId);
                if (tech == null || string.IsNullOrEmpty(tech.EffectTag))
                    continue;

                switch (actionType)
                {
                    case MilitaryActionType.AsymmetricDefense:
                        if (tech.EffectTag.Contains("asymmetric_defense_success"))
                            bonus += ExtractBonusValue(tech.EffectTag, "asymmetric_defense_success");
                        if (tech.EffectTag.Contains("terrain_bonus"))
                            bonus += ExtractBonusValue(tech.EffectTag, "terrain_bonus");
                        break;

                    case MilitaryActionType.Proxy:
                        if (tech.EffectTag.Contains("proxy_action_success"))
                            bonus += ExtractBonusValue(tech.EffectTag, "proxy_action_success");
                        break;

                    case MilitaryActionType.SpecialForces:
                        if (tech.EffectTag.Contains("special_forces"))
                            bonus += ExtractBonusValue(tech.EffectTag, "special_forces");
                        break;

                    case MilitaryActionType.ChokepointThreat:
                        if (tech.EffectTag.Contains("chokepoint_threat_success"))
                            bonus += ExtractBonusValue(tech.EffectTag, "chokepoint_threat_success");
                        break;
                }
            }
            return bonus / 100f;
        }

        private TechBonus ParseTechBonus(TechDefinition tech)
        {
            return new TechBonus
            {
                TechId = tech.TechId,
                EffectTag = tech.EffectTag
            };
        }

        private float ExtractBonusValue(string effectTag, string key)
        {
            string searchPattern = key + "+";
            int keyIndex = effectTag.IndexOf(searchPattern, StringComparison.OrdinalIgnoreCase);
            if (keyIndex < 0)
            {
                searchPattern = key + "-";
                keyIndex = effectTag.IndexOf(searchPattern, StringComparison.OrdinalIgnoreCase);
                if (keyIndex < 0) return 0f;
            }

            int valueStart = keyIndex + searchPattern.Length;
            int valueEnd = valueStart;
            while (valueEnd < effectTag.Length && (char.IsDigit(effectTag[valueEnd]) || effectTag[valueEnd] == '-' || effectTag[valueEnd] == '+'))
                valueEnd++;

            string valueStr = effectTag.Substring(valueStart, valueEnd - valueStart);
            if (float.TryParse(valueStr, out float value))
                return value;

            return 0f;
        }

        private bool MeetsPrerequisites(TechDefinition tech)
        {
            if (tech.Prerequisites == null || tech.Prerequisites.Length == 0)
                return true;

            foreach (var prereq in tech.Prerequisites)
            {
                if (!_completedTechs.Contains(prereq))
                    return false;
            }
            return true;
        }

        private TechDefinition GetTechDefinition(string techId)
        {
            return _techTree.Find(t => t.TechId == techId);
        }
    }

    public struct TechBonus
    {
        public string TechId;
        public string EffectTag;
    }
}
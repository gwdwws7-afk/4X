using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.C5
{
    public enum ResolutionType
    {
        Condemnation,
        Ceasefire,
        Sanctions,
        Peacekeeping,
        Embargo,
        Recognition
    }
    
    public enum VotingResult
    {
        Passed,
        Failed,
        Vetoed,
        Pending
    }
    
    public class UNResolution
    {
        public string ResolutionId;
        public ResolutionType Type;
        public string Proposer;
        public string Target;
        public VotingResult Result;
        public int TurnProposed;
        public Dictionary<string, int> VotesFor;
        public Dictionary<string, int> VotesAgainst;
        public int TotalVotesFor;
        public int TotalVotesAgainst;
    }
    
    public class InternationalOrgsSystem : GameSystem
    {
        [Header("Voting Thresholds")]
        public int SimpleMajorityThreshold = 51;
        public int TwoThirdsThreshold = 67;
        public int VetoPowerCount = 5;
        
        [Header("Resolution Effects")]
        public int CondemnationRelationPenalty = -15;
        public int SanctionsGoldLeafPenalty = -50;
        public int EmbargoBlockadeBonus = 10;
        
        [Header("Turn Between Resolutions")]
        public int ResolutionCooldown = 3;
        
        private List<UNResolution> _resolutionHistory = new List<UNResolution>();
        private Dictionary<string, int> _factionLastResolutionTurn = new Dictionary<string, int>();
        private int _resolutionIdCounter = 0;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Debug.Log("[InternationalOrgsSystem] Initialized");
        }
        
        public bool CanProposeResolution(string faction, ResolutionType type)
        {
            faction = GameIds.ResolveFactionId(faction);
            if (_factionLastResolutionTurn.TryGetValue(faction, out var lastTurn))
            {
                if (State.CurrentTurn - lastTurn < ResolutionCooldown)
                    return false;
            }
            return true;
        }
        
        public UNResolution ProposeResolution(string proposer, string target, ResolutionType type)
        {
            proposer = GameIds.ResolveFactionId(proposer);
            target = GameIds.ResolveFactionId(target);
            if (!CanProposeResolution(proposer, type))
                return null;
            
            var resolution = new UNResolution
            {
                ResolutionId = $"resolution_{_resolutionIdCounter++}",
                Type = type,
                Proposer = proposer,
                Target = target,
                Result = VotingResult.Pending,
                TurnProposed = State.CurrentTurn,
                VotesFor = new Dictionary<string, int>(),
                VotesAgainst = new Dictionary<string, int>(),
                TotalVotesFor = 0,
                TotalVotesAgainst = 0
            };
            
            _factionLastResolutionTurn[proposer] = State.CurrentTurn;
            
            Debug.Log($"[InternationalOrgsSystem] Resolution proposed: {resolution.ResolutionId} ({type}) against {target}");
            
            return resolution;
        }
        
        public VotingResult CastVote(UNResolution resolution, string faction, bool voteFor)
        {
            faction = GameIds.ResolveFactionId(faction);
            if (resolution.Result != VotingResult.Pending)
                return resolution.Result;
            
            int votingWeight = CalculateVotingWeight(faction);

            // Keep one effective vote per faction by replacing previous choice.
            int existingFor = 0;
            string existingForKey = null;
            foreach (var pair in resolution.VotesFor)
            {
                if (GameIds.ResolveFactionId(pair.Key) == faction)
                {
                    existingFor = pair.Value;
                    existingForKey = pair.Key;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(existingForKey))
            {
                resolution.TotalVotesFor = Mathf.Max(0, resolution.TotalVotesFor - existingFor);
                resolution.VotesFor.Remove(existingForKey);
            }

            int existingAgainst = 0;
            string existingAgainstKey = null;
            foreach (var pair in resolution.VotesAgainst)
            {
                if (GameIds.ResolveFactionId(pair.Key) == faction)
                {
                    existingAgainst = pair.Value;
                    existingAgainstKey = pair.Key;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(existingAgainstKey))
            {
                resolution.TotalVotesAgainst = Mathf.Max(0, resolution.TotalVotesAgainst - existingAgainst);
                resolution.VotesAgainst.Remove(existingAgainstKey);
            }
            
            if (voteFor)
            {
                resolution.VotesFor[faction] = votingWeight;
                resolution.TotalVotesFor += votingWeight;
            }
            else
            {
                resolution.VotesAgainst[faction] = votingWeight;
                resolution.TotalVotesAgainst += votingWeight;
            }
            
            Debug.Log($"[InternationalOrgsSystem] {faction} voted {(voteFor ? "FOR" : "AGAINST")} {resolution.ResolutionId}");
            
            return resolution.Result;
        }
        
        public VotingResult TallyVotes(UNResolution resolution)
        {
            if (resolution.Result != VotingResult.Pending)
                return resolution.Result;
            
            if (IsVetoed(resolution))
            {
                resolution.Result = VotingResult.Vetoed;
                Debug.Log($"[InternationalOrgsSystem] {resolution.ResolutionId} vetoed!");
                return VotingResult.Vetoed;
            }
            
            int totalVotes = resolution.TotalVotesFor + resolution.TotalVotesAgainst;
            if (totalVotes == 0)
                return VotingResult.Pending;
            
            int forPercentage = (resolution.TotalVotesFor * 100) / totalVotes;
            
            if (forPercentage >= TwoThirdsThreshold)
            {
                resolution.Result = VotingResult.Passed;
                ApplyResolutionEffects(resolution);
            }
            else if (forPercentage >= SimpleMajorityThreshold)
            {
                resolution.Result = VotingResult.Passed;
                ApplyResolutionEffects(resolution);
            }
            else
            {
                resolution.Result = VotingResult.Failed;
            }
            
            _resolutionHistory.Add(resolution);
            
            Debug.Log($"[InternationalOrgsSystem] {resolution.ResolutionId} {resolution.Result} ({forPercentage}% for)");
            
            return resolution.Result;
        }
        
        private bool IsVetoed(UNResolution resolution)
        {
            string[] vetoPowers =
            {
                GameIds.Faction.Vashid,
                GameIds.Faction.Aurean,
                GameIds.Faction.SacredFire,
                GameIds.Faction.GoldenHord,
                GameIds.Faction.AshConfederacy
            };
            
            foreach (var vetoPower in vetoPowers)
            {
                int vetoWeight = 0;
                bool hasVeto = false;
                foreach (var pair in resolution.VotesAgainst)
                {
                    if (GameIds.ResolveFactionId(pair.Key) == vetoPower)
                    {
                        vetoWeight = pair.Value;
                        hasVeto = true;
                        break;
                    }
                }

                if (!hasVeto)
                    continue;

                int currentTotal = resolution.TotalVotesFor + resolution.TotalVotesAgainst;
                if (currentTotal > 0 && (vetoWeight * 100 / currentTotal) > 30)
                    return true;
                }
            
            return false;
        }
        
        private int CalculateVotingWeight(string factionId)
        {
            factionId = GameIds.ResolveFactionId(factionId);
            int baseWeight = 10;
            
            var faction = State.GetFaction(factionId);
            if (faction != null)
            {
                baseWeight += Mathf.RoundToInt(faction.ControlledPoints / 20f);
            }
            
            return baseWeight;
        }
        
        private void ApplyResolutionEffects(UNResolution resolution)
        {
            resolution.Target = GameIds.ResolveFactionId(resolution.Target);
            switch (resolution.Type)
            {
                case ResolutionType.Condemnation:
                    Events.RelationshipChanged(resolution.Target, CondemnationRelationPenalty);
                    break;
                    
                case ResolutionType.Sanctions:
                    var goldLeaf = State.GetResource(GameIds.Resource.GoldLeaf);
                    if (goldLeaf != null)
                    {
                        int oldAmount = goldLeaf.Amount;
                        goldLeaf.Amount = Mathf.Max(0, goldLeaf.Amount + SanctionsGoldLeafPenalty);
                        Events.ResourceChanged(GameIds.Resource.GoldLeaf, oldAmount, goldLeaf.Amount);
                    }
                    break;
                    
                case ResolutionType.Embargo:
                    var blockadeSystem = FindSystem<Systems.B2.BlockadeSystem>();
                    if (blockadeSystem != null)
                    {
                        blockadeSystem.ActivateBlockade(B1.BlockadeType.EnergyEmbargo);
                    }
                    break;
                    
                case ResolutionType.Ceasefire:
                    Debug.Log($"[InternationalOrgsSystem] Ceasefire resolution applied to {resolution.Target}");
                    break;
            }
        }
        
        public UNResolution GetResolution(string resolutionId)
        {
            return _resolutionHistory.Find(r => r.ResolutionId == resolutionId);
        }
        
        public UNResolution[] GetRecentResolutions(int count)
        {
            if (_resolutionHistory.Count <= count)
                return _resolutionHistory.ToArray();
            
            var result = new List<UNResolution>();
            for (int i = _resolutionHistory.Count - count; i < _resolutionHistory.Count; i++)
            {
                result.Add(_resolutionHistory[i]);
            }
            return result.ToArray();
        }
        
        private T FindSystem<T>() where T : GameSystem
        {
            if (GameManager.Instance == null)
                return null;

            foreach (var system in GameManager.Instance.Systems)
            {
                if (system is T typedSystem)
                    return typedSystem;
            }

            return null;
        }
    }
}

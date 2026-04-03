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
            if (_factionLastResolutionTurn.TryGetValue(faction, out var lastTurn))
            {
                if (State.CurrentTurn - lastTurn < ResolutionCooldown)
                    return false;
            }
            return true;
        }
        
        public UNResolution ProposeResolution(string proposer, string target, ResolutionType type)
        {
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
            if (resolution.Result != VotingResult.Pending)
                return resolution.Result;
            
            int votingWeight = CalculateVotingWeight(faction);
            
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
            string[] vetoPowers = { "Aurean", "North", "EastAlliance", "SacredFire", "GoldenHord" };
            
            foreach (var vetoPower in vetoPowers)
            {
                if (resolution.VotesAgainst.ContainsKey(vetoPower))
                {
                    int vetoWeight = resolution.VotesAgainst[vetoPower];
                    int totalPossible = 100;
                    int currentTotal = resolution.TotalVotesFor + resolution.TotalVotesAgainst;
                    
                    if (currentTotal > 0 && (vetoWeight * 100 / currentTotal) > 30)
                        return true;
                }
            }
            
            return false;
        }
        
        private int CalculateVotingWeight(string factionId)
        {
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
            switch (resolution.Type)
            {
                case ResolutionType.Condemnation:
                    Events.RelationshipChanged(resolution.Target, CondemnationRelationPenalty);
                    break;
                    
                case ResolutionType.Sanctions:
                    var goldLeaf = State.GetResource("GoldLeaf");
                    if (goldLeaf != null)
                        goldLeaf.Amount = Mathf.Max(0, goldLeaf.Amount + SanctionsGoldLeafPenalty);
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
            foreach (var system in State != null ? new List<GameSystem>() : new List<GameSystem>())
            {
                if (system is T)
                    return (T)system;
            }
            return default(T);
        }
    }
}

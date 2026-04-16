using UnityEngine;
using System;

namespace EventideAge.Core
{
    public enum FeedbackSeverity
    {
        Info = 0,
        Warning = 1,
        Critical = 2
    }

    [CreateAssetMenu(fileName = "GameEvents", menuName = "EventideAge/GameEvents")]
    public class GameEvents : ScriptableObject
    {
        public event Action<int, int> OnTurnChanged;
        public event Action<int> OnTurnEnded;
        public event Action<int> OnPhaseChanged;
        public event Action<int> OnPhaseExited;
        public event Action<int> OnActionPointsChanged;
        public event Action<string, int, int> OnResourceChanged;
        public event Action<string, int> OnRelationshipChanged;
        // Payload: nodeId, oldController, newController, controlPoints.
        // For control-points-only updates, oldController and newController may be equal.
        public event Action<string, string, string, int> OnNodeControlChanged;
        public event Action<string, int> OnFactionPolicyChanged;
        public event Action<string, int> OnVictoryProgressChanged;
        public event Action<string> OnGameEnded;
        public event Action<string, string, FeedbackSeverity> OnActionLogAdded;
        public event Action<string, string, int, bool> OnConsequenceAdded;
        public event Action<string, FeedbackSeverity> OnGlobalAlertRaised;
        public event Action<string, string, FeedbackSeverity> OnNotificationAdded;
        public event Action<string, string, FeedbackSeverity> OnAlertAdded;
        public event Action<string, string, FeedbackSeverity> OnNarrativeEventAdded;
        public event Action<string, string, FeedbackSeverity> OnIntelReportAdded;
        
        public void TurnChanged(int oldTurn, int newTurn)
        {
            OnTurnChanged?.Invoke(oldTurn, newTurn);
        }
        
        public void TurnEnded(int turnNumber)
        {
            OnTurnEnded?.Invoke(turnNumber);
        }
        
        public void PhaseChanged(int newPhaseIndex)
        {
            OnPhaseChanged?.Invoke(newPhaseIndex);
        }
        
        public void PhaseExited(int phaseIndex)
        {
            OnPhaseExited?.Invoke(phaseIndex);
        }
        
        public void ActionPointsChanged(int remaining)
        {
            OnActionPointsChanged?.Invoke(remaining);
        }
        
        public void ResourceChanged(string resourceId, int oldAmount, int newAmount)
        {
            OnResourceChanged?.Invoke(resourceId, oldAmount, newAmount);
        }
        
        public void RelationshipChanged(string factionId, int delta)
        {
            OnRelationshipChanged?.Invoke(factionId, delta);
        }
        
        public void NodeControlChanged(string nodeId, string oldController, string newController, int controlPoints)
        {
            OnNodeControlChanged?.Invoke(nodeId, oldController, newController, controlPoints);
        }
        
        public void FactionPolicyChanged(string factionId, int satisfaction)
        {
            OnFactionPolicyChanged?.Invoke(factionId, satisfaction);
        }
        
        public void VictoryProgressChanged(string pathId, int progress)
        {
            OnVictoryProgressChanged?.Invoke(pathId, progress);
        }
        
        public void GameEnded(string reason)
        {
            OnGameEnded?.Invoke(reason);
        }

        public void ActionLogAdded(string sourceId, string message, FeedbackSeverity severity = FeedbackSeverity.Info)
        {
            OnActionLogAdded?.Invoke(sourceId, message, severity);
        }

        public void ConsequenceAdded(string sourceActionId, string description, int durationTurns, bool reversible)
        {
            OnConsequenceAdded?.Invoke(sourceActionId, description, durationTurns, reversible);
        }

        public void GlobalAlertRaised(string message, FeedbackSeverity severity = FeedbackSeverity.Warning)
        {
            OnGlobalAlertRaised?.Invoke(message, severity);
        }

        public void NotificationAdded(string sourceId, string message, FeedbackSeverity severity = FeedbackSeverity.Info)
        {
            OnNotificationAdded?.Invoke(sourceId, message, severity);
        }

        public void AlertAdded(string sourceId, string message, FeedbackSeverity severity = FeedbackSeverity.Warning)
        {
            OnAlertAdded?.Invoke(sourceId, message, severity);
        }

        public void NarrativeEventAdded(string eventId, string message, FeedbackSeverity severity = FeedbackSeverity.Info)
        {
            OnNarrativeEventAdded?.Invoke(eventId, message, severity);
        }

        public void IntelReportAdded(string sourceId, string message, FeedbackSeverity severity = FeedbackSeverity.Info)
        {
            OnIntelReportAdded?.Invoke(sourceId, message, severity);
        }
    }
}

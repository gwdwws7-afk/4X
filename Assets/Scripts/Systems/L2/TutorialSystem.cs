using System;
using System.Collections.Generic;
using UnityEngine;
using EventideAge.Core;
using EventideAge.Systems.I1;

namespace EventideAge.Systems.L2
{
    public enum TutorialLifecycleState
    {
        Inactive = 0,
        Active = 1,
        Completed = 2,
        Skipped = 3
    }

    [Serializable]
    internal class TutorialProgressSnapshot
    {
        public TutorialLifecycleState State;
        public bool HintsEnabled;
        public string ActiveStepId;
        public string[] CompletedStepIds = Array.Empty<string>();
    }

    public class TutorialSystem : GameSystem
    {
        private const string kDefaultProgressKey = "L2.Tutorial.Progress.v1";

        [Header("L2 Tutorial")]
        public TutorialFlowConfig TutorialFlowConfigAsset;
        public bool AutoStartOnInitialize = true;
        public bool HintsEnabled = true;
        public string ProgressSaveKey = kDefaultProgressKey;

        private readonly List<string> _completedStepIds = new List<string>();
        private string _activeStepId;
        private TutorialLifecycleState _state = TutorialLifecycleState.Inactive;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            LoadProgress();

            if (AutoStartOnInitialize
                && TutorialFlowConfigAsset != null
                && _state != TutorialLifecycleState.Completed
                && _state != TutorialLifecycleState.Skipped)
            {
                StartTutorial(TutorialFlowConfigAsset, keepExistingProgress: true);
            }
        }

        public override void OnTurnStarted(int turnNumber)
        {
            TryActivateNextStep();
        }

        public override void OnPhaseEntered(int phaseIndex)
        {
            TryActivateNextStep();
        }

        public TutorialLifecycleState GetLifecycleState()
        {
            return _state;
        }

        public int GetCompletedStepCount()
        {
            return _completedStepIds.Count;
        }

        public int GetTotalStepCount()
        {
            return TutorialFlowConfigAsset?.Steps?.Length ?? 0;
        }

        public string GetActiveStepId()
        {
            return _activeStepId;
        }

        public bool IsTutorialActive()
        {
            return _state == TutorialLifecycleState.Active;
        }

        public void EnableHints(bool enabled)
        {
            HintsEnabled = enabled;
            SaveProgress();
        }

        public void StartTutorial(TutorialFlowConfig config = null, bool keepExistingProgress = false)
        {
            if (config != null)
            {
                TutorialFlowConfigAsset = config;
            }

            if (TutorialFlowConfigAsset == null || TutorialFlowConfigAsset.Steps.Length == 0)
            {
                _state = TutorialLifecycleState.Inactive;
                _activeStepId = null;
                SaveProgress();
                return;
            }

            if (!keepExistingProgress)
            {
                _completedStepIds.Clear();
                _activeStepId = null;
            }

            _state = TutorialLifecycleState.Active;
            Events.NotificationAdded("L2", "Tutorial started.", FeedbackSeverity.Info);
            TryActivateNextStep();
            SaveProgress();
        }

        public void SkipTutorial()
        {
            _state = TutorialLifecycleState.Skipped;
            _activeStepId = null;
            SaveProgress();
            Events.AlertAdded("L2", "Tutorial skipped by player.", FeedbackSeverity.Warning);
        }

        public void ResetTutorial()
        {
            _completedStepIds.Clear();
            _activeStepId = null;
            _state = TutorialLifecycleState.Inactive;
            SaveProgress();
        }

        public bool AcknowledgeActiveStep()
        {
            if (_state != TutorialLifecycleState.Active || string.IsNullOrWhiteSpace(_activeStepId))
            {
                return false;
            }

            if (!_completedStepIds.Contains(_activeStepId))
            {
                _completedStepIds.Add(_activeStepId);
            }

            Events.ActionLogAdded("L2", $"Tutorial step completed: {_activeStepId}", FeedbackSeverity.Info);
            _activeStepId = null;

            if (_completedStepIds.Count >= GetTotalStepCount())
            {
                _state = TutorialLifecycleState.Completed;
                Events.GlobalAlertRaised("Tutorial completed. Full game guidance disabled.", FeedbackSeverity.Info);
            }
            else
            {
                TryActivateNextStep();
            }

            SaveProgress();
            return true;
        }

        private void TryActivateNextStep()
        {
            if (_state != TutorialLifecycleState.Active)
            {
                return;
            }

            if (TutorialFlowConfigAsset == null || TutorialFlowConfigAsset.Steps.Length == 0)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(_activeStepId))
            {
                return;
            }

            TutorialStepTemplate[] steps = TutorialFlowConfigAsset.Steps;
            for (int i = 0; i < steps.Length; i++)
            {
                TutorialStepTemplate step = steps[i];
                if (step == null || !step.Enabled || string.IsNullOrWhiteSpace(step.StepId))
                {
                    continue;
                }

                if (_completedStepIds.Contains(step.StepId))
                {
                    continue;
                }

                if (!IsStepTriggered(step))
                {
                    continue;
                }

                _activeStepId = step.StepId;
                if (HintsEnabled)
                {
                    Events.NarrativeEventAdded(
                        $"L2.{step.StepId}",
                        string.IsNullOrWhiteSpace(step.Narrative) ? step.EventName : step.Narrative,
                        FeedbackSeverity.Info);
                }
                Events.NotificationAdded(
                    "L2",
                    $"Tutorial step active: {step.EventName} ({step.StepId})",
                    FeedbackSeverity.Info);
                SaveProgress();
                return;
            }
        }

        private bool IsStepTriggered(TutorialStepTemplate step)
        {
            switch (step.Trigger)
            {
                case EventTrigger.TurnBased:
                    return State.CurrentTurn >= Mathf.Max(1, step.TriggerTurn);
                case EventTrigger.ConditionBased:
                case EventTrigger.ActionBased:
                    return EvaluateCondition(step.TriggerCondition);
                case EventTrigger.Random:
                    return UnityEngine.Random.value <= 1f;
                default:
                    return false;
            }
        }

        private bool EvaluateCondition(string condition)
        {
            if (string.IsNullOrWhiteSpace(condition))
            {
                return false;
            }

            string[] clauses = condition.Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < clauses.Length; i++)
            {
                if (!EvaluateClause(clauses[i].Trim()))
                {
                    return false;
                }
            }

            return true;
        }

        private bool EvaluateClause(string clause)
        {
            if (TryParseComparison(clause, out string left, out string op, out string right) == false)
            {
                return false;
            }

            if (string.Equals(left, "turn", StringComparison.OrdinalIgnoreCase))
            {
                return int.TryParse(right, out int value) && Compare(State.CurrentTurn, op, value);
            }

            if (left.StartsWith("resource:", StringComparison.OrdinalIgnoreCase))
            {
                string resourceId = left.Substring("resource:".Length);
                var resource = State.GetResource(resourceId);
                int amount = resource?.Amount ?? 0;
                return int.TryParse(right, out int value) && Compare(amount, op, value);
            }

            if (left.StartsWith("relation:", StringComparison.OrdinalIgnoreCase))
            {
                string factionId = left.Substring("relation:".Length);
                var faction = State.GetFaction(factionId);
                int relation = faction?.RelationshipWithPlayer ?? 0;
                return int.TryParse(right, out int value) && Compare(relation, op, value);
            }

            if (left.StartsWith("node_control:", StringComparison.OrdinalIgnoreCase))
            {
                string nodeId = left.Substring("node_control:".Length);
                var node = State.GetNode(nodeId);
                string controller = node?.ControllingFactionId ?? string.Empty;
                string expected = GameIds.ResolveFactionId(right);
                bool equals = string.Equals(controller, expected, StringComparison.OrdinalIgnoreCase);
                return op == "!=" ? !equals : equals;
            }

            return false;
        }

        private static bool TryParseComparison(string expression, out string left, out string op, out string right)
        {
            left = null;
            op = null;
            right = null;

            string[] operators = { ">=", "<=", "!=", "==", "=", ">", "<" };
            for (int i = 0; i < operators.Length; i++)
            {
                int index = expression.IndexOf(operators[i], StringComparison.Ordinal);
                if (index <= 0)
                {
                    continue;
                }

                left = expression.Substring(0, index).Trim();
                op = operators[i];
                right = expression.Substring(index + operators[i].Length).Trim();
                if (!string.IsNullOrWhiteSpace(left) && !string.IsNullOrWhiteSpace(right))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool Compare(int left, string op, int right)
        {
            switch (op)
            {
                case ">":
                    return left > right;
                case "<":
                    return left < right;
                case ">=":
                    return left >= right;
                case "<=":
                    return left <= right;
                case "!=":
                    return left != right;
                case "=":
                case "==":
                    return left == right;
                default:
                    return false;
            }
        }

        private void LoadProgress()
        {
            _completedStepIds.Clear();
            _activeStepId = null;

            string key = GetProgressSaveKey();
            if (!PlayerPrefs.HasKey(key))
            {
                _state = TutorialLifecycleState.Inactive;
                return;
            }

            string json = PlayerPrefs.GetString(key, string.Empty);
            if (string.IsNullOrWhiteSpace(json))
            {
                _state = TutorialLifecycleState.Inactive;
                return;
            }

            TutorialProgressSnapshot snapshot = JsonUtility.FromJson<TutorialProgressSnapshot>(json);
            if (snapshot == null)
            {
                _state = TutorialLifecycleState.Inactive;
                return;
            }

            _state = snapshot.State;
            HintsEnabled = snapshot.HintsEnabled;
            _activeStepId = snapshot.ActiveStepId;

            if (snapshot.CompletedStepIds != null)
            {
                for (int i = 0; i < snapshot.CompletedStepIds.Length; i++)
                {
                    string stepId = snapshot.CompletedStepIds[i];
                    if (!string.IsNullOrWhiteSpace(stepId))
                    {
                        _completedStepIds.Add(stepId);
                    }
                }
            }
        }

        private void SaveProgress()
        {
            string key = GetProgressSaveKey();
            var snapshot = new TutorialProgressSnapshot
            {
                State = _state,
                HintsEnabled = HintsEnabled,
                ActiveStepId = _activeStepId,
                CompletedStepIds = _completedStepIds.ToArray()
            };

            string json = JsonUtility.ToJson(snapshot);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        private string GetProgressSaveKey()
        {
            return string.IsNullOrWhiteSpace(ProgressSaveKey) ? kDefaultProgressKey : ProgressSaveKey.Trim();
        }
    }
}

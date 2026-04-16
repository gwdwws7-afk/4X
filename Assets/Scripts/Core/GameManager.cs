using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace EventideAge.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Core Assets")]
        public GameState State;
        public GameEvents Events;
        public GameConfig Config;
        
        [Header("Systems")]
        public List<GameSystem> Systems;
        
        public static GameManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            InitializeGame();
        }
        
        public void InitializeGame()
        {
            if (Config == null)
            {
                Debug.LogError("GameConfig is not assigned!");
                return;
            }
            
            if (Systems == null)
            {
                Systems = new List<GameSystem>();
            }

            DiscoverSystemsFromChildren();
            
            State.Config = Config;
            State.Initialize();
            
            foreach (var system in Systems)
            {
                system.Initialize(State, Events);
            }
            
            WireSystemReferences();
            
            Events.OnTurnChanged += HandleTurnChanged;
            Events.OnPhaseChanged += HandlePhaseChanged;
            Events.OnActionPointsChanged += HandleAPChanged;

            Events.ActionLogAdded("Core", "Game initialized", FeedbackSeverity.Info);
            
            Debug.Log($"[GameManager] Initialized. Turn {State.CurrentTurn}, Phase {State.CurrentPhaseIndex}");
        }
        
        private void HandleTurnChanged(int oldTurn, int newTurn)
        {
            State.ResetTurnActionPoints();
            Events.ActionPointsChanged(State.ActionPointsRemaining);
            Events.ActionLogAdded("Core", $"Turn advanced: {oldTurn} -> {newTurn}", FeedbackSeverity.Info);

            foreach (var system in Systems)
            {
                system.OnTurnStarted(newTurn);
            }
        }
        
        private void HandlePhaseChanged(int newPhaseIndex)
        {
            State.PreparePhaseActionPoints(newPhaseIndex);
            Events.ActionPointsChanged(State.ActionPointsRemaining);
            Events.ActionLogAdded("Core", $"Phase entered: {newPhaseIndex}", FeedbackSeverity.Info);

            foreach (var system in Systems)
            {
                system.OnPhaseEntered(newPhaseIndex);
            }
        }
        
        private void HandleAPChanged(int remaining)
        {
            Debug.Log($"[GameManager] AP Changed: {remaining}");
        }
        
        public bool SpendActionPoints(int cost)
        {
            if (!State.TrySpendActionPoints(cost))
            {
                int immediate = State.CurrentPhaseActionPointsRemaining + State.UniversalActionPointsRemaining;
                Debug.LogWarning($"[GameManager] Not enough AP: need {cost}, immediate {immediate}, turn-remaining {State.ActionPointsRemaining}");
                return false;
            }

            Events.ActionPointsChanged(State.ActionPointsRemaining);
            return true;
        }

        public bool CanSpendActionPoints(int cost)
        {
            return State != null && State.CanSpendActionPoints(cost);
        }
        
        public void AdvancePhase()
        {
            int currentPhase = State.CurrentPhaseIndex;
            int totalPhases = State.Config?.PhaseConfigs?.Length ?? 6;

            State.ExpireCurrentPhaseActionPoints();
            Events.ActionPointsChanged(State.ActionPointsRemaining);
            Events.PhaseExited(currentPhase);

            int nextPhase = (currentPhase + 1) % totalPhases;

            if (nextPhase == 0)
            {
                int oldTurn = State.CurrentTurn;
                Events.TurnEnded(State.CurrentTurn);
                State.CurrentTurn++;
                Events.TurnChanged(oldTurn, State.CurrentTurn);
            }
            
            State.CurrentPhaseIndex = nextPhase;
            Events.PhaseChanged(nextPhase);
        }
        
        public void AdvanceTurn()
        {
            int oldTurn = State.CurrentTurn;
            State.CurrentTurn++;
            Events.TurnChanged(oldTurn, State.CurrentTurn);
            State.CurrentPhaseIndex = 0;
            Events.PhaseChanged(0);
        }
        
        private void WireSystemReferences()
        {
            foreach (var targetSystem in Systems)
            {
                if (targetSystem == null) continue;
                
                System.Type type = targetSystem.GetType();
                
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var prop in properties)
                {
                    if (!prop.CanWrite) continue;
                    if (!typeof(GameSystem).IsAssignableFrom(prop.PropertyType)) continue;
                    
                    GameSystem foundSystem = FindSystemOfType(prop.PropertyType);
                    if (foundSystem != null && foundSystem != targetSystem)
                    {
                        prop.SetValue(targetSystem, foundSystem);
                        Debug.Log($"[GameManager] Wired {foundSystem.GetType().Name} to {type.Name}.{prop.Name}");
                    }
                }
                
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
                foreach (var field in fields)
                {
                    if (!typeof(GameSystem).IsAssignableFrom(field.FieldType)) continue;
                    
                    GameSystem foundSystem = FindSystemOfType(field.FieldType);
                    if (foundSystem != null && foundSystem != targetSystem)
                    {
                        field.SetValue(targetSystem, foundSystem);
                        Debug.Log($"[GameManager] Wired {foundSystem.GetType().Name} to {type.Name}.{field.Name}");
                    }
                }
            }
        }
        
        private GameSystem FindSystemOfType(System.Type systemType)
        {
            foreach (var system in Systems)
            {
                if (system != null && systemType.IsAssignableFrom(system.GetType()))
                {
                    return system;
                }
            }
            return null;
        }
        
        private void DiscoverSystemsFromChildren()
        {
            if (Systems == null) Systems = new List<GameSystem>();
            
            var childSystems = GetComponentsInChildren<GameSystem>(true);
            foreach (var system in childSystems)
            {
                if (system != this && !Systems.Contains(system))
                {
                    Systems.Add(system);
                    Debug.Log($"[GameManager] Discovered system: {system.GetType().Name}");
                }
            }
        }
    }
}

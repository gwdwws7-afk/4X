using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;
using EventideAge.Systems.A2;
using EventideAge.Systems.A4;
using EventideAge.Systems.B5;
using EventideAge.Systems.C2;
using EventideAge.Systems.C3;
using EventideAge.Systems.D1;
using EventideAge.Systems.F1;
using EventideAge.Systems.J;
using EventideAge.Systems.L4;

namespace EventideAge.UI
{
    public class LaunchFlowUIController : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private bool _autoBuildPanel = true;
        [SerializeField] private string _panelName = "LaunchFlowPanel";
        [SerializeField] private string _saveSlot = "release_quickslot";
        [SerializeField] private bool _startWithGuideExpanded = true;

        [Header("Targets")]
        [SerializeField] private string[] _preferredNodes = new[]
        {
            GameIds.Node.IraqBorder,
            GameIds.Node.SyriaZone,
            GameIds.Node.Hormuz,
            GameIds.Node.Caspian,
            GameIds.Node.Mediterranean
        };

        private GameManager _manager;
        private PhaseEngine _phaseEngine;
        private SaveSystem _saveSystem;
        private EconomicSettlementSystem _settlementSystem;
        private DiplomaticProtocolsSystem _protocolSystem;
        private IdeologySystem _ideologySystem;
        private MilitaryOperationsSystem _militarySystem;
        private IntelligenceSystem _intelligenceSystem;
        private VictoryDefeatSystem _victorySystem;
        private LocalizationSystem _localizationSystem;

        private Text _statusText;
        private Text _nodeText;
        private Text _guideText;
        private Text _phaseText;
        private Text _titleText;
        private Text _hintText;

        private Button _runTurnButton;
        private Button _nextPhaseButton;
        private Button _runCampaignButton;
        private Button _guideButton;
        private Button _diplomacyButton;
        private Button _strategyButton;
        private Button _combatButton;
        private Button _logisticsButton;
        private Button _intelButton;
        private Button _nextNodeButton;
        private Button _saveButton;
        private Button _loadButton;

        private int _nodeIndex;
        private bool _guideExpanded;
        private float _nextRefreshAt;

        private static readonly Color kPanelColor = new Color(0.07f, 0.11f, 0.17f, 0.94f);
        private static readonly Color kHeaderColor = new Color(0.12f, 0.23f, 0.33f, 0.98f);
        private static readonly Color kPrimaryButtonColor = new Color(0.17f, 0.34f, 0.45f, 0.96f);
        private static readonly Color kSecondaryButtonColor = new Color(0.19f, 0.24f, 0.33f, 0.96f);
        private static readonly Color kDangerButtonColor = new Color(0.36f, 0.2f, 0.18f, 0.96f);
        private static readonly Color kDisabledColor = new Color(0.24f, 0.26f, 0.28f, 0.8f);
        private static readonly Color kActivePhaseColor = new Color(0.22f, 0.45f, 0.3f, 0.98f);

        private void Start()
        {
            _guideExpanded = _startWithGuideExpanded;
            RefreshBindings();
            if (_autoBuildPanel)
            {
                BuildPanelIfNeeded();
            }

            RefreshLocalizedTexts();
            UpdateStatus();
        }

        private void Update()
        {
            if (Time.unscaledTime < _nextRefreshAt)
            {
                return;
            }

            _nextRefreshAt = Time.unscaledTime + 0.3f;
            UpdateStatus();
        }

        public void RefreshBindings()
        {
            _manager = GameManager.Instance != null ? GameManager.Instance : FindObjectOfType<GameManager>();
            _phaseEngine = FindSystem<PhaseEngine>();
            _saveSystem = FindSystem<SaveSystem>();
            _settlementSystem = FindSystem<EconomicSettlementSystem>();
            _protocolSystem = FindSystem<DiplomaticProtocolsSystem>();
            _ideologySystem = FindSystem<IdeologySystem>();
            _militarySystem = FindSystem<MilitaryOperationsSystem>();
            _intelligenceSystem = FindSystem<IntelligenceSystem>();
            _victorySystem = FindSystem<VictoryDefeatSystem>();
            _localizationSystem = FindSystem<LocalizationSystem>();
        }

        private T FindSystem<T>() where T : GameSystem
        {
            if (_manager == null || _manager.Systems == null)
            {
                return null;
            }

            for (int i = 0; i < _manager.Systems.Count; i++)
            {
                if (_manager.Systems[i] is T typed)
                {
                    return typed;
                }
            }

            return null;
        }

        private void BuildPanelIfNeeded()
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                var canvasGo = new GameObject("LaunchFlowCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                canvas = canvasGo.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            Transform existing = canvas.transform.Find(_panelName);
            if (existing != null)
            {
                CacheExistingRefs(existing);
                return;
            }

            var panelGo = new GameObject(_panelName, typeof(RectTransform), typeof(Image));
            panelGo.transform.SetParent(canvas.transform, false);
            var panelRect = panelGo.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(1f, 0f);
            panelRect.anchorMax = new Vector2(1f, 0f);
            panelRect.pivot = new Vector2(1f, 0f);
            panelRect.anchoredPosition = new Vector2(-20f, 20f);
            panelRect.sizeDelta = new Vector2(460f, 430f);
            panelGo.GetComponent<Image>().color = kPanelColor;

            var header = new GameObject("Header", typeof(RectTransform), typeof(Image));
            header.transform.SetParent(panelGo.transform, false);
            var headerRect = header.GetComponent<RectTransform>();
            headerRect.anchorMin = new Vector2(0f, 1f);
            headerRect.anchorMax = new Vector2(1f, 1f);
            headerRect.pivot = new Vector2(0.5f, 1f);
            headerRect.anchoredPosition = Vector2.zero;
            headerRect.sizeDelta = new Vector2(0f, 38f);
            header.GetComponent<Image>().color = kHeaderColor;

            _titleText = CreateText(header.transform, "Title", new Vector2(12f, -8f), new Vector2(330f, 24f), 15, TextAnchor.MiddleLeft, FontStyle.Bold);
            _titleText.text = "Launch Flow Control";

            _guideButton = CreateButton(
                header.transform,
                "GuideToggleButton",
                _guideExpanded ? "Hide Guide" : "Show Guide",
                new Vector2(336f, -5f),
                new Vector2(108f, 28f),
                kPrimaryButtonColor,
                ToggleGuide);

            _statusText = CreateText(panelGo.transform, "StatusText", new Vector2(14f, -50f), new Vector2(430f, 76f), 14, TextAnchor.UpperLeft, FontStyle.Normal);
            _phaseText = CreateText(panelGo.transform, "PhaseText", new Vector2(14f, -128f), new Vector2(430f, 24f), 13, TextAnchor.MiddleLeft, FontStyle.Bold);
            _nodeText = CreateText(panelGo.transform, "NodeText", new Vector2(14f, -154f), new Vector2(430f, 24f), 13, TextAnchor.MiddleLeft, FontStyle.Normal);

            _runTurnButton = CreateButton(panelGo.transform, "RunTurnButton", "Run Full Turn", new Vector2(14f, -186f), new Vector2(140f, 34f), kPrimaryButtonColor, RunFullTurn);
            _nextPhaseButton = CreateButton(panelGo.transform, "NextPhaseButton", "Next Phase", new Vector2(160f, -186f), new Vector2(140f, 34f), kSecondaryButtonColor, AdvancePhaseOnly);
            _runCampaignButton = CreateButton(panelGo.transform, "RunCampaignButton", "Run to Turn 24", new Vector2(306f, -186f), new Vector2(138f, 34f), kDangerButtonColor, RunToEndgame);

            _diplomacyButton = CreateButton(panelGo.transform, "DiplomacyButton", "Diplomacy", new Vector2(14f, -226f), new Vector2(84f, 30f), kSecondaryButtonColor, DoDiplomacyAction);
            _strategyButton = CreateButton(panelGo.transform, "StrategyButton", "Strategy", new Vector2(103f, -226f), new Vector2(84f, 30f), kSecondaryButtonColor, DoIdeologyAction);
            _combatButton = CreateButton(panelGo.transform, "CombatButton", "Combat", new Vector2(192f, -226f), new Vector2(84f, 30f), kSecondaryButtonColor, DoMilitaryAction);
            _logisticsButton = CreateButton(panelGo.transform, "LogisticsButton", "Logistics", new Vector2(281f, -226f), new Vector2(84f, 30f), kSecondaryButtonColor, DoSettlementAction);
            _intelButton = CreateButton(panelGo.transform, "IntelButton", "Intel", new Vector2(370f, -226f), new Vector2(74f, 30f), kSecondaryButtonColor, DoIntelAction);

            _nextNodeButton = CreateButton(panelGo.transform, "NextNodeButton", "Switch Node", new Vector2(14f, -262f), new Vector2(140f, 30f), kSecondaryButtonColor, CycleTargetNode);
            _saveButton = CreateButton(panelGo.transform, "SaveButton", "Save", new Vector2(160f, -262f), new Vector2(140f, 30f), kSecondaryButtonColor, SaveQuick);
            _loadButton = CreateButton(panelGo.transform, "LoadButton", "Load", new Vector2(306f, -262f), new Vector2(138f, 30f), kSecondaryButtonColor, LoadQuick);

            _guideText = CreateText(panelGo.transform, "GuideText", new Vector2(14f, -298f), new Vector2(430f, 120f), 12, TextAnchor.UpperLeft, FontStyle.Normal);
            _guideText.text =
                "Recommended flow\\n" +
                "1. Diplomacy: lock a stable relation baseline.\\n" +
                "2. Strategy: use ideology action to build momentum.\\n" +
                "3. Combat: run proxy/spec-op on current target node.\\n" +
                "4. Logistics: execute settlement and review health alerts.\\n" +
                "5. Intel: recon target node before ending turn.";
            _guideText.gameObject.SetActive(_guideExpanded);

            _hintText = CreateText(panelGo.transform, "HintText", new Vector2(14f, -408f), new Vector2(430f, 18f), 11, TextAnchor.MiddleLeft, FontStyle.Italic);
            _hintText.text = "Use this panel to validate the full loop: Map -> Diplomacy -> Report -> Event.";

            RefreshLocalizedTexts();
        }

        private void CacheExistingRefs(Transform root)
        {
            _statusText = root.Find("StatusText")?.GetComponent<Text>();
            _nodeText = root.Find("NodeText")?.GetComponent<Text>();
            _phaseText = root.Find("PhaseText")?.GetComponent<Text>();
            _guideText = root.Find("GuideText")?.GetComponent<Text>();
            _hintText = root.Find("HintText")?.GetComponent<Text>();

            _runTurnButton = root.Find("RunTurnButton")?.GetComponent<Button>();
            _nextPhaseButton = root.Find("NextPhaseButton")?.GetComponent<Button>();
            _runCampaignButton = root.Find("RunCampaignButton")?.GetComponent<Button>();
            _diplomacyButton = root.Find("DiplomacyButton")?.GetComponent<Button>();
            _strategyButton = root.Find("StrategyButton")?.GetComponent<Button>();
            _combatButton = root.Find("CombatButton")?.GetComponent<Button>();
            _logisticsButton = root.Find("LogisticsButton")?.GetComponent<Button>();
            _intelButton = root.Find("IntelButton")?.GetComponent<Button>();
            _nextNodeButton = root.Find("NextNodeButton")?.GetComponent<Button>();
            _saveButton = root.Find("SaveButton")?.GetComponent<Button>();
            _loadButton = root.Find("LoadButton")?.GetComponent<Button>();

            var header = root.Find("Header");
            _guideButton = header != null ? header.Find("GuideToggleButton")?.GetComponent<Button>() : null;
            _titleText = header != null ? header.Find("Title")?.GetComponent<Text>() : null;
        }

        private static Text CreateText(
            Transform parent,
            string name,
            Vector2 anchoredPos,
            Vector2 size,
            int fontSize,
            TextAnchor anchor,
            FontStyle fontStyle)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(Text));
            go.transform.SetParent(parent, false);
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = anchoredPos;
            rect.sizeDelta = size;

            var text = go.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            if (text.font == null)
            {
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            }

            text.fontSize = fontSize;
            text.fontStyle = fontStyle;
            text.alignment = anchor;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.color = new Color(0.9f, 0.94f, 0.98f, 1f);
            return text;
        }

        private static Button CreateButton(
            Transform parent,
            string name,
            string label,
            Vector2 anchoredPos,
            Vector2 size,
            Color color,
            UnityEngine.Events.UnityAction onClick)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            go.transform.SetParent(parent, false);
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = anchoredPos;
            rect.sizeDelta = size;

            var image = go.GetComponent<Image>();
            image.color = color;

            var button = go.GetComponent<Button>();
            button.targetGraphic = image;
            button.onClick.AddListener(onClick);

            var text = CreateText(go.transform, "Text", Vector2.zero, Vector2.zero, 12, TextAnchor.MiddleCenter, FontStyle.Bold);
            text.text = label;
            var textRect = text.rectTransform;
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            return button;
        }

        private void ToggleGuide()
        {
            _guideExpanded = !_guideExpanded;
            if (_guideText != null)
            {
                _guideText.gameObject.SetActive(_guideExpanded);
            }
            RefreshLocalizedTexts();
        }

        private void RefreshLocalizedTexts()
        {
            if (_titleText != null)
            {
                _titleText.text = Localize("ui.launch.title", "Launch Flow Control");
            }

            SetButtonLabel(_guideButton, _guideExpanded
                ? Localize("ui.launch.guide.toggle.hide", "Hide Guide")
                : Localize("ui.launch.guide.toggle.show", "Show Guide"));
            SetButtonLabel(_runTurnButton, Localize("ui.launch.run_turn", "Run Full Turn"));
            SetButtonLabel(_nextPhaseButton, Localize("ui.launch.next_phase", "Next Phase"));
            SetButtonLabel(_runCampaignButton, Localize("ui.launch.run_campaign", "Run to Turn 24"));
            SetButtonLabel(_diplomacyButton, Localize("ui.launch.diplomacy", "Diplomacy"));
            SetButtonLabel(_strategyButton, Localize("ui.launch.strategy", "Strategy"));
            SetButtonLabel(_combatButton, Localize("ui.launch.combat", "Combat"));
            SetButtonLabel(_logisticsButton, Localize("ui.launch.logistics", "Logistics"));
            SetButtonLabel(_intelButton, Localize("ui.launch.intel", "Intel"));
            SetButtonLabel(_nextNodeButton, Localize("ui.launch.next_node", "Switch Node"));
            SetButtonLabel(_saveButton, Localize("ui.launch.save", "Save"));
            SetButtonLabel(_loadButton, Localize("ui.launch.load", "Load"));

            if (_guideText != null)
            {
                _guideText.text = Localize(
                    "ui.launch.guide.body",
                    "Recommended flow\n" +
                    "1. Diplomacy: lock a stable relation baseline.\n" +
                    "2. Strategy: use ideology action to build momentum.\n" +
                    "3. Combat: run proxy/spec-op on current target node.\n" +
                    "4. Logistics: execute settlement and review health alerts.\n" +
                    "5. Intel: recon target node before ending turn.");
            }

            if (_hintText != null)
            {
                _hintText.text = Localize(
                    "ui.launch.hint",
                    "Use this panel to validate the full loop: Map -> Diplomacy -> Report -> Event.");
            }
        }

        private string Localize(string key, string fallback)
        {
            if (_localizationSystem == null)
            {
                return fallback;
            }

            return _localizationSystem.Translate(key, fallback);
        }

        private static void SetButtonLabel(Button button, string label)
        {
            if (button == null)
            {
                return;
            }

            var text = button.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = label;
            }
        }

        private void RunToEndgame()
        {
            if (!EnsureReady())
            {
                return;
            }

            int guard = 256;
            while (!_victorySystem.IsGameEnded() && _manager.State.CurrentTurn <= GameConfig.kMaxTurns && guard-- > 0)
            {
                RunFullTurn();
            }

            UpdateStatus();
        }

        private void RunFullTurn()
        {
            if (!EnsureReady())
            {
                return;
            }

            int startTurn = _manager.State.CurrentTurn;
            int guard = 16;
            while (!_victorySystem.IsGameEnded() && _manager.State.CurrentTurn == startTurn && guard-- > 0)
            {
                ExecuteActionForCurrentPhase();
                _phaseEngine.AdvanceToNextPhase();
            }

                _manager.Events.NarrativeEventAdded(
                "release.turn.summary",
                $"Turn {startTurn} completed. Current turn: {_manager.State.CurrentTurn}.",
                FeedbackSeverity.Info);
            UpdateStatus();
        }

        private void AdvancePhaseOnly()
        {
            if (!EnsureReady())
            {
                return;
            }

            _phaseEngine.AdvanceToNextPhase();
            UpdateStatus();
        }

        private void ExecuteActionForCurrentPhase()
        {
            int phase = _manager.State.CurrentPhaseIndex;
            switch (phase)
            {
                case 0:
                    DoDiplomacyAction();
                    break;
                case 1:
                    DoIdeologyAction();
                    break;
                case 2:
                    DoMilitaryAction();
                    break;
                case 3:
                    DoSettlementAction();
                    break;
                case 4:
                    DoIntelAction();
                    break;
                default:
                    _manager.Events.ActionLogAdded("ReleaseFlow", "AI response phase executing.", FeedbackSeverity.Info);
                    break;
            }
        }

        private void DoDiplomacyAction()
        {
            if (!EnsureReady() || _protocolSystem == null)
            {
                return;
            }

            var proposal = _protocolSystem.ProposeProtocol(GameIds.Faction.Vashid, GameIds.Faction.Aurean, ProtocolType.TradeAgreement);
            if (proposal != null)
            {
                _protocolSystem.SignProtocol(proposal.ProtocolId);
                _manager.Events.NotificationAdded("ReleaseFlow.Diplomacy", $"Protocol signed: {proposal.ProtocolId} ({proposal.Type}).", FeedbackSeverity.Info);
            }
            else
            {
                _manager.Events.ActionLogAdded("ReleaseFlow.Diplomacy", "Protocol proposal skipped (already active or limit reached).", FeedbackSeverity.Warning);
            }

            UpdateStatus();
        }

        private void DoIdeologyAction()
        {
            if (!EnsureReady() || _ideologySystem == null)
            {
                return;
            }

            bool success = _ideologySystem.ExecuteAction(IdeologyActionType.Propaganda, GameIds.Faction.Aurean);
            _manager.Events.ActionLogAdded(
                "ReleaseFlow.Strategy",
                success ? "Ideology propaganda executed." : "Ideology propaganda failed or blocked.",
                success ? FeedbackSeverity.Info : FeedbackSeverity.Warning);
            UpdateStatus();
        }

        private void DoMilitaryAction()
        {
            if (!EnsureReady() || _militarySystem == null)
            {
                return;
            }

            string target = GetCurrentTargetNode();
            bool executed = _militarySystem.ExecuteAction(MilitaryActionType.Proxy, target);
            if (!executed)
            {
                executed = _militarySystem.ExecuteAction(MilitaryActionType.SpecialForces, target);
            }

            _manager.Events.ActionLogAdded(
                "ReleaseFlow.Combat",
                executed ? $"Combat action executed at {target}." : $"Combat action blocked at {target}.",
                executed ? FeedbackSeverity.Info : FeedbackSeverity.Warning);
            UpdateStatus();
        }

        private void DoIntelAction()
        {
            if (!EnsureReady() || _intelligenceSystem == null)
            {
                return;
            }

            string target = GetCurrentTargetNode();
            var report = _intelligenceSystem.ExecuteIntelligenceAction(IntelligenceActionType.Reconnaissance, target);
            if (report == null)
            {
                _manager.Events.IntelReportAdded("ReleaseFlow.Intel", $"Recon failed on {target} (resource or state requirement not met).", FeedbackSeverity.Warning);
            }
            else
            {
                _manager.Events.IntelReportAdded(
                    "ReleaseFlow.Intel",
                    $"Recon {target}: fog={report.FogLevel}, reliability={Mathf.RoundToInt(report.Reliability * 100f)}%",
                    report.IsDeceptive ? FeedbackSeverity.Warning : FeedbackSeverity.Info);
            }

            UpdateStatus();
        }

        private void DoSettlementAction()
        {
            if (!EnsureReady() || _settlementSystem == null)
            {
                return;
            }

            var result = _settlementSystem.ExecuteTurnSettlement();
            if (result != null && result.Health != null)
            {
                _manager.Events.NotificationAdded(
                    "ReleaseFlow.Logistics",
                    $"Settlement completed. Economy health {result.Health.Overall} ({result.Health.Grade}).",
                    result.Health.Overall < 50 ? FeedbackSeverity.Warning : FeedbackSeverity.Info);
            }
            else
            {
                _manager.Events.ActionLogAdded("ReleaseFlow.Logistics", "Settlement executed but no result payload was returned.", FeedbackSeverity.Warning);
            }

            UpdateStatus();
        }

        private void CycleTargetNode()
        {
            if (_preferredNodes == null || _preferredNodes.Length == 0)
            {
                return;
            }

            _nodeIndex = (_nodeIndex + 1) % _preferredNodes.Length;
            UpdateStatus();
        }

        private void SaveQuick()
        {
            if (!EnsureReady() || _saveSystem == null)
            {
                return;
            }

            bool ok = _saveSystem.SaveGame(_saveSlot);
            _manager.Events.NotificationAdded("ReleaseFlow.Save", ok ? $"Save succeeded: {_saveSlot}" : $"Save failed: {_saveSlot}", ok ? FeedbackSeverity.Info : FeedbackSeverity.Warning);
            UpdateStatus();
        }

        private void LoadQuick()
        {
            if (!EnsureReady() || _saveSystem == null)
            {
                return;
            }

            bool ok = _saveSystem.LoadGame(_saveSlot);
            _manager.Events.NotificationAdded("ReleaseFlow.Save", ok ? $"Load succeeded: {_saveSlot}" : $"Load failed: {_saveSlot}", ok ? FeedbackSeverity.Info : FeedbackSeverity.Warning);
            UpdateStatus();
        }

        private bool EnsureReady()
        {
            if (_manager == null || _phaseEngine == null)
            {
                RefreshBindings();
            }

            if (_manager == null || _manager.State == null || _manager.Events == null || _phaseEngine == null)
            {
                Debug.LogWarning("[LaunchFlowUIController] Missing GameManager or core systems.");
                return false;
            }

            return true;
        }

        private string GetCurrentTargetNode()
        {
            if (_manager == null || _manager.State == null || _preferredNodes == null || _preferredNodes.Length == 0)
            {
                return GameIds.Node.IraqBorder;
            }

            for (int i = 0; i < _preferredNodes.Length; i++)
            {
                string candidate = _preferredNodes[(_nodeIndex + i) % _preferredNodes.Length];
                if (_manager.State.GetNode(candidate) != null)
                {
                    _nodeIndex = (_nodeIndex + i) % _preferredNodes.Length;
                    return candidate;
                }
            }

            return _preferredNodes[_nodeIndex % _preferredNodes.Length];
        }

        private void UpdateStatus()
        {
            if (_statusText == null || _manager == null || _manager.State == null)
            {
                return;
            }

            int phaseIndex = _manager.State.CurrentPhaseIndex;
            string phaseName = ResolvePhaseName(phaseIndex);
            bool ended = _victorySystem != null && _victorySystem.IsGameEnded();
            string endState = ended ? $"Ended: {_victorySystem.GetEndReason()}" : "In progress";

            _statusText.text =
                $"Turn {_manager.State.CurrentTurn}/{GameConfig.kMaxTurns}  Phase {phaseIndex} ({phaseName})\\n" +
                $"AP {_manager.State.ActionPointsRemaining} (phase {_manager.State.CurrentPhaseActionPointsRemaining} / universal {_manager.State.UniversalActionPointsRemaining})\\n" +
                $"{endState}";

            if (_phaseText != null)
            {
                _phaseText.text = $"Current flow step: {phaseName}";
            }

            if (_nodeText != null)
            {
                _nodeText.text = $"Target node: {GetCurrentTargetNode()}  Save slot: {_saveSlot}";
            }

            UpdateButtonState(ended);
            UpdatePhaseButtonColor(phaseIndex);
        }

        private void UpdateButtonState(bool ended)
        {
            SetInteractable(_runTurnButton, !ended);
            SetInteractable(_nextPhaseButton, !ended);
            SetInteractable(_runCampaignButton, !ended);
            SetInteractable(_diplomacyButton, !ended);
            SetInteractable(_strategyButton, !ended);
            SetInteractable(_combatButton, !ended);
            SetInteractable(_logisticsButton, !ended);
            SetInteractable(_intelButton, !ended);
            SetInteractable(_nextNodeButton, true);
            SetInteractable(_saveButton, true);
            SetInteractable(_loadButton, true);
        }

        private void UpdatePhaseButtonColor(int phaseIndex)
        {
            ApplyPhaseColor(_diplomacyButton, phaseIndex == 0);
            ApplyPhaseColor(_strategyButton, phaseIndex == 1);
            ApplyPhaseColor(_combatButton, phaseIndex == 2);
            ApplyPhaseColor(_logisticsButton, phaseIndex == 3);
            ApplyPhaseColor(_intelButton, phaseIndex == 4);
        }

        private void SetInteractable(Button button, bool interactable)
        {
            if (button == null)
            {
                return;
            }

            button.interactable = interactable;
            var image = button.GetComponent<Image>();
            if (image != null && !interactable)
            {
                image.color = kDisabledColor;
            }
        }

        private void ApplyPhaseColor(Button button, bool active)
        {
            if (button == null || !button.interactable)
            {
                return;
            }

            var image = button.GetComponent<Image>();
            if (image == null)
            {
                return;
            }

            image.color = active ? kActivePhaseColor : kSecondaryButtonColor;
        }

        private string ResolvePhaseName(int phaseIndex)
        {
            if (_manager == null || _manager.State == null || _manager.State.Config == null || _manager.State.Config.PhaseConfigs == null)
            {
                return "Unknown";
            }

            if (phaseIndex < 0 || phaseIndex >= _manager.State.Config.PhaseConfigs.Length)
            {
                return "Unknown";
            }

            return _manager.State.Config.PhaseConfigs[phaseIndex].PhaseName;
        }
    }
}

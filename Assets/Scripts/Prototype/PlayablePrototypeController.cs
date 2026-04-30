using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EventideAge.Prototype
{
    public class PlayablePrototypeController : MonoBehaviour
    {
        private enum Faction
        {
            Neutral = 0,
            Player = 1,
            AI = 2
        }

        private sealed class NodeSeed
        {
            public string Id;
            public Vector2 Position;
            public int Yield;
            public Faction Owner;
            public Faction Unit;
            public string[] Neighbors;
        }

        private sealed class NodeState
        {
            public string Id;
            public Vector3 Position;
            public int Yield;
            public Faction Owner;
            public Faction Unit;
            public List<string> Neighbors;
            public GameObject NodeObject;
            public Renderer NodeRenderer;
            public GameObject UnitObject;
            public TextMesh Label;
        }

        private sealed class EdgeRender
        {
            public string Left;
            public string Right;
            public LineRenderer Renderer;
        }

        private readonly Dictionary<string, NodeState> _nodes = new Dictionary<string, NodeState>(StringComparer.OrdinalIgnoreCase);
        private readonly List<Tuple<string, string>> _edges = new List<Tuple<string, string>>();
        private readonly List<EdgeRender> _edgeRenderers = new List<EdgeRender>();
        private readonly HashSet<string> _visibleNodeIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly Queue<string> _battleLog = new Queue<string>();
        private readonly System.Random _rng = new System.Random(404);

        private Material _lineMaterial;
        private NodeState _selectedNode;

        private Text _hudText;
        private Text _hintText;
        private Text _resourceText;
        private Text _fogText;
        private Text _battleLogText;
        private Button _endTurnButton;
        private Button _ceasefireButton;
        private Button _sanctionButton;

        private int _turn = 1;
        private const int kMaxTurns = 24;
        private int _playerAp = 3;
        private const int kPlayerApPerTurn = 3;
        private const int kAiApPerTurn = 2;

        private int _playerGold = 30;
        private int _aiGold = 30;
        private int _diplomacy = 55;

        private const int kCeasefireApCost = 1;
        private const int kCeasefireGoldCost = 6;
        private const int kCeasefireDuration = 2;
        private int _ceasefireTurnsRemaining;

        private const int kSanctionApCost = 1;
        private const int kSanctionGoldCost = 8;
        private const int kSanctionCooldownDuration = 2;
        private const int kSanctionAIGoldHit = 12;
        private const int kSanctionAISuppressionDuration = 2;
        private int _sanctionCooldownTurns;
        private int _aiSuppressionTurnsRemaining;

        private bool _gameEnded;
        private string _statusMessage = "Select a blue unit block, then click an adjacent node to move/capture.";

        private void Start()
        {
            SetupCamera();
            EnsureEventSystem();
            EnsureLineMaterial();

            CreateMapBackdrop();
            CreateCurveContours();
            CreatePlayableNodes();
            CreateRouteCurves();
            CreateHud();

            AddBattleLog("SYSTEM", "Prototype initialized.");
            RefreshAllVisuals();
            UpdateHud();
        }

        private void Update()
        {
            if (_gameEnded)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandlePointerClick();
            }
        }

        private void SetupCamera()
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                var camGo = new GameObject("Main Camera");
                cam = camGo.AddComponent<Camera>();
                camGo.tag = "MainCamera";
            }

            cam.orthographic = true;
            cam.orthographicSize = 5.2f;
            cam.transform.position = new Vector3(-0.3f, 0.8f, -10f);
            cam.transform.rotation = Quaternion.identity;
            cam.backgroundColor = new Color(0.06f, 0.08f, 0.11f, 1f);

            if (FindObjectOfType<Light>() == null)
            {
                var lightGo = new GameObject("Directional Light");
                var light = lightGo.AddComponent<Light>();
                light.type = LightType.Directional;
                light.intensity = 0.9f;
                light.transform.rotation = Quaternion.Euler(40f, -25f, 0f);
            }
        }

        private void EnsureEventSystem()
        {
            if (FindObjectOfType<EventSystem>() != null)
            {
                return;
            }

            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }

        private void EnsureLineMaterial()
        {
            _lineMaterial = new Material(Shader.Find("Sprites/Default"));
        }

        private void CreateMapBackdrop()
        {
            var backdrop = GameObject.CreatePrimitive(PrimitiveType.Quad);
            backdrop.name = "MapBackdrop";
            backdrop.transform.SetParent(transform, false);
            backdrop.transform.position = new Vector3(-0.3f, 0.8f, 1.2f);
            backdrop.transform.localScale = new Vector3(12.6f, 9.2f, 1f);
            var renderer = backdrop.GetComponent<Renderer>();
            renderer.material = new Material(Shader.Find("Standard"));
            renderer.material.color = new Color(0.08f, 0.11f, 0.15f, 1f);
            Destroy(backdrop.GetComponent<Collider>());
        }

        private void CreateCurveContours()
        {
            Vector3[] outerContour =
            {
                new Vector3(-4.5f, 2.1f, 0.25f),
                new Vector3(-3.8f, 2.9f, 0.25f),
                new Vector3(-2.6f, 3.5f, 0.25f),
                new Vector3(-1.3f, 3.9f, 0.25f),
                new Vector3(0.1f, 4.0f, 0.25f),
                new Vector3(1.9f, 3.6f, 0.25f),
                new Vector3(3.2f, 2.5f, 0.25f),
                new Vector3(3.7f, 1.2f, 0.25f),
                new Vector3(3.0f, -0.1f, 0.25f),
                new Vector3(2.3f, -1.0f, 0.25f),
                new Vector3(1.1f, -1.8f, 0.25f),
                new Vector3(-0.2f, -2.3f, 0.25f),
                new Vector3(-1.6f, -2.2f, 0.25f),
                new Vector3(-2.8f, -1.6f, 0.25f),
                new Vector3(-3.6f, -0.6f, 0.25f),
                new Vector3(-4.1f, 0.6f, 0.25f),
                new Vector3(-4.5f, 2.1f, 0.25f)
            };

            Vector3[] northernArc =
            {
                new Vector3(-2.8f, 3.1f, 0.24f),
                new Vector3(-1.7f, 3.3f, 0.24f),
                new Vector3(-0.5f, 3.45f, 0.24f),
                new Vector3(0.9f, 3.35f, 0.24f),
                new Vector3(2.2f, 3.05f, 0.24f)
            };

            Vector3[] gulfArc =
            {
                new Vector3(-1.3f, 0.6f, 0.24f),
                new Vector3(-0.4f, 0.2f, 0.24f),
                new Vector3(0.5f, -0.05f, 0.24f),
                new Vector3(1.5f, -0.25f, 0.24f),
                new Vector3(2.2f, -0.8f, 0.24f)
            };

            CreateCurveLine("MapContourMain", outerContour, new Color(0.52f, 0.6f, 0.68f, 1f), 0.06f, 12);
            CreateCurveLine("MapContourNorth", northernArc, new Color(0.34f, 0.44f, 0.52f, 0.95f), 0.04f, 10);
            CreateCurveLine("MapContourGulf", gulfArc, new Color(0.3f, 0.5f, 0.56f, 0.95f), 0.05f, 10);
        }

        private void CreatePlayableNodes()
        {
            NodeSeed[] seeds =
            {
                new NodeSeed { Id = "Tabriz", Position = new Vector2(-1.0f, 3.0f), Yield = 4, Owner = Faction.Player, Unit = Faction.Player, Neighbors = new [] { "Tehran", "Baku", "Baghdad" } },
                new NodeSeed { Id = "Tehran", Position = new Vector2(0.1f, 2.4f), Yield = 5, Owner = Faction.Player, Unit = Faction.Player, Neighbors = new [] { "Tabriz", "Baku", "Hormuz", "Kabul", "Basra" } },
                new NodeSeed { Id = "Baku", Position = new Vector2(0.3f, 3.45f), Yield = 3, Owner = Faction.Neutral, Unit = Faction.Neutral, Neighbors = new [] { "Tabriz", "Tehran", "Kabul" } },
                new NodeSeed { Id = "Baghdad", Position = new Vector2(-2.0f, 1.6f), Yield = 4, Owner = Faction.AI, Unit = Faction.AI, Neighbors = new [] { "Tabriz", "Basra", "Damascus", "Riyadh" } },
                new NodeSeed { Id = "Damascus", Position = new Vector2(-3.1f, 1.0f), Yield = 4, Owner = Faction.AI, Unit = Faction.AI, Neighbors = new [] { "Baghdad", "Mediterranean" } },
                new NodeSeed { Id = "Mediterranean", Position = new Vector2(-3.95f, 1.55f), Yield = 2, Owner = Faction.Neutral, Unit = Faction.Neutral, Neighbors = new [] { "Damascus" } },
                new NodeSeed { Id = "Basra", Position = new Vector2(-1.1f, 0.7f), Yield = 3, Owner = Faction.Neutral, Unit = Faction.Neutral, Neighbors = new [] { "Baghdad", "Tehran", "Hormuz", "Riyadh" } },
                new NodeSeed { Id = "Hormuz", Position = new Vector2(0.35f, -1.25f), Yield = 5, Owner = Faction.Player, Unit = Faction.Player, Neighbors = new [] { "Tehran", "Basra", "Riyadh" } },
                new NodeSeed { Id = "Riyadh", Position = new Vector2(-2.25f, -1.05f), Yield = 3, Owner = Faction.AI, Unit = Faction.AI, Neighbors = new [] { "Baghdad", "Basra", "Hormuz" } },
                new NodeSeed { Id = "Kabul", Position = new Vector2(2.35f, 2.05f), Yield = 4, Owner = Faction.Neutral, Unit = Faction.Neutral, Neighbors = new [] { "Tehran", "Baku" } }
            };

            foreach (NodeSeed seed in seeds)
            {
                var nodeGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                nodeGo.name = $"Node_{seed.Id}";
                nodeGo.transform.SetParent(transform, false);
                nodeGo.transform.position = new Vector3(seed.Position.x, seed.Position.y, 0f);
                nodeGo.transform.localScale = new Vector3(0.72f, 0.72f, 0.12f);

                var renderer = nodeGo.GetComponent<Renderer>();
                renderer.material = new Material(Shader.Find("Standard"));

                var labelGo = new GameObject("Label");
                labelGo.transform.SetParent(nodeGo.transform, false);
                labelGo.transform.localPosition = new Vector3(0f, 0.55f, -0.12f);
                var textMesh = labelGo.AddComponent<TextMesh>();
                textMesh.text = seed.Id;
                textMesh.fontSize = 38;
                textMesh.characterSize = 0.08f;
                textMesh.anchor = TextAnchor.MiddleCenter;
                textMesh.alignment = TextAlignment.Center;
                textMesh.color = new Color(0.92f, 0.94f, 0.96f, 1f);

                var state = new NodeState
                {
                    Id = seed.Id,
                    Position = nodeGo.transform.position,
                    Yield = seed.Yield,
                    Owner = seed.Owner,
                    Unit = seed.Unit,
                    Neighbors = new List<string>(seed.Neighbors),
                    NodeObject = nodeGo,
                    NodeRenderer = renderer,
                    Label = textMesh
                };

                _nodes[state.Id] = state;
            }

            foreach (NodeState node in _nodes.Values)
            {
                foreach (string neighbor in node.Neighbors)
                {
                    AddEdge(node.Id, neighbor);
                }
            }
        }

        private void AddEdge(string a, string b)
        {
            if (string.Equals(a, b, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            string left = string.CompareOrdinal(a, b) < 0 ? a : b;
            string right = string.CompareOrdinal(a, b) < 0 ? b : a;
            if (_edges.Any(edge => edge.Item1 == left && edge.Item2 == right))
            {
                return;
            }

            _edges.Add(new Tuple<string, string>(left, right));
        }

        private void CreateRouteCurves()
        {
            _edgeRenderers.Clear();
            foreach (Tuple<string, string> edge in _edges)
            {
                NodeState from = _nodes[edge.Item1];
                NodeState to = _nodes[edge.Item2];
                Vector3 midpoint = (from.Position + to.Position) * 0.5f;
                Vector3 direction = (to.Position - from.Position).normalized;
                Vector3 normal = new Vector3(-direction.y, direction.x, 0f);
                float bend = Mathf.Lerp(0.08f, 0.28f, Mathf.Clamp01(Vector3.Distance(from.Position, to.Position) / 4f));

                Vector3[] controls =
                {
                    from.Position + new Vector3(0f, 0f, 0.2f),
                    midpoint + normal * bend + new Vector3(0f, 0f, 0.2f),
                    to.Position + new Vector3(0f, 0f, 0.2f)
                };

                LineRenderer lr = CreateCurveLine($"Route_{edge.Item1}_{edge.Item2}", controls, new Color(0.28f, 0.33f, 0.4f, 0.9f), 0.028f, 8);
                _edgeRenderers.Add(new EdgeRender { Left = edge.Item1, Right = edge.Item2, Renderer = lr });
            }
        }

        private LineRenderer CreateCurveLine(string name, Vector3[] controlPoints, Color color, float width, int samplesPerSegment)
        {
            var go = new GameObject(name);
            go.transform.SetParent(transform, false);
            var lr = go.AddComponent<LineRenderer>();
            lr.material = _lineMaterial;
            lr.textureMode = LineTextureMode.Stretch;
            lr.alignment = LineAlignment.TransformZ;
            lr.startWidth = width;
            lr.endWidth = width;
            lr.numCornerVertices = 4;
            lr.numCapVertices = 4;
            lr.useWorldSpace = true;
            lr.startColor = color;
            lr.endColor = color;
            lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lr.receiveShadows = false;

            List<Vector3> points = BuildSmoothPath(controlPoints, samplesPerSegment);
            lr.positionCount = points.Count;
            lr.SetPositions(points.ToArray());
            return lr;
        }

        private static List<Vector3> BuildSmoothPath(IReadOnlyList<Vector3> points, int samplesPerSegment)
        {
            var result = new List<Vector3>();
            if (points == null || points.Count == 0)
            {
                return result;
            }

            if (points.Count == 1)
            {
                result.Add(points[0]);
                return result;
            }

            int samples = Mathf.Max(2, samplesPerSegment);
            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector3 p0 = points[Mathf.Max(i - 1, 0)];
                Vector3 p1 = points[i];
                Vector3 p2 = points[i + 1];
                Vector3 p3 = points[Mathf.Min(i + 2, points.Count - 1)];

                for (int s = 0; s < samples; s++)
                {
                    float t = s / (float)samples;
                    result.Add(CatmullRom(p0, p1, p2, p3, t));
                }
            }

            result.Add(points[points.Count - 1]);
            return result;
        }

        private static Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;
            return 0.5f * (
                2f * p1 +
                (-p0 + p2) * t +
                (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
                (-p0 + 3f * p1 - 3f * p2 + p3) * t3
            );
        }

        private void CreateHud()
        {
            var canvasGo = new GameObject("PrototypeCanvas");
            canvasGo.transform.SetParent(transform, false);
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGo.AddComponent<CanvasScaler>();
            canvasGo.AddComponent<GraphicRaycaster>();

            _hudText = CreateText(canvas.transform, "HudText", new Vector2(16f, -16f), new Vector2(900f, 84f), 18, TextAnchor.UpperLeft);
            _hintText = CreateText(canvas.transform, "HintText", new Vector2(16f, -106f), new Vector2(980f, 72f), 16, TextAnchor.UpperLeft);
            _resourceText = CreateText(canvas.transform, "ResourceText", new Vector2(16f, -186f), new Vector2(560f, 220f), 15, TextAnchor.UpperLeft);

            _fogText = CreateText(canvas.transform, "FogText", new Vector2(16f, -412f), new Vector2(560f, 80f), 15, TextAnchor.UpperLeft);
            _fogText.color = new Color(0.78f, 0.86f, 0.95f, 0.95f);

            _battleLogText = CreateText(canvas.transform, "BattleLogText", new Vector2(16f, -16f), new Vector2(560f, 470f), 15, TextAnchor.UpperLeft);
            RectTransform battleRect = _battleLogText.rectTransform;
            battleRect.anchorMin = new Vector2(1f, 1f);
            battleRect.anchorMax = new Vector2(1f, 1f);
            battleRect.pivot = new Vector2(1f, 1f);
            battleRect.anchoredPosition = new Vector2(-16f, -16f);
            _battleLogText.color = new Color(0.87f, 0.92f, 0.99f, 1f);

            _endTurnButton = CreateActionButton(
                canvas.transform,
                "EndTurnButton",
                "End Turn",
                new Vector2(20f, 20f),
                new Vector2(190f, 56f),
                new Color(0.13f, 0.2f, 0.32f, 0.95f),
                EndTurn);

            _ceasefireButton = CreateActionButton(
                canvas.transform,
                "CeasefireButton",
                "Ceasefire",
                new Vector2(220f, 20f),
                new Vector2(190f, 56f),
                new Color(0.16f, 0.3f, 0.24f, 0.95f),
                ActivateCeasefire);

            _sanctionButton = CreateActionButton(
                canvas.transform,
                "SanctionButton",
                "Sanctions",
                new Vector2(420f, 20f),
                new Vector2(190f, 56f),
                new Color(0.35f, 0.18f, 0.14f, 0.95f),
                ActivateSanctions);

            CreateText(canvas.transform, "InstructionText", new Vector2(16f, 24f), new Vector2(1050f, 34f), 15, TextAnchor.LowerLeft)
                .text = "Left-click blue unit block -> click adjacent node to move. Diplomacy buttons cost AP+Gold. Curved borders/routes are map placeholders.";
        }

        private Button CreateActionButton(
            Transform parent,
            string name,
            string label,
            Vector2 anchoredPos,
            Vector2 size,
            Color color,
            Action clickAction)
        {
            var buttonGo = CreatePanel(parent, name, anchoredPos, size, new Vector2(1f, 0f), new Vector2(1f, 0f));
            var image = buttonGo.AddComponent<Image>();
            image.color = color;
            var button = buttonGo.AddComponent<Button>();
            button.targetGraphic = image;
            button.onClick.AddListener(() => clickAction());

            Text buttonText = CreateText(buttonGo.transform, "Text", Vector2.zero, Vector2.zero, 19, TextAnchor.MiddleCenter);
            RectTransform rect = buttonText.rectTransform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            buttonText.text = label;
            buttonText.color = new Color(0.93f, 0.96f, 1f, 1f);
            return button;
        }

        private static GameObject CreatePanel(Transform parent, string name, Vector2 anchoredPos, Vector2 sizeDelta, Vector2 anchorMin, Vector2 anchorMax)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.pivot = new Vector2(anchorMax.x, anchorMin.y);
            rect.anchoredPosition = new Vector2(-anchoredPos.x, anchoredPos.y);
            rect.sizeDelta = sizeDelta;
            return go;
        }

        private static Text CreateText(Transform parent, string name, Vector2 anchoredPos, Vector2 sizeDelta, int fontSize, TextAnchor anchor)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = anchoredPos;
            rect.sizeDelta = sizeDelta;

            var text = go.AddComponent<Text>();
            text.font = LoadBuiltinUiFont();
            text.fontSize = fontSize;
            text.alignment = anchor;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.color = new Color(0.9f, 0.93f, 0.97f, 1f);
            return text;
        }

        private static Font LoadBuiltinUiFont()
        {
            Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            if (font != null)
            {
                return font;
            }

            font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            if (font != null)
            {
                return font;
            }

            Debug.LogWarning("[PlayablePrototype] Built-in UI font not found. UI text may not render.");
            return null;
        }

        private void HandlePointerClick()
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Camera cam = Camera.main;
            if (cam == null)
            {
                return;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 50f))
            {
                return;
            }

            NodeState clickedNode = _nodes.Values.FirstOrDefault(node => node.NodeObject == hit.collider.gameObject);
            if (clickedNode == null)
            {
                return;
            }

            HandleNodeInteraction(clickedNode);
        }

        private void HandleNodeInteraction(NodeState clicked)
        {
            if (!IsNodeVisible(clicked.Id))
            {
                _statusMessage = "That location is hidden by fog. Move closer to scout it.";
                UpdateHud();
                return;
            }

            if (_selectedNode == null)
            {
                if (clicked.Unit != Faction.Player)
                {
                    _statusMessage = "Select a blue unit block first.";
                    UpdateHud();
                    return;
                }

                SelectNode(clicked);
                return;
            }

            if (_selectedNode == clicked)
            {
                DeselectNode();
                return;
            }

            if (clicked.Unit == Faction.Player)
            {
                SelectNode(clicked);
                return;
            }

            if (!_selectedNode.Neighbors.Contains(clicked.Id))
            {
                _statusMessage = $"{clicked.Id} is not adjacent to {_selectedNode.Id}.";
                UpdateHud();
                return;
            }

            if (_playerAp <= 0)
            {
                _statusMessage = "No AP left this turn. Press End Turn.";
                UpdateHud();
                return;
            }

            if (IsCeasefireActive() && (clicked.Owner == Faction.AI || clicked.Unit == Faction.AI))
            {
                _statusMessage = "Ceasefire active: direct attacks are locked this turn.";
                UpdateHud();
                return;
            }

            ExecutePlayerMove(_selectedNode, clicked);
            DeselectNode();
        }

        private void SelectNode(NodeState node)
        {
            _selectedNode = node;
            _statusMessage = $"Selected {node.Id}. AP {_playerAp}. Click adjacent node to move/capture.";
            RefreshAllVisuals();
            UpdateHud();
        }

        private void DeselectNode()
        {
            _selectedNode = null;
            RefreshAllVisuals();
            UpdateHud();
        }

        private void ExecutePlayerMove(NodeState source, NodeState target)
        {
            _playerAp -= 1;
            source.Unit = Faction.Neutral;

            bool attackedAiUnit = target.Unit == Faction.AI;
            bool capturedNode = target.Owner != Faction.Player;
            Faction previousOwner = target.Owner;

            if (attackedAiUnit)
            {
                _diplomacy = Mathf.Max(0, _diplomacy - 6);
                AddBattleLog("BATTLE", $"Player assault at {target.Id}, AI unit removed.");
            }

            if (capturedNode)
            {
                target.Owner = Faction.Player;
                _playerGold += target.Yield;

                if (previousOwner == Faction.AI)
                {
                    _aiGold = Mathf.Max(0, _aiGold - target.Yield);
                    AddBattleLog("BATTLE", $"Player captured {target.Id} from AI (+{target.Yield} strategic yield).");
                }
                else
                {
                    AddBattleLog("EXPAND", $"Player secured neutral node {target.Id} (+{target.Yield} yield).");
                }
            }
            else
            {
                AddBattleLog("MOVE", $"Player repositioned {source.Id} -> {target.Id}.");
            }

            target.Unit = Faction.Player;

            if (attackedAiUnit)
            {
                _statusMessage = $"Player attacked {target.Id} and removed AI unit.";
            }
            else if (capturedNode)
            {
                _statusMessage = $"Player captured {target.Id}.";
            }
            else
            {
                _statusMessage = $"Player repositioned to {target.Id}.";
            }

            RefreshAllVisuals();
            CheckEndState();
            UpdateHud();
        }

        private void ActivateCeasefire()
        {
            if (_gameEnded)
            {
                return;
            }

            if (_ceasefireTurnsRemaining > 0)
            {
                _statusMessage = $"Ceasefire already active ({_ceasefireTurnsRemaining} turns left).";
                UpdateHud();
                return;
            }

            if (_playerAp < kCeasefireApCost)
            {
                _statusMessage = "Not enough AP to sign ceasefire.";
                UpdateHud();
                return;
            }

            if (_playerGold < kCeasefireGoldCost)
            {
                _statusMessage = "Not enough Gold to broker ceasefire.";
                UpdateHud();
                return;
            }

            _playerAp -= kCeasefireApCost;
            _playerGold -= kCeasefireGoldCost;
            _ceasefireTurnsRemaining = kCeasefireDuration;
            _diplomacy = Mathf.Min(100, _diplomacy + 8);
            _statusMessage = $"Ceasefire signed for {kCeasefireDuration} turns.";
            AddBattleLog("DIPLO", "Ceasefire agreement signed. Direct attacks paused.");
            UpdateHud();
        }

        private void ActivateSanctions()
        {
            if (_gameEnded)
            {
                return;
            }

            if (_sanctionCooldownTurns > 0)
            {
                _statusMessage = $"Sanctions on cooldown ({_sanctionCooldownTurns} turns).";
                UpdateHud();
                return;
            }

            if (_playerAp < kSanctionApCost)
            {
                _statusMessage = "Not enough AP to issue sanctions.";
                UpdateHud();
                return;
            }

            if (_playerGold < kSanctionGoldCost)
            {
                _statusMessage = "Not enough Gold to enforce sanctions.";
                UpdateHud();
                return;
            }

            _playerAp -= kSanctionApCost;
            _playerGold -= kSanctionGoldCost;
            _aiGold = Mathf.Max(0, _aiGold - kSanctionAIGoldHit);
            _diplomacy = Mathf.Max(0, _diplomacy - 1);
            _sanctionCooldownTurns = kSanctionCooldownDuration;
            _aiSuppressionTurnsRemaining = Mathf.Max(_aiSuppressionTurnsRemaining, kSanctionAISuppressionDuration);

            _statusMessage = "Sanctions applied: AI economy hit and operations slowed.";
            AddBattleLog("DIPLO", $"Sanctions reduced AI gold by {kSanctionAIGoldHit} and applied operation slowdown.");
            UpdateHud();
        }

        private void EndTurn()
        {
            if (_gameEnded)
            {
                return;
            }

            AddBattleLog("TURN", $"Turn {_turn} ended by player.");
            ApplyIncome();
            RunAiTurn();
            EnsureReinforcement();

            if (_ceasefireTurnsRemaining > 0)
            {
                _ceasefireTurnsRemaining -= 1;
            }

            if (_aiSuppressionTurnsRemaining > 0)
            {
                _aiSuppressionTurnsRemaining -= 1;
            }

            if (_sanctionCooldownTurns > 0)
            {
                _sanctionCooldownTurns -= 1;
            }

            _turn += 1;
            _playerAp = kPlayerApPerTurn;
            if (_turn > kMaxTurns)
            {
                ResolveAttritionResult();
                return;
            }

            _statusMessage = "New turn started. Plan your next move.";
            AddBattleLog("TURN", $"Turn {_turn} started.");
            RefreshAllVisuals();
            CheckEndState();
            UpdateHud();
        }

        private void ApplyIncome()
        {
            int playerIncome = _nodes.Values.Where(n => n.Owner == Faction.Player).Sum(n => n.Yield);
            int aiIncome = _nodes.Values.Where(n => n.Owner == Faction.AI).Sum(n => n.Yield);
            _playerGold += playerIncome;
            _aiGold += aiIncome;
            AddBattleLog("ECON", $"Income applied: Player +{playerIncome}, AI +{aiIncome}.");
        }

        private void RunAiTurn()
        {
            int aiAp = Mathf.Max(1, kAiApPerTurn - (_aiSuppressionTurnsRemaining > 0 ? 1 : 0));
            bool ceasefireActive = IsCeasefireActive();
            List<NodeState> aiUnits = _nodes.Values
                .Where(node => node.Unit == Faction.AI)
                .OrderBy(_ => _rng.Next())
                .ToList();

            foreach (NodeState source in aiUnits)
            {
                if (aiAp <= 0)
                {
                    break;
                }

                NodeState target = ChooseAiTarget(source, ceasefireActive);
                if (target == null)
                {
                    continue;
                }

                source.Unit = Faction.Neutral;
                target.Unit = Faction.AI;

                if (target.Owner == Faction.Player)
                {
                    target.Owner = Faction.AI;
                    _diplomacy = Mathf.Max(0, _diplomacy - 4);
                    _statusMessage = $"AI captured {target.Id}.";
                    AddBattleLog("BATTLE", $"AI captured player node {target.Id}.");
                }
                else if (target.Owner == Faction.Neutral)
                {
                    target.Owner = Faction.AI;
                    _statusMessage = $"AI expanded into {target.Id}.";
                    AddBattleLog("EXPAND", $"AI expanded into neutral node {target.Id}.");
                }
                else
                {
                    AddBattleLog("MOVE", $"AI repositioned {source.Id} -> {target.Id}.");
                }

                aiAp -= 1;
            }

            RefreshAllVisuals();
        }

        private NodeState ChooseAiTarget(NodeState source, bool ceasefireActive)
        {
            List<NodeState> neighbors = source.Neighbors
                .Where(id => _nodes.ContainsKey(id))
                .Select(id => _nodes[id])
                .Where(node => node.Unit != Faction.AI)
                .ToList();

            if (ceasefireActive)
            {
                neighbors = neighbors
                    .Where(node => node.Owner != Faction.Player && node.Unit != Faction.Player)
                    .ToList();
            }

            if (neighbors.Count == 0)
            {
                return null;
            }

            NodeState attackTarget = neighbors
                .Where(node => node.Owner == Faction.Player || node.Unit == Faction.Player)
                .OrderByDescending(node => node.Yield)
                .FirstOrDefault();

            if (attackTarget != null)
            {
                return attackTarget;
            }

            NodeState neutralTarget = neighbors
                .Where(node => node.Owner == Faction.Neutral)
                .OrderByDescending(node => node.Yield)
                .FirstOrDefault();

            if (neutralTarget != null)
            {
                return neutralTarget;
            }

            return neighbors[_rng.Next(neighbors.Count)];
        }

        private void EnsureReinforcement()
        {
            if (!_nodes.Values.Any(node => node.Unit == Faction.Player))
            {
                NodeState spawn = _nodes["Tehran"];
                spawn.Unit = Faction.Player;
                spawn.Owner = Faction.Player;
                AddBattleLog("REINFORCE", "Player reinforcement deployed at Tehran.");
                _statusMessage = "Player reinforcement deployed at Tehran.";
            }

            if (!_nodes.Values.Any(node => node.Unit == Faction.AI))
            {
                NodeState spawn = _nodes["Baghdad"];
                spawn.Unit = Faction.AI;
                spawn.Owner = Faction.AI;
                AddBattleLog("REINFORCE", "AI reinforcement deployed at Baghdad.");
                _statusMessage = "AI reinforcement deployed at Baghdad.";
            }
        }

        private void ResolveAttritionResult()
        {
            int playerOwned = _nodes.Values.Count(node => node.Owner == Faction.Player);
            int aiOwned = _nodes.Values.Count(node => node.Owner == Faction.AI);

            if (playerOwned > aiOwned)
            {
                EndGame($"Turn limit reached. Player wins by control ({playerOwned} vs {aiOwned}).");
            }
            else if (aiOwned > playerOwned)
            {
                EndGame($"Turn limit reached. AI wins by control ({aiOwned} vs {playerOwned}).");
            }
            else
            {
                EndGame($"Turn limit reached. Draw ({playerOwned}:{aiOwned}).");
            }
        }

        private void CheckEndState()
        {
            int playerOwned = _nodes.Values.Count(node => node.Owner == Faction.Player);
            int aiOwned = _nodes.Values.Count(node => node.Owner == Faction.AI);
            int threshold = 6;

            if (playerOwned >= threshold)
            {
                EndGame($"Player wins by territorial control ({playerOwned} nodes).");
                return;
            }

            if (aiOwned >= threshold)
            {
                EndGame($"AI wins by territorial control ({aiOwned} nodes).");
                return;
            }

            if (_diplomacy <= 0)
            {
                EndGame("Diplomacy collapsed. Coalition pressure forces player defeat.");
            }
        }

        private void EndGame(string message)
        {
            _gameEnded = true;
            _statusMessage = message;
            AddBattleLog("END", message);

            if (_endTurnButton != null)
            {
                _endTurnButton.interactable = false;
            }

            if (_ceasefireButton != null)
            {
                _ceasefireButton.interactable = false;
            }

            if (_sanctionButton != null)
            {
                _sanctionButton.interactable = false;
            }

            UpdateHud();
        }

        private void RefreshAllVisuals()
        {
            RecomputeVisibility();
            foreach (NodeState node in _nodes.Values)
            {
                RefreshNodeVisual(node);
            }

            RefreshRouteVisuals();
        }

        private void RecomputeVisibility()
        {
            _visibleNodeIds.Clear();
            List<NodeState> anchors = _nodes.Values
                .Where(node => node.Owner == Faction.Player || node.Unit == Faction.Player)
                .ToList();

            foreach (NodeState anchor in anchors)
            {
                RevealNode(anchor.Id);
                for (int i = 0; i < anchor.Neighbors.Count; i++)
                {
                    RevealNode(anchor.Neighbors[i]);
                }
            }

            if (_visibleNodeIds.Count == 0 && _nodes.ContainsKey("Tehran"))
            {
                RevealNode("Tehran");
            }
        }

        private void RevealNode(string nodeId)
        {
            if (_nodes.ContainsKey(nodeId))
            {
                _visibleNodeIds.Add(nodeId);
            }
        }

        private bool IsNodeVisible(string nodeId)
        {
            return _visibleNodeIds.Contains(nodeId);
        }

        private void RefreshRouteVisuals()
        {
            for (int i = 0; i < _edgeRenderers.Count; i++)
            {
                EdgeRender edge = _edgeRenderers[i];
                if (edge.Renderer == null)
                {
                    continue;
                }

                bool leftVisible = IsNodeVisible(edge.Left);
                bool rightVisible = IsNodeVisible(edge.Right);
                bool fullyVisible = leftVisible && rightVisible;

                Color color = fullyVisible
                    ? new Color(0.34f, 0.44f, 0.56f, 0.95f)
                    : new Color(0.14f, 0.16f, 0.2f, 0.22f);

                edge.Renderer.startColor = color;
                edge.Renderer.endColor = color;
                edge.Renderer.startWidth = fullyVisible ? 0.03f : 0.018f;
                edge.Renderer.endWidth = edge.Renderer.startWidth;
            }
        }

        private void RefreshNodeVisual(NodeState node)
        {
            if (node == null || node.NodeRenderer == null)
            {
                return;
            }

            bool visible = IsNodeVisible(node.Id);
            Color baseColor = visible
                ? GetFactionColor(node.Owner)
                : new Color(0.12f, 0.13f, 0.16f, 1f);

            if (visible && _selectedNode == node)
            {
                baseColor = Color.Lerp(baseColor, new Color(1f, 0.92f, 0.18f, 1f), 0.52f);
            }

            node.NodeRenderer.material.color = baseColor;
            node.NodeObject.transform.localScale = _selectedNode == node
                ? new Vector3(0.86f, 0.86f, 0.12f)
                : new Vector3(0.72f, 0.72f, 0.12f);

            if (node.Label != null)
            {
                node.Label.text = visible ? $"{node.Id}\n+{node.Yield}" : "???";
                node.Label.color = visible
                    ? new Color(0.92f, 0.94f, 0.96f, 1f)
                    : new Color(0.48f, 0.52f, 0.58f, 1f);
            }

            if (node.UnitObject != null)
            {
                Destroy(node.UnitObject);
                node.UnitObject = null;
            }

            if (!visible || node.Unit == Faction.Neutral)
            {
                return;
            }

            var unit = GameObject.CreatePrimitive(PrimitiveType.Cube);
            unit.name = $"Unit_{node.Id}";
            unit.transform.SetParent(node.NodeObject.transform, false);
            unit.transform.localPosition = new Vector3(0f, 0f, -0.18f);
            unit.transform.localScale = new Vector3(0.34f, 0.34f, 0.26f);
            var unitRenderer = unit.GetComponent<Renderer>();
            unitRenderer.material = new Material(Shader.Find("Standard"));
            unitRenderer.material.color = node.Unit == Faction.Player
                ? new Color(0.28f, 0.74f, 1f, 1f)
                : new Color(1f, 0.36f, 0.32f, 1f);
            Destroy(unit.GetComponent<Collider>());
            node.UnitObject = unit;
        }

        private static Color GetFactionColor(Faction faction)
        {
            switch (faction)
            {
                case Faction.Player:
                    return new Color(0.14f, 0.42f, 0.74f, 1f);
                case Faction.AI:
                    return new Color(0.65f, 0.2f, 0.16f, 1f);
                default:
                    return new Color(0.36f, 0.38f, 0.42f, 1f);
            }
        }

        private bool IsCeasefireActive()
        {
            return _ceasefireTurnsRemaining > 0;
        }

        private void AddBattleLog(string channel, string message)
        {
            string line = $"T{_turn:00} [{channel}] {message}";
            _battleLog.Enqueue(line);
            while (_battleLog.Count > 14)
            {
                _battleLog.Dequeue();
            }
        }

        private string BuildBattleLogText()
        {
            if (_battleLog.Count == 0)
            {
                return "[BATTLE REPORT]\nNo records yet.";
            }

            string[] lines = _battleLog.Reverse().ToArray();
            return "[BATTLE REPORT]\n" + string.Join("\n", lines);
        }

        private string BuildResourcePanelText()
        {
            List<NodeState> playerNodes = _nodes.Values
                .Where(node => node.Owner == Faction.Player)
                .OrderByDescending(node => node.Yield)
                .ThenBy(node => node.Id)
                .ToList();

            int playerIncome = playerNodes.Sum(node => node.Yield);
            string playerBreakdown = playerNodes.Count == 0
                ? "none"
                : string.Join(", ",
                    playerNodes.Take(4).Select(node => $"{node.Id}+{node.Yield}").ToArray());

            int visibleAiIncome = _nodes.Values
                .Where(node => node.Owner == Faction.AI && IsNodeVisible(node.Id))
                .Sum(node => node.Yield);
            int hiddenAiNodes = _nodes.Values.Count(node => node.Owner == Faction.AI && !IsNodeVisible(node.Id));

            List<NodeState> visibleNeutral = _nodes.Values
                .Where(node => node.Owner == Faction.Neutral && IsNodeVisible(node.Id))
                .OrderByDescending(node => node.Yield)
                .ThenBy(node => node.Id)
                .ToList();
            string neutralOpportunity = visibleNeutral.Count == 0
                ? "none visible"
                : string.Join(", ",
                    visibleNeutral.Take(3).Select(node => $"{node.Id}+{node.Yield}").ToArray());

            return
                "[RESOURCE OUTPUT]\n" +
                $"Player income: +{playerIncome} ({playerBreakdown})\n" +
                $"AI known income: +{visibleAiIncome} (hidden AI nodes: {hiddenAiNodes})\n" +
                $"Visible neutral opportunities: {neutralOpportunity}";
        }

        private string BuildFogText()
        {
            int visible = _visibleNodeIds.Count;
            int total = _nodes.Count;
            int hidden = Mathf.Max(0, total - visible);
            List<string> hiddenIds = _nodes.Values
                .Where(node => !IsNodeVisible(node.Id))
                .Select(node => node.Id)
                .Take(4)
                .ToList();
            string hiddenSample = hiddenIds.Count == 0 ? "none" : string.Join(", ", hiddenIds.ToArray());

            return
                "[FOG OF WAR]\n" +
                $"Visible nodes: {visible}/{total} (hidden: {hidden})\n" +
                $"Last hidden sample: {hiddenSample}";
        }

        private void UpdateHud()
        {
            int playerOwned = _nodes.Values.Count(node => node.Owner == Faction.Player);
            int aiOwned = _nodes.Values.Count(node => node.Owner == Faction.AI);
            int neutralOwned = _nodes.Values.Count(node => node.Owner == Faction.Neutral);

            string effectText = $"Ceasefire:{_ceasefireTurnsRemaining}  SanctionCD:{_sanctionCooldownTurns}  AISuppression:{_aiSuppressionTurnsRemaining}";

            if (_hudText != null)
            {
                _hudText.text =
                    $"Turn {_turn}/{kMaxTurns}    AP {_playerAp}/{kPlayerApPerTurn}    Gold P/AI {_playerGold}/{_aiGold}    Diplomacy {_diplomacy}\n" +
                    $"Nodes P/AI/N {playerOwned}/{aiOwned}/{neutralOwned}    Effects {effectText}";
            }

            if (_hintText != null)
            {
                _hintText.text = _gameEnded
                    ? $"[Game Over] {_statusMessage}\nPress Play again to restart this prototype scene."
                    : _statusMessage;
            }

            if (_resourceText != null)
            {
                _resourceText.text = BuildResourcePanelText();
            }

            if (_fogText != null)
            {
                _fogText.text = BuildFogText();
            }

            if (_battleLogText != null)
            {
                _battleLogText.text = BuildBattleLogText();
            }

            if (_ceasefireButton != null)
            {
                _ceasefireButton.interactable = !_gameEnded && _playerAp >= kCeasefireApCost && _playerGold >= kCeasefireGoldCost && _ceasefireTurnsRemaining == 0;
            }

            if (_sanctionButton != null)
            {
                _sanctionButton.interactable = !_gameEnded && _playerAp >= kSanctionApCost && _playerGold >= kSanctionGoldCost && _sanctionCooldownTurns == 0;
            }

            if (_endTurnButton != null)
            {
                _endTurnButton.interactable = !_gameEnded;
            }
        }
    }
}

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace EventideAge.Editor
{
    public static class R4UxVisualAuditRunner
    {
        private const string kMainGameScenePath = "Assets/Scenes/MainGameScene.unity";
        private const string kUiRootName = "P0HighFrequencyUIRoot";
        private const string kLogTag = "[R4UxVisualAuditRunner]";

        private readonly struct AuditCase
        {
            public readonly string CaseId;
            public readonly int Width;
            public readonly int Height;

            public AuditCase(string caseId, int width, int height)
            {
                CaseId = caseId;
                Width = width;
                Height = height;
            }
        }

        private readonly struct PanelRect
        {
            public readonly string PanelId;
            public readonly Rect PixelRect;
            public readonly bool InBounds;

            public PanelRect(string panelId, Rect pixelRect, bool inBounds)
            {
                PanelId = panelId;
                PixelRect = pixelRect;
                InBounds = inBounds;
            }
        }

        private static readonly AuditCase[] Cases =
        {
            new AuditCase("R4-06-WIN-720P", 1280, 720),
            new AuditCase("R4-06-WIN-1080P", 1920, 1080),
            new AuditCase("R4-06-WIN-1440P", 2560, 1440)
        };

        private static readonly string[] HighFrequencyPanels =
        {
            "MapPanelP0",
            "DiplomacyPanelP0",
            "BattleReportPanelP0",
            "EventPanelP0"
        };

        public static void RunR406VisualAudit()
        {
            if (!File.Exists(kMainGameScenePath))
            {
                Debug.LogError($"{kLogTag} Scene not found: {kMainGameScenePath}");
                return;
            }

            var scene = EditorSceneManager.OpenScene(kMainGameScenePath, OpenSceneMode.Single);
            if (!scene.IsValid())
            {
                Debug.LogError($"{kLogTag} Failed to open scene: {kMainGameScenePath}");
                return;
            }

            Canvas canvas = UnityEngine.Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError($"{kLogTag} Canvas not found in scene.");
                return;
            }

            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
            Transform uiRoot = FindUiRoot(canvas.transform);
            if (uiRoot == null)
            {
                Debug.LogError($"{kLogTag} UI root not found: {kUiRootName}");
                return;
            }

            string evidenceDir = Path.GetFullPath(Path.Combine(Application.dataPath, "..", "production", "evidence", "r4", "validation"));
            Directory.CreateDirectory(evidenceDir);

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            string runId = $"{timestamp}_R4-06_UX_VISUAL_AUDIT_RUN01";
            string csvPath = Path.Combine(evidenceDir, runId + ".csv");
            string logPath = Path.Combine(evidenceDir, runId + ".log");

            var csv = new StringBuilder();
            csv.AppendLine("Timestamp,CaseId,Resolution,ScaleFactor,CanvasUnits,PanelsFound,OutOfBoundsPanels,OverlapPairs,MaxOverlapAreaPx,Result");

            var log = new StringBuilder();
            log.AppendLine("# R4-06 UX Visual Audit Run 01");
            log.AppendLine();
            log.AppendLine($"- Timestamp: {timestamp}");
            log.AppendLine($"- Scene: {kMainGameScenePath}");
            log.AppendLine($"- UI Root: {kUiRootName}");
            log.AppendLine($"- Panel Scope: {string.Join(", ", HighFrequencyPanels)}");
            log.AppendLine();

            for (int i = 0; i < Cases.Length; i++)
            {
                AuditCase testCase = Cases[i];
                float scaleFactor = ComputeScaleFactor(scaler, testCase.Width, testCase.Height);
                float canvasWidth = testCase.Width / Mathf.Max(scaleFactor, 0.0001f);
                float canvasHeight = testCase.Height / Mathf.Max(scaleFactor, 0.0001f);

                int foundCount = 0;
                int outOfBoundsCount = 0;
                var panelRects = new List<PanelRect>();
                var missingPanels = new List<string>();

                for (int p = 0; p < HighFrequencyPanels.Length; p++)
                {
                    string panelName = HighFrequencyPanels[p];
                    Transform panelTransform = uiRoot.Find(panelName);
                    if (panelTransform == null)
                    {
                        panelTransform = FindChildRecursive(uiRoot, panelName);
                    }

                    if (panelTransform == null)
                    {
                        missingPanels.Add(panelName);
                        continue;
                    }

                    RectTransform rect = panelTransform.GetComponent<RectTransform>();
                    if (rect == null)
                    {
                        missingPanels.Add(panelName);
                        continue;
                    }

                    Rect pixelRect = ComputePixelRect(rect, canvasWidth, canvasHeight, scaleFactor);
                    bool inBounds = pixelRect.xMin >= 0f
                        && pixelRect.yMin >= 0f
                        && pixelRect.xMax <= testCase.Width
                        && pixelRect.yMax <= testCase.Height;

                    panelRects.Add(new PanelRect(panelName, pixelRect, inBounds));
                    foundCount++;
                    if (!inBounds)
                    {
                        outOfBoundsCount++;
                    }
                }

                string overlapPairs;
                float maxOverlapArea;
                ComputeOverlapSummary(panelRects, out overlapPairs, out maxOverlapArea);

                bool pass = missingPanels.Count == 0
                    && outOfBoundsCount == 0
                    && string.IsNullOrEmpty(overlapPairs);

                string result = pass ? "PASS" : "FAIL";
                string resolutionLabel = $"{testCase.Width}x{testCase.Height}";
                string canvasUnits = $"{canvasWidth:F2}x{canvasHeight:F2}";
                string safeOverlapPairs = string.IsNullOrEmpty(overlapPairs) ? "NONE" : overlapPairs;

                csv.AppendLine(
                    $"{timestamp},{testCase.CaseId},{resolutionLabel},{scaleFactor:F6},{canvasUnits},{foundCount}/{HighFrequencyPanels.Length},{outOfBoundsCount},{safeOverlapPairs},{maxOverlapArea:F2},{result}");

                log.AppendLine($"## {testCase.CaseId} ({resolutionLabel})");
                log.AppendLine($"- ScaleFactor: {scaleFactor:F6}");
                log.AppendLine($"- CanvasUnits: {canvasUnits}");
                log.AppendLine($"- PanelsFound: {foundCount}/{HighFrequencyPanels.Length}");
                log.AppendLine($"- OutOfBoundsPanels: {outOfBoundsCount}");
                log.AppendLine($"- OverlapPairs: {safeOverlapPairs}");
                log.AppendLine($"- MaxOverlapAreaPx: {maxOverlapArea:F2}");
                if (missingPanels.Count > 0)
                {
                    log.AppendLine($"- MissingPanels: {string.Join(", ", missingPanels)}");
                }

                for (int p = 0; p < panelRects.Count; p++)
                {
                    PanelRect panel = panelRects[p];
                    Rect r = panel.PixelRect;
                    log.AppendLine(
                        $"  - {panel.PanelId}: min({r.xMin:F2},{r.yMin:F2}) max({r.xMax:F2},{r.yMax:F2}) size({r.width:F2}x{r.height:F2}) inBounds={panel.InBounds}");
                }

                log.AppendLine($"- Result: {result}");
                log.AppendLine();
            }

            File.WriteAllText(csvPath, csv.ToString(), Encoding.UTF8);
            File.WriteAllText(logPath, log.ToString(), Encoding.UTF8);

            string relCsvPath = ToRepoRelativePath(csvPath);
            string relLogPath = ToRepoRelativePath(logPath);
            Debug.Log($"{kLogTag} R4-06 visual audit completed.");
            Debug.Log($"{kLogTag} CSV: {relCsvPath}");
            Debug.Log($"{kLogTag} LOG: {relLogPath}");
        }

        private static Transform FindUiRoot(Transform canvasTransform)
        {
            if (canvasTransform == null)
            {
                return null;
            }

            Transform direct = canvasTransform.Find(kUiRootName);
            if (direct != null)
            {
                return direct;
            }

            return FindChildRecursive(canvasTransform, kUiRootName);
        }

        private static Transform FindChildRecursive(Transform parent, string childName)
        {
            if (parent == null)
            {
                return null;
            }

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                if (string.Equals(child.name, childName, StringComparison.Ordinal))
                {
                    return child;
                }

                Transform nested = FindChildRecursive(child, childName);
                if (nested != null)
                {
                    return nested;
                }
            }

            return null;
        }

        private static Rect ComputePixelRect(RectTransform rect, float canvasWidth, float canvasHeight, float scaleFactor)
        {
            Vector2 anchor = rect.anchorMin;
            Vector2 pivot = rect.pivot;
            Vector2 size = rect.rect.size;
            Vector2 anchorPoint = new Vector2(anchor.x * canvasWidth, anchor.y * canvasHeight);
            Vector2 pivotPos = anchorPoint + rect.anchoredPosition;
            Vector2 min = pivotPos - Vector2.Scale(pivot, size);
            Vector2 max = min + size;

            return Rect.MinMaxRect(
                min.x * scaleFactor,
                min.y * scaleFactor,
                max.x * scaleFactor,
                max.y * scaleFactor);
        }

        private static void ComputeOverlapSummary(List<PanelRect> panelRects, out string overlapPairs, out float maxOverlapArea)
        {
            maxOverlapArea = 0f;
            var pairs = new List<string>();

            for (int i = 0; i < panelRects.Count; i++)
            {
                for (int j = i + 1; j < panelRects.Count; j++)
                {
                    Rect a = panelRects[i].PixelRect;
                    Rect b = panelRects[j].PixelRect;
                    Rect overlap = GetIntersection(a, b);
                    if (overlap.width <= 0f || overlap.height <= 0f)
                    {
                        continue;
                    }

                    float area = overlap.width * overlap.height;
                    if (area > maxOverlapArea)
                    {
                        maxOverlapArea = area;
                    }

                    pairs.Add($"{panelRects[i].PanelId}<->{panelRects[j].PanelId}:{area:F2}");
                }
            }

            overlapPairs = pairs.Count == 0 ? string.Empty : string.Join("|", pairs);
        }

        private static Rect GetIntersection(Rect a, Rect b)
        {
            float minX = Mathf.Max(a.xMin, b.xMin);
            float minY = Mathf.Max(a.yMin, b.yMin);
            float maxX = Mathf.Min(a.xMax, b.xMax);
            float maxY = Mathf.Min(a.yMax, b.yMax);
            if (maxX <= minX || maxY <= minY)
            {
                return Rect.zero;
            }

            return Rect.MinMaxRect(minX, minY, maxX, maxY);
        }

        private static float ComputeScaleFactor(CanvasScaler scaler, int width, int height)
        {
            if (scaler == null)
            {
                return 1f;
            }

            if (scaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
            {
                return Mathf.Max(0.0001f, scaler.scaleFactor);
            }

            Vector2 reference = scaler.referenceResolution;
            if (reference.x <= 0f || reference.y <= 0f)
            {
                reference = new Vector2(1920f, 1080f);
            }

            float widthScale = width / reference.x;
            float heightScale = height / reference.y;

            switch (scaler.screenMatchMode)
            {
                case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
                    {
                        const float kLogBase = 2f;
                        float logWidth = Mathf.Log(widthScale, kLogBase);
                        float logHeight = Mathf.Log(heightScale, kLogBase);
                        float weighted = Mathf.Lerp(logWidth, logHeight, scaler.matchWidthOrHeight);
                        return Mathf.Pow(kLogBase, weighted);
                    }
                case CanvasScaler.ScreenMatchMode.Expand:
                    return Mathf.Min(widthScale, heightScale);
                case CanvasScaler.ScreenMatchMode.Shrink:
                    return Mathf.Max(widthScale, heightScale);
                default:
                    return Mathf.Max(0.0001f, scaler.scaleFactor);
            }
        }

        private static string ToRepoRelativePath(string absolutePath)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string full = Path.GetFullPath(absolutePath);
            if (!full.StartsWith(projectRoot, StringComparison.OrdinalIgnoreCase))
            {
                return full;
            }

            string rel = full.Substring(projectRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return rel.Replace('\\', '/');
        }
    }
}
#endif

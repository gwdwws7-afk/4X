#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EventideAge.UI;

namespace EventideAge.Editor
{
    public static class P0HighFrequencyUiSetup
    {
        private const string kMainGameScenePath = "Assets/Scenes/MainGameScene.unity";
        private const string kPlayablePrototypeScenePath = "Assets/Scenes/PlayablePrototype.unity";
        private const string kPrefabFolderPath = "Assets/Prefabs/UI/P0";

        private const string kMapPrefabPath = kPrefabFolderPath + "/MapPanelP0.prefab";
        private const string kDiplomacyPrefabPath = kPrefabFolderPath + "/DiplomacyPanelP0.prefab";
        private const string kBattlePrefabPath = kPrefabFolderPath + "/BattleReportPanelP0.prefab";
        private const string kEventPrefabPath = kPrefabFolderPath + "/EventPanelP0.prefab";
        private const string kNotificationPrefabPath = kPrefabFolderPath + "/NotificationPanelP0.prefab";
        private const string kAlertPrefabPath = kPrefabFolderPath + "/AlertPanelP0.prefab";
        private const string kIntelPrefabPath = kPrefabFolderPath + "/IntelPanelP0.prefab";
        private const string kActionPrefabPath = kPrefabFolderPath + "/ActionPanelP0.prefab";
        private const string kConsequencePrefabPath = kPrefabFolderPath + "/ConsequencePanelP0.prefab";
        private const string kGlobalAlertPrefabPath = kPrefabFolderPath + "/GlobalAlertPanelP0.prefab";
        private const string kResourcePanelPrefabPath = kPrefabFolderPath + "/ResourcePanelP0.prefab";
        private const string kPhaseIndicatorPrefabPath = kPrefabFolderPath + "/PhaseIndicatorPanelP0.prefab";
        private const string kVictoryProgressPrefabPath = kPrefabFolderPath + "/VictoryProgressPanelP0.prefab";
        private const string kResourceItemPrefabPath = kPrefabFolderPath + "/ResourceItemP0.prefab";
        private const string kVictoryPathBarPrefabPath = kPrefabFolderPath + "/VictoryPathBarP0.prefab";

        private const string kMapBaseTexturePath = "Assets/Art/Map/art_map_base_regions_v001.png";
        private const string kMapNodesRoutesTexturePath = "Assets/Art/Map/art_map_nodes_routes_sheet_v001.png";
        private const string kUnitsSheetTexturePath = "Assets/Art/Units/art_units_faction_tokens_sheet_v001.png";
        private const string kUiSheetTexturePath = "Assets/Art/UI/art_ui_panels_buttons_skin_sheet_v001.png";
        private const string kIconSheetTexturePath = "Assets/Art/Icons/art_icons_resources_phases_status_sheet_v001.png";
        private const string kEventsSheetTexturePath = "Assets/Art/Events/art_events_news_illustrations_sheet_v001.png";
        private const string kPortraitsSheetTexturePath = "Assets/Art/Portraits/art_portraits_leaders_factions_sheet_v001.png";
        private const string kOverlaysSheetTexturePath = "Assets/Art/Overlays/art_map_terrain_fog_overlays_sheet_v001.png";
        private const string kDiplomacySheetTexturePath = "Assets/Art/Diplomacy/art_diplomacy_protocol_badges_sheet_v001.png";
        private const string kMarkersSheetTexturePath = "Assets/Art/Markers/art_markers_blockade_alert_status_sheet_v001.png";
        private const string kVfxSheetTexturePath = "Assets/Art/VFX/art_vfx_map_ui_feedback_sheet_v001.png";

        private static Font s_builtinFont;

        [MenuItem("EventideAge/Art/Build P0 High-Frequency UI Prefabs")]
        public static void BuildP0HighFrequencyUiPrefabs()
        {
            P0ArtImportSetup.ApplyP0ImportSettings();
            EnsureFolder("Assets/Prefabs");
            EnsureFolder("Assets/Prefabs/UI");
            EnsureFolder(kPrefabFolderPath);

            Sprite mapBaseSprite = AssetDatabase.LoadAssetAtPath<Sprite>(kMapBaseTexturePath);
            Sprite panelSprite = LoadSpriteFromSheet(kUiSheetTexturePath, "p0_ui_r00_c00");
            Sprite panelAccentSprite = LoadSpriteFromSheet(kUiSheetTexturePath, "p0_ui_r00_c01");
            Sprite mapIconSprite = LoadSpriteFromSheet(kIconSheetTexturePath, "p0_icons_r00_c00");
            Sprite diplomacyIconSprite = LoadSpriteFromSheet(kIconSheetTexturePath, "p0_icons_r00_c01");
            Sprite battleIconSprite = LoadSpriteFromSheet(kIconSheetTexturePath, "p0_icons_r00_c02");
            Sprite eventIconSprite = LoadSpriteFromSheet(kIconSheetTexturePath, "p0_icons_r00_c03");
            Sprite mapRouteSprite = LoadSpriteFromSheet(kMapNodesRoutesTexturePath, "p0_map_nodes_routes_r00_c00");
            Sprite mapNodeSprite = LoadSpriteFromSheet(kMapNodesRoutesTexturePath, "p0_map_nodes_routes_r00_c01");
            Sprite mapUnitSpriteA = LoadSpriteFromSheet(kUnitsSheetTexturePath, "p0_units_r00_c00");
            Sprite mapUnitSpriteB = LoadSpriteFromSheet(kUnitsSheetTexturePath, "p0_units_r00_c01");
            Sprite mapUnitSpriteC = LoadSpriteFromSheet(kUnitsSheetTexturePath, "p0_units_r00_c02");
            Sprite mapOverlaySprite = LoadSpriteFromSheet(kOverlaysSheetTexturePath, "p1_overlays_r00_c00");
            Sprite diplomacyBadgeSprite = LoadSpriteFromSheet(kDiplomacySheetTexturePath, "p1_diplomacy_r00_c00");
            Sprite battleFxSprite = LoadSpriteFromSheet(kVfxSheetTexturePath, "p1_vfx_r00_c00");
            Sprite eventArtSprite = LoadSpriteFromSheet(kEventsSheetTexturePath, "p1_events_r00_c00");
            Sprite portraitSprite = LoadSpriteFromSheet(kPortraitsSheetTexturePath, "p1_portraits_r00_c00");
            Sprite statusMarkerSprite = LoadSpriteFromSheet(kMarkersSheetTexturePath, "p1_markers_r00_c00");
            Sprite notificationIconSprite = LoadSpriteFromSheet(kMarkersSheetTexturePath, "p1_markers_r00_c01");
            Sprite intelIconSprite = LoadSpriteFromSheet(kIconSheetTexturePath, "p0_icons_r01_c00");
            Sprite intelArtSprite = LoadSpriteFromSheet(kOverlaysSheetTexturePath, "p1_overlays_r00_c01");
            Sprite notificationArtSprite = LoadSpriteFromSheet(kEventsSheetTexturePath, "p1_events_r00_c01");
            Sprite alertBadgeSprite = LoadSpriteFromSheet(kMarkersSheetTexturePath, "p1_markers_r00_c02");
            Sprite alertBgSprite = LoadSpriteFromSheet(kVfxSheetTexturePath, "p1_vfx_r00_c01");
            Sprite actionIconSprite = LoadSpriteFromSheet(kIconSheetTexturePath, "p0_icons_r00_c04");
            Sprite actionArtSprite = LoadSpriteFromSheet(kVfxSheetTexturePath, "p1_vfx_r00_c02");
            Sprite consequenceIconSprite = LoadSpriteFromSheet(kMarkersSheetTexturePath, "p1_markers_r00_c03");
            Sprite consequenceArtSprite = LoadSpriteFromSheet(kEventsSheetTexturePath, "p1_events_r00_c02");
            Sprite globalAlertBadgeSprite = LoadSpriteFromSheet(kMarkersSheetTexturePath, "p1_markers_r00_c04");
            Sprite globalAlertBgSprite = LoadSpriteFromSheet(kVfxSheetTexturePath, "p1_vfx_r00_c03");
            Sprite resourceIconSprite = LoadSpriteFromSheet(kIconSheetTexturePath, "p0_icons_r00_c05");
            Sprite phaseDotSprite = LoadSpriteFromSheet(kMarkersSheetTexturePath, "p1_markers_r00_c05");
            Sprite victoryIconSprite = LoadSpriteFromSheet(kDiplomacySheetTexturePath, "p1_diplomacy_r00_c01");
            Sprite victoryFillSprite = LoadSpriteFromSheet(kVfxSheetTexturePath, "p1_vfx_r00_c04");

            CreateResourceItemPrefab(
                kResourceItemPrefabPath,
                panelSprite,
                resourceIconSprite,
                statusMarkerSprite);

            CreateVictoryPathBarPrefab(
                kVictoryPathBarPrefabPath,
                panelSprite,
                victoryFillSprite);

            CreatePanelPrefab(
                kMapPrefabPath,
                "MapPanelP0",
                "Map",
                new Vector2(560f, 360f),
                new Color(0.18f, 0.27f, 0.35f, 0.96f),
                panelSprite,
                panelAccentSprite,
                mapIconSprite,
                mapBaseSprite,
                mapOverlaySprite,
                statusMarkerSprite,
                null,
                mapRouteSprite,
                mapNodeSprite,
                mapUnitSpriteA,
                mapUnitSpriteB,
                mapUnitSpriteC);

            CreatePanelPrefab(
                kDiplomacyPrefabPath,
                "DiplomacyPanelP0",
                "Diplomacy",
                new Vector2(520f, 320f),
                new Color(0.2f, 0.26f, 0.2f, 0.96f),
                panelSprite,
                panelAccentSprite,
                diplomacyBadgeSprite != null ? diplomacyBadgeSprite : diplomacyIconSprite,
                null,
                null,
                statusMarkerSprite,
                portraitSprite);

            CreatePanelPrefab(
                kBattlePrefabPath,
                "BattleReportPanelP0",
                "Battle Report",
                new Vector2(520f, 320f),
                new Color(0.32f, 0.24f, 0.2f, 0.96f),
                panelSprite,
                panelAccentSprite,
                battleIconSprite,
                battleFxSprite,
                null,
                statusMarkerSprite,
                null);

            CreatePanelPrefab(
                kEventPrefabPath,
                "EventPanelP0",
                "Events",
                new Vector2(520f, 320f),
                new Color(0.25f, 0.22f, 0.3f, 0.96f),
                panelSprite,
                panelAccentSprite,
                eventIconSprite,
                eventArtSprite,
                null,
                statusMarkerSprite,
                null);

            CreatePanelPrefab(
                kNotificationPrefabPath,
                "NotificationPanelP0",
                "Notifications",
                new Vector2(440f, 260f),
                new Color(0.19f, 0.19f, 0.25f, 0.95f),
                panelSprite,
                panelAccentSprite,
                notificationIconSprite,
                notificationArtSprite,
                null,
                statusMarkerSprite,
                null);

            CreatePanelPrefab(
                kIntelPrefabPath,
                "IntelPanelP0",
                "Intel",
                new Vector2(440f, 260f),
                new Color(0.18f, 0.22f, 0.3f, 0.95f),
                panelSprite,
                panelAccentSprite,
                intelIconSprite,
                intelArtSprite,
                null,
                statusMarkerSprite,
                null);

            CreatePanelPrefab(
                kActionPrefabPath,
                "ActionPanelP0",
                "Actions",
                new Vector2(440f, 180f),
                new Color(0.2f, 0.18f, 0.14f, 0.95f),
                panelSprite,
                panelAccentSprite,
                actionIconSprite,
                actionArtSprite,
                null,
                statusMarkerSprite,
                null);

            CreateConsequencePrefab(
                kConsequencePrefabPath,
                "ConsequencePanelP0",
                new Vector2(440f, 180f),
                panelSprite,
                panelAccentSprite,
                consequenceIconSprite,
                consequenceArtSprite,
                statusMarkerSprite);

            CreateAlertPrefab(
                kAlertPrefabPath,
                "AlertPanelP0",
                new Vector2(440f, 120f),
                panelSprite,
                panelAccentSprite,
                alertBadgeSprite,
                alertBgSprite);

            CreateAlertPrefab(
                kGlobalAlertPrefabPath,
                "GlobalAlertPanelP0",
                new Vector2(500f, 130f),
                panelSprite,
                panelAccentSprite,
                globalAlertBadgeSprite,
                globalAlertBgSprite);

            CreateResourcePanelPrefab(
                kResourcePanelPrefabPath,
                panelSprite,
                panelAccentSprite,
                resourceIconSprite);

            CreatePhaseIndicatorPanelPrefab(
                kPhaseIndicatorPrefabPath,
                panelSprite,
                phaseDotSprite);

            CreateVictoryProgressPanelPrefab(
                kVictoryProgressPrefabPath,
                panelSprite,
                panelAccentSprite,
                victoryIconSprite);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[P0HighFrequencyUiSetup] P0 high-frequency UI prefabs built.");
        }

        [MenuItem("EventideAge/Art/Apply P0 High-Frequency UI To MainGameScene")]
        public static void ApplyP0HighFrequencyUiToMainScene()
        {
            ApplyP0HighFrequencyUiToScene(kMainGameScenePath, bindSystemReferences: true, seedPrototypeContent: false);
        }

        [MenuItem("EventideAge/Release/Build Launch Main Scene")]
        public static void BuildLaunchMainScene()
        {
            SceneCreator.SetupBaselineScenesAndConfig();
            ApplyP0HighFrequencyUiToMainScene();
            Debug.Log("[P0HighFrequencyUiSetup] Launch main scene build complete.");
        }

        public static void ApplyP0HighFrequencyUiToMainSceneBatch()
        {
            ApplyP0HighFrequencyUiToMainScene();
        }

        public static void BuildLaunchMainSceneBatch()
        {
            BuildLaunchMainScene();
        }

        [MenuItem("EventideAge/Art/Apply P0 High-Frequency UI To PlayablePrototype")]
        public static void ApplyP0HighFrequencyUiToPlayablePrototype()
        {
            ApplyP0HighFrequencyUiToScene(kPlayablePrototypeScenePath, bindSystemReferences: false, seedPrototypeContent: true);
        }

        public static void ApplyP0HighFrequencyUiToPlayablePrototypeBatch()
        {
            ApplyP0HighFrequencyUiToPlayablePrototype();
        }

        private static void ApplyP0HighFrequencyUiToScene(string scenePath, bool bindSystemReferences, bool seedPrototypeContent)
        {
            if (!System.IO.File.Exists(scenePath))
            {
                Debug.LogError($"[P0HighFrequencyUiSetup] Scene not found: {scenePath}");
                return;
            }

            BuildP0HighFrequencyUiPrefabs();
            var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

            Canvas canvas = GetOrCreateCanvas();
            EnsureEventSystem();
            EnsureMainCamera();
            EnsureCanvasBackdrop(canvas, AssetDatabase.LoadAssetAtPath<Sprite>(kMapBaseTexturePath));

            GameObject uiRoot = GameObject.Find("P0HighFrequencyUIRoot");
            if (uiRoot == null)
            {
                uiRoot = new GameObject("P0HighFrequencyUIRoot", typeof(RectTransform));
                uiRoot.transform.SetParent(canvas.transform, false);
            }

            RectTransform uiRootRect = uiRoot.GetComponent<RectTransform>();
            uiRootRect.anchorMin = new Vector2(0f, 0f);
            uiRootRect.anchorMax = new Vector2(1f, 1f);
            uiRootRect.offsetMin = Vector2.zero;
            uiRootRect.offsetMax = Vector2.zero;

            ClearChildren(uiRoot.transform);

            GameObject mapPanel = InstantiatePrefab(kMapPrefabPath, uiRoot.transform);
            GameObject diplomacyPanel = InstantiatePrefab(kDiplomacyPrefabPath, uiRoot.transform);
            GameObject battlePanel = InstantiatePrefab(kBattlePrefabPath, uiRoot.transform);
            GameObject eventPanel = InstantiatePrefab(kEventPrefabPath, uiRoot.transform);
            GameObject notificationPanel = InstantiatePrefab(kNotificationPrefabPath, uiRoot.transform);
            GameObject alertPanel = InstantiatePrefab(kAlertPrefabPath, uiRoot.transform);
            GameObject intelPanel = InstantiatePrefab(kIntelPrefabPath, uiRoot.transform);
            GameObject actionPanel = InstantiatePrefab(kActionPrefabPath, uiRoot.transform);
            GameObject consequencePanel = InstantiatePrefab(kConsequencePrefabPath, uiRoot.transform);
            GameObject globalAlertPanel = InstantiatePrefab(kGlobalAlertPrefabPath, uiRoot.transform);
            GameObject resourcePanel = InstantiatePrefab(kResourcePanelPrefabPath, uiRoot.transform);
            GameObject phaseIndicatorPanel = InstantiatePrefab(kPhaseIndicatorPrefabPath, uiRoot.transform);
            GameObject victoryProgressPanel = InstantiatePrefab(kVictoryProgressPrefabPath, uiRoot.transform);

            if (mapPanel == null
                || diplomacyPanel == null
                || battlePanel == null
                || eventPanel == null
                || notificationPanel == null
                || alertPanel == null
                || intelPanel == null
                || actionPanel == null
                || consequencePanel == null
                || globalAlertPanel == null
                || resourcePanel == null
                || phaseIndicatorPanel == null
                || victoryProgressPanel == null)
            {
                Debug.LogError("[P0HighFrequencyUiSetup] Failed to instantiate one or more panel prefabs.");
                return;
            }

            ApplySurfaceLayout(
                mapPanel,
                diplomacyPanel,
                battlePanel,
                eventPanel,
                notificationPanel,
                alertPanel,
                intelPanel,
                actionPanel,
                consequencePanel,
                globalAlertPanel,
                resourcePanel,
                phaseIndicatorPanel,
                victoryProgressPanel);

            ApplyPanelOrder(
                mapPanel,
                diplomacyPanel,
                battlePanel,
                eventPanel,
                notificationPanel,
                intelPanel,
                actionPanel,
                consequencePanel,
                resourcePanel,
                phaseIndicatorPanel,
                globalAlertPanel,
                alertPanel,
                victoryProgressPanel);

            ApplyDefaultVisibility(
                seedPrototypeContent,
                notificationPanel,
                intelPanel,
                actionPanel,
                consequencePanel,
                globalAlertPanel,
                alertPanel,
                victoryProgressPanel);

            if (bindSystemReferences)
            {
                BindSystemReferences(
                    mapPanel,
                    diplomacyPanel,
                    battlePanel,
                    eventPanel,
                    notificationPanel,
                    alertPanel,
                    intelPanel,
                    actionPanel,
                    consequencePanel,
                    globalAlertPanel,
                    resourcePanel,
                    phaseIndicatorPanel,
                    victoryProgressPanel);

                EnsureLaunchFlowController(uiRoot.transform);
            }

            if (seedPrototypeContent)
            {
                SeedPrototypeContent(
                    mapPanel,
                    diplomacyPanel,
                    battlePanel,
                    eventPanel,
                    notificationPanel,
                    alertPanel,
                    intelPanel,
                    actionPanel,
                    consequencePanel,
                    globalAlertPanel,
                    resourcePanel,
                    phaseIndicatorPanel,
                    victoryProgressPanel);
            }

            EditorUtility.SetDirty(uiRoot);
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[P0HighFrequencyUiSetup] P0 panels applied to scene: {scenePath}");
        }

        private static void CreatePanelPrefab(
            string prefabPath,
            string rootName,
            string title,
            Vector2 size,
            Color tint,
            Sprite panelSprite,
            Sprite accentSprite,
            Sprite iconSprite,
            Sprite artSprite,
            Sprite overlaySprite,
            Sprite badgeSprite,
            Sprite portraitSprite,
            Sprite routeSprite = null,
            Sprite nodeSprite = null,
            Sprite unitSpriteA = null,
            Sprite unitSpriteB = null,
            Sprite unitSpriteC = null)
        {
            var root = new GameObject(rootName, typeof(RectTransform), typeof(CanvasGroup), typeof(Image));
            RectTransform rootRect = root.GetComponent<RectTransform>();
            rootRect.sizeDelta = size;

            Image rootImage = root.GetComponent<Image>();
            rootImage.color = tint;
            rootImage.sprite = panelSprite;
            rootImage.type = panelSprite != null ? Image.Type.Sliced : Image.Type.Simple;

            if (artSprite != null)
            {
                var artGo = new GameObject("ArtBackdrop", typeof(RectTransform), typeof(Image));
                artGo.transform.SetParent(root.transform, false);
                RectTransform artRect = artGo.GetComponent<RectTransform>();
                artRect.anchorMin = new Vector2(0.02f, 0.48f);
                artRect.anchorMax = new Vector2(0.98f, 0.98f);
                artRect.offsetMin = Vector2.zero;
                artRect.offsetMax = Vector2.zero;
                Image artImage = artGo.GetComponent<Image>();
                artImage.sprite = artSprite;
                artImage.color = new Color(1f, 1f, 1f, 0.16f);
                artImage.preserveAspect = false;
            }

            if (overlaySprite != null)
            {
                var overlayGo = new GameObject("ArtOverlay", typeof(RectTransform), typeof(Image));
                overlayGo.transform.SetParent(root.transform, false);
                RectTransform overlayRect = overlayGo.GetComponent<RectTransform>();
                overlayRect.anchorMin = new Vector2(0.02f, 0.48f);
                overlayRect.anchorMax = new Vector2(0.98f, 0.98f);
                overlayRect.offsetMin = Vector2.zero;
                overlayRect.offsetMax = Vector2.zero;
                Image overlayImage = overlayGo.GetComponent<Image>();
                overlayImage.sprite = overlaySprite;
                overlayImage.color = new Color(1f, 1f, 1f, 0.22f);
                overlayImage.preserveAspect = false;
            }

            if (routeSprite != null || nodeSprite != null || unitSpriteA != null || unitSpriteB != null || unitSpriteC != null)
            {
                var gameplayLayerRoot = new GameObject("GameplayArtLayer", typeof(RectTransform));
                gameplayLayerRoot.transform.SetParent(root.transform, false);
                RectTransform gameplayLayerRect = gameplayLayerRoot.GetComponent<RectTransform>();
                gameplayLayerRect.anchorMin = new Vector2(0.04f, 0.48f);
                gameplayLayerRect.anchorMax = new Vector2(0.96f, 0.94f);
                gameplayLayerRect.offsetMin = Vector2.zero;
                gameplayLayerRect.offsetMax = Vector2.zero;

                if (routeSprite != null)
                {
                    var routeGo = new GameObject("RouteNetworkLayer", typeof(RectTransform), typeof(Image));
                    routeGo.transform.SetParent(gameplayLayerRoot.transform, false);
                    RectTransform routeRect = routeGo.GetComponent<RectTransform>();
                    routeRect.anchorMin = new Vector2(0f, 0f);
                    routeRect.anchorMax = new Vector2(1f, 1f);
                    routeRect.offsetMin = Vector2.zero;
                    routeRect.offsetMax = Vector2.zero;
                    Image routeImage = routeGo.GetComponent<Image>();
                    routeImage.sprite = routeSprite;
                    routeImage.color = new Color(1f, 1f, 1f, 0.45f);
                    routeImage.preserveAspect = false;
                }

                if (nodeSprite != null)
                {
                    var nodeGo = new GameObject("NodeHighlightsLayer", typeof(RectTransform), typeof(Image));
                    nodeGo.transform.SetParent(gameplayLayerRoot.transform, false);
                    RectTransform nodeRect = nodeGo.GetComponent<RectTransform>();
                    nodeRect.anchorMin = new Vector2(0f, 0f);
                    nodeRect.anchorMax = new Vector2(1f, 1f);
                    nodeRect.offsetMin = Vector2.zero;
                    nodeRect.offsetMax = Vector2.zero;
                    Image nodeImage = nodeGo.GetComponent<Image>();
                    nodeImage.sprite = nodeSprite;
                    nodeImage.color = new Color(1f, 1f, 1f, 0.7f);
                    nodeImage.preserveAspect = false;
                }

                CreateMapUnitToken(gameplayLayerRoot.transform, "UnitTokenA", unitSpriteA, new Vector2(0.22f, 0.68f), new Color(1f, 1f, 1f, 0.95f), 38f);
                CreateMapUnitToken(gameplayLayerRoot.transform, "UnitTokenB", unitSpriteB, new Vector2(0.56f, 0.54f), new Color(1f, 1f, 1f, 0.95f), 38f);
                CreateMapUnitToken(gameplayLayerRoot.transform, "UnitTokenC", unitSpriteC, new Vector2(0.76f, 0.72f), new Color(1f, 1f, 1f, 0.95f), 38f);
            }

            var headerGo = new GameObject("Header", typeof(RectTransform), typeof(Image));
            headerGo.transform.SetParent(root.transform, false);
            RectTransform headerRect = headerGo.GetComponent<RectTransform>();
            headerRect.anchorMin = new Vector2(0.02f, 0.82f);
            headerRect.anchorMax = new Vector2(0.98f, 0.98f);
            headerRect.offsetMin = Vector2.zero;
            headerRect.offsetMax = Vector2.zero;
            Image headerImage = headerGo.GetComponent<Image>();
            headerImage.sprite = accentSprite;
            headerImage.color = new Color(1f, 1f, 1f, 0.3f);

            var iconGo = new GameObject("Icon", typeof(RectTransform), typeof(Image));
            iconGo.transform.SetParent(headerGo.transform, false);
            RectTransform iconRect = iconGo.GetComponent<RectTransform>();
            iconRect.anchorMin = new Vector2(0f, 0f);
            iconRect.anchorMax = new Vector2(0f, 1f);
            iconRect.pivot = new Vector2(0f, 0.5f);
            iconRect.sizeDelta = new Vector2(54f, 54f);
            iconRect.anchoredPosition = new Vector2(8f, 0f);
            Image iconImage = iconGo.GetComponent<Image>();
            iconImage.sprite = iconSprite;
            iconImage.color = Color.white;

            if (badgeSprite != null)
            {
                var badgeGo = new GameObject("StatusBadge", typeof(RectTransform), typeof(Image));
                badgeGo.transform.SetParent(headerGo.transform, false);
                RectTransform badgeRect = badgeGo.GetComponent<RectTransform>();
                badgeRect.anchorMin = new Vector2(1f, 0.5f);
                badgeRect.anchorMax = new Vector2(1f, 0.5f);
                badgeRect.pivot = new Vector2(1f, 0.5f);
                badgeRect.sizeDelta = new Vector2(40f, 40f);
                badgeRect.anchoredPosition = new Vector2(-10f, 0f);
                Image badgeImage = badgeGo.GetComponent<Image>();
                badgeImage.sprite = badgeSprite;
                badgeImage.color = new Color(1f, 1f, 1f, 0.92f);
            }

            if (portraitSprite != null)
            {
                var portraitGo = new GameObject("LeaderPortrait", typeof(RectTransform), typeof(Image));
                portraitGo.transform.SetParent(root.transform, false);
                RectTransform portraitRect = portraitGo.GetComponent<RectTransform>();
                portraitRect.anchorMin = new Vector2(0.78f, 0.56f);
                portraitRect.anchorMax = new Vector2(0.96f, 0.8f);
                portraitRect.offsetMin = Vector2.zero;
                portraitRect.offsetMax = Vector2.zero;
                Image portraitImage = portraitGo.GetComponent<Image>();
                portraitImage.sprite = portraitSprite;
                portraitImage.color = new Color(1f, 1f, 1f, 0.95f);
                portraitImage.preserveAspect = true;
            }

            Text titleText = CreateText(headerGo.transform, "TitleText", title, 24, FontStyle.Bold, TextAnchor.MiddleLeft, Color.white);
            RectTransform titleRect = titleText.rectTransform;
            titleRect.anchorMin = new Vector2(0f, 0f);
            titleRect.anchorMax = new Vector2(1f, 1f);
            titleRect.offsetMin = new Vector2(72f, 0f);
            titleRect.offsetMax = new Vector2(-12f, 0f);

            Text latestLabelText = CreateText(root.transform, "LatestLabel", "Latest", 16, FontStyle.Bold, TextAnchor.UpperLeft, new Color(0.94f, 0.94f, 0.94f, 1f));
            RectTransform latestLabelRect = latestLabelText.rectTransform;
            latestLabelRect.anchorMin = new Vector2(0.04f, 0.72f);
            latestLabelRect.anchorMax = new Vector2(0.96f, 0.8f);
            latestLabelRect.offsetMin = Vector2.zero;
            latestLabelRect.offsetMax = Vector2.zero;

            Text latestValueText = CreateText(root.transform, "LatestValue", "Waiting for runtime feed...", 15, FontStyle.Normal, TextAnchor.UpperLeft, new Color(0.95f, 0.95f, 0.95f, 0.95f));
            RectTransform latestValueRect = latestValueText.rectTransform;
            latestValueRect.anchorMin = new Vector2(0.04f, 0.57f);
            latestValueRect.anchorMax = new Vector2(0.96f, 0.72f);
            latestValueRect.offsetMin = Vector2.zero;
            latestValueRect.offsetMax = Vector2.zero;

            Text historyLabelText = CreateText(root.transform, "HistoryLabel", "History", 16, FontStyle.Bold, TextAnchor.UpperLeft, new Color(0.94f, 0.94f, 0.94f, 1f));
            RectTransform historyLabelRect = historyLabelText.rectTransform;
            historyLabelRect.anchorMin = new Vector2(0.04f, 0.5f);
            historyLabelRect.anchorMax = new Vector2(0.96f, 0.56f);
            historyLabelRect.offsetMin = Vector2.zero;
            historyLabelRect.offsetMax = Vector2.zero;

            var historyBg = new GameObject("HistoryBackground", typeof(RectTransform), typeof(Image));
            historyBg.transform.SetParent(root.transform, false);
            RectTransform historyBgRect = historyBg.GetComponent<RectTransform>();
            historyBgRect.anchorMin = new Vector2(0.04f, 0.06f);
            historyBgRect.anchorMax = new Vector2(0.96f, 0.48f);
            historyBgRect.offsetMin = Vector2.zero;
            historyBgRect.offsetMax = Vector2.zero;
            Image historyBgImage = historyBg.GetComponent<Image>();
            historyBgImage.color = new Color(0.04f, 0.05f, 0.07f, 0.72f);

            Text historyValueText = CreateText(historyBg.transform, "HistoryValue", "Waiting for runtime feed...", 14, FontStyle.Normal, TextAnchor.UpperLeft, new Color(0.92f, 0.92f, 0.94f, 0.95f));
            RectTransform historyValueRect = historyValueText.rectTransform;
            historyValueRect.anchorMin = new Vector2(0f, 0f);
            historyValueRect.anchorMax = new Vector2(1f, 1f);
            historyValueRect.offsetMin = new Vector2(10f, 8f);
            historyValueRect.offsetMax = new Vector2(-10f, -8f);
            historyValueText.horizontalOverflow = HorizontalWrapMode.Wrap;
            historyValueText.verticalOverflow = VerticalWrapMode.Overflow;

            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);
        }

        private static void CreateMapUnitToken(
            Transform parent,
            string name,
            Sprite sprite,
            Vector2 anchor,
            Color tint,
            float size)
        {
            if (parent == null || sprite == null)
            {
                return;
            }

            var tokenGo = new GameObject(name, typeof(RectTransform), typeof(Image));
            tokenGo.transform.SetParent(parent, false);

            RectTransform tokenRect = tokenGo.GetComponent<RectTransform>();
            tokenRect.anchorMin = anchor;
            tokenRect.anchorMax = anchor;
            tokenRect.pivot = new Vector2(0.5f, 0.5f);
            tokenRect.sizeDelta = new Vector2(size, size);
            tokenRect.anchoredPosition = Vector2.zero;

            Image tokenImage = tokenGo.GetComponent<Image>();
            tokenImage.sprite = sprite;
            tokenImage.color = tint;
            tokenImage.preserveAspect = true;
        }

        private static Text CreateText(Transform parent, string name, string content, int fontSize, FontStyle fontStyle, TextAnchor anchor, Color color)
        {
            var textGo = new GameObject(name, typeof(RectTransform), typeof(Text));
            textGo.transform.SetParent(parent, false);
            Text text = textGo.GetComponent<Text>();
            text.font = GetBuiltinFont();
            text.text = content;
            text.fontSize = fontSize;
            text.fontStyle = fontStyle;
            text.alignment = anchor;
            text.color = color;
            return text;
        }

        private static void CreateAlertPrefab(
            string prefabPath,
            string rootName,
            Vector2 size,
            Sprite panelSprite,
            Sprite accentSprite,
            Sprite badgeSprite,
            Sprite fxSprite)
        {
            var root = new GameObject(rootName, typeof(RectTransform), typeof(CanvasGroup), typeof(Image));
            RectTransform rootRect = root.GetComponent<RectTransform>();
            rootRect.sizeDelta = size;

            Image rootImage = root.GetComponent<Image>();
            rootImage.color = new Color(0.22f, 0.13f, 0.13f, 0.95f);
            rootImage.sprite = panelSprite;
            rootImage.type = panelSprite != null ? Image.Type.Sliced : Image.Type.Simple;

            var accentGo = new GameObject("Header", typeof(RectTransform), typeof(Image));
            accentGo.transform.SetParent(root.transform, false);
            RectTransform accentRect = accentGo.GetComponent<RectTransform>();
            accentRect.anchorMin = new Vector2(0.02f, 0.56f);
            accentRect.anchorMax = new Vector2(0.98f, 0.98f);
            accentRect.offsetMin = Vector2.zero;
            accentRect.offsetMax = Vector2.zero;
            Image accentImage = accentGo.GetComponent<Image>();
            accentImage.sprite = accentSprite;
            accentImage.color = new Color(1f, 1f, 1f, 0.25f);

            if (fxSprite != null)
            {
                var fxGo = new GameObject("FxOverlay", typeof(RectTransform), typeof(Image));
                fxGo.transform.SetParent(root.transform, false);
                RectTransform fxRect = fxGo.GetComponent<RectTransform>();
                fxRect.anchorMin = new Vector2(0.02f, 0.08f);
                fxRect.anchorMax = new Vector2(0.98f, 0.52f);
                fxRect.offsetMin = Vector2.zero;
                fxRect.offsetMax = Vector2.zero;
                Image fxImage = fxGo.GetComponent<Image>();
                fxImage.sprite = fxSprite;
                fxImage.color = new Color(1f, 1f, 1f, 0.2f);
                fxImage.preserveAspect = false;
            }

            var alertBgGo = new GameObject("AlertBackground", typeof(RectTransform), typeof(Image));
            alertBgGo.transform.SetParent(root.transform, false);
            RectTransform alertBgRect = alertBgGo.GetComponent<RectTransform>();
            alertBgRect.anchorMin = new Vector2(0.04f, 0.08f);
            alertBgRect.anchorMax = new Vector2(0.96f, 0.52f);
            alertBgRect.offsetMin = Vector2.zero;
            alertBgRect.offsetMax = Vector2.zero;
            Image alertBgImage = alertBgGo.GetComponent<Image>();
            alertBgImage.color = new Color(0.78f, 0.16f, 0.16f, 0.9f);

            Text label = CreateText(root.transform, "AlertLabel", "Alert", 16, FontStyle.Bold, TextAnchor.UpperLeft, Color.white);
            RectTransform labelRect = label.rectTransform;
            labelRect.anchorMin = new Vector2(0.12f, 0.62f);
            labelRect.anchorMax = new Vector2(0.96f, 0.95f);
            labelRect.offsetMin = Vector2.zero;
            labelRect.offsetMax = Vector2.zero;

            Text alertText = CreateText(root.transform, "AlertText", "[INFO] [SYSTEM] Waiting for alerts...", 14, FontStyle.Normal, TextAnchor.MiddleLeft, Color.white);
            RectTransform textRect = alertText.rectTransform;
            textRect.anchorMin = new Vector2(0.06f, 0.12f);
            textRect.anchorMax = new Vector2(0.94f, 0.48f);
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            if (badgeSprite != null)
            {
                var badgeGo = new GameObject("StatusBadge", typeof(RectTransform), typeof(Image));
                badgeGo.transform.SetParent(root.transform, false);
                RectTransform badgeRect = badgeGo.GetComponent<RectTransform>();
                badgeRect.anchorMin = new Vector2(0.02f, 0.58f);
                badgeRect.anchorMax = new Vector2(0.1f, 0.95f);
                badgeRect.offsetMin = Vector2.zero;
                badgeRect.offsetMax = Vector2.zero;
                Image badgeImage = badgeGo.GetComponent<Image>();
                badgeImage.sprite = badgeSprite;
                badgeImage.color = new Color(1f, 1f, 1f, 0.95f);
                badgeImage.preserveAspect = true;
            }

            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);
        }

        private static void CreateConsequencePrefab(
            string prefabPath,
            string rootName,
            Vector2 size,
            Sprite panelSprite,
            Sprite accentSprite,
            Sprite iconSprite,
            Sprite artSprite,
            Sprite badgeSprite)
        {
            CreatePanelPrefab(
                prefabPath,
                rootName,
                "Consequences",
                size,
                new Color(0.18f, 0.16f, 0.19f, 0.95f),
                panelSprite,
                accentSprite,
                iconSprite,
                artSprite,
                null,
                badgeSprite,
                null);
        }

        private static void CreateResourceItemPrefab(
            string prefabPath,
            Sprite panelSprite,
            Sprite iconSprite,
            Sprite markerSprite)
        {
            var root = new GameObject("ResourceItemP0", typeof(RectTransform), typeof(Image), typeof(ResourceItemUI));
            RectTransform rootRect = root.GetComponent<RectTransform>();
            rootRect.sizeDelta = new Vector2(210f, 40f);

            Image rootImage = root.GetComponent<Image>();
            rootImage.sprite = panelSprite;
            rootImage.type = panelSprite != null ? Image.Type.Sliced : Image.Type.Simple;
            rootImage.color = new Color(0.12f, 0.14f, 0.18f, 0.9f);

            var iconGo = new GameObject("Icon", typeof(RectTransform), typeof(Image));
            iconGo.transform.SetParent(root.transform, false);
            RectTransform iconRect = iconGo.GetComponent<RectTransform>();
            iconRect.anchorMin = new Vector2(0f, 0f);
            iconRect.anchorMax = new Vector2(0f, 1f);
            iconRect.pivot = new Vector2(0f, 0.5f);
            iconRect.sizeDelta = new Vector2(28f, 28f);
            iconRect.anchoredPosition = new Vector2(8f, 0f);
            Image iconImage = iconGo.GetComponent<Image>();
            iconImage.sprite = iconSprite != null ? iconSprite : markerSprite;
            iconImage.color = Color.white;

            var nameText = CreateText(root.transform, "NameText", "Resource", 12, FontStyle.Bold, TextAnchor.MiddleLeft, new Color(0.95f, 0.95f, 0.95f, 1f));
            RectTransform nameRect = nameText.rectTransform;
            nameRect.anchorMin = new Vector2(0f, 0.5f);
            nameRect.anchorMax = new Vector2(0.55f, 1f);
            nameRect.offsetMin = new Vector2(40f, -2f);
            nameRect.offsetMax = new Vector2(0f, -2f);

            var amountText = CreateText(root.transform, "AmountText", "0", 12, FontStyle.Bold, TextAnchor.MiddleRight, new Color(0.98f, 0.92f, 0.74f, 1f));
            RectTransform amountRect = amountText.rectTransform;
            amountRect.anchorMin = new Vector2(0.78f, 0.5f);
            amountRect.anchorMax = new Vector2(0.98f, 1f);
            amountRect.offsetMin = Vector2.zero;
            amountRect.offsetMax = new Vector2(-4f, -2f);

            var sliderGo = new GameObject("FillSlider", typeof(RectTransform), typeof(Slider));
            sliderGo.transform.SetParent(root.transform, false);
            RectTransform sliderRect = sliderGo.GetComponent<RectTransform>();
            sliderRect.anchorMin = new Vector2(0f, 0f);
            sliderRect.anchorMax = new Vector2(1f, 0.45f);
            sliderRect.offsetMin = new Vector2(40f, 4f);
            sliderRect.offsetMax = new Vector2(-10f, 0f);

            var backgroundGo = new GameObject("Background", typeof(RectTransform), typeof(Image));
            backgroundGo.transform.SetParent(sliderGo.transform, false);
            RectTransform bgRect = backgroundGo.GetComponent<RectTransform>();
            bgRect.anchorMin = new Vector2(0f, 0f);
            bgRect.anchorMax = new Vector2(1f, 1f);
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            Image bgImage = backgroundGo.GetComponent<Image>();
            bgImage.color = new Color(0.18f, 0.2f, 0.24f, 0.95f);

            var fillAreaGo = new GameObject("Fill Area", typeof(RectTransform));
            fillAreaGo.transform.SetParent(sliderGo.transform, false);
            RectTransform fillAreaRect = fillAreaGo.GetComponent<RectTransform>();
            fillAreaRect.anchorMin = new Vector2(0f, 0f);
            fillAreaRect.anchorMax = new Vector2(1f, 1f);
            fillAreaRect.offsetMin = new Vector2(2f, 2f);
            fillAreaRect.offsetMax = new Vector2(-2f, -2f);

            var fillGo = new GameObject("Fill", typeof(RectTransform), typeof(Image));
            fillGo.transform.SetParent(fillAreaGo.transform, false);
            RectTransform fillRect = fillGo.GetComponent<RectTransform>();
            fillRect.anchorMin = new Vector2(0f, 0f);
            fillRect.anchorMax = new Vector2(1f, 1f);
            fillRect.offsetMin = Vector2.zero;
            fillRect.offsetMax = Vector2.zero;
            Image fillImage = fillGo.GetComponent<Image>();
            fillImage.color = new Color(0.3f, 0.65f, 0.36f, 1f);

            Slider slider = sliderGo.GetComponent<Slider>();
            slider.targetGraphic = bgImage;
            slider.fillRect = fillRect;
            slider.direction = Slider.Direction.LeftToRight;
            slider.minValue = 0f;
            slider.maxValue = 100f;
            slider.value = 60f;

            ResourceItemUI itemUi = root.GetComponent<ResourceItemUI>();
            itemUi.Icon = iconImage;
            itemUi.NameText = nameText;
            itemUi.AmountText = amountText;
            itemUi.FillSlider = slider;

            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);
        }

        private static void CreateVictoryPathBarPrefab(
            string prefabPath,
            Sprite panelSprite,
            Sprite fillSprite)
        {
            var root = new GameObject("VictoryPathBarP0", typeof(RectTransform), typeof(Image), typeof(VictoryPathBar));
            RectTransform rootRect = root.GetComponent<RectTransform>();
            rootRect.sizeDelta = new Vector2(380f, 44f);

            Image bgImage = root.GetComponent<Image>();
            bgImage.sprite = panelSprite;
            bgImage.type = panelSprite != null ? Image.Type.Sliced : Image.Type.Simple;
            bgImage.color = new Color(0.1f, 0.12f, 0.17f, 0.9f);

            Text pathName = CreateText(root.transform, "PathNameText", "Path", 12, FontStyle.Bold, TextAnchor.MiddleLeft, Color.white);
            RectTransform pathRect = pathName.rectTransform;
            pathRect.anchorMin = new Vector2(0.03f, 0.5f);
            pathRect.anchorMax = new Vector2(0.42f, 0.95f);
            pathRect.offsetMin = Vector2.zero;
            pathRect.offsetMax = Vector2.zero;

            var progressBgGo = new GameObject("ProgressBackground", typeof(RectTransform), typeof(Image));
            progressBgGo.transform.SetParent(root.transform, false);
            RectTransform progressBgRect = progressBgGo.GetComponent<RectTransform>();
            progressBgRect.anchorMin = new Vector2(0.03f, 0.1f);
            progressBgRect.anchorMax = new Vector2(0.78f, 0.45f);
            progressBgRect.offsetMin = Vector2.zero;
            progressBgRect.offsetMax = Vector2.zero;
            Image progressBgImage = progressBgGo.GetComponent<Image>();
            progressBgImage.color = new Color(0.2f, 0.22f, 0.28f, 1f);

            var fillGo = new GameObject("ProgressFill", typeof(RectTransform), typeof(Image));
            fillGo.transform.SetParent(progressBgGo.transform, false);
            RectTransform fillRect = fillGo.GetComponent<RectTransform>();
            fillRect.anchorMin = new Vector2(0f, 0f);
            fillRect.anchorMax = new Vector2(1f, 1f);
            fillRect.offsetMin = Vector2.zero;
            fillRect.offsetMax = Vector2.zero;
            Image fillImage = fillGo.GetComponent<Image>();
            fillImage.sprite = fillSprite;
            fillImage.type = Image.Type.Filled;
            fillImage.fillMethod = Image.FillMethod.Horizontal;
            fillImage.fillAmount = 0.35f;
            fillImage.color = new Color(0.35f, 0.72f, 0.88f, 0.95f);

            Text progressText = CreateText(root.transform, "ProgressText", "35%", 12, FontStyle.Bold, TextAnchor.MiddleRight, new Color(0.95f, 0.95f, 0.95f, 1f));
            RectTransform progressRect = progressText.rectTransform;
            progressRect.anchorMin = new Vector2(0.8f, 0.1f);
            progressRect.anchorMax = new Vector2(0.97f, 0.9f);
            progressRect.offsetMin = Vector2.zero;
            progressRect.offsetMax = Vector2.zero;

            VictoryPathBar bar = root.GetComponent<VictoryPathBar>();
            bar.PathNameText = pathName;
            bar.ProgressFill = fillImage;
            bar.ProgressText = progressText;

            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);
        }

        private static void CreateResourcePanelPrefab(
            string prefabPath,
            Sprite panelSprite,
            Sprite accentSprite,
            Sprite iconSprite)
        {
            var root = new GameObject("ResourcePanelP0", typeof(RectTransform), typeof(CanvasGroup), typeof(Image));
            RectTransform rootRect = root.GetComponent<RectTransform>();
            rootRect.sizeDelta = new Vector2(740f, 136f);

            Image rootImage = root.GetComponent<Image>();
            rootImage.sprite = panelSprite;
            rootImage.type = panelSprite != null ? Image.Type.Sliced : Image.Type.Simple;
            rootImage.color = new Color(0.1f, 0.13f, 0.16f, 0.94f);

            var headerGo = new GameObject("Header", typeof(RectTransform), typeof(Image));
            headerGo.transform.SetParent(root.transform, false);
            RectTransform headerRect = headerGo.GetComponent<RectTransform>();
            headerRect.anchorMin = new Vector2(0.01f, 0.58f);
            headerRect.anchorMax = new Vector2(0.99f, 0.96f);
            headerRect.offsetMin = Vector2.zero;
            headerRect.offsetMax = Vector2.zero;
            Image headerImage = headerGo.GetComponent<Image>();
            headerImage.sprite = accentSprite;
            headerImage.color = new Color(1f, 1f, 1f, 0.2f);

            var iconGo = new GameObject("Icon", typeof(RectTransform), typeof(Image));
            iconGo.transform.SetParent(headerGo.transform, false);
            RectTransform iconRect = iconGo.GetComponent<RectTransform>();
            iconRect.anchorMin = new Vector2(0f, 0f);
            iconRect.anchorMax = new Vector2(0f, 1f);
            iconRect.pivot = new Vector2(0f, 0.5f);
            iconRect.sizeDelta = new Vector2(34f, 34f);
            iconRect.anchoredPosition = new Vector2(6f, 0f);
            Image iconImage = iconGo.GetComponent<Image>();
            iconImage.sprite = iconSprite;
            iconImage.color = Color.white;

            Text turnText = CreateText(headerGo.transform, "TurnText", "Turn 1", 16, FontStyle.Bold, TextAnchor.MiddleLeft, Color.white);
            RectTransform turnRect = turnText.rectTransform;
            turnRect.anchorMin = new Vector2(0f, 0f);
            turnRect.anchorMax = new Vector2(0.26f, 1f);
            turnRect.offsetMin = new Vector2(44f, 0f);
            turnRect.offsetMax = Vector2.zero;

            Text timeText = CreateText(headerGo.transform, "TimeText", "2028 H1", 15, FontStyle.Normal, TextAnchor.MiddleLeft, new Color(0.93f, 0.93f, 0.95f, 1f));
            RectTransform timeRect = timeText.rectTransform;
            timeRect.anchorMin = new Vector2(0.26f, 0f);
            timeRect.anchorMax = new Vector2(0.48f, 1f);
            timeRect.offsetMin = Vector2.zero;
            timeRect.offsetMax = Vector2.zero;

            Text phaseText = CreateText(headerGo.transform, "PhaseText", "Diplomacy", 15, FontStyle.Normal, TextAnchor.MiddleLeft, new Color(0.86f, 0.92f, 0.98f, 1f));
            RectTransform phaseRect = phaseText.rectTransform;
            phaseRect.anchorMin = new Vector2(0.48f, 0f);
            phaseRect.anchorMax = new Vector2(0.72f, 1f);
            phaseRect.offsetMin = Vector2.zero;
            phaseRect.offsetMax = Vector2.zero;

            Text apText = CreateText(headerGo.transform, "ActionPointsText", "AP: 11", 15, FontStyle.Bold, TextAnchor.MiddleRight, new Color(0.98f, 0.9f, 0.62f, 1f));
            RectTransform apRect = apText.rectTransform;
            apRect.anchorMin = new Vector2(0.72f, 0f);
            apRect.anchorMax = new Vector2(0.98f, 1f);
            apRect.offsetMin = Vector2.zero;
            apRect.offsetMax = new Vector2(-6f, 0f);

            var containerGo = new GameObject("ResourceBarContainer", typeof(RectTransform), typeof(HorizontalLayoutGroup));
            containerGo.transform.SetParent(root.transform, false);
            RectTransform containerRect = containerGo.GetComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.02f, 0.08f);
            containerRect.anchorMax = new Vector2(0.98f, 0.52f);
            containerRect.offsetMin = Vector2.zero;
            containerRect.offsetMax = Vector2.zero;
            HorizontalLayoutGroup hLayout = containerGo.GetComponent<HorizontalLayoutGroup>();
            hLayout.spacing = 6f;
            hLayout.childAlignment = TextAnchor.MiddleLeft;
            hLayout.childControlWidth = false;
            hLayout.childControlHeight = false;
            hLayout.childForceExpandWidth = false;
            hLayout.childForceExpandHeight = false;

            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);
        }

        private static void CreatePhaseIndicatorPanelPrefab(
            string prefabPath,
            Sprite panelSprite,
            Sprite markerSprite)
        {
            var root = new GameObject("PhaseIndicatorPanelP0", typeof(RectTransform), typeof(CanvasGroup), typeof(Image), typeof(PhaseIndicatorUI));
            RectTransform rootRect = root.GetComponent<RectTransform>();
            rootRect.sizeDelta = new Vector2(740f, 56f);

            Image rootImage = root.GetComponent<Image>();
            rootImage.sprite = panelSprite;
            rootImage.type = panelSprite != null ? Image.Type.Sliced : Image.Type.Simple;
            rootImage.color = new Color(0.12f, 0.14f, 0.2f, 0.92f);

            var layoutGo = new GameObject("PhaseButtons", typeof(RectTransform), typeof(HorizontalLayoutGroup));
            layoutGo.transform.SetParent(root.transform, false);
            RectTransform layoutRect = layoutGo.GetComponent<RectTransform>();
            layoutRect.anchorMin = new Vector2(0.02f, 0.12f);
            layoutRect.anchorMax = new Vector2(0.98f, 0.88f);
            layoutRect.offsetMin = Vector2.zero;
            layoutRect.offsetMax = Vector2.zero;
            HorizontalLayoutGroup layout = layoutGo.GetComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            string[] phaseNames = { "Diplomacy", "Strategy", "Military", "Logistics", "Intel", "AI" };
            Button[] phaseButtons = new Button[phaseNames.Length];
            Text[] phaseLabels = new Text[phaseNames.Length];
            for (int i = 0; i < phaseNames.Length; i++)
            {
                var buttonGo = new GameObject("PhaseButton_" + i, typeof(RectTransform), typeof(Image), typeof(Button));
                buttonGo.transform.SetParent(layoutGo.transform, false);
                Image buttonImage = buttonGo.GetComponent<Image>();
                buttonImage.color = new Color(0.28f, 0.3f, 0.37f, 0.9f);
                if (markerSprite != null)
                {
                    buttonImage.sprite = markerSprite;
                    buttonImage.type = Image.Type.Sliced;
                }

                Button button = buttonGo.GetComponent<Button>();
                button.targetGraphic = buttonImage;

                Text label = CreateText(buttonGo.transform, "Text", phaseNames[i], 12, FontStyle.Bold, TextAnchor.MiddleCenter, Color.white);
                RectTransform labelRect = label.rectTransform;
                labelRect.anchorMin = new Vector2(0f, 0f);
                labelRect.anchorMax = new Vector2(1f, 1f);
                labelRect.offsetMin = Vector2.zero;
                labelRect.offsetMax = Vector2.zero;

                phaseButtons[i] = button;
                phaseLabels[i] = label;
            }

            PhaseIndicatorUI phaseIndicator = root.GetComponent<PhaseIndicatorUI>();
            phaseIndicator.PhaseButtons = phaseButtons;
            phaseIndicator.PhaseLabels = phaseLabels;

            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);
        }

        private static void CreateVictoryProgressPanelPrefab(
            string prefabPath,
            Sprite panelSprite,
            Sprite accentSprite,
            Sprite iconSprite)
        {
            var root = new GameObject("VictoryProgressPanelP0", typeof(RectTransform), typeof(CanvasGroup), typeof(Image));
            RectTransform rootRect = root.GetComponent<RectTransform>();
            rootRect.sizeDelta = new Vector2(500f, 344f);

            Image rootImage = root.GetComponent<Image>();
            rootImage.sprite = panelSprite;
            rootImage.type = panelSprite != null ? Image.Type.Sliced : Image.Type.Simple;
            rootImage.color = new Color(0.11f, 0.12f, 0.18f, 0.95f);

            var headerGo = new GameObject("Header", typeof(RectTransform), typeof(Image));
            headerGo.transform.SetParent(root.transform, false);
            RectTransform headerRect = headerGo.GetComponent<RectTransform>();
            headerRect.anchorMin = new Vector2(0.02f, 0.84f);
            headerRect.anchorMax = new Vector2(0.98f, 0.98f);
            headerRect.offsetMin = Vector2.zero;
            headerRect.offsetMax = Vector2.zero;
            Image headerImage = headerGo.GetComponent<Image>();
            headerImage.sprite = accentSprite;
            headerImage.color = new Color(1f, 1f, 1f, 0.2f);

            var iconGo = new GameObject("Icon", typeof(RectTransform), typeof(Image));
            iconGo.transform.SetParent(headerGo.transform, false);
            RectTransform iconRect = iconGo.GetComponent<RectTransform>();
            iconRect.anchorMin = new Vector2(0f, 0f);
            iconRect.anchorMax = new Vector2(0f, 1f);
            iconRect.pivot = new Vector2(0f, 0.5f);
            iconRect.sizeDelta = new Vector2(34f, 34f);
            iconRect.anchoredPosition = new Vector2(8f, 0f);
            Image iconImage = iconGo.GetComponent<Image>();
            iconImage.sprite = iconSprite;
            iconImage.color = Color.white;

            Text titleText = CreateText(headerGo.transform, "TitleText", "Victory Progress", 18, FontStyle.Bold, TextAnchor.MiddleLeft, Color.white);
            RectTransform titleRect = titleText.rectTransform;
            titleRect.anchorMin = new Vector2(0f, 0f);
            titleRect.anchorMax = new Vector2(1f, 1f);
            titleRect.offsetMin = new Vector2(46f, 0f);
            titleRect.offsetMax = new Vector2(-10f, 0f);

            Text latestText = CreateText(root.transform, "LatestValue", "Waiting for runtime feed...", 14, FontStyle.Normal, TextAnchor.UpperLeft, new Color(0.94f, 0.94f, 0.96f, 0.95f));
            RectTransform latestRect = latestText.rectTransform;
            latestRect.anchorMin = new Vector2(0.04f, 0.74f);
            latestRect.anchorMax = new Vector2(0.96f, 0.83f);
            latestRect.offsetMin = Vector2.zero;
            latestRect.offsetMax = Vector2.zero;

            var containerGo = new GameObject("ProgressBarContainer", typeof(RectTransform), typeof(VerticalLayoutGroup));
            containerGo.transform.SetParent(root.transform, false);
            RectTransform containerRect = containerGo.GetComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.04f, 0.2f);
            containerRect.anchorMax = new Vector2(0.96f, 0.72f);
            containerRect.offsetMin = Vector2.zero;
            containerRect.offsetMax = Vector2.zero;
            VerticalLayoutGroup vLayout = containerGo.GetComponent<VerticalLayoutGroup>();
            vLayout.spacing = 6f;
            vLayout.childAlignment = TextAnchor.UpperCenter;
            vLayout.childControlWidth = true;
            vLayout.childControlHeight = false;
            vLayout.childForceExpandWidth = true;
            vLayout.childForceExpandHeight = false;

            var historyBgGo = new GameObject("HistoryBackground", typeof(RectTransform), typeof(Image));
            historyBgGo.transform.SetParent(root.transform, false);
            RectTransform historyBgRect = historyBgGo.GetComponent<RectTransform>();
            historyBgRect.anchorMin = new Vector2(0.04f, 0.06f);
            historyBgRect.anchorMax = new Vector2(0.96f, 0.18f);
            historyBgRect.offsetMin = Vector2.zero;
            historyBgRect.offsetMax = Vector2.zero;
            Image historyBgImage = historyBgGo.GetComponent<Image>();
            historyBgImage.color = new Color(0.05f, 0.06f, 0.08f, 0.75f);

            Text historyText = CreateText(historyBgGo.transform, "HistoryValue", "Waiting for runtime feed...", 13, FontStyle.Normal, TextAnchor.UpperLeft, new Color(0.9f, 0.9f, 0.92f, 0.95f));
            RectTransform historyRect = historyText.rectTransform;
            historyRect.anchorMin = new Vector2(0f, 0f);
            historyRect.anchorMax = new Vector2(1f, 1f);
            historyRect.offsetMin = new Vector2(8f, 4f);
            historyRect.offsetMax = new Vector2(-8f, -4f);

            var warningGo = new GameObject("DefeatWarningPanel", typeof(RectTransform), typeof(Image));
            warningGo.transform.SetParent(root.transform, false);
            RectTransform warningRect = warningGo.GetComponent<RectTransform>();
            warningRect.anchorMin = new Vector2(0.04f, 0.01f);
            warningRect.anchorMax = new Vector2(0.96f, 0.12f);
            warningRect.offsetMin = Vector2.zero;
            warningRect.offsetMax = Vector2.zero;
            Image warningImage = warningGo.GetComponent<Image>();
            warningImage.color = new Color(0.72f, 0.18f, 0.18f, 0.9f);

            Text warningText = CreateText(warningGo.transform, "DefeatWarningText", "No defeat risks.", 12, FontStyle.Bold, TextAnchor.MiddleLeft, Color.white);
            RectTransform warningTextRect = warningText.rectTransform;
            warningTextRect.anchorMin = new Vector2(0.02f, 0f);
            warningTextRect.anchorMax = new Vector2(0.98f, 1f);
            warningTextRect.offsetMin = Vector2.zero;
            warningTextRect.offsetMax = Vector2.zero;

            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);
        }

        private static Font GetBuiltinFont()
        {
            if (s_builtinFont != null)
            {
                return s_builtinFont;
            }

            s_builtinFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            if (s_builtinFont == null)
            {
                s_builtinFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            }

            return s_builtinFont;
        }

        private static void BindSystemReferences(
            GameObject mapPanel,
            GameObject diplomacyPanel,
            GameObject battlePanel,
            GameObject eventPanel,
            GameObject notificationPanel,
            GameObject alertPanel,
            GameObject intelPanel,
            GameObject actionPanel,
            GameObject consequencePanel,
            GameObject globalAlertPanel,
            GameObject resourcePanel,
            GameObject phaseIndicatorPanel,
            GameObject victoryProgressPanel)
        {
            MapPanelUI mapUi = Object.FindObjectOfType<MapPanelUI>();
            if (mapUi != null)
            {
                mapUi.MapPanel = mapPanel;
                mapUi.LatestMapText = FindText(mapPanel.transform, "LatestValue");
                mapUi.MapHistoryText = FindText(mapPanel.transform, "HistoryBackground/HistoryValue");
                EditorUtility.SetDirty(mapUi);
            }

            DiplomacyPanelUI diplomacyUi = Object.FindObjectOfType<DiplomacyPanelUI>();
            if (diplomacyUi != null)
            {
                diplomacyUi.DiplomacyPanel = diplomacyPanel;
                diplomacyUi.LatestDiplomacyText = FindText(diplomacyPanel.transform, "LatestValue");
                diplomacyUi.DiplomacyHistoryText = FindText(diplomacyPanel.transform, "HistoryBackground/HistoryValue");
                EditorUtility.SetDirty(diplomacyUi);
            }

            ActionLogUI actionLogUi = Object.FindObjectOfType<ActionLogUI>();
            if (actionLogUi != null)
            {
                actionLogUi.ActionLogPanel = battlePanel;
                actionLogUi.LatestEntryText = FindText(battlePanel.transform, "LatestValue");
                actionLogUi.HistoryText = FindText(battlePanel.transform, "HistoryBackground/HistoryValue");
                EditorUtility.SetDirty(actionLogUi);
            }

            EventPanelUI eventUi = Object.FindObjectOfType<EventPanelUI>();
            if (eventUi != null)
            {
                eventUi.EventPanel = eventPanel;
                eventUi.LatestEventText = FindText(eventPanel.transform, "LatestValue");
                eventUi.EventHistoryText = FindText(eventPanel.transform, "HistoryBackground/HistoryValue");
                EditorUtility.SetDirty(eventUi);
            }

            NotificationPanelUI notificationUi = Object.FindObjectOfType<NotificationPanelUI>();
            if (notificationUi != null)
            {
                notificationUi.NotificationPanel = notificationPanel;
                notificationUi.LatestNotificationText = FindText(notificationPanel.transform, "LatestValue");
                notificationUi.NotificationHistoryText = FindText(notificationPanel.transform, "HistoryBackground/HistoryValue");
                EditorUtility.SetDirty(notificationUi);
            }

            AlertPanelUI alertUi = Object.FindObjectOfType<AlertPanelUI>();
            if (alertUi != null)
            {
                alertUi.AlertPanel = alertPanel;
                alertUi.AlertText = FindText(alertPanel.transform, "AlertText");
                alertUi.AlertBackground = FindImage(alertPanel.transform, "AlertBackground");
                EditorUtility.SetDirty(alertUi);
            }

            IntelPanelUI intelUi = Object.FindObjectOfType<IntelPanelUI>();
            if (intelUi != null)
            {
                intelUi.IntelPanel = intelPanel;
                intelUi.LatestIntelText = FindText(intelPanel.transform, "LatestValue");
                intelUi.IntelHistoryText = FindText(intelPanel.transform, "HistoryBackground/HistoryValue");
                EditorUtility.SetDirty(intelUi);
            }

            ActionPanelUI actionUi = Object.FindObjectOfType<ActionPanelUI>();
            if (actionUi != null)
            {
                actionUi.ActionPanel = actionPanel;
                actionUi.LatestActionText = FindText(actionPanel.transform, "LatestValue");
                actionUi.ActionHistoryText = FindText(actionPanel.transform, "HistoryBackground/HistoryValue");
                EditorUtility.SetDirty(actionUi);
            }

            ConsequencePanelUI consequenceUi = Object.FindObjectOfType<ConsequencePanelUI>();
            if (consequenceUi != null)
            {
                consequenceUi.ConsequencePanel = consequencePanel;
                consequenceUi.ActiveConsequenceText = FindText(consequencePanel.transform, "HistoryBackground/HistoryValue");
                EditorUtility.SetDirty(consequenceUi);
            }

            GlobalAlertUI globalAlertUi = Object.FindObjectOfType<GlobalAlertUI>();
            if (globalAlertUi != null)
            {
                globalAlertUi.GlobalAlertPanel = globalAlertPanel;
                globalAlertUi.AlertText = FindText(globalAlertPanel.transform, "AlertText");
                globalAlertUi.AlertBackground = FindImage(globalAlertPanel.transform, "AlertBackground");
                EditorUtility.SetDirty(globalAlertUi);
            }

            UIManager uiManager = Object.FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.MapPanel = mapPanel;
                uiManager.DiplomacyPanel = diplomacyPanel;
                uiManager.ActionLogPanel = battlePanel;
                uiManager.EventPanel = eventPanel;
                uiManager.NotificationPanel = notificationPanel;
                uiManager.AlertPanel = alertPanel;
                uiManager.IntelPanel = intelPanel;
                uiManager.ActionPanel = actionPanel;
                uiManager.ConsequencePanel = consequencePanel;
                uiManager.GlobalAlertPanel = globalAlertPanel;
                uiManager.ResourcePanel = resourcePanel;
                uiManager.PhaseIndicatorPanel = phaseIndicatorPanel;
                uiManager.VictoryProgressPanel = victoryProgressPanel;
                uiManager.TurnText = FindHudText(resourcePanel.transform, "TurnText");
                uiManager.TimeText = FindHudText(resourcePanel.transform, "TimeText");
                uiManager.PhaseText = FindHudText(resourcePanel.transform, "PhaseText");
                uiManager.ActionPointsText = FindHudText(resourcePanel.transform, "ActionPointsText");
                uiManager.ResourceBarContainer = FindTransform(resourcePanel.transform, "ResourceBarContainer");
                uiManager.ResourceItemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(kResourceItemPrefabPath);
                EditorUtility.SetDirty(uiManager);
            }

            VictoryProgressUI victoryUi = EnsureVictoryProgressSystem();
            if (victoryUi != null)
            {
                victoryUi.VictoryProgressPanel = victoryProgressPanel;
                victoryUi.ProgressBarContainer = FindTransform(victoryProgressPanel.transform, "ProgressBarContainer");
                victoryUi.ProgressBarPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(kVictoryPathBarPrefabPath);
                victoryUi.DefeatWarningPanel = FindGameObject(victoryProgressPanel.transform, "DefeatWarningPanel");
                victoryUi.DefeatWarningText = FindText(victoryProgressPanel.transform, "DefeatWarningPanel/DefeatWarningText");
                EditorUtility.SetDirty(victoryUi);
            }

            PhaseIndicatorUI phaseUi = phaseIndicatorPanel != null ? phaseIndicatorPanel.GetComponent<PhaseIndicatorUI>() : null;
            if (phaseUi != null)
            {
                phaseUi.Initialize(phaseUi.PhaseButtons != null ? phaseUi.PhaseButtons.Length : 0);
                EditorUtility.SetDirty(phaseUi);
            }
        }

        private static void EnsureLaunchFlowController(Transform uiRoot)
        {
            LaunchFlowUIController controller = Object.FindObjectOfType<LaunchFlowUIController>();
            if (controller == null)
            {
                var host = new GameObject("LaunchFlowUIControllerHost");
                if (uiRoot != null)
                {
                    host.transform.SetParent(uiRoot, false);
                }

                controller = host.AddComponent<LaunchFlowUIController>();
            }
            else if (uiRoot != null && controller.transform.parent != uiRoot)
            {
                controller.transform.SetParent(uiRoot, false);
            }

            if (controller != null)
            {
                controller.RefreshBindings();
                EditorUtility.SetDirty(controller);
            }
        }

        private static void SeedPrototypeContent(
            GameObject mapPanel,
            GameObject diplomacyPanel,
            GameObject battlePanel,
            GameObject eventPanel,
            GameObject notificationPanel,
            GameObject alertPanel,
            GameObject intelPanel,
            GameObject actionPanel,
            GameObject consequencePanel,
            GameObject globalAlertPanel,
            GameObject resourcePanel,
            GameObject phaseIndicatorPanel,
            GameObject victoryProgressPanel)
        {
            SetPanelContent(
                mapPanel,
                "Prototype map loaded: 10 nodes, 16 curved routes.",
                "T01 Tehran secured\nT01 Hormuz holding\nT01 Baghdad contested");

            SetPanelContent(
                diplomacyPanel,
                "Ceasefire channel open. Pressure level moderate.",
                "T01 Ceasefire proposal pending\nT01 Sanction cooldown: 2 turns");

            SetPanelContent(
                battlePanel,
                "Frontline stable after last maneuver.",
                "T01 [BATTLE] Unit moved Tehran -> Basra\nT01 [REPORT] AI repositioned near Baghdad");

            SetPanelContent(
                eventPanel,
                "Regional tension rising after sanctions.",
                "T01 [EVENT] Energy transit risk updated\nT01 [EVENT] Coalition media response triggered");

            SetPanelContent(
                notificationPanel,
                "Protocol renewal window opened.",
                "T01 [INFO] C2.Protocol.Renew available\nT01 [WARNING] B2.SecondaryBlockade pressure rising");

            SetAlertPanelContent(
                alertPanel,
                "[CRITICAL] [B2.NavalBlockade] Strait throughput near zero.");

            SetPanelContent(
                intelPanel,
                "Signal intercept indicates route diversion.",
                "T01 [INFO] MapIntel: Basra route disrupted\nT01 [WARNING] D1.SpecialForces.Intel exposure risk");

            SetPanelContent(
                actionPanel,
                "Action queue updated for current phase.",
                "T01/P1 [INFO] C2.EnergyTransit.Sign executed\nT01/P2 [WARNING] D1.TotalWar denied (AP shortage)");

            SetPanelContent(
                consequencePanel,
                "Active consequences updated.",
                "[B2.NavalBlockade.Upgrade] Throughput reduced (3T, Irreversible)\n[D1.ChokepointThreat.Start] Route efficiency down (2T, Reversible)");

            SetAlertPanelContent(
                globalAlertPanel,
                "[WARNING] Global pressure index increased by coalition response.");

            SetHudPanelContent(
                resourcePanel,
                "Turn 1",
                "2028 H1",
                "Diplomacy",
                "AP: 11 (Phase 2, Universal 2)");

            SetPhaseIndicatorContent(phaseIndicatorPanel, 0);

            SetPanelContent(
                victoryProgressPanel,
                "Victory paths pending evaluation.",
                "Statecraft 35%\nEconomy 41%\nMilitary 28%\nLegitimacy 39%");
        }

        private static void SetPanelContent(GameObject panel, string latest, string history)
        {
            if (panel == null)
            {
                return;
            }

            Text latestText = FindText(panel.transform, "LatestValue");
            if (latestText != null)
            {
                latestText.text = latest;
            }

            Text historyText = FindText(panel.transform, "HistoryBackground/HistoryValue");
            if (historyText != null)
            {
                historyText.text = history;
            }
        }

        private static void SetAlertPanelContent(GameObject panel, string message)
        {
            if (panel == null)
            {
                return;
            }

            Text alertText = FindText(panel.transform, "AlertText");
            if (alertText != null)
            {
                alertText.text = message;
            }
        }

        private static void SetHudPanelContent(GameObject panel, string turn, string time, string phase, string ap)
        {
            if (panel == null)
            {
                return;
            }

            Text turnText = FindHudText(panel.transform, "TurnText");
            if (turnText != null) turnText.text = turn;

            Text timeText = FindHudText(panel.transform, "TimeText");
            if (timeText != null) timeText.text = time;

            Text phaseText = FindHudText(panel.transform, "PhaseText");
            if (phaseText != null) phaseText.text = phase;

            Text apText = FindHudText(panel.transform, "ActionPointsText");
            if (apText != null) apText.text = ap;
        }

        private static Text FindHudText(Transform root, string textName)
        {
            Text text = FindText(root, textName);
            if (text != null)
            {
                return text;
            }

            return FindText(root, "Header/" + textName);
        }

        private static void SetPhaseIndicatorContent(GameObject phaseIndicatorPanel, int currentPhaseIndex)
        {
            if (phaseIndicatorPanel == null)
            {
                return;
            }

            PhaseIndicatorUI phaseIndicator = phaseIndicatorPanel.GetComponent<PhaseIndicatorUI>();
            if (phaseIndicator != null)
            {
                phaseIndicator.SetCurrentPhase(currentPhaseIndex);
            }
        }

        private static Text FindText(Transform root, string relativePath)
        {
            Transform target = root.Find(relativePath);
            if (target == null)
            {
                return null;
            }

            return target.GetComponent<Text>();
        }

        private static Transform FindTransform(Transform root, string relativePath)
        {
            if (root == null)
            {
                return null;
            }

            return root.Find(relativePath);
        }

        private static GameObject FindGameObject(Transform root, string relativePath)
        {
            Transform target = FindTransform(root, relativePath);
            return target != null ? target.gameObject : null;
        }

        private static VictoryProgressUI EnsureVictoryProgressSystem()
        {
            VictoryProgressUI existing = Object.FindObjectOfType<VictoryProgressUI>();
            if (existing != null)
            {
                return existing;
            }

            GameObject gameManagerGo = GameObject.Find("GameManager");
            if (gameManagerGo == null)
            {
                return null;
            }

            Transform uiContainer = gameManagerGo.transform.Find("UI");
            if (uiContainer == null)
            {
                uiContainer = gameManagerGo.transform;
            }

            var go = new GameObject("VictoryProgressUI");
            go.transform.SetParent(uiContainer, false);
            return go.AddComponent<VictoryProgressUI>();
        }

        private static Image FindImage(Transform root, string relativePath)
        {
            Transform target = root.Find(relativePath);
            if (target == null)
            {
                return null;
            }

            return target.GetComponent<Image>();
        }

        private static void AnchorPanel(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition)
        {
            if (rect == null)
            {
                return;
            }

            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.pivot = new Vector2(anchorMin.x, anchorMin.y);
            rect.anchoredPosition = anchoredPosition;
        }

        private static void ApplySurfaceLayout(
            GameObject mapPanel,
            GameObject diplomacyPanel,
            GameObject battlePanel,
            GameObject eventPanel,
            GameObject notificationPanel,
            GameObject alertPanel,
            GameObject intelPanel,
            GameObject actionPanel,
            GameObject consequencePanel,
            GameObject globalAlertPanel,
            GameObject resourcePanel,
            GameObject phaseIndicatorPanel,
            GameObject victoryProgressPanel)
        {
            AnchorPanel(mapPanel.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(20f, -210f));
            AnchorPanel(diplomacyPanel.GetComponent<RectTransform>(), new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(-20f, -210f));
            AnchorPanel(battlePanel.GetComponent<RectTransform>(), new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-20f, 20f));
            AnchorPanel(eventPanel.GetComponent<RectTransform>(), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(20f, 20f));
            AnchorPanel(notificationPanel.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(20f, -560f));
            AnchorPanel(intelPanel.GetComponent<RectTransform>(), new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(-20f, -560f));
            AnchorPanel(actionPanel.GetComponent<RectTransform>(), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(-210f, 20f));
            AnchorPanel(consequencePanel.GetComponent<RectTransform>(), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(210f, 20f));
            AnchorPanel(resourcePanel.GetComponent<RectTransform>(), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -12f));
            AnchorPanel(phaseIndicatorPanel.GetComponent<RectTransform>(), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -154f));
            AnchorPanel(globalAlertPanel.GetComponent<RectTransform>(), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -220f));
            AnchorPanel(alertPanel.GetComponent<RectTransform>(), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -336f));
            AnchorPanel(victoryProgressPanel.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero);
        }

        private static void ApplyPanelOrder(
            GameObject mapPanel,
            GameObject diplomacyPanel,
            GameObject battlePanel,
            GameObject eventPanel,
            GameObject notificationPanel,
            GameObject intelPanel,
            GameObject actionPanel,
            GameObject consequencePanel,
            GameObject resourcePanel,
            GameObject phaseIndicatorPanel,
            GameObject globalAlertPanel,
            GameObject alertPanel,
            GameObject victoryProgressPanel)
        {
            mapPanel.transform.SetAsFirstSibling();
            diplomacyPanel.transform.SetSiblingIndex(1);
            battlePanel.transform.SetSiblingIndex(2);
            eventPanel.transform.SetSiblingIndex(3);
            notificationPanel.transform.SetSiblingIndex(4);
            intelPanel.transform.SetSiblingIndex(5);
            actionPanel.transform.SetSiblingIndex(6);
            consequencePanel.transform.SetSiblingIndex(7);
            resourcePanel.transform.SetSiblingIndex(8);
            phaseIndicatorPanel.transform.SetSiblingIndex(9);
            globalAlertPanel.transform.SetSiblingIndex(10);
            alertPanel.transform.SetSiblingIndex(11);
            victoryProgressPanel.transform.SetAsLastSibling();
        }

        private static void ApplyDefaultVisibility(
            bool seedPrototypeContent,
            GameObject notificationPanel,
            GameObject intelPanel,
            GameObject actionPanel,
            GameObject consequencePanel,
            GameObject globalAlertPanel,
            GameObject alertPanel,
            GameObject victoryProgressPanel)
        {
            bool showDebugPanels = seedPrototypeContent;
            notificationPanel.SetActive(showDebugPanels);
            intelPanel.SetActive(showDebugPanels);
            actionPanel.SetActive(showDebugPanels);
            consequencePanel.SetActive(showDebugPanels);
            globalAlertPanel.SetActive(showDebugPanels);
            alertPanel.SetActive(showDebugPanels);
            victoryProgressPanel.SetActive(false);
        }

        private static void ClearChildren(Transform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(parent.GetChild(i).gameObject);
            }
        }

        private static GameObject InstantiatePrefab(string prefabPath, Transform parent)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab == null)
            {
                Debug.LogError($"[P0HighFrequencyUiSetup] Prefab missing: {prefabPath}");
                return null;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            if (instance == null)
            {
                Debug.LogError($"[P0HighFrequencyUiSetup] Failed to instantiate prefab: {prefabPath}");
                return null;
            }

            instance.transform.SetParent(parent, false);
            return instance;
        }

        private static Canvas GetOrCreateCanvas()
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                return canvas;
            }

            var go = new GameObject("GameCanvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = go.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = go.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;
            return canvas;
        }

        private static void EnsureMainCamera()
        {
            Camera camera = Camera.main;
            if (camera == null)
            {
                GameObject cameraGo = GameObject.Find("Main Camera");
                if (cameraGo == null)
                {
                    cameraGo = new GameObject("Main Camera");
                }

                camera = cameraGo.GetComponent<Camera>();
                if (camera == null)
                {
                    camera = cameraGo.AddComponent<Camera>();
                }
            }

            if (camera == null)
            {
                return;
            }

            camera.enabled = true;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.05f, 0.07f, 0.1f, 1f);
            camera.orthographic = true;
            camera.orthographicSize = 5.4f;
            camera.nearClipPlane = 0.1f;
            camera.farClipPlane = 1000f;
            camera.depth = -10f;
            camera.cullingMask = ~0;
            camera.targetDisplay = 0;
            camera.transform.position = new Vector3(0f, 0f, -10f);
            camera.transform.rotation = Quaternion.identity;

            if (camera.gameObject.GetComponent<AudioListener>() == null)
            {
                camera.gameObject.AddComponent<AudioListener>();
            }
        }

        private static void EnsureCanvasBackdrop(Canvas canvas, Sprite backdropSprite)
        {
            if (canvas == null || backdropSprite == null)
            {
                return;
            }

            Transform existing = canvas.transform.Find("P0MapBackdrop");
            GameObject backdropGo = existing != null
                ? existing.gameObject
                : new GameObject("P0MapBackdrop", typeof(RectTransform), typeof(Image));

            if (backdropGo.transform.parent != canvas.transform)
            {
                backdropGo.transform.SetParent(canvas.transform, false);
            }

            RectTransform rect = backdropGo.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.pivot = new Vector2(0.5f, 0.5f);

            Image image = backdropGo.GetComponent<Image>();
            image.sprite = backdropSprite;
            image.type = Image.Type.Simple;
            image.preserveAspect = false;
            image.raycastTarget = false;
            image.color = new Color(1f, 1f, 1f, 0.88f);

            backdropGo.transform.SetAsFirstSibling();
        }

        private static void EnsureEventSystem()
        {
            if (Object.FindObjectOfType<EventSystem>() != null)
            {
                return;
            }

            _ = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        private static Sprite LoadSpriteFromSheet(string texturePath, string spriteName)
        {
            Object[] subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(texturePath);
            for (int i = 0; i < subAssets.Length; i++)
            {
                Sprite sprite = subAssets[i] as Sprite;
                if (sprite != null && sprite.name == spriteName)
                {
                    return sprite;
                }
            }

            Debug.LogWarning($"[P0HighFrequencyUiSetup] Sprite not found: {spriteName} in {texturePath}");
            return null;
        }

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
            {
                return;
            }

            string[] parts = path.Split('/');
            string current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }

                current = next;
            }
        }
    }
}
#endif

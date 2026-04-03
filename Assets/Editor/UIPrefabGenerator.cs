#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

namespace EventideAge.Editor
{
    public class UIPrefabGenerator : EditorWindow
    {
        [MenuItem("EventideAge/Generate UI Prefabs")]
        public static void GenerateUIPrefabs()
        {
            CreateResourceItemPrefab();
            CreateVictoryPathBarPrefab();
            CreateTechListItemPrefab();
            CreateRegionStatusPrefab();
            CreateUIPanelPrefabs();
            
            EditorUtility.DisplayDialog("UI Prefabs", "All UI prefabs generated successfully!", "OK");
        }

        private static void CreateResourceItemPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/ResourceItemUI.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("ResourceItem");
            go.AddComponent<RectTransform>();
            var image = go.AddComponent<Image>();
            image.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5;
            layout.padding = new RectOffset(5, 5, 3, 3);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            var iconObj = new GameObject("Icon");
            iconObj.transform.SetParent(go.transform);
            var iconImage = iconObj.AddComponent<Image>();
            iconImage.color = Color.white;
            iconObj.GetComponent<RectTransform>().sizeDelta = new Vector2(24, 24);

            var textObj = new GameObject("NameText");
            textObj.transform.SetParent(go.transform);
            var text = textObj.AddComponent<Text>();
            text.text = "Resource";
            text.fontSize = 14;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleLeft;
            textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 20);

            var fillObj = new GameObject("Fill");
            fillObj.transform.SetParent(go.transform);
            var fillImage = fillObj.AddComponent<Image>();
            fillImage.color = new Color(0.3f, 0.7f, 0.3f, 1f);
            var fillRect = fillObj.GetComponent<RectTransform>();
            fillRect.sizeDelta = new Vector2(60, 10);

            var sliderObj = new GameObject("Slider");
            sliderObj.transform.SetParent(go.transform);
            var slider = sliderObj.AddComponent<Slider>();
            slider.minValue = 0;
            slider.maxValue = 100;
            slider.value = 75;
            var sliderRect = sliderObj.GetComponent<RectTransform>();
            sliderRect.sizeDelta = new Vector2(60, 10);

            var valueTextObj = new GameObject("ValueText");
            valueTextObj.transform.SetParent(go.transform);
            var valueText = valueTextObj.AddComponent<Text>();
            valueText.text = "100/100";
            valueText.fontSize = 12;
            valueText.color = new Color(0.9f, 0.9f, 0.9f, 1f);
            valueText.alignment = TextAnchor.MiddleRight;
            valueTextObj.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 20);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 30;
            layoutElement.preferredHeight = 30;

            go.GetComponent<RectTransform>().sizeDelta = new Vector2(280, 30);

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created ResourceItemUI prefab at {prefabPath}");
        }

        private static void CreateVictoryPathBarPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/VictoryPathBar.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("VictoryPathBar");
            go.AddComponent<RectTransform>();

            var bgObj = new GameObject("Background");
            bgObj.transform.SetParent(go.transform);
            var bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.15f, 0.15f, 0.15f, 0.9f);
            bgObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.3f);
            bgObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.7f);
            bgObj.GetComponent<RectTransform>().offsetMin = new Vector2(5, 0);
            bgObj.GetComponent<RectTransform>().offsetMax = new Vector2(-5, 0);

            var fillObj = new GameObject("Fill");
            fillObj.transform.SetParent(go.transform);
            var fillImage = fillObj.AddComponent<Image>();
            fillImage.color = new Color(0.2f, 0.6f, 0.9f, 1f);
            var fillRect = fillObj.GetComponent<RectTransform>();
            fillRect.anchorMin = new Vector2(0, 0.3f);
            fillRect.anchorMax = new Vector2(0, 0.7f);
            fillRect.offsetMin = new Vector2(5, 0);
            fillRect.offsetMax = new Vector2(-5, 0);

            var nameObj = new GameObject("PathName");
            nameObj.transform.SetParent(go.transform);
            var nameText = nameObj.AddComponent<Text>();
            nameText.text = "Victory Path";
            nameText.fontSize = 12;
            nameText.color = Color.white;
            nameText.alignment = TextAnchor.MiddleLeft;
            nameObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.7f);
            nameObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            nameObj.GetComponent<RectTransform>().offsetMin = new Vector2(5, 0);
            nameObj.GetComponent<RectTransform>().offsetMax = new Vector2(-5, -2);

            var percentObj = new GameObject("PercentText");
            percentObj.transform.SetParent(go.transform);
            var percentText = percentObj.AddComponent<Text>();
            percentText.text = "0%";
            percentText.fontSize = 11;
            percentText.color = new Color(0.8f, 0.8f, 0.8f, 1f);
            percentText.alignment = TextAnchor.UpperCenter;
            percentObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            percentObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.3f);
            percentObj.GetComponent<RectTransform>().offsetMin = new Vector2(5, 0);
            percentObj.GetComponent<RectTransform>().offsetMax = new Vector2(-5, -2);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 35;
            layoutElement.preferredHeight = 35;

            go.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 35);

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created VictoryPathBar prefab at {prefabPath}");
        }

        private static void CreateTechListItemPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/TechListItem.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("TechListItem");
            go.AddComponent<RectTransform>();

            var bgObj = new GameObject("Background");
            bgObj.transform.SetParent(go.transform);
            var bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.2f, 0.2f, 0.25f, 0.9f);
            bgObj.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50);

            var nameObj = new GameObject("TechName");
            nameObj.transform.SetParent(go.transform);
            var nameText = nameObj.AddComponent<Text>();
            nameText.text = "Technology Name";
            nameText.fontSize = 14;
            nameText.color = Color.white;
            nameText.alignment = TextAnchor.MiddleLeft;
            nameObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.6f);
            nameObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.7f, 1);
            nameObj.GetComponent<RectTransform>().offsetMin = new Vector2(10, 0);
            nameObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, -5);

            var descObj = new GameObject("Description");
            descObj.transform.SetParent(go.transform);
            var descText = descObj.AddComponent<Text>();
            descText.text = "Tech description";
            descText.fontSize = 11;
            descText.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            descText.alignment = TextAnchor.UpperLeft;
            descObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.1f);
            descObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.7f, 0.55f);
            descObj.GetComponent<RectTransform>().offsetMin = new Vector2(10, 0);
            descObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, -2);

            var costObj = new GameObject("Cost");
            costObj.transform.SetParent(go.transform);
            var costText = costObj.AddComponent<Text>();
            costText.text = "Cost: 100";
            costText.fontSize = 12;
            costText.color = new Color(0.9f, 0.7f, 0.3f, 1f);
            costText.alignment = TextAnchor.MiddleRight;
            costObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.7f, 0.6f);
            costObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            costObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            costObj.GetComponent<RectTransform>().offsetMax = new Vector2(-10, -5);

            var progressObj = new GameObject("Progress");
            progressObj.transform.SetParent(go.transform);
            var progressImage = progressObj.AddComponent<Image>();
            progressImage.color = new Color(0.3f, 0.5f, 0.8f, 1f);
            progressObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.7f, 0.1f);
            progressObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
            progressObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            progressObj.GetComponent<RectTransform>().offsetMax = new Vector2(-10, -2);

            var buttonObj = new GameObject("ResearchButton");
            buttonObj.transform.SetParent(go.transform);
            var buttonImage = buttonObj.AddComponent<Image>();
            buttonImage.color = new Color(0.3f, 0.5f, 0.3f, 1f);
            var button = buttonObj.AddComponent<Button>();
            button.transition = Selectable.Transition.ColorTint;
            button.targetGraphic = buttonImage;
            button.colors = new ColorBlock { normalColor = new Color(0.3f, 0.5f, 0.3f, 1f), highlightedColor = new Color(0.4f, 0.6f, 0.4f, 1f), pressedColor = new Color(0.2f, 0.4f, 0.2f, 1f) };
            buttonObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.8f, 0.15f);
            buttonObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.98f, 0.85f);
            buttonObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            buttonObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var buttonTextObj = new GameObject("ButtonText");
            buttonTextObj.transform.SetParent(buttonObj.transform);
            var buttonText = buttonTextObj.AddComponent<Text>();
            buttonText.text = "研究";
            buttonText.fontSize = 11;
            buttonText.color = Color.white;
            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonTextObj.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            buttonTextObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            buttonTextObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);

            go.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 50;
            layoutElement.preferredHeight = 50;
            layoutElement.flexibleWidth = 1;

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created TechListItem prefab at {prefabPath}");
        }

        private static void CreateRegionStatusPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/RegionStatusItem.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("RegionStatusItem");
            go.AddComponent<RectTransform>();

            var bgObj = new GameObject("Background");
            bgObj.transform.SetParent(go.transform);
            var bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.18f, 0.18f, 0.22f, 0.95f);
            bgObj.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60);

            var nameObj = new GameObject("RegionName");
            nameObj.transform.SetParent(go.transform);
            var nameText = nameObj.AddComponent<Text>();
            nameText.text = "Region";
            nameText.fontSize = 13;
            nameText.color = Color.white;
            nameText.alignment = TextAnchor.MiddleLeft;
            nameObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.65f);
            nameObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            nameObj.GetComponent<RectTransform>().offsetMin = new Vector2(8, 0);
            nameObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, -3);

            var stabilityLabelObj = new GameObject("StabilityLabel");
            stabilityLabelObj.transform.SetParent(go.transform);
            var stabilityLabel = stabilityLabelObj.AddComponent<Text>();
            stabilityLabel.text = "稳定:";
            stabilityLabel.fontSize = 11;
            stabilityLabel.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            stabilityLabel.alignment = TextAnchor.MiddleLeft;
            stabilityLabelObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.35f);
            stabilityLabelObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.25f, 0.6f);
            stabilityLabelObj.GetComponent<RectTransform>().offsetMin = new Vector2(8, 0);
            stabilityLabelObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, -2);

            var stabilityValueObj = new GameObject("StabilityValue");
            stabilityValueObj.transform.SetParent(go.transform);
            var stabilityValue = stabilityValueObj.AddComponent<Text>();
            stabilityValue.text = "85";
            stabilityValue.fontSize = 11;
            stabilityValue.color = new Color(0.3f, 0.8f, 0.3f, 1f);
            stabilityValue.alignment = TextAnchor.MiddleLeft;
            stabilityValueObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.25f, 0.35f);
            stabilityValueObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.6f);
            stabilityValueObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            stabilityValueObj.GetComponent<RectTransform>().offsetMax = new Vector2(-5, -2);

            var governanceLabelObj = new GameObject("GovernanceLabel");
            governanceLabelObj.transform.SetParent(go.transform);
            var governanceLabel = governanceLabelObj.AddComponent<Text>();
            governanceLabel.text = "治理:";
            governanceLabel.fontSize = 11;
            governanceLabel.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            governanceLabel.alignment = TextAnchor.MiddleLeft;
            governanceLabelObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.05f);
            governanceLabelObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.25f, 0.3f);
            governanceLabelObj.GetComponent<RectTransform>().offsetMin = new Vector2(8, 0);
            governanceLabelObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, -2);

            var governanceValueObj = new GameObject("GovernanceValue");
            governanceValueObj.transform.SetParent(go.transform);
            var governanceValue = governanceValueObj.AddComponent<Text>();
            governanceValue.text = "70";
            governanceValue.fontSize = 11;
            governanceValue.color = new Color(0.9f, 0.7f, 0.2f, 1f);
            governanceValue.alignment = TextAnchor.MiddleLeft;
            governanceValueObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.25f, 0.05f);
            governanceValueObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.3f);
            governanceValueObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            governanceValueObj.GetComponent<RectTransform>().offsetMax = new Vector2(-5, -2);

            var controlObj = new GameObject("ControlBar");
            controlObj.transform.SetParent(go.transform);
            var controlImage = controlObj.AddComponent<Image>();
            controlImage.color = new Color(0.3f, 0.3f, 0.35f, 1f);
            controlObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.52f, 0.2f);
            controlObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.98f, 0.8f);
            controlObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            controlObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var controllerObj = new GameObject("ControllerText");
            controllerObj.transform.SetParent(go.transform);
            var controllerText = controllerObj.AddComponent<Text>();
            controllerText.text = "Vashid";
            controllerText.fontSize = 10;
            controllerText.color = new Color(0.5f, 0.8f, 0.5f, 1f);
            controllerText.alignment = TextAnchor.MiddleCenter;
            controllerObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.52f, 0.05f);
            controllerObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.98f, 0.2f);
            controllerObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            controllerObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, -2);

            go.GetComponent<RectTransform>().sizeDelta = new Vector2(280, 60);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 60;
            layoutElement.preferredHeight = 60;
            layoutElement.flexibleWidth = 1;

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created RegionStatusItem prefab at {prefabPath}");
        }

        private static void CreateUIPanelPrefabs()
        {
            CreateMainMenuPanelPrefab();
            CreateGameHUDPanelPrefab();
            CreateVictoryProgressPanelPrefab();
            CreateTechTreePanelPrefab();
            CreateNuclearStatusPanelPrefab();
            CreateProxyAffairsPanelPrefab();
            CreatePhaseIndicatorPanelPrefab();
        }

        private static void CreateMainMenuPanelPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/MainMenuPanel.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("MainMenuPanel");
            go.AddComponent<RectTransform>();
            go.AddComponent<CanvasGroup>();

            var bgObj = new GameObject("Background");
            bgObj.transform.SetParent(go.transform);
            var bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.05f, 0.05f, 0.1f, 0.98f);
            bgObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            bgObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            bgObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            bgObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var titleObj = new GameObject("Title");
            titleObj.transform.SetParent(go.transform);
            var titleText = titleObj.AddComponent<Text>();
            titleText.text = "瓦希德帝国";
            titleText.fontSize = 48;
            titleText.color = new Color(0.9f, 0.75f, 0.3f, 1f);
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.fontStyle = FontStyle.Bold;
            titleObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.7f);
            titleObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.9f);
            titleObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            titleObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var subtitleObj = new GameObject("Subtitle");
            subtitleObj.transform.SetParent(go.transform);
            var subtitleText = subtitleObj.AddComponent<Text>();
            subtitleText.text = "Eventide Age - 4X Strategy";
            subtitleText.fontSize = 18;
            subtitleText.color = new Color(0.6f, 0.6f, 0.65f, 1f);
            subtitleText.alignment = TextAnchor.MiddleCenter;
            subtitleObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.6f);
            subtitleObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.68f);
            subtitleObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            subtitleObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var buttonContainer = new GameObject("ButtonContainer");
            buttonContainer.transform.SetParent(go.transform);
            var vLayout = buttonContainer.AddComponent<VerticalLayoutGroup>();
            vLayout.spacing = 15;
            vLayout.padding = new RectOffset(0, 0, 20, 20);
            vLayout.childAlignment = TextAnchor.MiddleCenter;
            vLayout.childControlWidth = true;
            vLayout.childControlHeight = true;
            vLayout.childForceExpandWidth = false;
            vLayout.childForceExpandHeight = false;
            buttonContainer.GetComponent<RectTransform>().anchorMin = new Vector2(0.35f, 0.25f);
            buttonContainer.GetComponent<RectTransform>().anchorMax = new Vector2(0.65f, 0.55f);
            buttonContainer.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            buttonContainer.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            CreateMenuButton(buttonContainer.transform, "NewGameButton", "新游戏", () => Debug.Log("New Game"));
            CreateMenuButton(buttonContainer.transform, "LoadGameButton", "加载游戏", () => Debug.Log("Load Game"));
            CreateMenuButton(buttonContainer.transform, "SettingsButton", "设置", () => Debug.Log("Settings"));
            CreateMenuButton(buttonContainer.transform, "ExitButton", "退出", () => Debug.Log("Exit"));

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created MainMenuPanel prefab at {prefabPath}");
        }

        private static void CreateMenuButton(Transform parent, string name, string text, UnityEngine.Events.UnityAction action)
        {
            var btnObj = new GameObject(name);
            btnObj.transform.SetParent(parent);

            var btn = btnObj.AddComponent<Button>();
            btn.transition = Selectable.Transition.ColorTint;

            var bgImg = btnObj.AddComponent<Image>();
            bgImg.color = new Color(0.2f, 0.22f, 0.28f, 0.9f);
            btn.targetGraphic = bgImg;
            btn.colors = new ColorBlock
            {
                normalColor = new Color(0.2f, 0.22f, 0.28f, 0.9f),
                highlightedColor = new Color(0.3f, 0.35f, 0.45f, 1f),
                pressedColor = new Color(0.15f, 0.18f, 0.25f, 1f),
                disabledColor = new Color(0.3f, 0.3f, 0.3f, 0.5f),
                colorMultiplier = 1f
            };

            var txtObj = new GameObject("Text");
            txtObj.transform.SetParent(btnObj.transform);
            var txt = txtObj.AddComponent<Text>();
            txt.text = text;
            txt.fontSize = 16;
            txt.color = Color.white;
            txt.alignment = TextAnchor.MiddleCenter;
            txtObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            txtObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            txtObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            txtObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            btnObj.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 45);
        }

        private static void CreateGameHUDPanelPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/GameHUDPanel.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("GameHUDPanel");
            go.AddComponent<RectTransform>();

            var topBar = new GameObject("TopBar");
            topBar.transform.SetParent(go.transform);
            var topBg = topBar.AddComponent<Image>();
            topBg.color = new Color(0.1f, 0.1f, 0.15f, 0.9f);
            topBar.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.9f);
            topBar.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            topBar.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            topBar.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var turnTextObj = new GameObject("TurnText");
            turnTextObj.transform.SetParent(topBar.transform);
            var turnText = turnTextObj.AddComponent<Text>();
            turnText.text = "回合: 1";
            turnText.fontSize = 16;
            turnText.color = Color.white;
            turnText.alignment = TextAnchor.MiddleLeft;
            turnTextObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            turnTextObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            turnTextObj.GetComponent<RectTransform>().offsetMin = new Vector2(15, 0);
            turnTextObj.GetComponent<RectTransform>().offsetMax = new Vector2(150, 0);

            var timeTextObj = new GameObject("TimeText");
            timeTextObj.transform.SetParent(topBar.transform);
            var timeText = timeTextObj.AddComponent<Text>();
            timeText.text = "2028.0";
            timeText.fontSize = 16;
            timeText.color = new Color(0.8f, 0.8f, 0.6f, 1f);
            timeText.alignment = TextAnchor.MiddleLeft;
            timeTextObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            timeTextObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            timeTextObj.GetComponent<RectTransform>().offsetMin = new Vector2(160, 0);
            timeTextObj.GetComponent<RectTransform>().offsetMax = new Vector2(280, 0);

            var phaseTextObj = new GameObject("PhaseText");
            phaseTextObj.transform.SetParent(topBar.transform);
            var phaseText = phaseTextObj.AddComponent<Text>();
            phaseText.text = "外交阶段";
            phaseText.fontSize = 18;
            phaseText.color = new Color(0.4f, 0.7f, 0.9f, 1f);
            phaseText.alignment = TextAnchor.MiddleCenter;
            phaseTextObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.3f, 0);
            phaseTextObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.7f, 1);
            phaseTextObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            phaseTextObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var apTextObj = new GameObject("APText");
            apTextObj.transform.SetParent(topBar.transform);
            var apText = apTextObj.AddComponent<Text>();
            apText.text = "行动点: 11/11";
            apText.fontSize = 16;
            apText.color = new Color(0.6f, 0.9f, 0.6f, 1f);
            apText.alignment = TextAnchor.MiddleRight;
            apTextObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.85f, 0);
            apTextObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            apTextObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            apTextObj.GetComponent<RectTransform>().offsetMax = new Vector2(-15, 0);

            var bottomBar = new GameObject("BottomBar");
            bottomBar.transform.SetParent(go.transform);
            var bottomBg = bottomBar.AddComponent<Image>();
            bottomBg.color = new Color(0.08f, 0.08f, 0.12f, 0.95f);
            bottomBar.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            bottomBar.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.15f);
            bottomBar.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            bottomBar.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var resourceContainer = new GameObject("ResourceBarContainer");
            resourceContainer.transform.SetParent(bottomBar.transform);
            var hLayout = resourceContainer.AddComponent<HorizontalLayoutGroup>();
            hLayout.spacing = 10;
            hLayout.padding = new RectOffset(10, 10, 5, 5);
            hLayout.childAlignment = TextAnchor.MiddleLeft;
            hLayout.childControlWidth = false;
            hLayout.childControlHeight = true;
            hLayout.childForceExpandWidth = false;
            hLayout.childForceExpandHeight = false;
            resourceContainer.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            resourceContainer.GetComponent<RectTransform>().anchorMax = new Vector2(0.7f, 1);
            resourceContainer.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            resourceContainer.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var actionContainer = new GameObject("ActionBarContainer");
            actionContainer.transform.SetParent(bottomBar.transform);
            actionContainer.GetComponent<RectTransform>().anchorMin = new Vector2(0.75f, 0.1f);
            actionContainer.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.9f);
            actionContainer.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            actionContainer.GetComponent<RectTransform>().offsetMax = new Vector2(-10, 0);

            go.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            go.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created GameHUDPanel prefab at {prefabPath}");
        }

        private static void CreateVictoryProgressPanelPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/VictoryProgressPanel.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("VictoryProgressPanel");
            go.AddComponent<RectTransform>();
            go.AddComponent<CanvasGroup>();

            var bgObj = new GameObject("Background");
            bgObj.transform.SetParent(go.transform);
            var bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.1f, 0.12f, 0.18f, 0.95f);
            bgObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.7f, 0.3f);
            bgObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.85f);
            bgObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            bgObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var titleObj = new GameObject("Title");
            titleObj.transform.SetParent(go.transform);
            var titleText = titleObj.AddComponent<Text>();
            titleText.text = "胜利进度";
            titleText.fontSize = 14;
            titleText.color = new Color(0.9f, 0.85f, 0.5f, 1f);
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.fontStyle = FontStyle.Bold;
            titleObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.7f, 0.85f);
            titleObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.95f);
            titleObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            titleObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var container = new GameObject("ProgressBarContainer");
            container.transform.SetParent(go.transform);
            var vLayout = container.AddComponent<VerticalLayoutGroup>();
            vLayout.spacing = 5;
            vLayout.padding = new RectOffset(8, 8, 5, 8);
            vLayout.childAlignment = TextAnchor.UpperCenter;
            vLayout.childControlWidth = true;
            vLayout.childControlHeight = true;
            vLayout.childForceExpandWidth = true;
            vLayout.childForceExpandHeight = false;
            container.GetComponent<RectTransform>().anchorMin = new Vector2(0.7f, 0.3f);
            container.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.83f);
            container.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            container.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var contentFitter = container.AddComponent<ContentSizeFitter>();
            contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created VictoryProgressPanel prefab at {prefabPath}");
        }

        private static void CreateTechTreePanelPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/TechTreePanel.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("TechTreePanel");
            go.AddComponent<RectTransform>();
            go.AddComponent<CanvasGroup>();

            var bgObj = new GameObject("Background");
            bgObj.transform.SetParent(go.transform);
            var bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.08f, 0.1f, 0.15f, 0.97f);
            bgObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.15f, 0.15f);
            bgObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.65f, 0.85f);
            bgObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            bgObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var titleObj = new GameObject("Title");
            titleObj.transform.SetParent(go.transform);
            var titleText = titleObj.AddComponent<Text>();
            titleText.text = "军事科技";
            titleText.fontSize = 18;
            titleText.color = new Color(0.7f, 0.85f, 0.9f, 1f);
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.fontStyle = FontStyle.Bold;
            titleObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.15f, 0.82f);
            titleObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.65f, 0.92f);
            titleObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            titleObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var currentResearchObj = new GameObject("CurrentResearch");
            currentResearchObj.transform.SetParent(go.transform);
            var currentResearchText = currentResearchObj.AddComponent<Text>();
            currentResearchText.text = "当前研究: 无";
            currentResearchText.fontSize = 12;
            currentResearchText.color = new Color(0.6f, 0.8f, 0.6f, 1f);
            currentResearchText.alignment = TextAnchor.MiddleLeft;
            currentResearchObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.16f, 0.73f);
            currentResearchObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.8f);
            currentResearchObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            currentResearchObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var progressObj = new GameObject("ProgressBar");
            progressObj.transform.SetParent(go.transform);
            var progressBg = progressObj.AddComponent<Image>();
            progressBg.color = new Color(0.2f, 0.2f, 0.25f, 1f);
            progressObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.16f, 0.67f);
            progressObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.64f, 0.72f);
            progressObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            progressObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var techListContainer = new GameObject("TechListContainer");
            techListContainer.transform.SetParent(go.transform);
            var scrollView = techListContainer.AddComponent<ScrollRect>();
            scrollView.horizontal = false;
            scrollView.vertical = true;
            scrollView.movementType = ScrollRect.MovementType.Clamped;
            scrollView.scrollSensitivity = 30f;
            techListContainer.GetComponent<RectTransform>().anchorMin = new Vector2(0.15f, 0.15f);
            techListContainer.GetComponent<RectTransform>().anchorMax = new Vector2(0.65f, 0.65f);
            techListContainer.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            techListContainer.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var viewportObj = new GameObject("Viewport");
            viewportObj.transform.SetParent(techListContainer.transform);
            var viewportMask = viewportObj.AddComponent<Mask>();
            viewportMask.showMaskGraphic = false;
            var viewportImage = viewportObj.AddComponent<Image>();
            viewportImage.color = new Color(0.1f, 0.1f, 0.15f, 1f);
            viewportObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            viewportObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            viewportObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            viewportObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var contentObj = new GameObject("Content");
            contentObj.transform.SetParent(viewportObj.transform);
            var contentLayout = contentObj.AddComponent<VerticalLayoutGroup>();
            contentLayout.spacing = 3;
            contentLayout.padding = new RectOffset(5, 5, 5, 5);
            contentLayout.childAlignment = TextAnchor.UpperCenter;
            contentLayout.childControlWidth = true;
            contentLayout.childControlHeight = true;
            contentLayout.childForceExpandWidth = true;
            contentLayout.childForceExpandHeight = false;
            var contentFitter = contentObj.AddComponent<ContentSizeFitter>();
            contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            contentObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            contentObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            contentObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            scrollView.content = contentObj.GetComponent<RectTransform>();

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created TechTreePanel prefab at {prefabPath}");
        }

        private static void CreateNuclearStatusPanelPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/NuclearStatusPanel.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("NuclearStatusPanel");
            go.AddComponent<RectTransform>();
            go.AddComponent<CanvasGroup>();

            var bgObj = new GameObject("Background");
            bgObj.transform.SetParent(go.transform);
            var bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.15f, 0.08f, 0.08f, 0.95f);
            bgObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.02f, 0.3f);
            bgObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.18f, 0.65f);
            bgObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            bgObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var titleObj = new GameObject("Title");
            titleObj.transform.SetParent(go.transform);
            var titleText = titleObj.AddComponent<Text>();
            titleText.text = "核威慑状态";
            titleText.fontSize = 13;
            titleText.color = new Color(0.9f, 0.4f, 0.4f, 1f);
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.fontStyle = FontStyle.Bold;
            titleObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.02f, 0.58f);
            titleObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.18f, 0.65f);
            titleObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            titleObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var warheadLabelObj = new GameObject("WarheadLabel");
            warheadLabelObj.transform.SetParent(go.transform);
            var warheadLabel = warheadLabelObj.AddComponent<Text>();
            warheadLabel.text = "弹头数量:";
            warheadLabel.fontSize = 11;
            warheadLabel.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            warheadLabel.alignment = TextAnchor.MiddleLeft;
            warheadLabelObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.03f, 0.48f);
            warheadLabelObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.1f, 0.55f);
            warheadLabelObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            warheadLabelObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var warheadValueObj = new GameObject("WarheadValue");
            warheadValueObj.transform.SetParent(go.transform);
            var warheadValue = warheadValueObj.AddComponent<Text>();
            warheadValue.text = "0";
            warheadValue.fontSize = 14;
            warheadValue.color = new Color(0.9f, 0.5f, 0.3f, 1f);
            warheadValue.alignment = TextAnchor.MiddleLeft;
            warheadValueObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.48f);
            warheadValueObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.17f, 0.55f);
            warheadValueObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            warheadValueObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var capabilityLabelObj = new GameObject("CapabilityLabel");
            capabilityLabelObj.transform.SetParent(go.transform);
            var capabilityLabel = capabilityLabelObj.AddComponent<Text>();
            capabilityLabel.text = "威慑等级:";
            capabilityLabel.fontSize = 11;
            capabilityLabel.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            capabilityLabel.alignment = TextAnchor.MiddleLeft;
            capabilityLabelObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.03f, 0.40f);
            capabilityLabelObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.1f, 0.47f);
            capabilityLabelObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            capabilityLabelObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var capabilityValueObj = new GameObject("CapabilityValue");
            capabilityValueObj.transform.SetParent(go.transform);
            var capabilityValue = capabilityValueObj.AddComponent<Text>();
            capabilityValue.text = "无";
            capabilityValue.fontSize = 12;
            capabilityValue.color = new Color(0.8f, 0.3f, 0.3f, 1f);
            capabilityValue.alignment = TextAnchor.MiddleLeft;
            capabilityValueObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.40f);
            capabilityValueObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.17f, 0.47f);
            capabilityValueObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            capabilityValueObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var cooldownLabelObj = new GameObject("CooldownLabel");
            cooldownLabelObj.transform.SetParent(go.transform);
            var cooldownLabel = cooldownLabelObj.AddComponent<Text>();
            cooldownLabel.text = "冷却:";
            cooldownLabel.fontSize = 11;
            cooldownLabel.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            cooldownLabel.alignment = TextAnchor.MiddleLeft;
            cooldownLabelObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.03f, 0.32f);
            cooldownLabelObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.1f, 0.39f);
            cooldownLabelObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            cooldownLabelObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var cooldownValueObj = new GameObject("CooldownValue");
            cooldownValueObj.transform.SetParent(go.transform);
            var cooldownValue = cooldownValueObj.AddComponent<Text>();
            cooldownValue.text = "0";
            cooldownValue.fontSize = 12;
            cooldownValue.color = new Color(0.6f, 0.6f, 0.6f, 1f);
            cooldownValue.alignment = TextAnchor.MiddleLeft;
            cooldownValueObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.32f);
            cooldownValueObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.17f, 0.39f);
            cooldownValueObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            cooldownValueObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created NuclearStatusPanel prefab at {prefabPath}");
        }

        private static void CreateProxyAffairsPanelPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/ProxyAffairsPanel.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("ProxyAffairsPanel");
            go.AddComponent<RectTransform>();
            go.AddComponent<CanvasGroup>();

            var bgObj = new GameObject("Background");
            bgObj.transform.SetParent(go.transform);
            var bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.1f, 0.12f, 0.08f, 0.95f);
            bgObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.02f, 0.02f);
            bgObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.22f, 0.28f);
            bgObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            bgObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var titleObj = new GameObject("Title");
            titleObj.transform.SetParent(go.transform);
            var titleText = titleObj.AddComponent<Text>();
            titleText.text = "代理事务";
            titleText.fontSize = 13;
            titleText.color = new Color(0.8f, 0.8f, 0.5f, 1f);
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.fontStyle = FontStyle.Bold;
            titleObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.02f, 0.85f);
            titleObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.22f, 0.95f);
            titleObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            titleObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var scrollObj = new GameObject("RegionList");
            scrollObj.transform.SetParent(go.transform);
            var scrollRect = scrollObj.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.02f, 0.02f);
            scrollObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.22f, 0.83f);
            scrollObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            scrollObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var viewportObj = new GameObject("Viewport");
            viewportObj.transform.SetParent(scrollObj.transform);
            var mask = viewportObj.AddComponent<Mask>();
            mask.showMaskGraphic = false;
            var viewportImage = viewportObj.AddComponent<Image>();
            viewportImage.color = new Color(0.1f, 0.12f, 0.08f, 1f);
            viewportObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            viewportObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            viewportObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            viewportObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var contentObj = new GameObject("Content");
            contentObj.transform.SetParent(viewportObj.transform);
            var vLayout = contentObj.AddComponent<VerticalLayoutGroup>();
            vLayout.spacing = 3;
            vLayout.padding = new RectOffset(3, 3, 3, 3);
            vLayout.childAlignment = TextAnchor.UpperCenter;
            vLayout.childControlWidth = true;
            vLayout.childControlHeight = true;
            vLayout.childForceExpandWidth = true;
            vLayout.childForceExpandHeight = false;
            var fitter = contentObj.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            contentObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            contentObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            contentObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            scrollRect.content = contentObj.GetComponent<RectTransform>();

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created ProxyAffairsPanel prefab at {prefabPath}");
        }

        private static void CreatePhaseIndicatorPanelPrefab()
        {
            var prefabPath = "Assets/Prefabs/UI/PhaseIndicatorPanel.prefab";
            CreatePrefabDirectory(prefabPath);

            var go = new GameObject("PhaseIndicatorPanel");
            go.AddComponent<RectTransform>();

            var bgObj = new GameObject("Background");
            bgObj.transform.SetParent(go.transform);
            var bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.12f, 0.12f, 0.18f, 0.9f);
            bgObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.85f);
            bgObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.95f);
            bgObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            bgObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            var layoutObj = new GameObject("PhaseButtons");
            layoutObj.transform.SetParent(go.transform);
            var hLayout = layoutObj.AddComponent<HorizontalLayoutGroup>();
            hLayout.spacing = 5;
            hLayout.padding = new RectOffset(5, 5, 5, 5);
            hLayout.childAlignment = TextAnchor.MiddleCenter;
            hLayout.childControlWidth = true;
            hLayout.childControlHeight = true;
            hLayout.childForceExpandWidth = true;
            hLayout.childForceExpandHeight = false;
            layoutObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            layoutObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            layoutObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            layoutObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            string[] phases = { "外交", "战略", "作战", "后勤", "情报", "AI" };
            Color[] phaseColors = {
                new Color(0.4f, 0.6f, 0.8f, 1f),
                new Color(0.7f, 0.5f, 0.3f, 1f),
                new Color(0.8f, 0.3f, 0.3f, 1f),
                new Color(0.5f, 0.7f, 0.4f, 1f),
                new Color(0.5f, 0.4f, 0.7f, 1f),
                new Color(0.6f, 0.6f, 0.6f, 1f)
            };

            for (int i = 0; i < phases.Length; i++)
            {
                var btnObj = new GameObject($"Phase{i}");
                btnObj.transform.SetParent(layoutObj.transform);

                var btn = btnObj.AddComponent<Button>();
                var btnImg = btnObj.AddComponent<Image>();
                btnImg.color = phaseColors[i];
                btn.targetGraphic = btnImg;
                btn.colors = new ColorBlock
                {
                    normalColor = phaseColors[i],
                    highlightedColor = new Color(phaseColors[i].r + 0.2f, phaseColors[i].g + 0.2f, phaseColors[i].b + 0.2f, 1f),
                    pressedColor = new Color(phaseColors[i].r - 0.2f, phaseColors[i].g - 0.2f, phaseColors[i].b - 0.2f, 1f),
                    disabledColor = new Color(0.3f, 0.3f, 0.3f, 0.5f),
                    colorMultiplier = 1f
                };

                var txtObj = new GameObject("Text");
                txtObj.transform.SetParent(btnObj.transform);
                var txt = txtObj.AddComponent<Text>();
                txt.text = phases[i];
                txt.fontSize = 11;
                txt.color = Color.white;
                txt.alignment = TextAnchor.MiddleCenter;
                txtObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                txtObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                txtObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                txtObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

                btnObj.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 30);
            }

            go.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            go.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
            Debug.Log($"[UIPrefabGenerator] Created PhaseIndicatorPanel prefab at {prefabPath}");
        }

        private static void CreatePrefabDirectory(string path)
        {
            var directory = System.IO.Path.GetDirectoryName(path);
            if (!AssetDatabase.IsValidFolder(directory))
            {
                var folders = directory.Split('/');
                var currentPath = folders[0];
                for (int i = 1; i < folders.Length; i++)
                {
                    if (!AssetDatabase.IsValidFolder(currentPath + "/" + folders[i]))
                    {
                        AssetDatabase.CreateFolder(currentPath, folders[i]);
                    }
                    currentPath += "/" + folders[i];
                }
            }
        }
    }
}
#endif
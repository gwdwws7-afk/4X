#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EventideAge.Editor
{
    public class ProjectSetup : EditorWindow
    {
        [MenuItem("EventideAge/Setup Project")]
        public static void SetupProjectMenu()
        {
            SceneSetup.RunOneClickSetup();
            Debug.Log("[ProjectSetup] Routed to SceneSetup.RunOneClickSetup().");
        }

        [MenuItem("EventideAge/Setup Project (Window)")]
        public static void ShowWindow()
        {
            var window = GetWindow<ProjectSetup>("EventideAge Setup");
            window.minSize = new Vector2(420, 220);
        }

        private void OnGUI()
        {
            GUILayout.Label("EventideAge Project Setup", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.HelpBox(
                "Setup Project now reuses the same one-click pipeline as " +
                "EventideAge -> Setup Complete Scene to avoid setup drift.\n\n" +
                "Recommended:\n" +
                "1) EventideAge -> Setup Complete Scene (or Setup Project)\n" +
                "2) EventideAge -> Run All Tests",
                MessageType.Info);

            if (GUILayout.Button("Run Setup Project (One Click)", GUILayout.Height(34)))
            {
                SetupProjectMenu();
            }

            if (GUILayout.Button("Open Setup Complete Scene Window", GUILayout.Height(28)))
            {
                SceneSetup.ShowWindow();
            }
        }
    }
}
#endif

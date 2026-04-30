#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;

namespace EventideAge.Editor
{
    public static class P0ArtImportSetup
    {
        private const string kMapBasePath = "Assets/Art/Map/art_map_base_regions_v001.png";
        private const string kMapNodeRouteSheetPath = "Assets/Art/Map/art_map_nodes_routes_sheet_v001.png";
        private const string kUnitsSheetPath = "Assets/Art/Units/art_units_faction_tokens_sheet_v001.png";
        private const string kIconsSheetPath = "Assets/Art/Icons/art_icons_resources_phases_status_sheet_v001.png";
        private const string kUiSheetPath = "Assets/Art/UI/art_ui_panels_buttons_skin_sheet_v001.png";
        private const string kEventsSheetPath = "Assets/Art/Events/art_events_news_illustrations_sheet_v001.png";
        private const string kPortraitsSheetPath = "Assets/Art/Portraits/art_portraits_leaders_factions_sheet_v001.png";
        private const string kOverlaysSheetPath = "Assets/Art/Overlays/art_map_terrain_fog_overlays_sheet_v001.png";
        private const string kDiplomacySheetPath = "Assets/Art/Diplomacy/art_diplomacy_protocol_badges_sheet_v001.png";
        private const string kMarkersSheetPath = "Assets/Art/Markers/art_markers_blockade_alert_status_sheet_v001.png";
        private const string kVfxSheetPath = "Assets/Art/VFX/art_vfx_map_ui_feedback_sheet_v001.png";

        [MenuItem("EventideAge/Art/Apply P0 Import Settings")]
        public static void ApplyP0ImportSettings()
        {
            ConfigureSingleSprite(kMapBasePath, 100, "p0_map");
            ConfigureGridSpriteSheet(kMapNodeRouteSheetPath, 114, 114, 100, "p0_map_nodes_routes");
            ConfigureGridSpriteSheet(kUnitsSheetPath, 128, 128, 100, "p0_units");
            ConfigureGridSpriteSheet(kIconsSheetPath, 128, 128, 100, "p0_icons");
            ConfigureGridSpriteSheet(kUiSheetPath, 128, 128, 100, "p0_ui");
            ConfigureGridSpriteSheet(kEventsSheetPath, 128, 128, 100, "p1_events");
            ConfigureGridSpriteSheet(kPortraitsSheetPath, 128, 128, 100, "p1_portraits");
            ConfigureGridSpriteSheet(kOverlaysSheetPath, 128, 128, 100, "p1_overlays");
            ConfigureGridSpriteSheet(kDiplomacySheetPath, 128, 128, 100, "p1_diplomacy");
            ConfigureGridSpriteSheet(kMarkersSheetPath, 128, 128, 100, "p1_markers");
            ConfigureGridSpriteSheet(kVfxSheetPath, 128, 128, 100, "p1_vfx");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[P0ArtImportSetup] P0 art import settings applied.");
        }

        private static void ConfigureSingleSprite(string assetPath, int pixelsPerUnit, string packingTag)
        {
            var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null)
            {
                Debug.LogWarning($"[P0ArtImportSetup] Missing texture: {assetPath}");
                return;
            }

            ApplyCommonSpriteTextureSettings(importer, pixelsPerUnit, packingTag);
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.SaveAndReimport();
        }

        private static void ConfigureGridSpriteSheet(string assetPath, int cellWidth, int cellHeight, int pixelsPerUnit, string prefix)
        {
            var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null)
            {
                Debug.LogWarning($"[P0ArtImportSetup] Missing texture: {assetPath}");
                return;
            }

            ApplyCommonSpriteTextureSettings(importer, pixelsPerUnit, prefix);
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.SaveAndReimport();

            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (texture == null)
            {
                Debug.LogWarning($"[P0ArtImportSetup] Failed to load texture after import: {assetPath}");
                return;
            }

            int width = texture.width;
            int height = texture.height;
            int columns = width / cellWidth;
            int rows = height / cellHeight;

            if (columns <= 0 || rows <= 0)
            {
                Debug.LogWarning($"[P0ArtImportSetup] Invalid grid size for {assetPath}. Texture={width}x{height}, cell={cellWidth}x{cellHeight}");
                return;
            }

            List<SpriteRect> sprites = new List<SpriteRect>(columns * rows);
            for (int row = 0; row < rows; row++)
            {
                int y = height - (row + 1) * cellHeight;
                for (int column = 0; column < columns; column++)
                {
                    int x = column * cellWidth;
                    sprites.Add(new SpriteRect
                    {
                        name = $"{prefix}_r{row:D2}_c{column:D2}",
                        rect = new Rect(x, y, cellWidth, cellHeight),
                        alignment = (int)SpriteAlignment.Center,
                        pivot = new Vector2(0.5f, 0.5f),
                        border = Vector4.zero,
                        spriteID = GUID.Generate()
                    });
                }
            }

            importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null)
            {
                Debug.LogWarning($"[P0ArtImportSetup] Importer became unavailable: {assetPath}");
                return;
            }

            var spriteDataProviderFactories = new SpriteDataProviderFactories();
            spriteDataProviderFactories.Init();
            var spriteEditorDataProvider = spriteDataProviderFactories.GetSpriteEditorDataProviderFromObject(importer);
            if (spriteEditorDataProvider == null)
            {
                Debug.LogWarning($"[P0ArtImportSetup] Sprite editor data provider unavailable: {assetPath}");
                return;
            }

            spriteEditorDataProvider.InitSpriteEditorDataProvider();
            spriteEditorDataProvider.SetSpriteRects(sprites.ToArray());

            var nameFileIdProvider = spriteEditorDataProvider.GetDataProvider<ISpriteNameFileIdDataProvider>();
            if (nameFileIdProvider != null)
            {
                List<SpriteNameFileIdPair> pairs = new List<SpriteNameFileIdPair>(sprites.Count);
                foreach (var sprite in sprites)
                {
                    pairs.Add(new SpriteNameFileIdPair(sprite.name, sprite.spriteID));
                }
                nameFileIdProvider.SetNameFileIdPairs(pairs);
            }

            spriteEditorDataProvider.Apply();
            importer.SaveAndReimport();
        }

        private static void ApplyCommonSpriteTextureSettings(TextureImporter importer, int pixelsPerUnit, string packingTag)
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.spritePixelsPerUnit = pixelsPerUnit;
            importer.alphaIsTransparency = true;
            importer.mipmapEnabled = false;
            importer.isReadable = false;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.filterMode = FilterMode.Bilinear;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.sRGBTexture = true;
            var settings = new TextureImporterSettings();
            importer.ReadTextureSettings(settings);
            settings.spriteMeshType = SpriteMeshType.FullRect;
            settings.spriteGenerateFallbackPhysicsShape = false;
            importer.SetTextureSettings(settings);
            importer.maxTextureSize = 2048;
        }
    }
}
#endif

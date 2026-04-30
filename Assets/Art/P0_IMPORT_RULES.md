# P0 Art Import Rules (Unity 2022.3)

Apply from Unity menu:

- `EventideAge -> Art -> Apply P0 Import Settings`

This script configures import settings and slices sprite sheets in a deterministic grid.

## Assets

| Asset | Path | Mode | Grid |
|---|---|---|---|
| Map base | `Assets/Art/Map/art_map_base_regions_v001.png` | Single | N/A |
| Node/route sheet | `Assets/Art/Map/art_map_nodes_routes_sheet_v001.png` | Multiple | `114x114` |
| Unit token sheet | `Assets/Art/Units/art_units_faction_tokens_sheet_v001.png` | Multiple | `128x128` |
| Icon sheet | `Assets/Art/Icons/art_icons_resources_phases_status_sheet_v001.png` | Multiple | `128x128` |
| UI skin sheet | `Assets/Art/UI/art_ui_panels_buttons_skin_sheet_v001.png` | Multiple | `128x128` |

## Shared importer settings

- Texture Type: `Sprite (2D and UI)`
- Compression: `Uncompressed`
- Mip Maps: `Off`
- NPOT Scale: `None`
- Wrap Mode: `Clamp`
- Filter Mode: `Bilinear`
- Sprite Mesh Type: `Full Rect`
- Generate Physics Shape: `Off`
- Max Size: `2048`


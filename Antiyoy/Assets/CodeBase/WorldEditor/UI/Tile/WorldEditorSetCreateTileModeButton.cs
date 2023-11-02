using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.WorldEditor.Data;
using UnityEngine;

namespace CodeBase.WorldEditor.UI.Tile
{
    public class WorldEditorSetCreateTileModeButton : WorldEditorButtonBase
    {
        [SerializeField] private RegionType _regionType;

        private protected override void OnClick()
        {
            WorldEditorController.SetMode(WorldEditorMode.CreateTile);
            WorldEditorController.SetRegionType(_regionType);
        }
    }
}
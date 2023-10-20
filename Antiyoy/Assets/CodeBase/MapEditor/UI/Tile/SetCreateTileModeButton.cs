using CodeBase.Gameplay.World.Region.Data;
using CodeBase.MapEditor.Data;
using UnityEngine;

namespace CodeBase.MapEditor.UI
{
    public class SetCreateTileModeButton : MapEditorButtonBase
    {
        [SerializeField] private RegionType _regionType;

        private protected override void OnClick()
        {
            MapEditorController.SetMode(MapEditorMode.CreateTile);
            MapEditorController.SetRegionType(_regionType);
        }
    }
}
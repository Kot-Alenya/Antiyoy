using CodeBase.Gameplay.World.Region.Data;
using UnityEngine;

namespace CodeBase.MapEditor.UI
{
    public class SetTilesModeButton : MapEditorButtonBase
    {
        [SerializeField] private RegionType _tilesRegionType;

        private protected override void OnClick() => MapEditorController.CreateTilesMode(_tilesRegionType);
    }
}
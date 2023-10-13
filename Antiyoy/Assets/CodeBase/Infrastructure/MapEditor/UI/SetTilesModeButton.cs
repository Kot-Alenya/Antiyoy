using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Utilities.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor.UI
{
    public class SetTilesModeButton : ButtonBase, IMapEditorUIElement
    {
        [SerializeField] private RegionType _tilesRegionType;

        private MapEditorController _controller;

        public void Construct(MapEditorController controller) => _controller = controller;

        private protected override void OnClick() => _controller.CreateTilesMode(_tilesRegionType);
    }
}
using CodeBase.Gameplay.Region.Data;
using CodeBase.Utilities.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor.UI
{
    public class SetRegionModeButton : ButtonBase, IMapEditorUIElement
    {
        [SerializeField] private RegionType _region;

        private MapEditorController _controller;

        public void Constructor(MapEditorController controller) => _controller = controller;

        private protected override void OnClick() => _controller.SetRegionMode(_region);
    }
}
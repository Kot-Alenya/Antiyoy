using CodeBase.Utilities.UI;

namespace CodeBase.Infrastructure.MapEditor.UI
{
    public class RemoveRegionModeButton : ButtonBase, IMapEditorUIElement
    {
        private MapEditorController _controller;

        public void Constructor(MapEditorController controller) => _controller = controller;

        private protected override void OnClick() => _controller.RemoveRegionMode();
    }
}
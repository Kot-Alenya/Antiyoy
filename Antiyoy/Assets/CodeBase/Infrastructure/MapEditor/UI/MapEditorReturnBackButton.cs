using CodeBase.Utilities.UI;

namespace CodeBase.Infrastructure.MapEditor.UI
{
    public class MapEditorReturnBackButton : ButtonBase, IMapEditorUIElement
    {
        private MapEditorController _controller;

        public void Construct(MapEditorController controller) => _controller = controller;

        private protected override void OnClick() => _controller.ReturnBack();
    }
}
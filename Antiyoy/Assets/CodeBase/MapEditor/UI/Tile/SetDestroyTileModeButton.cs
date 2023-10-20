using CodeBase.MapEditor.Data;

namespace CodeBase.MapEditor.UI
{
    public class SetDestroyTileModeButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.SetMode(MapEditorMode.DestroyTile);
    }
}
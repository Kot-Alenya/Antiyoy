using CodeBase.MapEditor.Data;

namespace CodeBase.MapEditor.UI.Tile
{
    public class SetDestroyTileModeButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.SetMode(MapEditorMode.DestroyTile);
    }
}
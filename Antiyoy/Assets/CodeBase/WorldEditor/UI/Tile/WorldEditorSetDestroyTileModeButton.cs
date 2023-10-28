using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor.UI.Tile
{
    public class WorldEditorSetDestroyTileModeButton : WorldEditorButtonBase
    {
        private protected override void OnClick() => WorldEditorController.SetMode(WorldEditorMode.DestroyTile);
    }
}
namespace CodeBase.MapEditor.UI
{
    public class RemoveTilesModeButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.DestroyTilesMode();
    }
}
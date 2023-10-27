namespace CodeBase.MapEditor.UI
{
    public class MapEditorSaveWorldButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.SaveWorld();
    }
}
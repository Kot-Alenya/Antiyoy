namespace CodeBase.WorldEditor.UI
{
    public class WorldEditorSaveWorldButton : WorldEditorButtonBase
    {
        private protected override void OnClick() => WorldEditorController.SaveWorld();
    }
}
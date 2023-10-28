namespace CodeBase.WorldEditor.UI.VersionControl
{
    public class WorldEditorReturnBackButton : WorldEditorButtonBase
    {
        private protected override void OnClick() => WorldEditorController.ReturnBack();
    }
}
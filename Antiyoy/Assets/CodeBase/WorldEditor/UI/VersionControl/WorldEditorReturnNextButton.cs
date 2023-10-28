namespace CodeBase.WorldEditor.UI.VersionControl
{
    public class WorldEditorReturnNextButton : WorldEditorButtonBase
    {
        private protected override void OnClick() => WorldEditorController.ReturnNext();
    }
}
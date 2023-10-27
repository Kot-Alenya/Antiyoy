namespace CodeBase.MapEditor.UI.VersionControl
{
    public class MapEditorReturnNextButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.ReturnNext();
    }
}
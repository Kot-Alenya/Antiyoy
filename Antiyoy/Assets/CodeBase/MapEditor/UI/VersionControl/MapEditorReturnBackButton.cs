namespace CodeBase.MapEditor.UI.VersionControl
{
    public class MapEditorReturnBackButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.ReturnBack();
    }
}
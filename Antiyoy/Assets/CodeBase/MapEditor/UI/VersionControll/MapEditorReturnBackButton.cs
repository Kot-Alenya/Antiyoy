namespace CodeBase.MapEditor.UI.VersionControll
{
    public class MapEditorReturnBackButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.ReturnBack();
    }
}
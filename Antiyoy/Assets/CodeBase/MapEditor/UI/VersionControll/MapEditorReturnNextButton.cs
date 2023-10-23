namespace CodeBase.MapEditor.UI.VersionControll
{
    public class MapEditorReturnNextButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.ReturnNext();
    }
}
namespace CodeBase.MapEditor.UI
{
    public class MapEditorReturnNextButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.ReturnNext();
    }
}
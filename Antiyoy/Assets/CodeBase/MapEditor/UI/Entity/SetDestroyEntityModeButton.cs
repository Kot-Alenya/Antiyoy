using CodeBase.MapEditor.Data;

namespace CodeBase.MapEditor.UI
{
    public class SetDestroyEntityModeButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.SetMode(MapEditorMode.DestroyEntity);
    }
}
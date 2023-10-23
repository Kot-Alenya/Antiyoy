using CodeBase.MapEditor.Data;

namespace CodeBase.MapEditor.UI.Entity
{
    public class SetDestroyEntityModeButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.SetMode(MapEditorMode.DestroyEntity);
    }
}
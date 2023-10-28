using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor.UI.Entity
{
    public class WorldEditorSetDestroyEntityModeButton : WorldEditorButtonBase
    {
        private protected override void OnClick() => WorldEditorController.SetMode(WorldEditorMode.DestroyEntity);
    }
}
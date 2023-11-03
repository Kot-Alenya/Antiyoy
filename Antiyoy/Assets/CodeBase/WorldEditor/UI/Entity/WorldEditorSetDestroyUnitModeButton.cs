using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor.UI.Entity
{
    public class WorldEditorSetDestroyUnitModeButton : WorldEditorButtonBase
    {
        private protected override void OnClick() => WorldEditorController.SetMode(WorldEditorMode.DestroyUnit);
    }
}
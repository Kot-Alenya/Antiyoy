using CodeBase.Utilities.UI;
using CodeBase.WorldEditor.Controller;
using Zenject;

namespace CodeBase.WorldEditor.UI
{
    public abstract class WorldEditorButtonBase : ButtonBase
    {
        private protected IWorldEditorController WorldEditorController;

        [Inject]
        private void Construct(IWorldEditorController worldEditorController) =>
            WorldEditorController = worldEditorController;
    }
}
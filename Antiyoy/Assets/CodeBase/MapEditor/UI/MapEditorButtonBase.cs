using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.MapEditor.UI
{
    public abstract class MapEditorButtonBase : ButtonBase
    {
        private protected IMapEditorController MapEditorController;

        [Inject]
        private void Construct(IMapEditorController mapEditorController) => MapEditorController = mapEditorController;
    }
}
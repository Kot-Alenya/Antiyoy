using _dev;
using CodeBase.Utilities.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor.UI
{
    public class SetEntityButton : ButtonBase
    {
        [SerializeField] private EntityType _region;

        private MapEditorController _controller;

        public void Constructor(MapEditorController controller) => _controller = controller;

        private protected override void OnClick() => _controller.SetEntityMode(_region);
    }
}
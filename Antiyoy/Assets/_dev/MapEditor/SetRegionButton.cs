using CodeBase.Gameplay.Region.Data;
using CodeBase.Utilities.UI;
using UnityEngine;

namespace _dev.MapEditor
{
    public class SetRegionButton : ButtonBase
    {
        [SerializeField] private RegionType _region;

        private MapEditorObject _mapEditor;

        public void Constructor(MapEditorObject mapEditor) => _mapEditor = mapEditor;

        private protected override void OnClick() => _mapEditor.RegionSetMode(_region);
    }
}
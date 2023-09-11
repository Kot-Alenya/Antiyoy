using CodeBase.Gameplay.Region.Data;
using CodeBase.Utilities.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor.UI
{
    public class SetRegionButton : ButtonBase
    {
        [SerializeField] private RegionType _region;

        private IMapEditorUI _mapEditor;

        public void Constructor(IMapEditorUI mapEditor) => _mapEditor = mapEditor;

        private protected override void OnClick() => _mapEditor.SetModeRegion(_region);
    }
}
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.WorldEditor.Data;
using UnityEngine;

namespace CodeBase.WorldEditor.UI.Entity
{
    public class WorldEditorSetCreateUnitModeButton : WorldEditorButtonBase
    {
        [SerializeField] private UnitType _unitType;

        private protected override void OnClick()
        {
            WorldEditorController.SetMode(WorldEditorMode.CreateUnit);
            WorldEditorController.SetUnitType(_unitType);
        }
    }
}
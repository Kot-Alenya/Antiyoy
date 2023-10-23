using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.MapEditor.Data;
using UnityEngine;

namespace CodeBase.MapEditor.UI.Entity
{
    public class SetCreateEntityModeButton : MapEditorButtonBase
    {
        [SerializeField] private EntityType _entityType;

        private protected override void OnClick()
        {
            MapEditorController.SetMode(MapEditorMode.CreateEntity);
            MapEditorController.SetEntityType(_entityType);
        }
    }
}
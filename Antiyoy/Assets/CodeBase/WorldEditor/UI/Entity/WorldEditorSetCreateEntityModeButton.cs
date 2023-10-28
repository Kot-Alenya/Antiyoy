using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.WorldEditor.Data;
using UnityEngine;

namespace CodeBase.WorldEditor.UI.Entity
{
    public class WorldEditorSetCreateEntityModeButton : WorldEditorButtonBase
    {
        [SerializeField] private EntityType _entityType;

        private protected override void OnClick()
        {
            WorldEditorController.SetMode(WorldEditorMode.CreateEntity);
            WorldEditorController.SetEntityType(_entityType);
        }
    }
}
using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Entity
{
    public class EntityFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public EntityFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public EntityData Create(EntityType entityType, Transform root)
        {
            var presets = _staticDataProvider.Get<EntitiesPresetsCollection>();
            var preset = presets.Entities[entityType];
            var instance = Object.Instantiate(preset.Prefab, root);

            return new EntityData(instance, entityType, preset.Income);
        }

        public void Destroy(EntityData entity) => Object.Destroy(entity.Instance.gameObject);
    }
}
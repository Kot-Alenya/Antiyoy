using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Entity
{
    public class EntityFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public EntityFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public EntityData Create(TileData rootTile, EntityType entityType)
        {
            var config = _staticDataProvider.Get<EntitiesConfig>();
            var preset = config.Presets[entityType];
            var instance = Object.Instantiate(preset.Prefab, rootTile.Instance.transform);
            var entity = new EntityData(instance, rootTile, entityType, preset.Income);

            return entity;
        }

        public void Destroy(EntityData entity) => Object.Destroy(entity.Instance.gameObject);
    }
}
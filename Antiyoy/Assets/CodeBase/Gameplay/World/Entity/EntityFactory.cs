using CodeBase.Gameplay.World.NewTerrain.Data;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using EntityType = CodeBase.Gameplay.World.NewTerrain.Data.EntityType;

namespace CodeBase.Gameplay.World.Entity
{
    public class EntityFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public EntityFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        private EntityObject Create(TileData rootTile, EntityType entityType)
        {
            var config = _staticDataProvider.Get<EntitiesConfig>();
            var preset = config.Presets[entityType];
            var instance = Object.Instantiate(preset.Prefab, rootTile.Instance.transform);
            var entity = new EntityObject(instance, rootTile, entityType, preset.Income);

            return entity;
        }

        private void Destroy(EntityObject entity) => Object.Destroy(entity.Instance.gameObject);
    }
}
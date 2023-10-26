using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Tile;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Entity
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ITerrainEntities _terrainEntities;
        private readonly TileCollection _tileCollection;

        public EntityFactory(IStaticDataProvider staticDataProvider, TileCollection tileCollection,
            ITerrainEntities terrainEntities)
        {
            _staticDataProvider = staticDataProvider;
            _tileCollection = tileCollection;
            _terrainEntities = terrainEntities;
        }

        public void Create(TileData rootTile, EntityType entityType)
        {
            var presets = _staticDataProvider.Get<EntitiesPresetsCollection>();
            var preset = presets.Entities[entityType];
            var instance = Object.Instantiate(preset.Prefab, rootTile.Instance.transform);
            var entity = new EntityData(instance, rootTile, entityType, preset.Income);

            _terrainEntities.Set(entity, rootTile);
        }

        public void Destroy(EntityData entity)
        {
            _terrainEntities.Remove(entity);
            Object.Destroy(entity.Instance.gameObject);
        }

        public bool TryCreate(HexPosition hex, EntityType entityType)
        {
            if (_tileCollection.TryGet(hex, out var tile))
            {
                if (tile.Entity == null)
                    Create(tile, entityType);

                return true;
            }

            return false;
        }

        public bool TryDestroy(HexPosition hex)
        {
            if (_tileCollection.TryGet(hex, out var tile))
            {
                if (tile.Entity != null)
                    Destroy(tile.Entity);

                return true;
            }

            return true;
        }
    }
}
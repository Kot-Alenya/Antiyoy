using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Gameplay.Terrain.Region.Rebuild;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using CodeBase.Gameplay.Terrain.Tile.Data;
using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Entity
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ITileCollection _tileCollection;
        private readonly IRegionRebuilder _regionRebuilder;

        public EntityFactory(IStaticDataProvider staticDataProvider, ITileCollection tileCollection,
            IRegionRebuilder regionRebuilder)
        {
            _staticDataProvider = staticDataProvider;
            _tileCollection = tileCollection;
            _regionRebuilder = regionRebuilder;
        }

        public void Create(HexPosition hex, EntityType entityType) => Create(_tileCollection.Get(hex), entityType);

        public void Destroy(HexPosition hex) => Destroy(_tileCollection.Get(hex).Entity);

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

        private void Create(TileData rootTile, EntityType entityType)
        {
            var presets = _staticDataProvider.Get<EntitiesPresetsCollection>();
            var preset = presets.Entities[entityType];
            var instance = Object.Instantiate(preset.Prefab, rootTile.Instance.transform);
            var entity = new EntityData(instance, rootTile, entityType, preset.Income);

            rootTile.Entity = entity;
            _regionRebuilder.RebuildIncome(rootTile.Region);
        }

        private void Destroy(EntityData entity)
        {
            var rootTile = entity.RootTile;

            rootTile.Entity = null;
            _regionRebuilder.RebuildIncome(rootTile.Region);

            Object.Destroy(entity.Instance.gameObject);
        }
    }
}
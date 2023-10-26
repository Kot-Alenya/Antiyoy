using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Rebuild;
using CodeBase.Gameplay.World.Tile.Collection;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Entity
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
            _regionRebuilder.AddToRebuildBuffer(rootTile.Region);
        }

        private void Destroy(EntityData entity)
        {
            var rootTile = entity.RootTile;

            rootTile.Entity = null;
            _regionRebuilder.AddToRebuildBuffer(rootTile.Region);

            Object.Destroy(entity.Instance.gameObject);
        }
    }
}
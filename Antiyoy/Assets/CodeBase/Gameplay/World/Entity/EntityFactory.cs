using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Tile;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Entity
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ITileCollection _tileCollection;
        private readonly IRegionManager _regionManager;

        public EntityFactory(IStaticDataProvider staticDataProvider, ITileCollection tileCollection,
            IRegionManager regionManager)
        {
            _staticDataProvider = staticDataProvider;
            _tileCollection = tileCollection;
            _regionManager = regionManager;
        }

        public void Create(TileData rootTile, EntityType entityType)
        {
            var presets = _staticDataProvider.Get<EntitiesPresetsCollection>();
            var preset = presets.Entities[entityType];
            var instance = Object.Instantiate(preset.Prefab, rootTile.Instance.transform);
            var entity = new EntityData(instance, rootTile, entityType, preset.Income);

            rootTile.Entity = entity;
            _regionManager.AddToRecalculateBuffer(rootTile.Region);
        }

        public void Destroy(EntityData entity)
        {
            var rootTile = entity.RootTile;

            rootTile.Entity = null;
            _regionManager.AddToRecalculateBuffer(rootTile.Region);

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
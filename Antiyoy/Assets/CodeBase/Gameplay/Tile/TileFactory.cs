using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Infrastructure;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Region.Ecs.Components;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TileFactory
    {
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private readonly GameplayEcsWorld _world;
        private Transform _tilePlaceRoot;
        private TileConfig _config;
        private EcsPool<TileCreateRequest> _createTileRequestPool;
        private EcsPool<TileDestroyRequest> _destroyTileRequestPool;
        private EcsPool<TileComponent> _tilePool;
        private EcsPool<TilePlaceComponent> _tilePlacePool;
        private EcsPool<RegionCreateRequest> _createRegionRequestPool;
        private EcsPool<RegionDestroyRequest> _destroyRegionRequestPool;

        public TileFactory(GameplayStaticDataProvider staticDataProvider, GameplayEcsWorld world)
        {
            _staticDataProvider = staticDataProvider;
            _world = world;
        }

        public void Initialize(Transform tilePlaceRoot)
        {
            _tilePlaceRoot = tilePlaceRoot;
            _config = _staticDataProvider.GetTileConfig();

            _createTileRequestPool = _world.GetPool<TileCreateRequest>();
            _destroyTileRequestPool = _world.GetPool<TileDestroyRequest>();

            _createRegionRequestPool = _world.GetPool<RegionCreateRequest>();
            _destroyRegionRequestPool = _world.GetPool<RegionDestroyRequest>();

            _tilePool = _world.GetPool<TileComponent>();
            _tilePlacePool = _world.GetPool<TilePlaceComponent>();
        }

        public TilePlace CreatePlace(HexCoordinates hex)
        {
            var tilePlace = Object.Instantiate(_config.PlacePrefab, hex.ToWorldPosition(), Quaternion.identity);
            var entityId = _world.NewEntity();
            ref var tilePlaceComponent = ref _tilePlacePool.Add(entityId);

            tilePlace.transform.parent = _tilePlaceRoot;
            tilePlace.Hex = hex;
            tilePlace.EntityId = entityId;
            tilePlaceComponent.Value = tilePlace;

            return tilePlace;
        }

        public void CreateTile(TilePlace tilePlace, RegionType regionType)
        {
            DestroyTile(tilePlace);

            _createTileRequestPool.Add(tilePlace.EntityId);
            ref var regionRequest = ref _createRegionRequestPool.Add(tilePlace.EntityId);
            regionRequest.Type = regionType;
        }

        public void DestroyTile(TilePlace tilePlace)
        {
            if (_tilePool.Has(tilePlace.EntityId))
            {
                _destroyTileRequestPool.Add(tilePlace.EntityId);
                _destroyRegionRequestPool.Add(tilePlace.EntityId);
            }
        }
    }
}
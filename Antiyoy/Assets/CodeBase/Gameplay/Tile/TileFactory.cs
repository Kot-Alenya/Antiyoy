using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Infrastructure;
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
        private EcsPool<TileCreateRequest> _createRequestPool;
        private EcsPool<TileDestroyRequest> _destroyRequestPool;
        private EcsPool<TileComponent> _tilePool;
        private EcsPool<TilePlaceComponent> _tilePlacePool;

        public TileFactory(GameplayStaticDataProvider staticDataProvider, GameplayEcsWorld world)
        {
            _staticDataProvider = staticDataProvider;
            _world = world;
        }

        public void Initialize(Transform tilePlaceRoot)
        {
            _tilePlaceRoot = tilePlaceRoot;
            _config = _staticDataProvider.GetTileConfig();
            _createRequestPool = _world.GetPool<TileCreateRequest>();
            _destroyRequestPool = _world.GetPool<TileDestroyRequest>();
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

        public void CreateTile(TilePlace tilePlace)
        {
            DestroyTile(tilePlace);

            _createRequestPool.Add(tilePlace.EntityId);
        }

        public void DestroyTile(TilePlace tilePlace)
        {
            if (_tilePool.Has(tilePlace.EntityId))
                _destroyRequestPool.Add(tilePlace.EntityId);
        }
    }
}
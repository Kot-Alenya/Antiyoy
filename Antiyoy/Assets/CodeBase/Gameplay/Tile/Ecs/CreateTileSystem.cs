using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Infrastructure;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Gameplay.Tile.Ecs
{
    public class CreateTileSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private EcsFilter _createRequestFilter;
        private TileConfig _tileConfig;
        private EcsPool<TileComponent> _tilePool;
        private EcsPool<TilePlaceComponent> _tilePlacePool;

        public CreateTileSystem(GameplayEcsWorld world, GameplayStaticDataProvider staticDataProvider)
        {
            _world = world;
            _staticDataProvider = staticDataProvider;
        }

        public void Init(IEcsSystems systems)
        {
            _createRequestFilter = _world.Filter<TileCreateRequest>().End();
            _tilePool = _world.GetPool<TileComponent>();
            _tilePlacePool = _world.GetPool<TilePlaceComponent>();
            _tileConfig = _staticDataProvider.GetTileConfig();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _createRequestFilter)
                CreateTile(entity);
        }

        private void CreateTile(int entity)
        {
            ref var tilePlace = ref _tilePlacePool.Get(entity);
            ref var tile = ref _tilePool.Add(entity);

            tile.Controller = Object.Instantiate(_tileConfig.ControllerPrefab, tilePlace.Value.transform);
        }
    }
}
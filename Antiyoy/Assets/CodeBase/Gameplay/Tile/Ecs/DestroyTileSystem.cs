using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Gameplay.Tile.Ecs
{
    public class DestroyTileSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private EcsFilter _requestFilter;
        private EcsPool<TileComponent> _tilePool;

        public DestroyTileSystem(GameplayEcsWorld world) => _world = world;

        public void Init(IEcsSystems systems)
        {
            _requestFilter = _world.Filter<TileDestroyRequest>().End();
            _tilePool = _world.GetPool<TileComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _requestFilter)
                DestroyTile(entity);
        }

        private void DestroyTile(int entity)
        {
            ref var tile = ref _tilePool.Get(entity);

            Object.Destroy(tile.Controller.gameObject);
            _tilePool.Del(entity);
        }
    }
}
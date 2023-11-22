using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Region.Ecs.Components;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Region.Ecs
{
    public class DestroyRegionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private EcsFilter _regionDestroyRequestFilter;
        private EcsPool<RegionComponent> _regionPool;

        public DestroyRegionSystem(GameplayEcsWorld world) => _world = world;

        public void Init(IEcsSystems systems)
        {
            _regionDestroyRequestFilter = _world.Filter<RegionDestroyRequest>().End();
            _regionPool = _world.GetPool<RegionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _regionDestroyRequestFilter)
                _regionPool.Del(entity);
        }
    }
}
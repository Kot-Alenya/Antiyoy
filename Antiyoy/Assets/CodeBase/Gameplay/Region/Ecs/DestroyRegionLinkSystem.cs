using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Region.Ecs.Components;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Region.Ecs
{
    public class DestroyRegionLinkSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private EcsFilter _regionDestroyRequestFilter;
        private EcsPool<RegionLink> _regionLinkPool;
        private EcsPool<RegionRecalculateRequest> _regionRecalculateRequestPool;

        public DestroyRegionLinkSystem(GameplayEcsWorld world) => _world = world;

        public void Init(IEcsSystems systems)
        {
            _regionDestroyRequestFilter = _world.Filter<RegionLinkDestroyRequest>().End();
            _regionLinkPool = _world.GetPool<RegionLink>();
            _regionRecalculateRequestPool = _world.GetPool<RegionRecalculateRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _regionDestroyRequestFilter)
            {
                _regionRecalculateRequestPool.Del(entity);
                _regionLinkPool.Del(entity);
            }
        }
    }
}
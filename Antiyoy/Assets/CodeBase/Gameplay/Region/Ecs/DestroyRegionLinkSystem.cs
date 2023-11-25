using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Region.Ecs.Components;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Region.Ecs
{
    public class DestroyRegionLinkSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private readonly GameplayEcsEventsBus _eventsBus;
        private EcsFilter _regionDestroyRequestFilter;
        private EcsPool<RegionLink> _regionLinkPool;

        public DestroyRegionLinkSystem(GameplayEcsWorld world, GameplayEcsEventsBus eventsBus)
        {
            _world = world;
            _eventsBus = eventsBus;
        }

        public void Init(IEcsSystems systems)
        {
            _regionDestroyRequestFilter = _world.Filter<RegionLinkDestroyRequest>().End();
            _regionLinkPool = _world.GetPool<RegionLink>();
            _world.GetPool<RegionRecalculateEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _regionDestroyRequestFilter)
            {
                CreateRecalculateEvent(entity);
                _regionLinkPool.Del(entity);
            }
        }

        private void CreateRecalculateEvent(int entity)
        {
            ref var recalculateEvent = ref _eventsBus.NewEvent<RegionRecalculateEvent>();
            recalculateEvent.Controller = _regionLinkPool.Get(entity).Controller;
        }
    }
}
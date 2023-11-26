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
        private EcsPool<RegionLinkComponent> _regionLinkPool;

        public DestroyRegionLinkSystem(GameplayEcsWorld world, GameplayEcsEventsBus eventsBus)
        {
            _world = world;
            _eventsBus = eventsBus;
        }

        public void Init(IEcsSystems systems)
        {
            _regionDestroyRequestFilter = _world.Filter<RegionLinkDestroyRequest>().End();
            _regionLinkPool = _world.GetPool<RegionLinkComponent>();
            _world.GetPool<RegionRecalculateEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _regionDestroyRequestFilter)
            {
                var controller = _regionLinkPool.Get(entity).Controller;
                
                CreateRecalculateEvent(controller);
                controller.Entities.Remove(entity);

                _regionLinkPool.Del(entity);
            }
        }

        private void CreateRecalculateEvent(RegionController controller)
        {
            ref var recalculateEvent = ref _eventsBus.NewEvent<RegionRecalculateEvent>();
            recalculateEvent.Controller = controller;
        }
    }
}
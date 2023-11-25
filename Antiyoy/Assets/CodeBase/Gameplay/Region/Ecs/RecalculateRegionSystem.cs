using System.Collections.Generic;
using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Region.Ecs.Components;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Region.Ecs
{
    public class RecalculateRegionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private readonly GameplayEcsEventsBus _eventsBus;
        private List<RegionController> _controllersToRecalculateBuffer = new();
        private EcsFilter _regionCreateRequestFilter;
        private EcsPool<RegionLinkCreateRequest> _regionCreateRequestPool;
        private EcsPool<RegionLink> _regionLinkPool;
        private EcsFilter _recalculateEventFilter;
        private EcsPool<RegionRecalculateEvent> _recalculateEventPool;

        public RecalculateRegionSystem(GameplayEcsWorld world, GameplayEcsEventsBus eventsBus)
        {
            _world = world;
            _eventsBus = eventsBus;
        }

        public void Init(IEcsSystems systems)
        {
            _regionCreateRequestFilter = _world.Filter<RegionLinkCreateRequest>().End();
            _regionCreateRequestPool = _world.GetPool<RegionLinkCreateRequest>();
            _regionLinkPool = _world.GetPool<RegionLink>();
            _recalculateEventFilter = _eventsBus.GetEventBodies(out _recalculateEventPool);
        }

        public void Run(IEcsSystems systems)
        {
            FillRecalculateBuffer();

            _eventsBus.HasEvents<RegionRecalculateEvent>();
        }

        private void FillRecalculateBuffer()
        {
            _controllersToRecalculateBuffer.Clear();

            foreach (var entity in _recalculateEventFilter)
            {
                var controller = _recalculateEventPool.Get(entity).Controller;

                if (!_controllersToRecalculateBuffer.Contains(controller) && controller != null)
                    _controllersToRecalculateBuffer.Add(controller);
            }
        }
    }
}
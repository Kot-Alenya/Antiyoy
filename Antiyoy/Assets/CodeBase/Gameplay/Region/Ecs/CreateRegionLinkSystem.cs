using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Region.Ecs.Components;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Region.Ecs
{
    public class CreateRegionLinkSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private readonly RegionFactory _regionFactory;
        private readonly GameplayEcsEventsBus _eventsBus;
        private EcsFilter _regionCreateRequestFilter;
        private EcsPool<RegionLinkCreateRequest> _regionCreateRequestPool;
        private EcsPool<RegionLink> _regionLinkPool;
        private EcsPool<TilePlaceComponent> _tilePlacePool;

        public CreateRegionLinkSystem(GameplayEcsWorld world, RegionFactory regionFactory,
            GameplayEcsEventsBus eventsBus)
        {
            _world = world;
            _regionFactory = regionFactory;
            _eventsBus = eventsBus;
        }

        public void Init(IEcsSystems systems)
        {
            _regionCreateRequestFilter = _world.Filter<RegionLinkCreateRequest>().End();
            _regionCreateRequestPool = _world.GetPool<RegionLinkCreateRequest>();
            _regionLinkPool = _world.GetPool<RegionLink>();
            _tilePlacePool = _world.GetPool<TilePlaceComponent>();
            _world.GetPool<RegionRecalculateEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _regionCreateRequestFilter)
            {
                var controller = FindOrCreateRegionController(entity);

                CreateLink(entity, controller);
                CreateRecalculateEvent(controller);
            }
        }

        private RegionController FindOrCreateRegionController(int entity)
        {
            var linkCreateRequest = _regionCreateRequestPool.Get(entity);
            var rootTilePlace = _tilePlacePool.Get(entity).Value;

            foreach (var connection in rootTilePlace.Connections)
            {
                if (!_regionLinkPool.Has(connection.EntityId))
                    continue;

                var regionLink = _regionLinkPool.Get(connection.EntityId);

                if (regionLink.Type == linkCreateRequest.Type)
                    return regionLink.Controller;
            }

            return _regionFactory.CreateController(linkCreateRequest.Type);
        }

        private void CreateLink(int entity, RegionController controller)
        {
            ref var link = ref _regionLinkPool.Add(entity);
            link.Controller = controller;
        }

        private void CreateRecalculateEvent(RegionController controller)
        {
            ref var recalculateEvent = ref _eventsBus.NewEvent<RegionRecalculateEvent>();
            recalculateEvent.Controller = controller;
        }
    }
}
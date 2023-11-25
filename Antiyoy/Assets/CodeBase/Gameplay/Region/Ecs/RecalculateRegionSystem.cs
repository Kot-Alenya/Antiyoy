using System.Collections.Generic;
using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Infrastructure;
using CodeBase.Gameplay.Region.Ecs.Components;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Region.Ecs
{
    public class RecalculateRegionSystem  : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private readonly List<RegionController> _passedControllersBuffer = new();
        private EcsFilter _regionCreateRequestFilter;
        private EcsPool<RegionLinkCreateRequest> _regionCreateRequestPool;
        private EcsPool<RegionLink> _regionLinkPool;
        private RegionsConfig _regionsConfig;
        private EcsPool<TileComponent> _tilePool;
        private EcsPool<RegionRecalculateEvent> _recalculateRequestPool;
        private EcsPool<TilePlaceComponent> _tilePlacePool;
        
        public void Init(IEcsSystems systems)
        {
            _regionCreateRequestFilter = _world.Filter<RegionLinkCreateRequest>().End();
            _regionCreateRequestPool = _world.GetPool<RegionLinkCreateRequest>();
            _regionLinkPool = _world.GetPool<RegionLink>();
        }
        
        public void Run(IEcsSystems systems)
        {
            //фильтруем ругионы(исключаем дубли)
            //выбираем 1 контроллер, проходимся по его тайлам.
        }
        
        private void FindRecalculationRequestsAndCreate(int entity)
        {
            ref var tilePlace = ref _tilePlacePool.Get(entity);

            foreach (var connection in tilePlace.Value.Connections)
            {
                if (!_regionLinkPool.Has(connection.EntityId))
                    continue;

                var link = _regionLinkPool.Get(connection.EntityId);

                if (!_passedControllersBuffer.Contains(link.Controller))
                    CreateRecalculationRequest(link.Controller,entity);
            }
        }

        private void CreateRecalculationRequest(RegionController controller, int entity)
        {
            //ref var recalculateRequest = ref _recalculateRequestPool.Add(entity);
            //recalculateRequest.Controller = 
        }
    }
}
using System;
using System.Collections.Generic;
using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Region.Ecs.Components;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine.Pool;

namespace CodeBase.Gameplay.Region.Ecs.Recalculate
{
    public class RecalculateRegionGeometrySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private readonly GameplayEcsEventsBus _eventsBus;
        private readonly RegionFactory _regionFactory;
        private readonly List<RegionController> _controllersToRecalculateBuffer = new();
        private readonly ListPool<int> _listPoolInt;

        private EcsFilter _recalculateEventFilter;
        private EcsPool<RegionRecalculateEvent> _recalculateEventPool;
        private EcsPool<TilePlaceComponent> _tilePlacePool;
        private EcsPool<RegionLinkComponent> _regionLink;

        public RecalculateRegionGeometrySystem(GameplayEcsWorld world, GameplayEcsEventsBus eventsBus,
            RegionFactory regionFactory)
        {
            _world = world;
            _eventsBus = eventsBus;
            _regionFactory = regionFactory;
        }

        public void Init(IEcsSystems systems)
        {
            _recalculateEventFilter = _eventsBus.GetEventBodies(out _recalculateEventPool);
            _tilePlacePool = _world.GetPool<TilePlaceComponent>();
            _regionLink = _world.GetPool<RegionLinkComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            if (!_eventsBus.HasEvents<RegionRecalculateEvent>())
                return;

            FillRecalculateBuffer();

            foreach (var controller in _controllersToRecalculateBuffer)
                Recalculate(controller);
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

        private void Recalculate(RegionController controller)
        {
            if (controller.Entities.Count <= 0)
            {
                _regionFactory.Destroy(controller);
                return;
            }

            var splitResult = Split(controller);

            foreach (var region in splitResult)
                Join(region);
        }

        private List<RegionController> Split(RegionController rootController)
        {
            var regions = GetRegionParts(rootController);
            var result = new List<RegionController> { rootController };

            if (regions.Count <= 1)
                return result;

            RemoveBiggestFromList(regions);

            foreach (var region in regions)
            {
                var newController = _regionFactory.CreateControllerAndRegister(rootController.Type, region);

                foreach (var entity in region)
                {
                    _regionLink.Get(entity).Controller = newController;
                    rootController.Entities.Remove(entity);
                }

                newController.Entities.MatchColorsAll();
                result.Add(newController);
            }

            return result;
        }

        private void RemoveBiggestFromList(List<RegionEntitiesCollection> regions)
        {
            var biggestPart = regions[0];

            for (var i = 0; i < regions.Count; i++)
                if (regions[i].Count > biggestPart.Count)
                    biggestPart = regions[i];

            regions.Remove(biggestPart);
        }

        private RegionController GetBiggestFromList(List<RegionController> regions)
        {
            var biggestPart = regions[0];

            for (var i = 0; i < regions.Count; i++)
                if (regions[i].Entities.Count > biggestPart.Entities.Count)
                    biggestPart = regions[i];

            return biggestPart;
        }

        private List<RegionEntitiesCollection> GetRegionParts(RegionController controller)
        {
            var allPassedEntitiesBuffer = new List<int>();
            var frontEntitiesBuffer = new List<int>();
            var parts = new List<RegionEntitiesCollection>();

            while (allPassedEntitiesBuffer.Count < controller.Entities.Count)
            {
                if (!TryGetUnPassedEntity(allPassedEntitiesBuffer, controller.Entities, out var unPassedEntity))
                    break;

                var currentPassedEntitiesBuffer = new List<int>();
                frontEntitiesBuffer.Clear();
                frontEntitiesBuffer.Add(unPassedEntity);

                WaveSearch(frontEntitiesBuffer, currentPassedEntitiesBuffer, controller.Type, null);

                allPassedEntitiesBuffer.AddRange(currentPassedEntitiesBuffer);
                parts.Add(_regionFactory.CreateEntitiesCollection(currentPassedEntitiesBuffer));
            }

            return parts;
        }

        private bool TryGetUnPassedEntity(List<int> passedEntities, RegionEntitiesCollection allEntities,
            out int entity)
        {
            foreach (var e in allEntities)
            {
                if (!passedEntities.Contains(e))
                {
                    entity = e;
                    return true;
                }
            }

            entity = 0;
            return false;
        }

        private void WaveSearch(List<int> frontEntitiesBuffer, List<int> passedEntitiesBuffer, RegionType regionType,
            Action<int> additionalSearchAction)
        {
            while (frontEntitiesBuffer.Count > 0)
            {
                var rootEntity = frontEntitiesBuffer[0];

                foreach (var tilePlace in _tilePlacePool.Get(rootEntity).Value.Connections)
                {
                    if (!_regionLink.Has(tilePlace.EntityId))
                        continue;

                    var controller = _regionLink.Get(tilePlace.EntityId).Controller;

                    if (controller.Type != regionType)
                        continue;

                    if (frontEntitiesBuffer.Contains(tilePlace.EntityId))
                        continue;

                    if (passedEntitiesBuffer.Contains(tilePlace.EntityId))
                        continue;

                    additionalSearchAction?.Invoke(tilePlace.EntityId);
                    frontEntitiesBuffer.Add(tilePlace.EntityId);
                }

                passedEntitiesBuffer.Add(rootEntity);
                frontEntitiesBuffer.Remove(rootEntity);
            }
        }

        private void Join(RegionController rootController)
        {
            var regionsToJoin = GetRegionsToJoin(rootController);

            if (regionsToJoin.Count <= 1)
                return;

            var biggest = GetBiggestFromList(regionsToJoin);

            regionsToJoin.Remove(biggest);

            foreach (var region in regionsToJoin)
            {
                foreach (var entity in region.Entities)
                {
                    _regionLink.Get(entity).Controller = biggest;
                    biggest.Entities.AddAndMatchColor(entity);
                }

                _regionFactory.Destroy(region);
            }
        }

        private List<RegionController> GetRegionsToJoin(RegionController region)
        {
            var passedEntitiesBuffer = new List<int>();
            var frontEntitiesBuffer = new List<int> { region.Entities[0] };
            var regionsToJoin = new List<RegionController> { region };

            WaveSearch(frontEntitiesBuffer, passedEntitiesBuffer, region.Type, entity =>
            {
                var otherController = _regionLink.Get(entity).Controller;

                if (otherController != region)
                    if (!regionsToJoin.Contains(otherController))
                        regionsToJoin.Add(otherController);
            });

            return regionsToJoin;
        }
    }
}
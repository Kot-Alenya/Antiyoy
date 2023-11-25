using CodeBase.Gameplay.Region.Ecs;
using CodeBase.Gameplay.Region.Ecs.Components;
using CodeBase.Gameplay.Region.Ecs.Recalculate;
using CodeBase.Gameplay.Tile.Ecs;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Zenject;

namespace CodeBase.Gameplay.CommonEcs
{
    public class GameplayEcsSystemFactory
    {
        private const string MainWorldName = "Main";
        private const string EventWorldName = "Events";

        private readonly IInstantiator _instantiator;
        private readonly GameplayEcsWorld _world;
        private readonly GameplayEcsEventsBus _eventsBus;

        private GameplayEcsSystemFactory(IInstantiator instantiator, GameplayEcsWorld world,
            GameplayEcsEventsBus eventsBus)
        {
            _instantiator = instantiator;
            _world = world;
            _eventsBus = eventsBus;
        }

        public IEcsSystems[] CreateGameplaySystems()
        {
            return new[]
            {
                CreateMainGameplaySystems(),
                CreateMainGameplayDebugSystems(),
                CreateEventsDebugSystems()
            };
        }

        private IEcsSystems CreateMainGameplaySystems()
        {
            var systems = new EcsSystems(_world);

            systems.Add(Create<DestroyRegionLinkSystem>());
            systems.Add(Create<DestroyTileSystem>());

            systems.Add(Create<CreateTileSystem>());
            systems.Add(Create<CreateRegionLinkSystem>());

            systems.Add(Create<RecalculateRegionGeometrySystem>());

            systems.DelHere<RegionLinkCreateRequest>();
            systems.DelHere<RegionLinkDestroyRequest>();

            systems.DelHere<TileCreateRequest>();
            systems.DelHere<TileDestroyRequest>();

            systems.Add(_eventsBus.GetDestroyEventsSystem()
                .IncReplicant<RegionRecalculateEvent>());

            return systems;
        }

        private IEcsSystems CreateMainGameplayDebugSystems()
        {
            var systems = new EcsSystems(_world);
#if UNITY_EDITOR
            systems.AddWorld(_world, MainWorldName);
            systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(MainWorldName));
            systems.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem(MainWorldName));
#endif
            return systems;
        }

        private IEcsSystems CreateEventsDebugSystems()
        {
            var systems = new EcsSystems(_eventsBus.GetEventsWorld());
#if UNITY_EDITOR
            systems.AddWorld(_eventsBus.GetEventsWorld(), EventWorldName);
            systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EventWorldName));
            systems.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem(EventWorldName));
#endif
            return systems;
        }

        private T Create<T>() where T : IEcsSystem => _instantiator.Instantiate<T>();
    }
}
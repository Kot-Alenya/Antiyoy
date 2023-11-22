using CodeBase.Gameplay.Infrastructure;
using CodeBase.Gameplay.Region.Ecs;
using CodeBase.Gameplay.Region.Ecs.Components;
using CodeBase.Gameplay.Tile.Ecs;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Zenject;

namespace CodeBase.Gameplay.CommonEcs
{
    public class GameplayEcsFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private readonly GameplayEcsControllerProvider _controllerProvider;
        private readonly GameplayEcsSystemFactory _systemFactory;
        private readonly GameplayEcsWorld _world;

        public GameplayEcsFactory(IInstantiator instantiator, GameplayStaticDataProvider staticDataProvider,
            GameplayEcsControllerProvider controllerProvider, GameplayEcsSystemFactory systemFactory,
            GameplayEcsWorld world)
        {
            _instantiator = instantiator;
            _staticDataProvider = staticDataProvider;
            _controllerProvider = controllerProvider;
            _systemFactory = systemFactory;
            _world = world;
        }

        public void Create()
        {
            var prefab = _staticDataProvider.GetEcsWorldConfig().ControllerPrefab;
            var controller = _instantiator.InstantiatePrefabForComponent<GameplayEcsController>(prefab);

            controller.Initialize(CreateSystems());
            _controllerProvider.Initialize(controller);
        }

        private IEcsSystems CreateSystems()
        {
            var systems = new EcsSystems(_world);

            systems.Add(_systemFactory.Create<DestroyRegionSystem>());
            systems.Add(_systemFactory.Create<DestroyTileSystem>());

            systems.Add(_systemFactory.Create<CreateTileSystem>());
            systems.Add(_systemFactory.Create<CreateRegionSystem>());

            systems.DelHere<RegionCreateRequest>();
            systems.DelHere<RegionDestroyRequest>();

            systems.DelHere<TileCreateRequest>();
            systems.DelHere<TileDestroyRequest>();

            return systems;
        }
    }
}
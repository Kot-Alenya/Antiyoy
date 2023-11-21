using CodeBase.Gameplay.Infrastructure;
using CodeBase.Gameplay.Tile;
using CodeBase.Gameplay.Tile.Ecs;
using Leopotam.EcsLite;
using Zenject;

namespace CodeBase.Gameplay.CommonEcs
{
    public class GameplayEcsFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private readonly GameplayEcsControllerProvider _controllerProvider;
        private readonly GameplayEcsSystemFactory _systemFactory;

        public GameplayEcsFactory(IInstantiator instantiator, GameplayStaticDataProvider staticDataProvider,
            GameplayEcsControllerProvider controllerProvider,GameplayEcsSystemFactory systemFactory)
        {
            _instantiator = instantiator;
            _staticDataProvider = staticDataProvider;
            _controllerProvider = controllerProvider;
            _systemFactory = systemFactory;
        }

        public void Create()
        {
            var world = _instantiator.Instantiate<GameplayEcsWorld>();
            var prefab = _staticDataProvider.GetEcsWorldConfig().ControllerPrefab;
            var controller = _instantiator.InstantiatePrefabForComponent<GameplayEcsController>(prefab);

            controller.Initialize(CreateSystems(world));
            _controllerProvider.Initialize(controller);
        }

        private IEcsSystems CreateSystems(GameplayEcsWorld world)
        {
            var systems = new EcsSystems(world);

            systems.Add(_systemFactory.Create<CreateTileSystem>());
            
            return systems;
        }
    }
}
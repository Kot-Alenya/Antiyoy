using CodeBase.Gameplay.Infrastructure;
using Leopotam.EcsLite;
using Zenject;

namespace CodeBase.Gameplay.Ecs
{
    public class GameplayEcsFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private readonly GameplayEcsControllerProvider _controllerProvider;

        public GameplayEcsFactory(IInstantiator instantiator, GameplayStaticDataProvider staticDataProvider,
            GameplayEcsControllerProvider controllerProvider)
        {
            _instantiator = instantiator;
            _staticDataProvider = staticDataProvider;
            _controllerProvider = controllerProvider;
        }

        public void Create()
        {
            var world = _instantiator.Instantiate<GameplayEcsWorld>();
            var systems = CreateSystems(world);
            var prefab = _staticDataProvider.GetEcsWorldConfig().ControllerPrefab;
            var controller = _instantiator.InstantiatePrefabForComponent<GameplayEcsController>(prefab);

            controller.Initialize(systems);
            _controllerProvider.Initialize(controller);
        }

        private IEcsSystems CreateSystems(GameplayEcsWorld world)
        {
            var systems = new EcsSystems(world);

            return systems;
        }
    }
}
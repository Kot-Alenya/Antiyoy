using CodeBase.Gameplay.Infrastructure;
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
            GameplayEcsControllerProvider controllerProvider, GameplayEcsSystemFactory systemFactory)
        {
            _instantiator = instantiator;
            _staticDataProvider = staticDataProvider;
            _controllerProvider = controllerProvider;
            _systemFactory = systemFactory;
        }

        public void Create()
        {
            var prefab = _staticDataProvider.GetEcsWorldConfig().ControllerPrefab;
            var controller = _instantiator.InstantiatePrefabForComponent<GameplayEcsController>(prefab);

            controller.Initialize(_systemFactory.CreateMainSystems());
            _controllerProvider.Initialize(controller);
        }
    }
}
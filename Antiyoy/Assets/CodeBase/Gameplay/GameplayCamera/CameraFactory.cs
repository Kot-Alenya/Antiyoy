using CodeBase.Gameplay.Infrastructure;
using Zenject;

namespace CodeBase.Gameplay.GameplayCamera
{
    public class CameraFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private readonly CameraProvider _provider;

        public CameraFactory(IInstantiator instantiator, GameplayStaticDataProvider staticDataProvider,
            CameraProvider provider)
        {
            _instantiator = instantiator;
            _staticDataProvider = staticDataProvider;
            _provider = provider;
        }

        public void Create()
        {
            var config = _staticDataProvider.GetCameraConfig();
            var controller = _instantiator.InstantiatePrefabForComponent<CameraController>(config.Prefab.gameObject);

            controller.Initialize();
            _provider.Initialize(controller);
        }
    }
}
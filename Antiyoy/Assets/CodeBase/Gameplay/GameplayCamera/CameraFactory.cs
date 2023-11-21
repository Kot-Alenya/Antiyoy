using CodeBase.Infrastructure;
using Zenject;

namespace CodeBase.Gameplay.GameplayCamera
{
    public class CameraFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly GameplayStaticDataProvider _staticDataProvider;

        public CameraFactory(IInstantiator instantiator, GameplayStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _staticDataProvider = staticDataProvider;
        }

        public void Create()
        {
            var config = _staticDataProvider.GetCameraConfig();
            var controller = _instantiator.InstantiatePrefabForComponent<CameraController>(config.Prefab.gameObject);

            controller.Initialize();
        }
    }
}
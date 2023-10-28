using CodeBase.Gameplay.Camera.Data;
using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class CameraFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataProvider _staticDataProvider;

        public CameraFactory(DiContainer container, IStaticDataProvider staticDataProvider)
        {
            _container = container;
            _staticDataProvider = staticDataProvider;
        }

        public void Create()
        {
            var cameraStaticData = _staticDataProvider.Get<CameraStaticData>();
            var instance = Object.Instantiate(cameraStaticData.Prefab);
            var movement = new CameraMovement(cameraStaticData, instance);
            var camera = new CameraObject(instance, movement);

            _container.Bind<ICameraController>().FromInstance(camera).AsSingle();
            _container.InjectGameObject(instance.gameObject);
        }
    }
}
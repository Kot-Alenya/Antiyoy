using CodeBase.Gameplay.Camera.Data;
using CodeBase.Infrastructure.Services.StaticData;
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

        public ICameraController Create()
        {
            var cameraStaticData = _staticDataProvider.Get<CameraStaticData>();
            var instance = Object.Instantiate(cameraStaticData.Prefab);
            var movement = new CameraMovement(cameraStaticData, instance);
            var camera = new CameraObject(instance, movement);

            CreateCameraInput(instance, camera);

            _container.Bind<ICameraController>().FromInstance(camera).AsSingle();

            return camera;
        }

        private void CreateCameraInput(CameraPrefabData instance, ICameraController camera)
        {
            foreach (var input in instance.Inputs)
                input.Constructor(camera);
        }
    }
}
using CodeBase.Gameplay.Camera.Data;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure;
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
            var objectData = Object.Instantiate(cameraStaticData.ObjectPrefab);
            var movement = new CameraMovement(cameraStaticData, objectData);
            var cameraObject = new CameraObject(objectData, movement);

            _container.Bind<ICameraController>().FromInstance(cameraObject).AsSingle();

            CreateCameraInput(cameraObject);

            return cameraObject;
        }

        private void CreateCameraInput(CameraObject cameraObject)
        {
            foreach (var input in cameraObject.Data.Inputs)
                input.Constructor(cameraObject);
        }
    }
}
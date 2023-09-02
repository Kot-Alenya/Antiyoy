using CodeBase.Gameplay.Camera.Data;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class CameraFactory
    {
        private readonly CameraStaticData _staticData;

        public CameraFactory(StaticData data) =>
            _staticData = data.CameraStaticData;

        public void Create()
        {
            var objectData = Object.Instantiate(_staticData.ObjectPrefab);
            var movement = new CameraMovement(_staticData, objectData);
            var cameraObject = new CameraObject(objectData, movement);

            CreateCameraInput(cameraObject);
        }

        private void CreateCameraInput(CameraObject cameraObject)
        {
            foreach (var input in cameraObject.Data.Inputs)
                input.Constructor(cameraObject);
        }
    }
}
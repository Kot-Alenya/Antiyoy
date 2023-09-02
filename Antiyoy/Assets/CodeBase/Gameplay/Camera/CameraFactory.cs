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
            var gameObject = Object.Instantiate(_staticData.ObjectPrefab);
            var movement = new CameraMovement(gameObject, _staticData.MoveVelocity, _staticData.ZoomVelocity);
            var cameraObject = new CameraObject(gameObject, movement);

            CreateCameraInput(cameraObject);
        }

        private void CreateCameraInput(CameraObject cameraObject)
        {
            foreach (var prefab in _staticData.InputPrefabs)
            {
                var input = Object.Instantiate(prefab, cameraObject.Data.transform);
                input.Constructor(cameraObject);
            }
        }
    }
}
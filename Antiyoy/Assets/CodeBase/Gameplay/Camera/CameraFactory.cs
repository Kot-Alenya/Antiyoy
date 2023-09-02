using CodeBase.Gameplay.Camera.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class CameraFactory
    {
        private CameraStaticData _staticData;

        public void Initialize(CameraStaticData staticData)
        {
            _staticData = staticData;
        }

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
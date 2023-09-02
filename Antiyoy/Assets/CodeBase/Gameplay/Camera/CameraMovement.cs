using CodeBase.Gameplay.Camera.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class CameraMovement
    {
        private readonly CameraStaticData _staticData;
        private readonly UnityEngine.Camera _camera;

        public CameraMovement(CameraStaticData staticData, CameraObjectStaticData cameraObjectData)
        {
            _staticData = staticData;
            _camera = cameraObjectData.Camera;
        }

        public void Move(Vector2 direction)
        {
            var currentPosition = _camera.transform.position;
            var nextPosition = currentPosition + (Vector3)direction;
            var newPosition = Vector3.Lerp(currentPosition, nextPosition,
                _staticData.MoveVelocity * Time.fixedDeltaTime);

            _camera.transform.position = newPosition;
        }

        public void Zoom(bool isIncrease)
        {
            var offset = isIncrease ? -_staticData.ZoomVelocity : _staticData.ZoomVelocity;
            var newSize = _camera.orthographicSize + offset;

            _camera.orthographicSize = Mathf.Clamp(newSize, _staticData.MinZoomValue, _staticData.MaxZoomValue);
        }
    }
}
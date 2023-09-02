using CodeBase.Gameplay.Camera.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class CameraMovement
    {
        private readonly CameraObjectStaticData _data;
        private readonly float _moveVelocity;
        private readonly float _zoomVelocity;

        public CameraMovement(CameraObjectStaticData data, float moveVelocity, float zoomVelocity)
        {
            _data = data;
            _moveVelocity = moveVelocity;
            _zoomVelocity = zoomVelocity;
        }

        public void Move(Vector2 direction)
        {
            var currentPosition = _data.Camera.transform.position;
            var nextPosition = currentPosition + (Vector3)direction;
            var newPosition = Vector3.Lerp(currentPosition, nextPosition, _moveVelocity * Time.fixedDeltaTime);

            _data.Camera.transform.position = newPosition;
        }

        public void Zoom(bool isIncrease)
        {
            var offset = isIncrease ? -_zoomVelocity : _zoomVelocity;
            var newSize = _data.Camera.orthographicSize + offset;

            _data.Camera.orthographicSize = Mathf.Clamp(newSize, 1, 10);
        }
    }
}
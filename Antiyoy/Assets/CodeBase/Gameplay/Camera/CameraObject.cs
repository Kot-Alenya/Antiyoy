using CodeBase.Gameplay.Camera.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class CameraObject : ICameraController
    {
        private readonly CameraMovement _movement;
        private readonly CameraPrefabData _data;

        public CameraObject(CameraPrefabData data, CameraMovement movement)
        {
            _data = data;
            _movement = movement;
        }

        public void Move(Vector2 direction) => _movement.Move(direction);

        public void Zoom(bool isIncrease) => _movement.Zoom(isIncrease);

        public Ray GetRay(Vector3 screenPoint) => _data.Camera.ScreenPointToRay(screenPoint);
    }
}
using CodeBase.Gameplay.Camera.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class CameraObject : ICameraController
    {
        private readonly CameraMovement _movement;

        public CameraObject(CameraPrefabData data, CameraMovement movement)
        {
            Data = data;
            _movement = movement;
        }

        public CameraPrefabData Data { get; }

        public void Move(Vector2 direction) => _movement.Move(direction);

        public void Zoom(bool isIncrease) => _movement.Zoom(isIncrease);
    }
}
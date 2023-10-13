using CodeBase.Gameplay.Camera.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public interface ICameraController
    {
        public CameraPrefabData Data { get; }

        public void Move(Vector2 direction);

        public void Zoom(bool isIncrease);
    }
}
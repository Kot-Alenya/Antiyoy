using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public interface ICameraController
    {
        public void Move(Vector2 direction);

        public void Zoom(bool isIncrease);

        public Ray GetRay(Vector3 screenPoint);
    }
}
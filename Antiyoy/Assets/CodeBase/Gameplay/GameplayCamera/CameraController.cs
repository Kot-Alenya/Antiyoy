using UnityEngine;

namespace CodeBase.Gameplay.GameplayCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraMovement _movement;

        public void Initialize() => _movement.Initialize();

        public void Move(Vector2 direction) => _movement.Move(direction);

        public void Zoom(bool isIncrease) => _movement.Zoom(isIncrease);
    }
}
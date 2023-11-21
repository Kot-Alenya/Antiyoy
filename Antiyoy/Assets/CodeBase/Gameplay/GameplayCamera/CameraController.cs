using UnityEngine;

namespace CodeBase.Gameplay.GameplayCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraMovement _movement;

        public void Initialize() => _movement.Initialize();

        public void Move(Vector2 direction) => _movement.Move(direction);

        public void Zoom(bool isIncrease) => _movement.Zoom(isIncrease);

        public RaycastHit2D RaycastFromMousePosition()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            return Physics2D.Raycast(ray.origin, ray.direction);
        }
    }
}
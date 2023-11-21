using CodeBase.Gameplay.Infrastructure;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.GameplayCamera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private GameplayStaticDataProvider _staticDataProvider;
        private CameraConfig _config;

        [Inject]
        public void Constructor(GameplayStaticDataProvider gameplayStaticDataProvider) =>
            _staticDataProvider = gameplayStaticDataProvider;

        public void Initialize() => _config = _staticDataProvider.GetCameraConfig();

        public void Move(Vector2 direction)
        {
            var currentPosition = _camera.transform.position;
            var nextPosition = currentPosition + (Vector3)direction;
            var newPosition = Vector3.Lerp(currentPosition, nextPosition, _config.MoveVelocity * Time.fixedDeltaTime);

            _camera.transform.position = newPosition;
        }

        public void Zoom(bool isIncrease)
        {
            var offset = isIncrease ? -_config.ZoomVelocity : _config.ZoomVelocity;
            var newSize = _camera.orthographicSize + offset;

            _camera.orthographicSize = Mathf.Clamp(newSize, 1, 10);
        }
    }
}
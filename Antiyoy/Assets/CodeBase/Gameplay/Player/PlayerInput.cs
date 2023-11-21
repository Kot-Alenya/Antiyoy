using CodeBase.Gameplay.GameplayCamera;
using CodeBase.Gameplay.Tile;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private CameraProvider _cameraProvider;
        private TileFactory _tileFactory;
        private CameraController _cameraController;

        [Inject]
        public void Construct(CameraProvider cameraProvider, TileFactory tileFactory)
        {
            _cameraProvider = cameraProvider;
            _tileFactory = tileFactory;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                CreateTile();

            if (Input.GetMouseButtonDown(1))
                DestroyTile();
        }

        private void CreateTile()
        {
            var hit = _cameraProvider.GetController().RaycastFromMousePosition();

            if (hit.transform == null)
                return;

            if (hit.transform.TryGetComponent<TilePlace>(out var tilePlace))
                _tileFactory.CreateTile(tilePlace);
        }

        private void DestroyTile()
        {
            var hit = _cameraProvider.GetController().RaycastFromMousePosition();

            if (hit.transform == null)
                return;

            if (hit.transform.TryGetComponent<TilePlace>(out var tilePlace))
                _tileFactory.DestroyTile(tilePlace);
        }
    }
}
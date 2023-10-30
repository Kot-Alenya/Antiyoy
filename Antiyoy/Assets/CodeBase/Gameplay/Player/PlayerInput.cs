using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Player.Controller;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private ICameraController _cameraController;
        private IPlayerController _playerController;
        private ITileCollection _tileCollection;

        [Inject]
        private void Construct(ICameraController cameraController, IPlayerController playerController,
            ITileCollection tileCollection)
        {
            _cameraController = cameraController;
            _playerController = playerController;
            _tileCollection = tileCollection;
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButton(0))
            {
                if (TryGetTile(out var tile))
                    _playerController.SelectTile(tile);
                else
                    _playerController.UnSelectTile();
            }
        }

        private bool TryGetTile(out TileData tile)
        {
            var ray = _cameraController.GetRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.transform == default)
            {
                tile = default;
                return false;
            }

            return _tileCollection.TryGet(HexMath.FromWorldPosition(hit.point), out tile);
        }
    }
}
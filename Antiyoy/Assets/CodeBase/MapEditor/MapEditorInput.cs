using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Data.Hex;
using UnityEngine;
using Zenject;

namespace CodeBase.MapEditor
{
    public class MapEditorInput : MonoBehaviour
    {
        private IMapEditorController _mapEditorController;
        private ICameraController _cameraController;

        [Inject]
        private void Construct(ICameraController cameraController, IMapEditorController mapEditorController)
        {
            _cameraController = cameraController;
            _mapEditorController = mapEditorController;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
                _mapEditorController.ProcessTiles();

            if (!Input.GetMouseButton(0))
                return;

            var ray = _cameraController.GetRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.transform == default)
                return;

            _mapEditorController.SelectTile(HexMath.FromWorldPosition(hit.point));
        }
    }
}
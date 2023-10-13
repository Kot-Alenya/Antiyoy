using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Data.Hex;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorInput : MonoBehaviour
    {
        private MapEditorController _mapEditorController;
        private ICameraController _cameraController;

        public void Construct(ICameraController cameraController, MapEditorController controller)
        {
            _cameraController = cameraController;
            _mapEditorController = controller;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
                _mapEditorController.ProcessTiles();

            if (!Input.GetMouseButton(0))
                return;

            var ray = _cameraController.Data.Camera.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.transform == default)
                return;

            _mapEditorController.SelectTile(HexMath.FromWorldPosition(hit.point));
        }
    }
}
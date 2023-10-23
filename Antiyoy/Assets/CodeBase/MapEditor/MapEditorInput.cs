using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Hex;
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
            ReturnWorld();

            if (Input.GetMouseButtonUp(0))
                _mapEditorController.ProcessTiles();

            if (Input.GetMouseButton(0))
            {
                var ray = _cameraController.GetRay(Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.transform == default)
                    return;

                _mapEditorController.SelectTile(HexMath.FromWorldPosition(hit.point));
            }
        }

        private void ReturnWorld()
        {
            if (!Input.GetKey(KeyCode.LeftControl) || Input.GetMouseButton(0))
                return;

            if (Input.GetKeyDown(KeyCode.Z))
                _mapEditorController.ReturnBack();
            else if (Input.GetKeyDown(KeyCode.Y))
                _mapEditorController.ReturnNext();
        }
    }
}
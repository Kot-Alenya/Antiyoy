using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorInput : MonoBehaviour
    {
        private CameraObject _cameraObject;
        private MapEditorController _controller;

        public void Constructor(CameraObject cameraObject, MapEditorController controller)
        {
            _cameraObject = cameraObject;
            _controller = controller;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
                _controller.ProcessTiles();

            if (!Input.GetMouseButtonDown(0))
                return;

            var ray = _cameraObject.Data.Camera.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.transform == null)
                return;

            if (hit.transform.TryGetComponent<TileObject>(out var tile))
                _controller.SelectTile(tile, hit.point);

            //как регулировать backround?
            //terrain.SetBackground();
            //создаём террейн тайлов, но эти тайлы 
        }
    }
}
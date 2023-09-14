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
            //всё поле чёрное, но, места для тайлов - серые.
            //тоесть есть дефолтный цывет тайлов.
            //нейтральный регион - тоже регион.
            //расчёт поля прост => мы можем просто создавать тайлы и удалять их!
            
            //при нажатии на мышь, указываем регионы.
            
            if (!Input.GetMouseButtonDown(0))
                return;

            var ray = _cameraObject.Data.Camera.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.transform == null)
                return;

            if (hit.transform.TryGetComponent<TileObject>(out var tile))
                _controller.ProcessTile(tile);
        }
    }
}
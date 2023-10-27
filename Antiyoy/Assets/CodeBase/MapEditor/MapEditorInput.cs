using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.MapEditor.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.MapEditor
{
    public class MapEditorInput : MonoBehaviour
    {
        private IMapEditorController _mapEditorController;
        private ICameraController _cameraController;
        private IStaticDataProvider _staticDataProvider;

        [Inject]
        private void Construct(ICameraController cameraController, IMapEditorController mapEditorController,IStaticDataProvider staticDataProvider)
        {
            _cameraController = cameraController;
            _mapEditorController = mapEditorController;
            _staticDataProvider = staticDataProvider;
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
            var staticData = _staticDataProvider.Get<MapEditorStaticData>();

            if (!Input.GetKey(staticData.ReturnWorldFirstKey) || Input.GetMouseButton(0))
                return;

            if (Input.GetKeyDown(staticData.ReturnWorldBackSecondKey))
                _mapEditorController.ReturnBack();
            else if (Input.GetKeyDown(staticData.ReturnWorldNextSecondKey))
                _mapEditorController.ReturnNext();
        }
    }
}
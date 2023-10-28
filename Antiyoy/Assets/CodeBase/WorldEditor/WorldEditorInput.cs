using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Hex;
using CodeBase.Infrastructure.Project.Services.StaticData;
using CodeBase.WorldEditor.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.WorldEditor
{
    public class WorldEditorInput : MonoBehaviour
    {
        private IWorldEditorController _worldEditorController;
        private ICameraController _cameraController;
        private IStaticDataProvider _staticDataProvider;

        [Inject]
        private void Construct(ICameraController cameraController, IWorldEditorController worldEditorController,
            IStaticDataProvider staticDataProvider)
        {
            _cameraController = cameraController;
            _worldEditorController = worldEditorController;
            _staticDataProvider = staticDataProvider;
        }

        private void Update()
        {
            ReturnWorld();

            if (Input.GetMouseButtonUp(0))
                _worldEditorController.ProcessTiles();

            if (Input.GetMouseButton(0))
            {
                var ray = _cameraController.GetRay(Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.transform == default)
                    return;

                _worldEditorController.SelectTile(HexMath.FromWorldPosition(hit.point));
            }
        }

        private void ReturnWorld()
        {
            var staticData = _staticDataProvider.Get<WorldEditorStaticData>();

            if (!Input.GetKey(staticData.ReturnWorldFirstKey) || Input.GetMouseButton(0))
                return;

            if (Input.GetKeyDown(staticData.ReturnWorldBackSecondKey))
                _worldEditorController.ReturnBack();
            else if (Input.GetKeyDown(staticData.ReturnWorldNextSecondKey))
                _worldEditorController.ReturnNext();
        }
    }
}
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Data.Hex;
using UnityEngine;
using Zenject;

namespace CodeBase.Dev.DebugWindow
{
    public class DebugWindowInput : MonoBehaviour
    {
        private ICameraController _cameraController;
        private IWorldController _worldController;
        private IDebugWindowController _debugWindowController;

        [Inject]
        private void Construct(ICameraController cameraController, IWorldController worldController,
            IDebugWindowController debugWindowController)
        {
            _cameraController = cameraController;
            _worldController = worldController;
            _debugWindowController = debugWindowController;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(1))
                return;

            var ray = _cameraController.GetRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            var window = _debugWindowController.Window;

            if (hit.transform == default)
            {
                window.Close();
                return;
            }

            var hitHex = HexMath.FromWorldPosition(hit.point);

            if (_worldController.Terrain.TryGetTile(hitHex, out var tile))
            {
                window.Open();
                window.UpdateInformation(tile);
            }
            else
                window.Close();
        }
    }
}
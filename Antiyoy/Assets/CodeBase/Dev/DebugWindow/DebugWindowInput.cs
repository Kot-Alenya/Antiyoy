using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using UnityEngine;
using Zenject;

namespace CodeBase.Dev.DebugWindow
{
    public class DebugWindowInput : MonoBehaviour
    {
        private ICameraController _cameraController;
        private ITerrain _terrain;
        private IDebugWindowController _debugWindowController;

        [Inject]
        private void Construct(ICameraController cameraController, ITerrain terrain,
            IDebugWindowController debugWindowController)
        {
            _cameraController = cameraController;
            _terrain = terrain;
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
                window.Hide();
                return;
            }

            var hitHex = HexMath.FromWorldPosition(hit.point);

            if (_terrain.TryGetTile(hitHex, out var tile))
            {
                window.Show();
                window.UpdateInformation(tile);
            }
            else
                window.Hide();
        }
    }
}
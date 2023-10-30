﻿using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using UnityEngine;
using Zenject;

namespace CodeBase.Dev.DebugWindow
{
    public class DebugWindowInput : MonoBehaviour
    {
        private ICameraController _cameraController;
        private ITileCollection _tileCollection;
        private IDebugWindowController _debugWindowController;

        [Inject]
        private void Construct(ICameraController cameraController, ITileCollection tileCollection,
            IDebugWindowController debugWindowController)
        {
            _cameraController = cameraController;
            _tileCollection = tileCollection;
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

            if (_tileCollection.TryGet(hitHex, out var tile))
            {
                window.Show();
                window.UpdateInformation(tile);
            }
            else
                window.Hide();
        }
    }
}
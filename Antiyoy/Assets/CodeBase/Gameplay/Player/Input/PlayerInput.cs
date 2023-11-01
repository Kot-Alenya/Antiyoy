using System;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Hex;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.Gameplay.Player.Input
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        public event Action<HexPosition> OnPlayerInput;

        private ICameraController _cameraController;

        [Inject]
        private void Construct(ICameraController cameraController) => _cameraController = cameraController;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                var ray = _cameraController.GetRay(UnityEngine.Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction);
                var hitPoint = hit.transform == default ? Vector2.one * float.MinValue : hit.point;

                OnPlayerInput?.Invoke(HexMath.FromWorldPosition(hitPoint));
            }
        }
    }
}
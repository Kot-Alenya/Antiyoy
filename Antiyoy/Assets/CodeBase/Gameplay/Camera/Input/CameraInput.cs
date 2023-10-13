using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera.Input
{
    public abstract class CameraInput : MonoBehaviour
    {
        private protected ICameraController CameraController;

        [Inject]
        private void Constructor(ICameraController cameraController) => CameraController = cameraController;
    }
}
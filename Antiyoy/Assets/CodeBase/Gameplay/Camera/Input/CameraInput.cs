using UnityEngine;

namespace CodeBase.Gameplay.Camera.Input
{
    public abstract class CameraInput : MonoBehaviour
    {
        private protected ICameraController CameraController;

        public void Constructor(ICameraController cameraController) => CameraController = cameraController;
    }
}
using UnityEngine;

namespace CodeBase.Gameplay.GameplayCamera.CameraInput
{
    public class CameraMouseInput : CameraInputBase
    {
        private void Update() => Zoom();

        private void Zoom()
        {
            var mouseScrollValue = Input.GetAxis("Mouse ScrollWheel");

            if (mouseScrollValue > 0.1)
                CameraController.Zoom(true);
            else if (mouseScrollValue < -0.1)
                CameraController.Zoom(false);
        }
    }
}
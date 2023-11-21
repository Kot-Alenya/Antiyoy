using UnityEngine;

namespace CodeBase.Gameplay.PlayerCamera.CameraInput
{
    public class CameraKeyboardInput : CameraInputBase
    {
        private void Update() => Move();

        private void Move()
        {
            if (Input.GetKey(KeyCode.W))
                CameraController.Move(Vector2.up);
            if (Input.GetKey(KeyCode.S))
                CameraController.Move(Vector2.down);
            if (Input.GetKey(KeyCode.A))
                CameraController.Move(Vector2.left);
            if (Input.GetKey(KeyCode.D))
                CameraController.Move(Vector2.right);
        }
    }
}
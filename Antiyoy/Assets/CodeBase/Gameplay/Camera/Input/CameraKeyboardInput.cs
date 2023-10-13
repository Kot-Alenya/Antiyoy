using UnityEngine;

namespace CodeBase.Gameplay.Camera.Input
{
    public class CameraKeyboardInput : CameraInput
    {
        private void Update() => Move();

        private void Move()
        {
            if (UnityEngine.Input.GetKey(KeyCode.W))
                CameraController.Move(Vector2.up);
            if (UnityEngine.Input.GetKey(KeyCode.S))
                CameraController.Move(Vector2.down);
            if (UnityEngine.Input.GetKey(KeyCode.A))
                CameraController.Move(Vector2.left);
            if (UnityEngine.Input.GetKey(KeyCode.D))
                CameraController.Move(Vector2.right);
        }
    }
}
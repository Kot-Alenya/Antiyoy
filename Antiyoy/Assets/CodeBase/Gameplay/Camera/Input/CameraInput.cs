using UnityEngine;

namespace CodeBase.Gameplay.Camera.Input
{
    public abstract class CameraInput : MonoBehaviour
    {
        private protected CameraObject CameraObject;

        public void Constructor(CameraObject cameraObject) => CameraObject = cameraObject;
    }
}
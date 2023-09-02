namespace CodeBase.Gameplay.Camera.Input
{
    public class CameraMouseInput : CameraInput
    {
        private void Update() => Zoom();

        private void Zoom()
        {
            var mouseScrollValue = UnityEngine.Input.GetAxis("Mouse ScrollWheel");

            if (mouseScrollValue > 0.1)
                CameraObject.Zoom(true);
            else if (mouseScrollValue < -0.1)
                CameraObject.Zoom(false);
        }
    }
}
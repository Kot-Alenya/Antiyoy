namespace CodeBase.Gameplay.GameplayCamera
{
    public class CameraProvider
    {
        private CameraController _controller;

        public void Initialize(CameraController controller) => _controller = controller;

        public CameraController GetController() => _controller;
    }
}
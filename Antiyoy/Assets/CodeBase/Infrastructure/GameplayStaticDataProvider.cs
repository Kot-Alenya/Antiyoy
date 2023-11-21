using CodeBase.Gameplay.GameplayCamera;

namespace CodeBase.Infrastructure
{
    public class GameplayStaticDataProvider
    {
        private readonly CameraConfig _cameraConfig;

        public GameplayStaticDataProvider(CameraConfig cameraConfig) => _cameraConfig = cameraConfig;

        public CameraConfig GetCameraConfig() => _cameraConfig;
    }
}
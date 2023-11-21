using System;
using CodeBase.Gameplay.GameplayCamera;
using UnityEngine;

namespace CodeBase.Gameplay.Infrastructure
{
    [Serializable]
    public class GameplayStaticDataProvider
    {
        [SerializeField] private CameraConfig _cameraConfig;

        public CameraConfig GetCameraConfig() => _cameraConfig;
    }
}
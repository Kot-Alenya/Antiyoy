using System;
using CodeBase.Gameplay.GameplayCamera;
using CodeBase.Gameplay.Terrain;
using UnityEngine;

namespace CodeBase.Gameplay.Infrastructure
{
    [Serializable]
    public class GameplayStaticDataProvider
    {
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private TerrainConfig _terrainConfig;

        public CameraConfig GetCameraConfig() => _cameraConfig;

        public TerrainConfig GetTerrainConfig() => _terrainConfig;
    }
}
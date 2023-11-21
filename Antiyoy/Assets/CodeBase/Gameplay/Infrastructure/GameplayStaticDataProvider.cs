using System;
using CodeBase.Gameplay.Ecs;
using CodeBase.Gameplay.PlayerCamera;
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

        public GameplayEcsConfig GetEcsWorldConfig()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.PlayerCamera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Infrastructure
{
    [Serializable]
    public class GameplayStaticDataProvider
    {
        [SerializeField] private GameplayConfig _config;

        public CameraConfig GetCameraConfig() => _config.CameraConfig;

        public TerrainConfig GetTerrainConfig() => _config.TerrainConfig;

        public GameplayEcsConfig GetEcsWorldConfig() => _config.EcsConfig;

        public TileConfig GetTileConfig() => _config.TileConfig;
    }
}
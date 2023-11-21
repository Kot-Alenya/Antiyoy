using System;
using CodeBase.Gameplay.GameplayCamera;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.ProjectInfrastructure
{
    [Serializable]
    public class StaticData
    {
        public TileStaticData TileStaticData;
        public TerrainStaticData TerrainStaticData;
        public CameraConfig CameraConfig;
    }
}
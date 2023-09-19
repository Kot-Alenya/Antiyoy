using System;
using CodeBase.Gameplay.Camera.Data;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Infrastructure
{
    [Serializable]
    public class StaticData
    {
        public TileStaticData TileStaticData;
        public TerrainStaticData TerrainStaticData;
        public CameraStaticData CameraStaticData;
        public RegionStaticData RegionStaticData;
    }
}
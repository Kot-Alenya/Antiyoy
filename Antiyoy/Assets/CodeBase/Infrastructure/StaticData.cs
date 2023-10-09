using System;
using CodeBase.Gameplay.Camera.Data;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Tile.Data;

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
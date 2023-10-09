using System;
using CodeBase.Gameplay.Camera.Data;
using CodeBase.Gameplay.Map.Data;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile.Data;

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
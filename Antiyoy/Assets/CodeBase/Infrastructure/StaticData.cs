using System;
using CodeBase.Gameplay.Camera.Data;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Tile.Data;

namespace CodeBase.Infrastructure
{
    [Serializable]
    public class StaticData
    {
        public TileStaticData TileStaticData;
        public TerrainStaticData TerrainStaticData;
        public CameraStaticData CameraStaticData;
    }
}
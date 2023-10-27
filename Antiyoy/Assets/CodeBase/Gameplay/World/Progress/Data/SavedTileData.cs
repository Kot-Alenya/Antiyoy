using System;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World
{
    [Serializable]
    public class SavedTileData
    {
        public SavedHexPosition Hex;
        public RegionType RegionType;
        public EntityType EntityType;
    }
}
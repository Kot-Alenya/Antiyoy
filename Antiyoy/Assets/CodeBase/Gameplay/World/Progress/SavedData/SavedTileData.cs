using System;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.World.Progress.SavedData
{
    [Serializable]
    public struct SavedTileData
    {
        public SavedHexPosition Hex;
        public RegionType RegionType;
        public UnitType UnitType;
    }
}
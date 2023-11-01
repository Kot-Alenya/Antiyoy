using System;
using CodeBase.Gameplay.World.Region;

namespace CodeBase.Gameplay.World.Progress.SavedData
{
    [Serializable]
    public struct SavedTileData
    {
        public SavedHexPosition Hex;
        public RegionType RegionType;
        public EntityType EntityType;
    }
}
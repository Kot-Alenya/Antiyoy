﻿using System;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;

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
﻿using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;

namespace CodeBase.Gameplay.World.Version.Operation
{
    public readonly struct WorldCreateTileOperationData : IWorldOperationData
    {
        public readonly HexPosition Hex;
        public readonly RegionType RegionType;

        public WorldCreateTileOperationData(HexPosition hex, RegionType regionType)
        {
            Hex = hex;
            RegionType = regionType;
        }
    }
}
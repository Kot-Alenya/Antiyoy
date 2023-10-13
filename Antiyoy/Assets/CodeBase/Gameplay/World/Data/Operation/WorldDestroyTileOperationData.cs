using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;

namespace CodeBase.Gameplay.World.Data.Operation
{
    public readonly struct WorldDestroyTileOperationData : IWorldOperationData
    {
        public readonly HexPosition Hex;
        public readonly RegionType RegionType;

        public WorldDestroyTileOperationData(HexPosition hex, RegionType regionType)
        {
            Hex = hex;
            RegionType = regionType;
        }
    }
}
using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;

namespace CodeBase.Gameplay.World.Data.Operation
{
    public struct WorldDestroyTileOperationData : IWorldOperationData
    {
        public HexPosition Hex;
        public RegionType RegionType;

        public WorldDestroyTileOperationData(HexPosition hex, RegionType regionType)
        {
            Hex = hex;
            RegionType = regionType;
        }
    }
}
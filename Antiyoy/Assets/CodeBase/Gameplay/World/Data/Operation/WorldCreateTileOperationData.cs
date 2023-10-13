using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;

namespace CodeBase.Gameplay.World.Data.Operation
{
    public struct WorldCreateTileOperationData : IWorldOperationData
    {
        public HexPosition Hex;
        public RegionType RegionType;

        public WorldCreateTileOperationData(HexPosition hex, RegionType regionType)
        {
            Hex = hex;
            RegionType = regionType;
        }
    }
}
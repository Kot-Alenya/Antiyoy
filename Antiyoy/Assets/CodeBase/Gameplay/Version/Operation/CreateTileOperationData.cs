using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Region.Data;

namespace CodeBase.Gameplay.Version.Operation
{
    public readonly struct CreateTileOperationData : IOperationData
    {
        public readonly HexPosition Hex;
        public readonly RegionType RegionType;

        public CreateTileOperationData(HexPosition hex, RegionType regionType)
        {
            Hex = hex;
            RegionType = regionType;
        }
    }
}
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;

namespace CodeBase.Gameplay.World.Version.Operation
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
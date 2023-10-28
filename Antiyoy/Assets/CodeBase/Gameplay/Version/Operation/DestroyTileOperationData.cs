using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Region.Data;

namespace CodeBase.Gameplay.Version.Operation
{
    public readonly struct DestroyTileOperationData : IOperationData
    {
        public readonly HexPosition Hex;
        public readonly RegionType RegionType;

        public DestroyTileOperationData(HexPosition hex, RegionType regionType)
        {
            Hex = hex;
            RegionType = regionType;
        }
    }
}
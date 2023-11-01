using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Version.Operation
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
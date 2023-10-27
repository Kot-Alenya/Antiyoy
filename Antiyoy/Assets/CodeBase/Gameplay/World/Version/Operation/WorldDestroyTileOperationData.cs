using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Version.Operation
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
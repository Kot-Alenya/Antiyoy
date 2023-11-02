using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World.Terrain.Tile.Operation
{
    public readonly struct TileOperationData : IWorldVersionOperationData
    {
        public readonly HexPosition Hex;
        public readonly RegionType RegionType;

        public TileOperationData(HexPosition hex, RegionType regionType, IWorldVersionOperationHandler handler)
        {
            Hex = hex;
            RegionType = regionType;
            Handler = handler;
        }

        public IWorldVersionOperationHandler Handler { get; }
    }
}
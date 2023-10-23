using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Region.Data;

namespace CodeBase.Gameplay.World.Data.Operation
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
    
    public readonly struct WorldCreateEntityOperationData : IWorldOperationData
    {
        public readonly HexPosition Hex;
        public readonly EntityType EntityType;

        public WorldCreateEntityOperationData(HexPosition hex, EntityType entityType)
        {
            Hex = hex;
            EntityType = entityType;
        }
    }
}
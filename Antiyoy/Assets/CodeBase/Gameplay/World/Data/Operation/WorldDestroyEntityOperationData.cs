using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Entity.Data;

namespace CodeBase.Gameplay.World.Data.Operation
{
    public readonly struct WorldDestroyEntityOperationData : IWorldOperationData
    {
        public readonly HexPosition Hex;
        public readonly EntityType EntityType;

        public WorldDestroyEntityOperationData(HexPosition hex, EntityType entityType)
        {
            Hex = hex;
            EntityType = entityType;
        }
    }
}
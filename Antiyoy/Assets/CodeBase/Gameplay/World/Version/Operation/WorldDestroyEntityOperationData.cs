using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;

namespace CodeBase.Gameplay.World.Version.Operation
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
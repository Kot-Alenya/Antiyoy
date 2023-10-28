using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Entity.Data;

namespace CodeBase.Gameplay.Version.Operation
{
    public readonly struct CreateEntityOperationData : IOperationData
    {
        public readonly HexPosition Hex;
        public readonly EntityType EntityType;

        public CreateEntityOperationData(HexPosition hex, EntityType entityType)
        {
            Hex = hex;
            EntityType = entityType;
        }
    }
}
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World.Terrain.Entity.Operation
{
    public readonly struct EntityOperationData : IWorldVersionOperationData
    {
        public readonly HexPosition Hex;
        public readonly EntityType EntityType;

        public EntityOperationData(HexPosition hex, EntityType entityType, IWorldVersionOperationHandler handler)
        {
            Hex = hex;
            Handler = handler;
            EntityType = entityType;
        }

        public IWorldVersionOperationHandler Handler { get; }
    }
}
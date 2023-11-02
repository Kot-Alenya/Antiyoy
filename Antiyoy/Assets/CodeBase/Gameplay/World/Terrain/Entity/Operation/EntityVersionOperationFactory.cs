using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;

namespace CodeBase.Gameplay.World.Terrain.Entity.Operation
{
    public class EntityVersionOperationFactory
    {
        private readonly CreateEntityOperationHandler _createEntityOperationHandler;
        private readonly DestroyEntityOperationHandler _destroyEntityOperationHandler;

        public EntityVersionOperationFactory(CreateEntityOperationHandler createEntityOperationHandler,
            DestroyEntityOperationHandler destroyEntityOperationHandler)
        {
            _createEntityOperationHandler = createEntityOperationHandler;
            _destroyEntityOperationHandler = destroyEntityOperationHandler;
        }

        public EntityOperationData GetCreateOperation(HexPosition hex, EntityType entityType) =>
            new(hex, entityType, _createEntityOperationHandler);

        public EntityOperationData GetDestroyOperation(HexPosition hex, EntityType entityType) =>
            new(hex, entityType, _destroyEntityOperationHandler);
    }
}
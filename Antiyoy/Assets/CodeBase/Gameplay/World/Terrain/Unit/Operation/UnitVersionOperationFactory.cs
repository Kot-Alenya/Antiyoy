using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;

namespace CodeBase.Gameplay.World.Terrain.Entity.Operation
{
    public class UnitVersionOperationFactory
    {
        private readonly CreateUnitOperationHandler _createUnitOperationHandler;
        private readonly DestroyUnitOperationHandler _destroyUnitOperationHandler;

        public UnitVersionOperationFactory(CreateUnitOperationHandler createUnitOperationHandler,
            DestroyUnitOperationHandler destroyUnitOperationHandler)
        {
            _createUnitOperationHandler = createUnitOperationHandler;
            _destroyUnitOperationHandler = destroyUnitOperationHandler;
        }

        public UnitOperationData GetCreateOperation(HexPosition hex, UnitType unitType) =>
            new(hex, unitType, _createUnitOperationHandler);

        public UnitOperationData GetDestroyOperation(HexPosition hex, UnitType unitType) =>
            new(hex, unitType, _destroyUnitOperationHandler);
    }
}
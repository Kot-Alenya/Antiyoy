using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.World.Terrain.Unit.Operation
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

        public UnitOperationData GetCreateOperation(HexPosition hex, UnitType unitType, bool isCanMove) =>
            new(hex, unitType, _createUnitOperationHandler, isCanMove);

        public UnitOperationData GetDestroyOperation(HexPosition hex, UnitType unitType, bool isCanMove) =>
            new(hex, unitType, _destroyUnitOperationHandler, isCanMove);
    }
}
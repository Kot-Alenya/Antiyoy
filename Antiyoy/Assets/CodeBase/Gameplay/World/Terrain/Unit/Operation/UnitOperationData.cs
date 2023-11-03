using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World.Terrain.Unit.Operation
{
    public readonly struct UnitOperationData : IWorldVersionOperationData
    {
        public readonly HexPosition Hex;
        public readonly UnitType UnitType;
        public readonly bool IsCanMove;

        public UnitOperationData(HexPosition hex, UnitType unitType, IWorldVersionOperationHandler handler,
            bool isCanMove)
        {
            Hex = hex;
            Handler = handler;
            IsCanMove = isCanMove;
            UnitType = unitType;
        }

        public IWorldVersionOperationHandler Handler { get; }
    }
}
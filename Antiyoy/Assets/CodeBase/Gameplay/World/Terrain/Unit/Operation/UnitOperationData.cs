using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World.Terrain.Entity.Operation
{
    public readonly struct UnitOperationData : IWorldVersionOperationData
    {
        public readonly HexPosition Hex;
        public readonly UnitType UnitType;

        public UnitOperationData(HexPosition hex, UnitType unitType, IWorldVersionOperationHandler handler)
        {
            Hex = hex;
            Handler = handler;
            UnitType = unitType;
        }

        public IWorldVersionOperationHandler Handler { get; }
    }
}
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Unit
{
    public readonly struct PlayerCreateUnitStateData : IStateParameter
    {
        public readonly UnitType UnitType;

        public PlayerCreateUnitStateData(UnitType unitType) => UnitType = unitType;
    }
}
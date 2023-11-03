using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Entity
{
    public readonly struct PlayerCreateUnitStateData : IStateParameter
    {
        public readonly UnitType UnitType;

        public PlayerCreateUnitStateData(UnitType unitType) => UnitType = unitType;
    }
}
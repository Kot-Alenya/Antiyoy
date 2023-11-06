using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Unit.Move
{
    public readonly struct PlayerMoveUnitStateData : IStateParameter
    {
        public readonly UnitData_new Unit;

        public PlayerMoveUnitStateData(UnitData_new unit) => Unit = unit;
    }
}
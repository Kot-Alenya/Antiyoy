using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Unit.Move
{
    public readonly struct PlayerMoveUnitStateData : IStateParameter
    {
        public readonly UnitData Unit;

        public PlayerMoveUnitStateData(UnitData unit) => Unit = unit;
    }
}
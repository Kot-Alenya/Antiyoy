using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Region;
using CodeBase.Gameplay.Player.States.Unit.Move;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States
{
    public class PlayerDefaultState : IEnterState, IExitState
    {
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly IPlayerModel _playerModel;
        private readonly IPlayerInput _playerInput;
        private readonly ITerrain _terrain;

        public PlayerDefaultState(IPlayerModel playerModel, PlayerStateMachine playerStateMachine,
            IPlayerInput playerInput, ITerrain terrain)
        {
            _playerModel = playerModel;
            _playerInput = playerInput;
            _terrain = terrain;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter() => _playerInput.OnPlayerInput += HandleInput;

        public void Exit() => _playerInput.OnPlayerInput -= HandleInput;

        private void HandleInput(HexPosition hex)
        {
            if (!_terrain.TryGetTile(hex, out var tile))
                return;

            if (tile.Region.Type != _playerModel.PersistenceRegionType)
                return;

            if (tile.Unit != null && tile.Unit.IsCanMove)
                _playerStateMachine.SwitchTo<PlayerMoveUnitState, PlayerMoveUnitStateData>(
                    new PlayerMoveUnitStateData(tile.Unit));
            else
                _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                    new PlayerSelectRegionStateData(tile.Region));
        }
    }
}
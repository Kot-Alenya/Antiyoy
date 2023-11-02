using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Region;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States
{
    public class PlayerDefaultState : IEnterState, IExitState
    {
        private readonly PlayerData _playerData;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly IPlayerInput _playerInput;
        private readonly ITerrain _terrain;

        public PlayerDefaultState(PlayerData playerData, PlayerStateMachine playerStateMachine,
            IPlayerInput playerInput, ITerrain terrain)
        {
            _playerInput = playerInput;
            _terrain = terrain;
            _playerData = playerData;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter() => _playerInput.OnPlayerInput += HandleInput;

        public void Exit() => _playerInput.OnPlayerInput -= HandleInput;

        private void HandleInput(HexPosition hex)
        {
            if (_terrain.TryGetTile(hex, out var tile))
                if (tile.Region.Type == _playerData.RegionType)
                    _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                        new PlayerSelectRegionStateData(tile.Region));
        }
    }
}
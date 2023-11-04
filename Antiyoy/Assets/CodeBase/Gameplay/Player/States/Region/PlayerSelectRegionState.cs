using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Unit.Move;
using CodeBase.Gameplay.Player.UI;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Region
{
    public class PlayerSelectRegionState : IEnterState<PlayerSelectRegionStateData>, IExitState
    {
        private readonly PlayerData _playerData;
        private readonly PlayerTerrainFocus _playerTerrainFocus;
        private readonly IPlayerUIMediator _uiMediator;
        private readonly IPlayerInput _playerInput;
        private readonly ITerrain _terrain;
        private readonly PlayerStateMachine _playerStateMachine;

        public PlayerSelectRegionState(PlayerData playerData, PlayerTerrainFocus playerTerrainFocus,
            IPlayerUIMediator uiMediator, IPlayerInput playerInput, ITerrain terrain,
            PlayerStateMachine playerStateMachine)
        {
            _playerData = playerData;
            _playerTerrainFocus = playerTerrainFocus;
            _uiMediator = uiMediator;
            _playerInput = playerInput;
            _terrain = terrain;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter(PlayerSelectRegionStateData parameter)
        {
            _playerData.CurrentRegion = parameter.Region;

            _playerTerrainFocus.ShowTilesOutline(parameter.Region.Tiles);

            _uiMediator.SetIncomeCount(parameter.Region.Income);
            _uiMediator.SetCoinsCount(parameter.Region.CoinsCount);
            _uiMediator.ShowUIWindow();

            _playerInput.OnPlayerInput += HandleInput;
        }

        public void Exit()
        {
            _playerInput.OnPlayerInput -= HandleInput;

            _playerTerrainFocus.HideAllTilesOutlines();
            _uiMediator.HideUIWindow();
        }

        private void HandleInput(HexPosition hex)
        {
            if (!_terrain.TryGetTile(hex, out var tile))
                _playerStateMachine.SwitchTo<PlayerDefaultState>();

            else if (tile.Region.Type != _playerData.RegionType)
                _playerStateMachine.SwitchTo<PlayerDefaultState>();

            else if (tile.Region != _playerData.CurrentRegion)
                _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                    new PlayerSelectRegionStateData(tile.Region));

            else if (tile.Unit.Type != UnitType.None && tile.Unit.IsCanMove)
                _playerStateMachine.SwitchTo<PlayerMoveUnitState, PlayerMoveUnitStateData>(
                    new PlayerMoveUnitStateData(tile.Unit));
        }
    }
}
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Unit.Move;
using CodeBase.Gameplay.Player.UI;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Region
{
    public class PlayerSelectRegionState : IEnterState<PlayerSelectRegionStateData>, IExitState
    {
        private readonly PlayerTerrainView _playerTerrainView;
        private readonly IPlayerUIMediator _uiMediator;
        private readonly IPlayerInput _playerInput;
        private readonly ITerrain _terrain;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly IPlayerModel _playerModel;
        private RegionData _currentRegion;

        public PlayerSelectRegionState(PlayerTerrainView playerTerrainView,
            IPlayerUIMediator uiMediator, IPlayerInput playerInput, ITerrain terrain,
            PlayerStateMachine playerStateMachine, IPlayerModel playerModel)
        {
            _playerTerrainView = playerTerrainView;
            _uiMediator = uiMediator;
            _playerInput = playerInput;
            _terrain = terrain;
            _playerStateMachine = playerStateMachine;
            _playerModel = playerModel;
        }

        public void Enter(PlayerSelectRegionStateData parameter)
        {
            _currentRegion = parameter.Region;

            _playerModel.SelectRegion(_currentRegion);

            ShowView();
            _playerInput.OnPlayerInput += HandleInput;
        }

        public void Exit()
        {
            HideView();
            _playerInput.OnPlayerInput -= HandleInput;
        }

        private void ShowView()
        {
            _playerTerrainView.ShowTilesOutline(_currentRegion.Tiles);
            _uiMediator.SetIncomeCount(_currentRegion.Income);
            _uiMediator.SetCoinsCount(_currentRegion.CoinsCount);
            _uiMediator.ShowUIWindow();
        }

        private void HideView()
        {
            _playerTerrainView.HideAllTilesOutlines();
            _uiMediator.HideUIWindow();
        }

        private void HandleInput(HexPosition hex)
        {
            if (!_terrain.TryGetTile(hex, out var tile))
                _playerStateMachine.SwitchTo<PlayerDefaultState>();

            else if (tile.Region.Type != _currentRegion.Type)
                _playerStateMachine.SwitchTo<PlayerDefaultState>();

            else if (tile.Region != _currentRegion)
                _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                    new PlayerSelectRegionStateData(tile.Region));

            else if (tile.Unit != null && tile.Unit.IsCanMove)
                _playerStateMachine.SwitchTo<PlayerMoveUnitState, PlayerMoveUnitStateData>(
                    new PlayerMoveUnitStateData(tile.Unit));
        }
    }
}
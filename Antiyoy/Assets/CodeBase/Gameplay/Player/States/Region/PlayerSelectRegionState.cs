using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.UI;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Region
{
    public class PlayerSelectRegionState : IEnterState<PlayerSelectRegionStateData>, IExitState
    {
        private readonly PlayerData _playerData;
        private readonly PlayerTerrainSelectionView _selectionView;
        private readonly IPlayerUIMediator _uiMediator;
        private readonly IPlayerInput _playerInput;
        private readonly ITileCollection _tileCollection;
        private readonly PlayerStateMachine _playerStateMachine;

        public PlayerSelectRegionState(PlayerData playerData, PlayerTerrainSelectionView selectionView,
            IPlayerUIMediator uiMediator, IPlayerInput playerInput, ITileCollection tileCollection,
            PlayerStateMachine playerStateMachine)
        {
            _playerData = playerData;
            _selectionView = selectionView;
            _uiMediator = uiMediator;
            _playerInput = playerInput;
            _tileCollection = tileCollection;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter(PlayerSelectRegionStateData parameter)
        {
            _playerData.CurrentRegion = parameter.Region;
            _selectionView.SelectRegion(parameter.Region);
            _uiMediator.SetIncomeCount(parameter.Region.Income);
            _uiMediator.SetCoinsCount(_playerData.CoinsCount);
            _uiMediator.ShowUIWindow();

            _playerInput.OnPlayerInput += HandleInput;
        }

        public void Exit()
        {
            _playerInput.OnPlayerInput -= HandleInput;
            
            _selectionView.UnSelectRegion(_playerData.CurrentRegion);
            _uiMediator.HideUIWindow();
        }

        private void HandleInput(HexPosition hex)
        {
            if (!_tileCollection.TryGet(hex, out var tile))
                _playerStateMachine.SwitchTo<PlayerDefaultState>();
            
            else if (tile.Region.Type != _playerData.RegionType)
                _playerStateMachine.SwitchTo<PlayerDefaultState>();
            
            else if (tile.Region != _playerData.CurrentRegion)
            {
                _playerStateMachine.SwitchTo<PlayerDefaultState>();
                _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                    new PlayerSelectRegionStateData(tile.Region));
            }
        }
    }
}
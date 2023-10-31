using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Region;
using CodeBase.Gameplay.Player.UI;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States
{
    public class PlayerDefaultState : IEnterState, IExitState
    {
        private readonly PlayerData _playerData;
        private readonly PlayerTerrainSelectionView _selectionView;
        private readonly IPlayerUIMediator _uiMediator;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly IPlayerInput _playerInput;
        private readonly ITileCollection _tileCollection;

        public PlayerDefaultState(PlayerData playerData, PlayerTerrainSelectionView selectionView,
            IPlayerUIMediator uiMediator, PlayerStateMachine playerStateMachine, IPlayerInput playerInput,
            ITileCollection tileCollection)
        {
            _playerInput = playerInput;
            _tileCollection = tileCollection;
            _playerData = playerData;
            _selectionView = selectionView;
            _uiMediator = uiMediator;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            if (_playerData.CurrentRegion != null)
                _selectionView.UnSelectRegion(_playerData.CurrentRegion);
            _uiMediator.HideUIWindow();

            _playerInput.OnPlayerInput += HandleInput;
        }

        public void Exit() => _playerInput.OnPlayerInput -= HandleInput;

        private void HandleInput(HexPosition hex)
        {
            if (_tileCollection.TryGet(hex, out var tile))
                if (tile.Region.Type == _playerData.RegionType)
                    _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                        new PlayerSelectRegionStateData(tile.Region));
        }
    }
}
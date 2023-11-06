using System.Collections.Generic;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Region;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Unit.Move
{
    public class PlayerMoveUnitState : IEnterState<PlayerMoveUnitStateData>, IExitState
    {
        private readonly IPlayerInput _playerInput;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerTerrainView _playerTerrainView;
        private readonly ITerrain _terrain;
        private readonly IPlayerModel _playerModel;

        private List<TileData> _unitTilesToMove;
        private UnitData _currentUnit;

        public PlayerMoveUnitState(IPlayerInput playerInput, ITerrain terrain,
            PlayerStateMachine playerStateMachine, PlayerTerrainView playerTerrainView, IPlayerModel playerModel)
        {
            _playerInput = playerInput;
            _playerStateMachine = playerStateMachine;
            _playerTerrainView = playerTerrainView;
            _playerModel = playerModel;
            _terrain = terrain;
        }

        public void Enter(PlayerMoveUnitStateData parameter)
        {
            _unitTilesToMove = _playerModel.GetTilesToMoveUnit(parameter.Unit);
            _playerModel.SelectUnit(parameter.Unit);

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
            _playerTerrainView.ShowShadowField();
            _playerTerrainView.SetTilesAboveShadowFiled(_unitTilesToMove);
            _playerTerrainView.ShowTilesOutline(_unitTilesToMove);
        }

        private void HideView()
        {
            _playerTerrainView.HideShadowField();
            _playerTerrainView.SetAllTilesUnderShadowField();
            _playerTerrainView.HideAllTilesOutlines();
        }

        private void HandleInput(HexPosition hex)
        {
            var currentRegion = _playerModel.SelectedRegion;

            if (_terrain.TryGetTile(hex, out var tile) && _unitTilesToMove.Contains(tile))
            {
                _playerModel.MoveUnit(tile);
                currentRegion = _terrain.GetTile(hex).Region;
            }

            _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                new PlayerSelectRegionStateData(currentRegion));
        }
    }
}
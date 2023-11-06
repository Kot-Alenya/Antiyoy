using System.Collections.Generic;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Region;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Unit.Create
{
    public class PlayerCreateUnitState : IEnterState<PlayerCreateUnitStateData>, IExitState
    {
        private readonly IPlayerInput _playerInput;
        private readonly PlayerTerrainView _playerTerrainView;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly ITerrain _terrain;
        private readonly IPlayerModel _playerModel;

        private List<TileData> _tilesToCreateUnit;
        private UnitType _unitTypeToCreate;

        public PlayerCreateUnitState(PlayerStateMachine playerStateMachine, IPlayerInput playerInput,
            PlayerTerrainView playerTerrainView, ITerrain terrain, IPlayerModel playerModel)
        {
            _playerInput = playerInput;
            _playerTerrainView = playerTerrainView;
            _playerStateMachine = playerStateMachine;
            _terrain = terrain;
            _playerModel = playerModel;
        }

        public void Enter(PlayerCreateUnitStateData parameter)
        {
            _tilesToCreateUnit = _playerModel.GetTilesToCreateUnit(parameter.UnitType);
            _unitTypeToCreate = parameter.UnitType;

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
            _playerTerrainView.SetTilesAboveShadowFiled(_tilesToCreateUnit);
            _playerTerrainView.ShowTilesOutline(_tilesToCreateUnit);
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

            if (_terrain.TryGetTile(hex, out var tile) && _tilesToCreateUnit.Contains(tile))
            {
                _playerModel.CreateUnit(tile, _unitTypeToCreate);
                currentRegion = _terrain.GetTile(hex).Region;
            }

            _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                new PlayerSelectRegionStateData(currentRegion));
        }
    }
}
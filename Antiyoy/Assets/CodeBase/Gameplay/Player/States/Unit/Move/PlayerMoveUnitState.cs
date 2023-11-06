﻿using System.Collections.Generic;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Region;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Gameplay.World.Version;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Unit.Move
{
    public class PlayerMoveUnitState : IEnterState<PlayerMoveUnitStateData>, IExitState
    {
        private readonly IPlayerInput _playerInput;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerTerrainFocus _playerTerrainFocus;
        private readonly WorldFactory _worldFactory;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly ITerrain _terrain;
        private readonly PlayerData _playerData;
        private readonly UnitStaticDataHelper _unitStaticDataHelper;

        private List<TileData> _unitTilesToMove;
        private UnitData_new _currentUnit;

        public PlayerMoveUnitState(IPlayerInput playerInput, ITerrain terrain, PlayerData playerData,
            PlayerStateMachine playerStateMachine, PlayerTerrainFocus playerTerrainFocus, WorldFactory worldFactory,
            WorldVersionRecorder worldVersionRecorder, UnitStaticDataHelper unitStaticDataHelper)
        {
            _playerInput = playerInput;
            _playerStateMachine = playerStateMachine;
            _playerTerrainFocus = playerTerrainFocus;
            _worldFactory = worldFactory;
            _worldVersionRecorder = worldVersionRecorder;
            _unitStaticDataHelper = unitStaticDataHelper;
            _terrain = terrain;
            _playerData = playerData;
        }

        public void Enter(PlayerMoveUnitStateData parameter)
        {
            _currentUnit = parameter.Unit;
            _unitTilesToMove = PlayerMoveUnitUtilities.GetTilesToMoveUnit(parameter.Unit, _unitStaticDataHelper);

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
            _playerTerrainFocus.ShowShadowField();
            _playerTerrainFocus.SetTilesAboveShadowFiled(_unitTilesToMove);
            _playerTerrainFocus.ShowTilesOutline(_unitTilesToMove);
        }

        private void HideView()
        {
            _playerTerrainFocus.HideShadowField();
            _playerTerrainFocus.SetAllTilesUnderShadowField();
            _playerTerrainFocus.HideAllTilesOutlines();
        }

        private void HandleInput(HexPosition hex)
        {
            var currentRegion = _playerData.CurrentRegion;

            if (_terrain.TryGetTile(hex, out var tile) && _unitTilesToMove.Contains(tile))
            {
                if (tile.Unit != null &&
                    PlayerCombineUnitUtilities.TryCombinedUnitType(_currentUnit.Type, tile.Unit.Type, out var type))
                    MoveUnit(tile, type, tile.Unit.IsCanMove);
                else
                    MoveUnit(tile, _currentUnit.Type, false);

                currentRegion = _terrain.GetTile(hex).Region;
            }

            _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                new PlayerSelectRegionStateData(currentRegion));
        }

        private void MoveUnit(TileData toTile, UnitType unitToCreateType, bool isCanMove)
        {
            _worldFactory.CreateTile(toTile.Hex, _playerData.RegionType);
            _worldFactory.CreateUnit(toTile.Hex, unitToCreateType, isCanMove);
            _worldFactory.TryDestroyUnit(_currentUnit.RootTile.Hex);
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
        }
    }
}
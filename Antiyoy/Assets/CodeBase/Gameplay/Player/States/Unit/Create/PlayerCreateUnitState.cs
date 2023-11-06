using System.Collections.Generic;
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

namespace CodeBase.Gameplay.Player.States.Unit.Create
{
    public class PlayerCreateUnitState : IEnterState<PlayerCreateUnitStateData>, IExitState
    {
        private readonly PlayerData _playerData;
        private readonly IPlayerInput _playerInput;
        private readonly PlayerTerrainFocus _playerTerrainFocus;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly WorldFactory _worldFactory;
        private readonly ITerrain _terrain;

        private UnitType _unitToCreateType;
        private List<TileData> _tilesToCreateUnit;

        public PlayerCreateUnitState(PlayerStateMachine playerStateMachine, PlayerData playerData,
            IPlayerInput playerInput, PlayerTerrainFocus playerTerrainFocus, WorldVersionRecorder worldVersionRecorder,
            WorldFactory worldFactory, ITerrain terrain)
        {
            _playerInput = playerInput;
            _playerTerrainFocus = playerTerrainFocus;
            _playerData = playerData;
            _playerStateMachine = playerStateMachine;
            _worldVersionRecorder = worldVersionRecorder;
            _worldFactory = worldFactory;
            _terrain = terrain;
        }

        public void Enter(PlayerCreateUnitStateData parameter)
        {
            _unitToCreateType = parameter.UnitType;
            _tilesToCreateUnit =
                PlayerCreateUnitUtilities.GetTilesToCreateUnit(_unitToCreateType, _playerData.CurrentRegion);

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
            _playerTerrainFocus.SetTilesAboveShadowFiled(_tilesToCreateUnit);
            _playerTerrainFocus.ShowTilesOutline(_tilesToCreateUnit);
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

            if (_terrain.TryGetTile(hex, out var tile) && _tilesToCreateUnit.Contains(tile))
            {
                if (tile.Unit != null &&
                    PlayerCombineUnitUtilities.TryCombinedUnitType(_unitToCreateType, tile.Unit.Type, out var type))
                    CreateUnit(tile, type);
                else
                    CreateUnit(tile, _unitToCreateType);

                currentRegion = _terrain.GetTile(hex).Region;
            }

            _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                new PlayerSelectRegionStateData(currentRegion));
        }

        private void CreateUnit(TileData tile, UnitType unitType)
        {
            var isUnitCanMoveAfterCreation = IsUnitCanMoveAfterCreation(tile, unitType);

            _worldFactory.CreateTile(tile.Hex, _playerData.RegionType);
            _worldFactory.CreateUnit(tile.Hex, unitType, isUnitCanMoveAfterCreation);
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
        }

        private bool IsUnitCanMoveAfterCreation(TileData tile, UnitType unitType)
        {
            if (!unitType.IsCombat())
                return false;

            if (tile.Region.Type != _playerData.RegionType)
                return false;

            if (tile.Unit != null)
            {
                if (tile.Unit.Type == UnitType.Tree)
                    return false;

                if (tile.Unit.Type == UnitType.Grave)
                    return false;
                
                if (tile.Unit.Type.IsCombat())
                    return tile.Unit.IsCanMove;
            }

            return true;
        }
    }
}
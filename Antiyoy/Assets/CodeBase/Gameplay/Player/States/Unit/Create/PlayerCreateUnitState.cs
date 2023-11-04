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
                CreateUnit(tile);
                currentRegion = _terrain.GetTile(hex).Region;
            }

            _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                new PlayerSelectRegionStateData(currentRegion));
        }

        private void CreateUnit(TileData tile)
        {
            var isUnitCanMoveAfterCreation = PlayerCreateUnitUtilities.IsCombatUnit(_unitToCreateType);

            if (tile.Region.Type != _playerData.RegionType)
                isUnitCanMoveAfterCreation = false;
            else if (tile.Unit.Type == UnitType.Pine)
                isUnitCanMoveAfterCreation = false;

            _worldFactory.CreateTile(tile.Hex, _playerData.RegionType);
            _worldFactory.CreateUnit(tile.Hex, _unitToCreateType, isUnitCanMoveAfterCreation);
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
        }
    }
}
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

namespace CodeBase.Gameplay.Player.States.Unit
{
    public class PlayerCreateUnitState : IEnterState<PlayerCreateUnitStateData>, IExitState
    {
        private readonly PlayerData _playerData;
        private readonly IPlayerInput _playerInput;
        private readonly PlayerTileFocusView _focusView;

        private readonly PlayerStateMachine _playerStateMachine;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly WorldFactory _worldFactory;
        private readonly ITerrain _terrain;

        private List<TileData> _tilesToCreateUnits;
        private UnitType _unitTypeToCreate;

        public PlayerCreateUnitState(PlayerStateMachine playerStateMachine, PlayerData playerData,
            IPlayerInput playerInput, PlayerTileFocusView focusView, WorldVersionRecorder worldVersionRecorder,
            WorldFactory worldFactory, ITerrain terrain)
        {
            _playerInput = playerInput;
            _focusView = focusView;
            _playerData = playerData;
            _playerStateMachine = playerStateMachine;
            _worldVersionRecorder = worldVersionRecorder;
            _worldFactory = worldFactory;
            _terrain = terrain;
        }

        public void Enter(PlayerCreateUnitStateData parameter)
        {
            _tilesToCreateUnits = GetTilesToCreateUnit();
            _unitTypeToCreate = parameter.UnitType;
            _focusView.FocusTiles(_tilesToCreateUnits);

            _playerInput.OnPlayerInput += HandleInput;
        }

        public void Exit()
        {
            _focusView.UnFocusAllTiles();
            _playerInput.OnPlayerInput -= HandleInput;
        }

        private void HandleInput(HexPosition hex)
        {
            var currentRegion = _playerData.CurrentRegion;

            if (_terrain.TryGetTile(hex, out var tile))
            {
                if (_tilesToCreateUnits.Contains(tile))
                {
                    CreateUnit(tile);
                    currentRegion = _terrain.GetTile(hex).Region;
                }
            }

            _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                new PlayerSelectRegionStateData(currentRegion));
        }

        private List<TileData> GetTilesToCreateUnit()
        {
            var tilesToCreateEntity = new List<TileData>();

            foreach (var tile in _playerData.CurrentRegion.Tiles)
            {
                if (tile.Unit.Type == UnitType.None)
                    tilesToCreateEntity.Add(tile);

                foreach (var neighbor in tile.Neighbors)
                    if (neighbor.Region.Type != _playerData.RegionType)
                        if (!tilesToCreateEntity.Contains(neighbor))
                            tilesToCreateEntity.Add(neighbor);
            }

            return tilesToCreateEntity;
        }

        private void CreateUnit(TileData tile)
        {
            var hex = tile.Hex;

            _worldFactory.TryDestroyUnit(tile.Hex);

            if (tile.Region != _playerData.CurrentRegion)
            {
                _worldFactory.TryDestroyTile(tile.Hex);
                _worldFactory.CreateTile(hex, _playerData.RegionType);
            }

            _worldFactory.CreateUnit(hex, _unitTypeToCreate, true);
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
        }
    }
}
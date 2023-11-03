using System.Collections.Generic;
using System.Linq;
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

        private UnitType _unitTypeToCreate;
        private List<TileData> _tilesToCreateUnitAndCanMove;
        private List<TileData> _tilesToCreateUnitAndCantMove;

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
            _unitTypeToCreate = parameter.UnitType;
            _tilesToCreateUnitAndCanMove = GetTilesToCreateUnitAndCanMove();
            _tilesToCreateUnitAndCantMove = GetTilesToCreateUnitAndCantMove();

            var tilesToFocus = _tilesToCreateUnitAndCanMove.Concat(_tilesToCreateUnitAndCantMove).ToList();
            _focusView.FocusTiles(tilesToFocus);

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
                if (!IsCombatUnit(_unitTypeToCreate))
                    CreateUnit(tile, false);
                else if (_tilesToCreateUnitAndCanMove.Contains(tile))
                    CreateUnit(tile, true);
                else if (_tilesToCreateUnitAndCantMove.Contains(tile))
                    CreateUnit(tile, false);

                currentRegion = _terrain.GetTile(hex).Region;
            }

            _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                new PlayerSelectRegionStateData(currentRegion));
        }

        private bool IsCombatUnit(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Peasant:
                case UnitType.Spearman:
                case UnitType.Baron:
                case UnitType.Knight:
                    return true;
            }

            return false;
        }

        private List<TileData> GetTilesToCreateUnitAndCanMove()
        {
            var tiles = new List<TileData>();

            foreach (var tile in _playerData.CurrentRegion.Tiles)
            {
                switch (tile.Unit.Type)
                {
                    case UnitType.None:
                    case UnitType.Peasant:
                    case UnitType.Spearman:
                    case UnitType.Baron:
                        tiles.Add(tile);
                        break;
                }
            }

            return tiles;
        }

        private List<TileData> GetTilesToCreateUnitAndCantMove()
        {
            var tiles = new List<TileData>();

            foreach (var tile in _playerData.CurrentRegion.Tiles)
            {
                switch (tile.Unit.Type)
                {
                    case UnitType.Pine:
                    case UnitType.Grave:
                        tiles.Add(tile);
                        break;
                }

                foreach (var neighbor in tile.Neighbors)
                    if (neighbor.Region.Type != _playerData.RegionType)
                        if (!tiles.Contains(neighbor))
                            tiles.Add(neighbor);
            }

            return tiles;
        }

        private void CreateUnit(TileData tile, bool isCanMove)
        {
            var hex = tile.Hex;

            _worldFactory.TryDestroyUnit(tile.Hex);

            if (tile.Region != _playerData.CurrentRegion)
            {
                _worldFactory.TryDestroyTile(tile.Hex);
                _worldFactory.CreateTile(hex, _playerData.RegionType);
            }

            _worldFactory.CreateUnit(hex, _unitTypeToCreate, isCanMove);
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
        }
    }
}
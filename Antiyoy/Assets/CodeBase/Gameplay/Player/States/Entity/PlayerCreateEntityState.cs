using System.Collections.Generic;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Region;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Version;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Entity
{
    public class PlayerCreateEntityState : IEnterState<PlayerCreateEntityStateData>, IExitState
    {
        private readonly PlayerData _playerData;
        private readonly IPlayerInput _playerInput;
        private readonly PlayerTileFocusView _focusView;

        private readonly PlayerStateMachine _playerStateMachine;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly WorldFactory _worldFactory;
        private readonly ITerrain _terrain;

        private List<TileData> _tilesToCreateEntities;
        private EntityType _entityTypeToCreate;

        public PlayerCreateEntityState(PlayerStateMachine playerStateMachine, PlayerData playerData,
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

        public void Enter(PlayerCreateEntityStateData parameter)
        {
            _tilesToCreateEntities = GetTilesToCreateEntity();
            _entityTypeToCreate = parameter.EntityType;
            _focusView.FocusTiles(_tilesToCreateEntities);

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
                if (_tilesToCreateEntities.Contains(tile))
                {
                    CreateEntity(tile);
                    currentRegion = _terrain.GetTile(hex).Region;
                }
            }

            _playerStateMachine.SwitchTo<PlayerSelectRegionState, PlayerSelectRegionStateData>(
                new PlayerSelectRegionStateData(currentRegion));
        }

        private List<TileData> GetTilesToCreateEntity()
        {
            var tilesToCreateEntity = new List<TileData>();

            foreach (var tile in _playerData.CurrentRegion.Tiles)
            {
                if (tile.Entity == null)
                    tilesToCreateEntity.Add(tile);

                foreach (var neighbor in tile.Neighbors)
                    if (neighbor.Region.Type != _playerData.RegionType)
                        if (!tilesToCreateEntity.Contains(neighbor))
                            tilesToCreateEntity.Add(neighbor);
            }

            return tilesToCreateEntity;
        }

        private void CreateEntity(TileData tile)
        {
            var hex = tile.Hex;

            _worldFactory.TryDestroyEntity(tile.Hex);

            if (tile.Region != _playerData.CurrentRegion)
            {
                _worldFactory.TryDestroyTile(tile.Hex);
                _worldFactory.CreateTile(hex, _playerData.RegionType);
            }

            _worldFactory.CreateEntity(hex, _entityTypeToCreate);
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
        }
    }
}
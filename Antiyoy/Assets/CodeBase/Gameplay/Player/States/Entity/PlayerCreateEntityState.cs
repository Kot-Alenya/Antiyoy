using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States.Region;
using CodeBase.Gameplay.Terrain.Entity;
using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Gameplay.Terrain.Region.Rebuild;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using CodeBase.Gameplay.Terrain.Tile.Data;
using CodeBase.Gameplay.Terrain.Tile.Factory;
using CodeBase.Gameplay.Version.Operation;
using CodeBase.Gameplay.Version.Recorder;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Entity
{
    public class PlayerCreateEntityState : IEnterState<PlayerCreateEntityStateData>, IExitState
    {
        private readonly IPlayerInput _playerInput;
        private readonly ITileCollection _tileCollection;
        private readonly PlayerTileFocusView _focusView;
        private readonly PlayerData _playerData;
        private readonly IEntityFactory _entityFactory;
        private readonly ITileFactory _tileFactory;
        private readonly IRegionRebuilder _regionRebuilder;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly IWorldVersionRecorder _worldVersionRecorder;

        private List<TileData> _tilesToCreateEntities;
        private EntityType _entityTypeToCreate;

        public PlayerCreateEntityState(IPlayerInput playerInput, ITileCollection tileCollection,
            PlayerTileFocusView focusView, PlayerData playerData, IEntityFactory entityFactory,
            ITileFactory tileFactory, IRegionRebuilder regionRebuilder, PlayerStateMachine playerStateMachine,
            IWorldVersionRecorder worldVersionRecorder)
        {
            _playerInput = playerInput;
            _tileCollection = tileCollection;
            _focusView = focusView;
            _playerData = playerData;
            _entityFactory = entityFactory;
            _tileFactory = tileFactory;
            _regionRebuilder = regionRebuilder;
            _playerStateMachine = playerStateMachine;
            _worldVersionRecorder = worldVersionRecorder;
        }

        public void Enter(PlayerCreateEntityStateData parameter)
        {
            _tilesToCreateEntities = GetTilesToCreateEntity();
            _entityTypeToCreate = parameter.EntityType;
            _focusView.FocusAllTiles();
            _focusView.UnFocusTiles(_tilesToCreateEntities);

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

            if (_tileCollection.TryGet(hex, out var tile))
            {
                if (_tilesToCreateEntities.Contains(tile))
                {
                    CreateEntity(tile);
                    currentRegion = _tileCollection.Get(hex).Region;
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

            if (tile.Entity != null)
                DestroyEntity(tile.Entity);

            if (tile.Region != _playerData.CurrentRegion)
            {
                DestroyTile(tile);
                CreateTile(hex);
            }

            CreateEntity(hex);

            _regionRebuilder.RebuildFromBufferAndClearBuffer();
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
        }

        private void DestroyEntity(EntityData entity)
        {
            _entityFactory.Destroy(entity.RootTile.Hex);
            _worldVersionRecorder.AddToBuffer(new DestroyEntityOperationData(entity.RootTile.Hex, entity.Type));
        }

        private void CreateEntity(HexPosition hex)
        {
            _entityFactory.Create(hex, _entityTypeToCreate);
            _worldVersionRecorder.AddToBuffer(new CreateEntityOperationData(hex, _entityTypeToCreate));
        }

        private void DestroyTile(TileData tile)
        {
            _tileFactory.Destroy(tile.Hex);
            _worldVersionRecorder.AddToBuffer(new DestroyTileOperationData(tile.Hex, tile.Region.Type));
        }

        private void CreateTile(HexPosition hex)
        {
            _tileFactory.Create(hex, _playerData.RegionType);
            _worldVersionRecorder.AddToBuffer(new CreateTileOperationData(hex, _playerData.RegionType));
        }
    }
}
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
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Entity
{
    public class PlayerCreateEntityState : IEnterState<PlayerCreateEntityStateData>, IExitState
    {
        private readonly IPlayerInput _playerInput;
        private readonly ITileCollection _tileCollection;
        private readonly PlayerTerrainSelectionView _selectionView;
        private readonly PlayerData _playerData;
        private readonly IEntityFactory _entityFactory;
        private readonly ITileFactory _tileFactory;
        private readonly IRegionRebuilder _regionRebuilder;
        private readonly PlayerStateMachine _playerStateMachine;

        private List<TileData> _tilesToCreateEntities;
        private EntityType _entityTypeToCreate;

        public PlayerCreateEntityState(IPlayerInput playerInput, ITileCollection tileCollection,
            PlayerTerrainSelectionView selectionView, PlayerData playerData, IEntityFactory entityFactory,
            ITileFactory tileFactory, IRegionRebuilder regionRebuilder, PlayerStateMachine playerStateMachine)
        {
            _playerInput = playerInput;
            _tileCollection = tileCollection;
            _selectionView = selectionView;
            _playerData = playerData;
            _entityFactory = entityFactory;
            _tileFactory = tileFactory;
            _regionRebuilder = regionRebuilder;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter(PlayerCreateEntityStateData parameter)
        {
            _tilesToCreateEntities = GetTilesToCreateEntities(parameter.EntityType);
            _entityTypeToCreate = parameter.EntityType;
            _selectionView.SelectTiles(_tilesToCreateEntities);

            _playerInput.OnPlayerInput += HandleInput;
        }

        public void Exit()
        {
            _selectionView.UnSelectTiles(_tilesToCreateEntities);

            _playerInput.OnPlayerInput -= HandleInput;
        }

        private void HandleInput(HexPosition hex)
        {
            if (_tileCollection.TryGet(hex, out var tile))
            {
                if (_tilesToCreateEntities.Contains(tile))
                {
                    _selectionView.UnSelectTiles(_tilesToCreateEntities);
                    _tilesToCreateEntities.Clear();
                    CreateEntity(tile);
                }
            }

            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }

        private List<TileData> GetTilesToCreateEntities(EntityType entityType)
        {
            var tiles = new List<TileData>(_playerData.CurrentRegion.Tiles);

            if (entityType == EntityType.Farm)
                return tiles;

            foreach (var regionTile in _playerData.CurrentRegion.Tiles)
            foreach (var regionNeighborTile in regionTile.Neighbors)
                if (!tiles.Contains(regionNeighborTile))
                    tiles.Add(regionNeighborTile);

            return tiles;
        }

        private void CreateEntity(TileData tile)
        {
            var hex = tile.Hex;

            if (tile.Entity != null)
                return;

            if (tile.Region != _playerData.CurrentRegion)
            {
                _tileFactory.Destroy(hex);
                _tileFactory.Create(hex, _playerData.RegionType);
            }

            _entityFactory.Create(hex, _entityTypeToCreate);
            _regionRebuilder.RebuildFromBufferAndClearBuffer();
        }
    }
}
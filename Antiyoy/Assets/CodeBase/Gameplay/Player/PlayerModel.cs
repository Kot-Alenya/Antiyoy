using System.Collections.Generic;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.States.Unit.Create;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.Player.States
{
    public class PlayerModel : IPlayerModel
    {
        private readonly PlayerData _playerData;

        private readonly PlayerUnitCreator _playerUnitCreator;
        private readonly PlayerUnitMover _playerUnitMover;
        private UnitType _unitTypeToCreate;
        private UnitData _currentUnit;

        public PlayerModel(PlayerData playerData, PlayerUnitMover playerUnitMover, PlayerUnitCreator playerUnitCreator)
        {
            _playerData = playerData;
            _playerUnitMover = playerUnitMover;
            _playerUnitCreator = playerUnitCreator;
            Instance = playerData.Instance;
            PersistenceRegionType = playerData.RegionType;
        }

        public RegionType PersistenceRegionType { get; }

        public RegionData SelectedRegion => _playerData.SelectedRegion;

        public PlayerPrefabData Instance { get; }

        public void SelectRegion(RegionData region) => _playerData.SelectedRegion = region;

        public void SelectUnit(UnitData unit) => _playerData.SelectedUnit = unit;

        public List<TileData> GetTilesToCreateUnit(UnitType unitType) =>
            _playerUnitCreator.GetTilesToCreateUnit(unitType);
        

        public void CreateUnit(TileData tile, UnitType unitType) => _playerUnitCreator.CreateUnit(tile, unitType);

        public List<TileData> GetTilesToMoveUnit(UnitData unit) => _playerUnitMover.GetTilesToMoveUnit(unit);

        public void MoveUnit(TileData tile) => _playerUnitMover.MoveUnit(tile);
    }
}
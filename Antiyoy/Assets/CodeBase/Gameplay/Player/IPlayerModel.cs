using System.Collections.Generic;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.Player.States
{
    public interface IPlayerModel
    {
        public RegionType PersistenceRegionType { get; }

        public RegionData SelectedRegion { get; }

        public PlayerPrefabData Instance { get; }

        public void SelectRegion(RegionData region);

        public void SelectUnit(UnitData unit);

        public List<TileData> GetTilesToCreateUnit(UnitType unitType);

        public void CreateUnit(TileData tile, UnitType unitType);

        public List<TileData> GetTilesToMoveUnit(UnitData unit);

        public void MoveUnit(TileData tile);
    }
}
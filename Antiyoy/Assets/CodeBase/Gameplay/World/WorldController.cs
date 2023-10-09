using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World
{
    public class WorldController
    {
        private readonly TerrainModel _terrainModel;

        public WorldController(TerrainModel terrainModel) => _terrainModel = terrainModel;

        public void CreateTile(HexPosition hex, RegionType regionType) => _terrainModel.CreateTile(hex, regionType);

        public void DestroyTile(TileData tile) => _terrainModel.DestroyTile(tile);

        public void RecalculateChangedRegions() => _terrainModel.RecalculateChangedRegions();

        public bool IsHexInTerrain(HexPosition hex) => _terrainModel.IsHexInTerrain(hex);
        
        public bool TryGetTile(HexPosition hex, out TileData tile) => _terrainModel.TryGetTile(hex, out  tile);
        
        public void Record()
        {
        }

        public void Next()
        {
        }

        public void Back()
        {
        }
    }
}
using CodeBase.Gameplay.Terrain.Data.Hex;
using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainController
    {
        private readonly TerrainModel _model;

        public TerrainController(TerrainModel model) => _model = model;

        public bool IsHexInTerrain(HexPosition hex) => _model.IsHexInTerrain(hex);

        public void CreateTile(HexPosition hex, RegionType regionType)
        {
            if (_model.TryGetTile(hex, out var tile))
                _model.DestroyTile(tile);

            _model.CreateTile(hex, regionType);
        }

        public void DestroyTile(HexPosition hex)
        {
            if (_model.TryGetTile(hex, out var tile))
                _model.DestroyTile(tile);
        }

        public void RecalculateChangedRegions() => _model.RecalculateChangedRegions();
    }
}
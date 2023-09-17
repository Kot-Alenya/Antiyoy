using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainController
    {
        private readonly TerrainModel _model;

        public TerrainController(TerrainModel model) => _model = model;

        public bool IsHexInTerrain(HexPosition hex) => _model.IsHexInTerrain(hex);

        public bool TryGetTile(HexPosition hex, out TileObject tile) => _model.TryGetTile(hex, out tile);

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
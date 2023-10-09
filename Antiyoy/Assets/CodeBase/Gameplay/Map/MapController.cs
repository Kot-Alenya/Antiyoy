using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Region.Model;
using CodeBase.Gameplay.Tile;

namespace CodeBase.Gameplay.Map
{
    public class MapController
    {
        private readonly TilesModel _tilesModel;
        private readonly RegionsModel _regionsModel;

        public MapController(TilesModel tilesModel, RegionsModel regionsModel)
        {
            _tilesModel = tilesModel;
            _regionsModel = regionsModel;
        }

        public void CreateTile(HexPosition hex, RegionType regionType)
        {
            if (!_tilesModel.IsHexInTerrain(hex))
                return;

            if (_tilesModel.TryGetTile(hex, out var tile))
                _tilesModel.DestroyTile(tile);

            _tilesModel.CreateTile(hex, regionType);
        }

        public void DestroyTile(HexPosition hex)
        {
            if (_tilesModel.TryGetTile(hex, out var tile))
                _tilesModel.DestroyTile(tile);
        }

        public void RecalculateChangedRegions() => _regionsModel.RecalculateChangedRegions();
    }
}
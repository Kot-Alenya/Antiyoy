using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Infrastructure.MapEditor.Data;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorController
    {
        private readonly MapEditorModel _model;

        public MapEditorController(MapEditorModel model) => _model = model;

        public void SetTilesMode(RegionType regionType)
        {
            _model.SetCurrentMode(MapEditorMode.SetTiles);
            _model.SetCurrentRegion(regionType);
        }

        public void RemoveTilesMode() => _model.SetCurrentMode(MapEditorMode.RemoveTiles);

        public void SelectTile(HexPosition hex) => _model.SelectTile(hex);

        public void ProcessTiles() => _model.ProcessTiles();

        public void ReturnBack() => _model.ReturnBack();

        public void ReturnNext() => _model.ReturnNext();
    }
}
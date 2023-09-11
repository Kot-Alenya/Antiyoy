using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile;
using CodeBase.Infrastructure.MapEditor.Data;
using CodeBase.Infrastructure.MapEditor.UI;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorController : IMapEditorUI
    {
        private readonly MapEditorModel _model;

        public MapEditorController(MapEditorModel model) => _model = model;

        public void SetModeRegion(RegionType region)
        {
            _model.SetMode(MapEditorMode.Region);
            _model.SetRegion(region);
        }

        public void ProcessTile(TileObject tileObject) => _model.ProcessTile(tileObject);
    }
}
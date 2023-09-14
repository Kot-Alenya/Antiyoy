using _dev;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile;
using CodeBase.Infrastructure.MapEditor.Data;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorController
    {
        private readonly MapEditorModel _model;

        public MapEditorController(MapEditorModel model) => _model = model;

        public void SetRegionMode(RegionType regionType)
        {
            _model.SetCurrentMode(MapEditorMode.SetRegion);
            _model.SetCurrentRegion(regionType);
        }

        public void RemoveRegionMode() => _model.SetCurrentMode(MapEditorMode.RemoveRegion);

        public void SetEntityMode(EntityType entityType)
        {
            _model.SetCurrentMode(MapEditorMode.SetEntity);
            _model.SetCurrentEntity(entityType);
        }

        public void ProcessTile(TileObject tileObject) => _model.ProcessTile(tileObject);
    }
}
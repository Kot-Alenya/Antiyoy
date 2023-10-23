using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Version;
using CodeBase.MapEditor.Data;

namespace CodeBase.MapEditor
{
    public class MapEditorController : IMapEditorController
    {
        private readonly MapEditorModel _mapEditorModel;
        private readonly IWorldVersionController _versionController;

        public MapEditorController(MapEditorModel mapEditorModel, IWorldVersionController versionController)
        {
            _mapEditorModel = mapEditorModel;
            _versionController = versionController;
        }

        public void SetMode(MapEditorMode mode) => _mapEditorModel.SetCurrentMode(mode);

        public void SetRegionType(RegionType regionType) => _mapEditorModel.SetCurrentRegion(regionType);

        public void SetEntityType(EntityType entityType) => _mapEditorModel.SetCurrentEntity(entityType);

        public void SelectTile(HexPosition hex) => _mapEditorModel.SelectTile(hex);

        public void ProcessTiles() => _mapEditorModel.ProcessTiles();

        public void ReturnBack() => _versionController.ReturnWorldBack();

        public void ReturnNext() => _versionController.ReturnWorldNext();
    }
}
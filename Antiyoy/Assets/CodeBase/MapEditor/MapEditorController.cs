using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.MapEditor.Data;
using CodeBase.MapEditor.UI;

namespace CodeBase.MapEditor
{
    public class MapEditorController : IMapEditorController
    {
        private readonly MapEditorModel _mapEditorModel;
        private readonly IWorldController _world;

        public MapEditorController(MapEditorModel mapEditorModel, IWorldController world)
        {
            _mapEditorModel = mapEditorModel;
            _world = world;
        }

        public void SetMode(MapEditorMode mode) => _mapEditorModel.SetCurrentMode(mode);

        public void SetRegionType(RegionType regionType) => _mapEditorModel.SetCurrentRegion(regionType);

        public void SetEntityType(EntityType entityType) => _mapEditorModel.SetCurrentEntity(entityType);

        public void SelectTile(HexPosition hex) => _mapEditorModel.SelectTile(hex);

        public void ProcessTiles() => _mapEditorModel.ProcessTiles();

        public void ReturnBack() => _world.ChangeHandler.ReturnWorldBack();

        public void ReturnNext() => _world.ChangeHandler.ReturnWorldNext();
    }
}
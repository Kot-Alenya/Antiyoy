using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.MapEditor.Data;

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

        public void CreateTilesMode(RegionType regionType)
        {
            _mapEditorModel.SetCurrentMode(MapEditorMode.CreateTiles);
            _mapEditorModel.SetCurrentRegion(regionType);
        }

        public void DestroyTilesMode() => _mapEditorModel.SetCurrentMode(MapEditorMode.DestroyTiles);

        public void SelectTile(HexPosition hex) => _mapEditorModel.SelectTile(hex);

        public void ProcessTiles() => _mapEditorModel.ProcessTiles();

        public void ReturnBack() => _world.ChangeHandler.ReturnWorldBack();

        public void ReturnNext() => _world.ChangeHandler.ReturnWorldNext();
    }
}
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Progress.Data;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Version.Handler;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.MapEditor.Data;

namespace CodeBase.MapEditor
{
    public class MapEditorController : IMapEditorController
    {
        private readonly MapEditorModel _mapEditorModel;
        private readonly IWorldVersionHandler _worldVersionHandler;
        private readonly IProgressSaveLoader _progressSaveLoader;

        public MapEditorController(MapEditorModel mapEditorModel, IWorldVersionHandler worldVersionHandler,
            IProgressSaveLoader progressSaveLoader)
        {
            _mapEditorModel = mapEditorModel;
            _worldVersionHandler = worldVersionHandler;
            _progressSaveLoader = progressSaveLoader;
        }

        public void SetMode(MapEditorMode mode) => _mapEditorModel.SetCurrentMode(mode);

        public void SetRegionType(RegionType regionType) => _mapEditorModel.SetCurrentRegion(regionType);

        public void SetEntityType(EntityType entityType) => _mapEditorModel.SetCurrentEntity(entityType);

        public void SelectTile(HexPosition hex) => _mapEditorModel.SelectTile(hex);

        public void ProcessTiles() => _mapEditorModel.ProcessTiles();

        public void ReturnBack() => _worldVersionHandler.ReturnBack();

        public void ReturnNext() => _worldVersionHandler.ReturnNext();

        public void SaveWorld() => _progressSaveLoader.Save<WorldProgressData>("World");
    }
}
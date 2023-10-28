using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Progress.Data;
using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Version.Handler;
using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor
{
    public class WorldEditorController : IWorldEditorController
    {
        private readonly WorldEditorModel _worldEditorModel;
        private readonly IWorldVersionHandler _worldVersionHandler;
        private readonly IProgressSaveLoader _progressSaveLoader;

        public WorldEditorController(WorldEditorModel worldEditorModel, IWorldVersionHandler worldVersionHandler,
            IProgressSaveLoader progressSaveLoader)
        {
            _worldEditorModel = worldEditorModel;
            _worldVersionHandler = worldVersionHandler;
            _progressSaveLoader = progressSaveLoader;
        }

        public void SetMode(WorldEditorMode mode) => _worldEditorModel.SetCurrentMode(mode);

        public void SetRegionType(RegionType regionType) => _worldEditorModel.SetCurrentRegion(regionType);

        public void SetEntityType(EntityType entityType) => _worldEditorModel.SetCurrentEntity(entityType);

        public void SelectTile(HexPosition hex) => _worldEditorModel.SelectTile(hex);

        public void ProcessTiles() => _worldEditorModel.ProcessTiles();

        public void ReturnBack() => _worldVersionHandler.ReturnBack();

        public void ReturnNext() => _worldVersionHandler.ReturnNext();

        public void SaveWorld() => _progressSaveLoader.Save<WorldProgressData>("World");
    }
}
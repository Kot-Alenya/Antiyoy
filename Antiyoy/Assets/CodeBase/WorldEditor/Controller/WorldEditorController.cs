using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Progress;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor.Controller
{
    public class WorldEditorController : IWorldEditorController
    {
        private readonly WorldEditorModel _worldEditorModel;
        private readonly IVersionHandler _versionHandler;
        private readonly IProgressSaveLoader _progressSaveLoader;

        public WorldEditorController(WorldEditorModel worldEditorModel, IVersionHandler versionHandler,
            IProgressSaveLoader progressSaveLoader)
        {
            _worldEditorModel = worldEditorModel;
            _versionHandler = versionHandler;
            _progressSaveLoader = progressSaveLoader;
        }

        public void SetMode(WorldEditorMode mode) => _worldEditorModel.SetCurrentMode(mode);

        public void SetRegionType(RegionType regionType) => _worldEditorModel.SetCurrentRegion(regionType);

        public void SetEntityType(EntityType entityType) => _worldEditorModel.SetCurrentEntity(entityType);

        public void SelectTile(HexPosition hex) => _worldEditorModel.SelectTile(hex);

        public void ProcessTiles() => _worldEditorModel.ProcessTiles();

        public void ReturnBack() => _versionHandler.ReturnBack();

        public void ReturnNext() => _versionHandler.ReturnNext();

        public void SaveWorld() => _progressSaveLoader.Save<WorldProgressData>("World");
    }
}
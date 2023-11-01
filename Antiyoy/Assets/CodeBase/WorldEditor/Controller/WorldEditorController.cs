using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Progress.Data;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Version.Handler;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor.Controller
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
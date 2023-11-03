using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Progress;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Gameplay.World.Version;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor.Controller
{
    public class WorldEditorController : IWorldEditorController
    {
        private readonly WorldEditorModel _worldEditorModel;
        private readonly WorldVersionManager _worldVersionManager;
        private readonly IProgressSaveLoader _progressSaveLoader;

        public WorldEditorController(WorldEditorModel worldEditorModel, WorldVersionManager worldVersionManager,
            IProgressSaveLoader progressSaveLoader)
        {
            _worldEditorModel = worldEditorModel;
            _worldVersionManager = worldVersionManager;
            _progressSaveLoader = progressSaveLoader;
        }

        public void SetMode(WorldEditorMode mode) => _worldEditorModel.SetCurrentMode(mode);

        public void SetRegionType(RegionType regionType) => _worldEditorModel.SetCurrentRegion(regionType);

        public void SetUnitType(UnitType unitType) => _worldEditorModel.SetCurrentUnit(unitType);

        public void SelectTile(HexPosition hex) => _worldEditorModel.SelectTile(hex);

        public void ProcessTiles() => _worldEditorModel.ProcessTiles();

        public void ReturnBack() => _worldVersionManager.ReturnBack();

        public void ReturnNext() => _worldVersionManager.ReturnNext();

        public void SaveWorld() => _progressSaveLoader.Save<WorldProgressData>("World");
    }
}
using CodeBase.Gameplay.Terrain;
using System;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Map;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile;
using CodeBase.Infrastructure.MapEditor.Data;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorModel
    {
        private readonly TileFactory _tileFactory;
        private readonly MapController _terrain;
        private readonly MapRecorder _recorder;
        private MapEditorMode _currentMode;
        private RegionType _currentRegion;

        public MapEditorModel(MapController terrain, MapRecorder recorder)
        {
            _terrain = terrain;
            _recorder = recorder;
        }

        public void SetCurrentMode(MapEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SelectTile(HexPosition hex)
        {
            if (_currentMode == MapEditorMode.SetTiles)
                _terrain.CreateTile(hex, _currentRegion);
            else if (_currentMode == MapEditorMode.RemoveTiles)
                _terrain.DestroyTile(hex);
        }

        public void ProcessTiles()
        {
            switch (_currentMode)
            {
                case MapEditorMode.None:
                    break;
                case MapEditorMode.SetTiles:
                    _terrain.RecalculateChangedRegions();
                    break;
                case MapEditorMode.RemoveTiles:
                    _terrain.RecalculateChangedRegions();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _recorder.Record();
        }

        public void ReturnBack() => _recorder.Back();

        public void ReturnNext() => _recorder.Next();
    }
}
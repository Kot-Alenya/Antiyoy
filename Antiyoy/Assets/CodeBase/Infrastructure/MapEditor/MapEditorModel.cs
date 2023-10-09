using System;
using System.Collections.Generic;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile;
using CodeBase.Infrastructure.MapEditor.Data;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorModel
    {
        private readonly TileFactory _tileFactory;
        private readonly WorldController _worldController;
        private readonly List<HexPosition> _selectedHex = new();
        private MapEditorMode _currentMode;
        private RegionType _currentRegion;

        public MapEditorModel(WorldController worldController) => _worldController = worldController;

        public void SetCurrentMode(MapEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SelectTile(HexPosition hex)
        {
            if (!_worldController.IsHexInTerrain(hex) || _selectedHex.Contains(hex))
                return;
            
            if (_currentMode == MapEditorMode.SetTiles)
                _worldController.CreateTile(hex, _currentRegion);
            else if (_currentMode == MapEditorMode.RemoveTiles)
                if (_worldController.TryGetTile(hex, out var tile))
                    _worldController.DestroyTile(tile);
            
            _selectedHex.Add(hex);
        }

        public void ProcessTiles()
        {
            switch (_currentMode)
            {
                case MapEditorMode.None:
                    break;
                case MapEditorMode.SetTiles:
                    _worldController.RecalculateChangedRegions();
                    break;
                case MapEditorMode.RemoveTiles:
                    _worldController.RecalculateChangedRegions();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _selectedHex.Clear();
            _worldController.Record();
        }

        public void ReturnBack() => _worldController.Back();

        public void ReturnNext() => _worldController.Next();
    }
}
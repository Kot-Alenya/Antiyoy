using CodeBase.Gameplay.Terrain;
using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Data.Hex;
using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Infrastructure.MapEditor.Data;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorModel
    {
        private readonly TileFactory _tileFactory;
        private readonly TerrainController _terrain;
        private readonly List<HexPosition> _selectedTiles = new();
        private MapEditorMode _currentMode;
        private RegionType _currentRegion;

        public MapEditorModel(TerrainController terrain) => _terrain = terrain;

        public void SetCurrentMode(MapEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SelectTile(HexPosition hex)
        {
            if (!_terrain.IsHexInTerrain(hex))
                return;

            if (_selectedTiles.Contains(hex))
                return;

            if (_currentMode == MapEditorMode.SetTiles)
                _terrain.CreateTile(hex, _currentRegion);
            else if (_currentMode == MapEditorMode.RemoveTiles)
                _terrain.DestroyTile(hex);

            _selectedTiles.Add(hex);
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

            _selectedTiles.Clear();
        }
    }
}
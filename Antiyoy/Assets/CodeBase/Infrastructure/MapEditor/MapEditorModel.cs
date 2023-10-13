using System;
using System.Collections.Generic;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Data.Operation;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile;
using CodeBase.Infrastructure.MapEditor.Data;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorModel
    {
        private readonly IWorldController _world;
        private readonly TileFactory _tileFactory;
        private readonly List<HexPosition> _selectedHex = new();
        private MapEditorMode _currentMode;
        private RegionType _currentRegion;

        public MapEditorModel(IWorldController world) => _world = world;

        public void SetCurrentMode(MapEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SelectTile(HexPosition hex)
        {
            if (!_world.Terrain.IsHexInTerrain(hex) || _selectedHex.Contains(hex))
                return;

            if (_currentMode == MapEditorMode.CreateTiles)
                CreateTile(hex);
            else if (_currentMode == MapEditorMode.DestroyTiles)
                DestroyTile(hex);

            _selectedHex.Add(hex);
        }

        public void ProcessTiles()
        {
            if (_selectedHex.Count <= 0)
                return;

            switch (_currentMode)
            {
                case MapEditorMode.None:
                    break;
                case MapEditorMode.CreateTiles:
                    _world.Terrain.RecalculateChangedRegions();
                    break;
                case MapEditorMode.DestroyTiles:
                    _world.Terrain.RecalculateChangedRegions();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _world.ChangeRecorder.RecordFromBuffer();
            _selectedHex.Clear();
        }

        private void CreateTile(HexPosition hex)
        {
            DestroyTile(hex);

            if (_world.Terrain.TryCreateTile(hex, _currentRegion))
                _world.ChangeRecorder.AddToBuffer(new WorldCreateTileOperationData(hex, _currentRegion));
        }

        private void DestroyTile(HexPosition hex)
        {
            if (!_world.Terrain.TryGetTile(hex, out var tile))
                return;

            _world.ChangeRecorder.AddToBuffer(new WorldDestroyTileOperationData(hex, tile.Region.Type));
            _world.Terrain.TryDestroyTile(hex);
        }
    }
}
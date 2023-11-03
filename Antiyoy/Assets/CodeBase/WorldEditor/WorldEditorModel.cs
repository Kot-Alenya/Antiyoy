using System;
using System.Collections.Generic;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Gameplay.World.Version;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor
{
    public class WorldEditorModel
    {
        private readonly ITerrain _terrain;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly WorldFactory _worldFactory;
        private readonly List<HexPosition> _selectedHex = new();
        private WorldEditorMode _currentMode;
        private RegionType _currentRegion;
        private UnitType _currentUnitType;

        public WorldEditorModel(ITerrain terrain, WorldVersionRecorder worldVersionRecorder, WorldFactory worldFactory)
        {
            _terrain = terrain;
            _worldVersionRecorder = worldVersionRecorder;
            _worldFactory = worldFactory;
        }

        public void SetCurrentMode(WorldEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SetCurrentUnit(UnitType unitType) => _currentUnitType = unitType;

        public void SelectTile(HexPosition hex)
        {
            if (!_terrain.IsInTerrain(hex) || _selectedHex.Contains(hex))
                return;

            switch (_currentMode)
            {
                case WorldEditorMode.None:
                    break;
                case WorldEditorMode.CreateTile:
                    _worldFactory.CreateTile(hex, _currentRegion);
                    break;
                case WorldEditorMode.DestroyTile:
                    _worldFactory.TryDestroyTile(hex);
                    break;
                case WorldEditorMode.CreateUnit:
                    _worldFactory.CreateUnit(hex, _currentUnitType, false);
                    break;
                case WorldEditorMode.DestroyUnit:
                    _worldFactory.TryDestroyUnit(hex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _selectedHex.Add(hex);
        }

        public void ProcessTiles()
        {
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
            _selectedHex.Clear();
        }
    }
}
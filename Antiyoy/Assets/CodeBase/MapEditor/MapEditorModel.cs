using System;
using System.Collections.Generic;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile;
using CodeBase.Gameplay.World.Version;
using CodeBase.Gameplay.World.Version.Operation;
using CodeBase.MapEditor.Data;

namespace CodeBase.MapEditor
{
    public class MapEditorModel
    {
        private readonly IWorldController _world;
        private readonly IWorldVersionController _versionController;
        private readonly TileFactory _tileFactory;
        private readonly List<HexPosition> _selectedHex = new();
        private MapEditorMode _currentMode;
        private RegionType _currentRegion;
        private EntityType _currentEntityType;

        public MapEditorModel(IWorldController world, IWorldVersionController versionController)
        {
            _world = world;
            _versionController = versionController;
        }

        public void SetCurrentMode(MapEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SetCurrentEntity(EntityType entityType) => _currentEntityType = entityType;

        public void SelectTile(HexPosition hex)
        {
            if (!_world.Terrain.IsHexInTerrain(hex) || _selectedHex.Contains(hex))
                return;

            switch (_currentMode)
            {
                case MapEditorMode.None:
                    break;
                case MapEditorMode.CreateTile:
                    CreateTile(hex);
                    break;
                case MapEditorMode.DestroyTile:
                    DestroyTile(hex);
                    break;
                case MapEditorMode.CreateEntity:
                    CreateEntity(hex);
                    break;
                case MapEditorMode.DestroyEntity:
                    DestroyEntity(hex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

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
                case MapEditorMode.CreateTile:
                case MapEditorMode.DestroyTile:
                case MapEditorMode.CreateEntity:
                case MapEditorMode.DestroyEntity:
                    _world.Terrain.RecalculateChangedRegions();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _versionController.RecordFromBufferAndClearBuffer();
            _selectedHex.Clear();
        }

        private void CreateTile(HexPosition hex)
        {
            DestroyTile(hex);

            if (_world.Terrain.TryCreateTile(hex, _currentRegion))
                _versionController.AddToBuffer(new WorldCreateTileOperationData(hex, _currentRegion));
        }

        private void DestroyTile(HexPosition hex)
        {
            if (!_world.Terrain.TryGetTile(hex, out var tile))
                return;

            _versionController.AddToBuffer(new WorldDestroyTileOperationData(hex, tile.Region.Type));
            _world.Terrain.TryDestroyTile(hex);
        }

        private void CreateEntity(HexPosition hex)
        {
            DestroyEntity(hex);

            if (_world.Terrain.TryCreateEntity(hex, _currentEntityType))
                _versionController.AddToBuffer(new WorldCreateEntityOperationData(hex, _currentEntityType));
        }

        private void DestroyEntity(HexPosition hex)
        {
            if (!_world.Terrain.TryGetTile(hex, out var tile))
                return;

            if (tile.Entity != null)
            {
                _versionController.AddToBuffer(new WorldDestroyEntityOperationData(hex, tile.Entity.Type));
                _world.Terrain.TryDestroyEntity(hex);
            }
        }
    }
}
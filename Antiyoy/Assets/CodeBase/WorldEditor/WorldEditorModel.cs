using System;
using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Entity.Operation;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Operation;
using CodeBase.Gameplay.World.Version;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor
{
    public class WorldEditorModel
    {
        private readonly ITerrain _terrain;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly TileVersionOperationFactory _tileVersionOperationFactory;
        private readonly EntityVersionOperationFactory _entityVersionOperationFactory;
        private readonly List<HexPosition> _selectedHex = new();
        private WorldEditorMode _currentMode;
        private RegionType _currentRegion;
        private EntityType _currentEntityType;

        public WorldEditorModel(ITerrain terrain, WorldVersionRecorder worldVersionRecorder,
            TileVersionOperationFactory tileVersionOperationFactory,
            EntityVersionOperationFactory entityVersionOperationFactory)
        {
            _terrain = terrain;
            _worldVersionRecorder = worldVersionRecorder;
            _tileVersionOperationFactory = tileVersionOperationFactory;
            _entityVersionOperationFactory = entityVersionOperationFactory;
        }

        public void SetCurrentMode(WorldEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SetCurrentEntity(EntityType entityType) => _currentEntityType = entityType;

        public void SelectTile(HexPosition hex)
        {
            if (!_terrain.IsInTerrain(hex) || _selectedHex.Contains(hex))
                return;

            switch (_currentMode)
            {
                case WorldEditorMode.None:
                    break;
                case WorldEditorMode.CreateTile:
                    CreateTile(hex);
                    break;
                case WorldEditorMode.DestroyTile:
                    TryDestroyTile(hex);
                    break;
                case WorldEditorMode.CreateEntity:
                    CreateEntity(hex);
                    break;
                case WorldEditorMode.DestroyEntity:
                    TryDestroyEntity(hex);
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

        private void CreateTile(HexPosition hex)
        {
            TryDestroyTile(hex);

            _terrain.CreateTile(hex, _currentRegion);
            _worldVersionRecorder.AddToBuffer(_tileVersionOperationFactory.GetCreateOperation(hex, _currentRegion));
        }

        private void TryDestroyTile(HexPosition hex)
        {
            if (!_terrain.TryGetTile(hex, out var tile))
                return;

            TryDestroyEntity(hex);

            _terrain.DestroyTile(tile);
            _worldVersionRecorder.AddToBuffer(_tileVersionOperationFactory.GetDestroyOperation(hex, _currentRegion));
        }

        private void CreateEntity(HexPosition hex)
        {
            TryDestroyEntity(hex);

            _terrain.CreateEntity(_terrain.GetTile(hex), _currentEntityType);
            _worldVersionRecorder.AddToBuffer(
                _entityVersionOperationFactory.GetCreateOperation(hex, _currentEntityType));
        }

        private void TryDestroyEntity(HexPosition hex)
        {
            if (!_terrain.TryGetTile(hex, out var tile))
                return;

            if (tile.Entity != null)
            {
                _terrain.DestroyEntity(tile.Entity);
                _worldVersionRecorder.AddToBuffer(
                    _entityVersionOperationFactory.GetDestroyOperation(hex, _currentEntityType));
            }
        }
    }
}
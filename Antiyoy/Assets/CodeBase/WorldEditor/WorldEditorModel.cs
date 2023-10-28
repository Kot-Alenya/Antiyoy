using System;
using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Region.Rebuild;
using CodeBase.Gameplay.World.Terrain.Tile.Collection;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using CodeBase.Gameplay.World.Version.Operation;
using CodeBase.Gameplay.World.Version.Recorder;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor
{
    public class WorldEditorModel
    {
        private readonly IWorldVersionRecorder _worldVersionRecorder;
        private readonly ITileFactory _tileFactory;
        private readonly IEntityFactory _entityFactory;
        private readonly ITileCollection _tileCollection;
        private readonly IRegionRebuilder _regionRebuilder;
        private readonly List<HexPosition> _selectedHex = new();
        private WorldEditorMode _currentMode;
        private RegionType _currentRegion;
        private EntityType _currentEntityType;

        public WorldEditorModel(IWorldVersionRecorder worldVersionRecorder, ITileFactory tileFactory,
            IEntityFactory entityFactory, ITileCollection tileCollection, IRegionRebuilder regionRebuilder)
        {
            _worldVersionRecorder = worldVersionRecorder;
            _tileFactory = tileFactory;
            _entityFactory = entityFactory;
            _tileCollection = tileCollection;
            _regionRebuilder = regionRebuilder;
        }

        public void SetCurrentMode(WorldEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SetCurrentEntity(EntityType entityType) => _currentEntityType = entityType;

        public void SelectTile(HexPosition hex)
        {
            if (!_tileCollection.IsInCollection(hex) || _selectedHex.Contains(hex))
                return;

            switch (_currentMode)
            {
                case WorldEditorMode.None:
                    break;
                case WorldEditorMode.CreateTile:
                    CreateTile(hex);
                    break;
                case WorldEditorMode.DestroyTile:
                    DestroyTile(hex);
                    break;
                case WorldEditorMode.CreateEntity:
                    CreateEntity(hex);
                    break;
                case WorldEditorMode.DestroyEntity:
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
                case WorldEditorMode.None:
                    break;
                case WorldEditorMode.CreateTile:
                case WorldEditorMode.DestroyTile:
                case WorldEditorMode.CreateEntity:
                case WorldEditorMode.DestroyEntity:
                    _regionRebuilder.RebuildFromBufferAndClearBuffer();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
            _selectedHex.Clear();
        }

        private void CreateTile(HexPosition hex)
        {
            DestroyTile(hex);

            _tileFactory.Create(hex, _currentRegion);
            _worldVersionRecorder.AddToBuffer(new CreateTileOperationData(hex, _currentRegion));
        }

        private void DestroyTile(HexPosition hex)
        {
            if (!_tileCollection.TryGet(hex, out var tile))
                return;

            if (tile.Entity != null)
                DestroyEntity(hex);

            _worldVersionRecorder.AddToBuffer(new DestroyTileOperationData(hex, tile.Region.Type));
            _tileFactory.Destroy(tile);
        }

        private void CreateEntity(HexPosition hex)
        {
            DestroyEntity(hex);

            _entityFactory.Create(hex, _currentEntityType);
            _worldVersionRecorder.AddToBuffer(new CreateEntityOperationData(hex, _currentEntityType));
        }

        private void DestroyEntity(HexPosition hex)
        {
            if (!_tileCollection.TryGet(hex, out var tile))
                return;

            if (tile.Entity != null)
            {
                _worldVersionRecorder.AddToBuffer(new DestroyEntityOperationData(hex, tile.Entity.Type));
                _entityFactory.Destroy(hex);
            }
        }
    }
}
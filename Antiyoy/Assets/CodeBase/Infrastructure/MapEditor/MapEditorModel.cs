using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Tile;
using System;
using _dev;
using CodeBase.Infrastructure.MapEditor.Data;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorModel
    {
        private readonly TerrainObject _terrainObject;
        private readonly EntityFactory _entityFactory;
        private MapEditorMode _currentMode;
        private RegionType _currentRegion;
        private EntityType _currentEntity;

        public MapEditorModel(TerrainObject terrainObject, EntityFactory entityFactory)
        {
            _terrainObject = terrainObject;
            _entityFactory = entityFactory;
        }

        public void SetCurrentMode(MapEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SetCurrentEntity(EntityType entityType) => _currentEntity = entityType;

        public void ProcessTile(TileObject tileObject)
        {
            switch (_currentMode)
            {
                case MapEditorMode.None:
                    break;
                case MapEditorMode.SetRegion:
                    SetRegion(tileObject);
                    break;
                case MapEditorMode.RemoveRegion:
                    RemoveRegion(tileObject);
                    break;
                case MapEditorMode.SetEntity:
                    //SetEntity(tileObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetRegion(TileObject tileObject) =>
            _terrainObject.Regions.Set(tileObject, _currentRegion);

        private void RemoveRegion(TileObject tileObject) =>
            _terrainObject.Regions.Remove(tileObject);
        
        private void SetEntity(TileObject tileObject)
        {
            //if (tileObject.Region.Type == RegionType.None)
            //     return;

            //if (tileObject.Region.Capital != null)
            //    return;

            var entity = _entityFactory.Create(_currentEntity, tileObject);
            tileObject.SetEntity(entity);
            //tileObject.Region.Capital = entity as CapitalController;
        }
    }
}
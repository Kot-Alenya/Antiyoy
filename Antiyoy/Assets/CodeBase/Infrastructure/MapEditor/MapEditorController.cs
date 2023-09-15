using _dev;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Tile;
using CodeBase.Infrastructure.MapEditor.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorController
    {
        private readonly TerrainObject _terrainObject;
        private readonly MapEditorModel _model;

        public MapEditorController(TerrainObject terrainObject, MapEditorModel model)
        {
            _terrainObject = terrainObject;
            _model = model;
        }

        public void SetRegionMode(RegionType regionType)
        {
            _model.SetCurrentMode(MapEditorMode.SetRegion);
            _model.SetCurrentRegion(regionType);
        }

        public void RemoveRegionMode() => _model.SetCurrentMode(MapEditorMode.RemoveRegion);

        public void SetEntityMode(EntityType entityType)
        {
            _model.SetCurrentMode(MapEditorMode.SetEntity);
            _model.SetCurrentEntity(entityType);
        }

        public void ProcessTile(TileObject tileObject) => _model.ProcessTile(tileObject);

        public void SelectTile(Vector2 hitPoint)
        {
            var hexPosition = HexMath.FromWorldPosition(hitPoint);

            if (_terrainObject.IsHexInTerrain(hexPosition))
                UnityEngine.Debug.Log(_terrainObject.Tiles.Get(hexPosition).Coordinates);
        }

        public void ProcessTiles()
        {
            //UnityEngine.Debug.Log("Process");
        }
    }
}
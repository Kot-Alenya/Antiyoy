using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Tile;
using System;
using CodeBase.Infrastructure.MapEditor.Data;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorModel
    {
        private readonly TerrainObject _terrainObject;
        private MapEditorMode _currentMode;
        private RegionObject _region;

        public MapEditorModel(TerrainObject terrainObject) => _terrainObject = terrainObject;

        public void SetMode(MapEditorMode mode) => _currentMode = mode;

        public void SetRegion(RegionType region) => _region = _terrainObject.Regions.Get(region);

        public void ProcessTile(TileObject tileObject)
        {
            switch (_currentMode)
            {
                case MapEditorMode.None:
                    break;
                case MapEditorMode.Region:
                    tileObject.SetRegion(_region);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
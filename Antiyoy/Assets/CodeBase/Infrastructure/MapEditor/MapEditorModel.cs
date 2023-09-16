using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Tile;
using System;
using System.Collections.Generic;
using _dev;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region;
using CodeBase.Infrastructure.MapEditor.Data;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorModel
    {
        private readonly TerrainObject _terrainObject;
        private readonly TileFactory _tileFactory;
        private readonly EntityFactory _entityFactory;
        private readonly List<TileObject> _currentTiles = new();
        private readonly List<RegionObject> _changedRegions = new();
        private MapEditorMode _currentMode;
        private RegionType _currentRegion;
        private EntityType _currentEntity;

        public MapEditorModel(TerrainObject terrainObject, EntityFactory entityFactory, TileFactory tileFactory)
        {
            _terrainObject = terrainObject;
            _entityFactory = entityFactory;
            _tileFactory = tileFactory;
        }

        public void SetCurrentMode(MapEditorMode mode) => _currentMode = mode;

        public void SetCurrentRegion(RegionType region) => _currentRegion = region;

        public void SetCurrentEntity(EntityType entityType) => _currentEntity = entityType;

        public void SelectTile(HexPosition hex)
        {
            if (!_terrainObject.TryGetTile(hex, out var tileObject))
                return;

            if (!_changedRegions.Contains(tileObject.Region))
                _changedRegions.Add(tileObject.Region);
            
            if (_currentMode == MapEditorMode.RemoveTiles)
                _terrainObject.DisableTile();
            else if (_currentMode == MapEditorMode.SetTiles)
            {
                _terrainObject.GetRegion(tileObject);
                _terrainObject.EnableTile(_currentRegion); //а как это расчитаеться ?
            }

            _currentTiles.Add(tileObject);

            //1 есть активные тайлы и не активные.
            //(3-1) тайлы создаются вместе с террейном.
            //3 операции активации и де активации аналогичны удалению и созданию, но ДЕШЕВЛЕ

            //СПОРНЫЕ:
            //4 не активные тайлы можно Не сохранять, они существуют только в mapEditor`e
            //5 операции активации и де-активации доступны только из mapEditor!
            //6 тайлы создаються не активными.
        }

        public void ProcessTiles()
        {
            switch (_currentMode)
            {
                case MapEditorMode.None:
                    break;
                case MapEditorMode.SetTiles:
                    RecalculateTiles();
                    break;
                case MapEditorMode.RemoveTiles:
                    RecalculateTiles();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _currentTiles.Clear();
        }

        private void RecalculateTiles()
        {
            foreach (var regionObject in _changedRegions)
            {
                _terrainObject.RecalculateRegion(regionObject);
            }//ЭТО НЕ ЗАТРАГИВАЕТ ВКЛЮЧЕННЫЕ ТАЙЛЫ!
            
            //пересчёт регионов.
            //нет тайлов вне регионов.
            //все тайлы должны быть в регионах
            //регионы: neutral, red, blue и т.д.

            //пересчёт региона,
        }
    }
}
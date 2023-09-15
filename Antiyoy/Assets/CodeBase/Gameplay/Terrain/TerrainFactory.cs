using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Tile;
using CodeBase.Gameplay.Tile.Data;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainFactory
    {
        private readonly TerrainStaticData _staticData;
        private readonly RegionFactory _regionFactory;
        private readonly TileFactory _tileFactory;

        public TerrainFactory(StaticData data, RegionFactory regionFactory, TileFactory tileFactory)
        {
            _staticData = data.TerrainStaticData;
            _regionFactory = regionFactory;
            _tileFactory = tileFactory;
        }

        public TerrainObject Create()
        {
            var gameObject = new GameObject(nameof(TerrainObject));
            var tiles = CreateTerrainTiles(gameObject.transform, _staticData.Size);
            var regions = new TerrainRegions();
            var terrain = new TerrainObject(tiles, regions, _staticData.Size);

            ConnectTiles(terrain);
            //как создавать террейн?

            //создать все нужные массивы.
            //добавлять в террейн новые тайлы, созданные в factory.
            //как определить местоположение для создания тайла?

            //создать все нужные массивы и упращённые тайлы.
            //настраиваем(активируем) тайлы в террейне.

            //создаём клектки(background), клетки определяют позиции гексов.

            return terrain;
        }

        private TerrainTiles CreateTerrainTiles(Transform root, Vector2Int size)
        {
            var tiles = new TerrainTiles(size);

            for (var y = 0; y < size.y; y++)
            for (var x = 0; x < size.x; x++)
            {
                var arrayIndex = new Vector2Int(x, y);
                var coordinates = HexMath.FromArrayIndex(arrayIndex);
                var tile = _tileFactory.Create(root, coordinates);

                tiles.Set(tile, coordinates);
            }

            return tiles;
        }

        private void ConnectTiles(TerrainObject terrain)
        {
            foreach (var tile in terrain.Tiles)
            foreach (var direction in HexPositionDirections.Directions)
            {
                var neighbourTileHex = tile.Coordinates + direction;

                if (!terrain.IsHexInTerrain(neighbourTileHex))
                    continue;

                var neighbourTile = terrain.Tiles.Get(neighbourTileHex);
                var connection = new TileConnection(neighbourTile);

                tile.Connections.Add(connection);
            }
        }

        private void CreateBackground(Transform root, Vector2Int size)
        {
            var instance = new GameObject();

            instance.transform.SetParent(root);
            //instance.transform.;

            //нужен доступ к расчёту позиции.
            //из hex в position & position в hex
        }
    }
}
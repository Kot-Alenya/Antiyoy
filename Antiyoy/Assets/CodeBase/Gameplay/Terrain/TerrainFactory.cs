using _dev;
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
        private readonly TileFactory _tileFactory;

        public TerrainFactory(StaticData data, TileFactory tileFactory)
        {
            _staticData = data.TerrainStaticData;
            _tileFactory = tileFactory;
        }

        public TerrainObject Create()
        {
            var gameObject = new GameObject(nameof(TerrainObject));
            var tiles = CreateTerrainTiles(gameObject.transform, _staticData.Size);
            var regions = CreateRegions(tiles);
            var terrain = new TerrainObject(tiles, regions);

            ConnectTiles(tiles);

            return terrain;
        }

        private TerrainTiles CreateTerrainTiles(Transform root, Vector2Int size)
        {
            var tiles = new TerrainTiles(size);

            for (var y = 0; y < size.y; y++)
            for (var x = 0; x < size.x; x++)
            {
                var coordinates = new HexCoordinates(x, y);
                var tile = _tileFactory.Create(root, coordinates);

                tiles.Set(tile, coordinates);
            }

            return tiles;
        }

        private void ConnectTiles(TerrainTiles tiles)
        {
            foreach (var tile in tiles)
            foreach (var direction in HexCoordinatesDirections.Directions)
            {
                var neighbourTileIndex = tile.Coordinates + direction;

                if (!tiles.IsIndexValid(neighbourTileIndex))
                    continue;

                var neighbourTile = tiles.Get(neighbourTileIndex);
                var connection = new TileConnection(neighbourTile);

                tile.Connections.Add(connection);
            }
        }

        private TerrainRegions CreateRegions(TerrainTiles tiles)
        {
            //tiles.
            return default;
        }
    }
}
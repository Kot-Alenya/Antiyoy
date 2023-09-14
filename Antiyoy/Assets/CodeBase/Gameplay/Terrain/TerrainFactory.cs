using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Region.Data;
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
            var regions = CreateRegions();
            var tiles = CreateTerrainTiles(regions, gameObject.transform, _staticData.Size);
            var terrain = new TerrainObject(tiles, regions);

            ConnectTiles(tiles);

            return terrain;
        }

        private TerrainRegions CreateRegions()
        {
            return new TerrainRegions();
        }

        private TerrainTiles CreateTerrainTiles(TerrainRegions regions, Transform root, Vector2Int size)
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
                var neighbourTileHex = tile.Coordinates + direction;

                if (!tiles.IsHexValid(neighbourTileHex))
                    continue;

                var neighbourTile = tiles.Get(neighbourTileHex);
                var connection = new TileConnection(neighbourTile);

                tile.Connections.Add(connection);
            }
        }
    }
}
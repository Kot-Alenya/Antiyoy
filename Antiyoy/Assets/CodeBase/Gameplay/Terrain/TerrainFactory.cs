using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Gameplay.Terrain.Tile.Object;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainObject
    {
        private readonly TerrainTiles _tiles;
    }

    public class TerrainGrid
    {
        private readonly TerrainTiles _tiles;

        public TerrainGrid(TerrainTiles tiles)
        {
            _tiles = tiles;
        }

        public void ConnectTiles(TileObject root, TileObject neighbour)
        {
            root.TileObjectData.Neighbours.Add(neighbour);
        }
    }

    public class TerrainFactory
    {
        private readonly TileFactory _tileFactory;
        private TerrainStaticData _staticData;

        public TerrainFactory(TileFactory tileFactory) => _tileFactory = tileFactory;

        public void Initialize(TerrainStaticData staticData) => _staticData = staticData;

        public void Create()
        {
            var root = new GameObject(nameof(TerrainObject)).transform;
            var tiles = CreateTerrainTiles(root, _staticData.Size);
            CreateTerrainGrid(tiles);

            DecorateTerrainTiles(tiles);
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

        private void CreateTerrainGrid(TerrainTiles tiles)
        {
            var grid = new TerrainGrid(tiles);

            foreach (var rootTile in tiles)
            foreach (var direction in HexCoordinatesDirections.Directions)
            {
                var neighbourTileIndex = rootTile.Coordinates + direction;

                if (tiles.IsIndexValid(neighbourTileIndex))
                    grid.ConnectTiles(rootTile, tiles.Get(neighbourTileIndex));
            }

            //return grid;
        }

        private void DecorateTerrainTiles(TerrainTiles tiles)
        {
            var colors = new[]
            {
                Color.blue, Color.green, Color.cyan, Color.magenta, Color.yellow, Color.white, Color.gray,
                Color.red
            };

            foreach (var tile in tiles)
            {
                if (tile == null)
                    continue;

                tile.TileObjectData.gameObject.GetComponent<SpriteRenderer>().color =
                    colors[Random.Range(0, colors.Length)];

                tile.TileObjectData.DebugText.text = tile.Coordinates.ToString();
            }
        }
    }
}
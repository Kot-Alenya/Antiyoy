using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainFactory
    {
        private readonly TileFactory _tileFactory;
        private TerrainStaticData _staticData;

        public TerrainFactory(TileFactory tileFactory) => _tileFactory = tileFactory;

        public void Initialize(TerrainStaticData staticData) => _staticData = staticData;

        public TerrainObject Create()
        {
            var gameObject = new GameObject(nameof(TerrainObject));
            var tiles = CreateTerrainTiles(gameObject.transform, _staticData.Size);
            var terrain = new TerrainObject();

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
            foreach (var direction in _staticData.WaveDirections)
            {
                var neighbourTileIndex = tile.Coordinates + direction;

                if (!tiles.IsIndexValid(neighbourTileIndex))
                    continue;

                var neighbourTile = tiles.Get(neighbourTileIndex);
                var connection = new TileConnection(neighbourTile);
                
                tile.AddConnection(connection);
            }
        }
    }
}
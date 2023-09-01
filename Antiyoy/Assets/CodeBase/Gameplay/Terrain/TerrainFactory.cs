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

        public void ConnectNodes(TileObject first, TileObject second)
        {
        }
    }

    public class TerrainFactory
    {
        private readonly TileFactory _tileFactory;
        private TerrainStaticData _staticData;

        public TerrainFactory(TileFactory tileFactory) => _tileFactory = tileFactory;

        public void Initialize(TerrainStaticData staticData)
        {
            _staticData = staticData;
        }

        public void Create()
        {
            var root = new GameObject(nameof(TerrainObject)).transform;
            var tiles = CreateTerrainTiles(root, _staticData.Size);
            CreateTerrainGrid(tiles, _staticData.Size);

            DecorateTerrainTiles(tiles);
        }

        private TerrainTiles CreateTerrainTiles(Transform root, Vector2Int size)
        {
            var tiles = new TerrainTiles(size);

            for (var y = 0; y < size.y; y++)
            for (var x = 0; x < size.x; x++)
            {
                var index = new Vector2Int(x, y);
                var tile = _tileFactory.Create(root, index);

                tiles.Set(tile);
            }

            return tiles;
        }

        private void CreateTerrainGrid(TerrainTiles tiles, Vector2Int size)
        {
        }

        private void DecorateTerrainTiles(TerrainTiles tiles)
        {
            var colors = new Color[]
            {
                Color.blue, Color.green, Color.cyan, Color.magenta, Color.yellow, Color.white, Color.black, Color.gray,
                Color.red
            };

            foreach (var tile in tiles)
            {
                if (tile == null)
                    continue;

                tile.TileObjectData.gameObject.GetComponent<SpriteRenderer>().color =
                    colors[Random.Range(0, colors.Length)];
            }
        }
    }
}
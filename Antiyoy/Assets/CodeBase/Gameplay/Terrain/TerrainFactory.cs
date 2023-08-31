using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Gameplay.Terrain.Tile.Object;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainObject
    {
        private readonly TerrainTiles _tiles;
        private readonly Transform _root;
    }

    public class TerrainFactory
    {
        private readonly TileFactory _tileFactory;
        private TerrainStaticData _staticData;
        private TerrainTileIndexOffsets _tileIndexOffsets;

        public TerrainFactory(TileFactory tileFactory) => _tileFactory = tileFactory;

        public void Initialize(TerrainStaticData staticData)
        {
            _staticData = staticData;
            _tileIndexOffsets = CreateTileIndexOffsets();
        }

        public void Create()
        {
            var root = new GameObject(nameof(TerrainObject)).transform;
            var tiles = CreateTerrainTiles(root, _staticData.Size);

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

        private TerrainTileIndexOffsets CreateTileIndexOffsets()
        {
            var up = new Vector2Int(0, 1);
            var down = new Vector2Int(0, -1);

            var rightUp = new Vector2Int(1, 1);
            var rightDown = new Vector2Int(1, 0);

            var leftUp = new Vector2Int(-1, 1);
            var leftDown = new Vector2Int(-1, 0);

            return new TerrainTileIndexOffsets
            {
                Up = up,
                Down = down,
                RightUp = rightUp,
                RightDown = rightDown,
                LeftUp = leftUp,
                LeftDown = leftDown,
                Offsets = new[]
                {
                    up, down, rightUp, rightDown, leftUp, leftDown
                }
            };
        }

        private TerrainTiles CreateTerrainTiles(Transform root, Vector2Int size)
        {
            var tiles = new TerrainTiles(size);
            var frontTiles = new List<TileObject>();

            var firstTile = _tileFactory.Create(root, Vector2.zero, Vector2Int.zero);
            SetTile(firstTile, frontTiles, tiles);

            for (var y = 0; y < size.y; y++)
            {
                for (var x = 0; x < size.x; x++)
                {
                    var index = new Vector2Int(x, y);
                    var position = GetTilePosition(index);
                    var newTile = _tileFactory.Create(root, position, index);

                    SetTile(newTile, frontTiles, tiles);
                }
            }

            /*
            for (var i = 0; i < 100; i++)
            {
                var tile = frontTiles[0];

                foreach (var offset in _tileIndexOffsets.Offsets)
                {
                    var newTileIndex = tile.Index + offset;

                    if (!tiles.IsTileInTerrain(newTileIndex) || tiles.IsTileCreated(newTileIndex))
                        continue;

                    if (newTileIndex != _tileIndexOffsets.RightDown)
                        continue;

                    var position = GetTilePosition(offset, tile);
                    var newTile = _tileFactory.Create(root, position, newTileIndex);

                    SetTile(newTile, frontTiles, tiles);
                }

                frontTiles.Remove(tile);
            }
            */

            return tiles;
        }

        private Vector2 GetTilePosition(Vector2Int index)
        {
            var positionOffsets = _tileFactory.GetTilePositionOffsets();
            var tileSmallerDiameter = 0.8660254f;
            var a = 0.75f;

            var x = index.x * a;
            var y = index.y * tileSmallerDiameter;
            var offsetY = tileSmallerDiameter / 2;

            if (index.x == 0)
                return new Vector2(0, y);

            if (index.x % 2 == 0)
                return new Vector2(x, y);

            return new Vector2(x, y + offsetY);
        }

        private void SetTile(TileObject tile, List<TileObject> frontTiles, TerrainTiles terrain)
        {
            frontTiles.Add(tile);
            terrain.Set(tile);
        }

        private Vector2 GetTilePosition(Vector2Int newTileIndexOffset, TileObject parentTile)
        {
            var positionOffsets = _tileFactory.GetTilePositionOffsets();
            var parentPosition = (Vector2)parentTile.TileObjectData.transform.position;

            if (newTileIndexOffset == _tileIndexOffsets.Up)
                return parentPosition + positionOffsets.Up;

            if (newTileIndexOffset == _tileIndexOffsets.Down)
                return parentPosition + positionOffsets.Down;

            if (newTileIndexOffset == _tileIndexOffsets.RightUp)
                return parentPosition + positionOffsets.RightUp;

            if (newTileIndexOffset == _tileIndexOffsets.RightDown)
                return parentPosition + positionOffsets.RightDown;

            if (newTileIndexOffset == _tileIndexOffsets.LeftUp)
                return parentPosition + positionOffsets.LeftUp;

            if (newTileIndexOffset == _tileIndexOffsets.LeftDown)
                return parentPosition + positionOffsets.LeftDown;

            return default;
        }
    }
}
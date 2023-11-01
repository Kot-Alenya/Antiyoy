using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Tile.Collection
{
    public static class TileCollectionUtilities
    {
        public static void ConnectWithNeighbors(TileData tile, ITileCollection tileCollection)
        {
            var neighbours = GetNeighbours(tile.Hex, tileCollection);

            foreach (var neighbour in neighbours)
            {
                neighbour.Neighbors.Add(tile);
                tile.Neighbors.Add(neighbour);
            }
        }

        public static void DisconnectFromNeighbors(TileData tile)
        {
            foreach (var neighbour in tile.Neighbors)
                RemoveFromNeighbors(neighbour, tile);

            tile.Neighbors.Clear();
        }

        private static List<TileData> GetNeighbours(HexPosition rootHex, ITileCollection tileCollection)
        {
            var neighbours = new List<TileData>();

            foreach (var direction in HexPositionDirections.Directions)
            {
                var neighbourHex = rootHex + direction;

                if (!tileCollection.IsInCollection(neighbourHex))
                    continue;

                var neighbour = tileCollection.Get(neighbourHex);

                if (neighbour != null)
                    neighbours.Add(neighbour);
            }

            return neighbours;
        }

        private static void RemoveFromNeighbors(TileData rootTile, TileData tileData)
        {
            for (var i = 0; i < rootTile.Neighbors.Count; i++)
            {
                if (rootTile.Neighbors[i] == tileData)
                {
                    rootTile.Neighbors.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Tile;

namespace CodeBase.Gameplay.World.NewTerrain
{
    public static class TerrainUtilities
    {
        public static void ConnectWithNeighbors(TileObject tile, TileArray array)
        {
            var neighbours = GetNeighbours(tile.Hex, array);

            foreach (var neighbour in neighbours)
            {
                neighbour.Neighbors.Add(tile);
                tile.Neighbors.Add(neighbour);
            }
        }

        public static void DisconnectFromNeighbors(TileObject tile)
        {
            foreach (var neighbour in tile.Neighbors)
                RemoveFromNeighbors(neighbour, tile);

            tile.Neighbors.Clear();
        }

        private static List<TileObject> GetNeighbours(HexPosition rootHex, TileArray array)
        {
            var neighbours = new List<TileObject>();

            foreach (var direction in HexPositionDirectionUtilities.Directions)
            {
                var neighbourHex = rootHex + direction;

                if (!array.IsInArray(neighbourHex))
                    continue;

                var neighbour = array.Get(neighbourHex);

                if (neighbour != null)
                    neighbours.Add(neighbour);
            }

            return neighbours;
        }

        private static void RemoveFromNeighbors(TileObject rootTile, TileObject tileData)
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
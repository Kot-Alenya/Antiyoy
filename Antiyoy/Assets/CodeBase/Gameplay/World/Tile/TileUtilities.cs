using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Tile;

namespace CodeBase.Gameplay.World.NewTerrain
{
    public static class TileUtilities
    {
        public static List<TileObject> FindNeighbors(HexPosition rootTileHex, TileArray tileArray)
        {
            var neighbours = new List<TileObject>();

            foreach (var direction in HexPositionDirectionUtilities.Directions)
                if (tileArray.TryGet(rootTileHex + direction, out var neighbor))
                    neighbours.Add(neighbor);

            return neighbours;
        }

        public static bool TryGetRegionFromNeighbors(TileObject tile, RegionType necessaryRegionType,
            out RegionObject region)
        {
            foreach (var neighbor in tile.Neighbors.Where(n => n.Region.Type == necessaryRegionType))
            {
                region = neighbor.Region;
                return true;
            }

            region = default;
            return false;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Tile
{
    public static class TileUtilities
    {
        public static List<TileData> FindNeighbors(HexPosition rootTileHex, TileArray tileArray)
        {
            var neighbours = new List<TileData>();

            foreach (var direction in HexPositionDirectionUtilities.Directions)
                if (tileArray.TryGet(rootTileHex + direction, out var neighbor))
                    neighbours.Add(neighbor);

            return neighbours;
        }

        public static bool TryGetRegionFromNeighbors(TileData tile, RegionType necessaryRegionType,
            out RegionData region)
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
using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainPathFinding
    {
        public List<TileData> GetTilesToMoveUnit(UnitData unit)
        {
            var result = new List<TileData>();
            var front = new List<TileData>(unit.RootTile.Neighbors);

            while (front.Count > 0)
            {
                var tile = front[0];

                foreach (var neighbor in tile.Neighbors)
                {
                    if (HexPosition.GetMagnitude(neighbor.Hex, unit.RootTile.Hex) > unit.Preset.MoveRange)
                        continue;

                    if (neighbor == unit.RootTile)
                        continue;

                    if (tile.Region != unit.RootTile.Region)
                        continue;

                    if (!front.Contains(neighbor) && !result.Contains(neighbor))
                        front.Add(neighbor);
                }

                front.Remove(tile);
                result.Add(tile);
            }

            return result;
        }
    }
}
using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.Player.States.Unit.Create
{
    public static class PlayerCreateUnitUtilities
    {
        public static List<TileData> GetTilesToCreateUnit(UnitType unitType, RegionData region)
        {
            if (PlayerUnitUtilities.IsCombatUnit(unitType))
                return GetTilesToCreateCombatUnit(region);

            return GetTilesToCreateNotCombatUnit(region);
        }



        private static List<TileData> GetTilesToCreateCombatUnit(RegionData region)
        {
            var result = new List<TileData>();

            foreach (var tile in region.Tiles)
            {
                foreach (var neighbor in tile.Neighbors)
                {
                    if (neighbor.Region.Type != region.Type)
                        if (!result.Contains(neighbor))
                            result.Add(neighbor);
                }

                switch (tile.Unit.Type)
                {
                    case UnitType.Farm:
                    case UnitType.Knight:
                        continue;
                }

                result.Add(tile);
            }

            return result;
        }

        private static List<TileData> GetTilesToCreateNotCombatUnit(RegionData region)
        {
            var result = new List<TileData>();

            foreach (var tile in region.Tiles)
            {
                switch (tile.Unit.Type)
                {
                    case UnitType.Farm:
                    case UnitType.Pine:
                        continue;
                }

                result.Add(tile);
            }

            return result;
        }
    }
}
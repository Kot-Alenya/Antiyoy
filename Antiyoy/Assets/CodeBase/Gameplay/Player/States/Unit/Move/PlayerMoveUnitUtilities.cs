﻿using System.Collections.Generic;
using CodeBase.Gameplay.Player.States.Unit.Create;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.Player.States.Unit.Move
{
    public static class PlayerMoveUnitUtilities
    {
        public static List<TileData> GetTilesToMoveUnit(UnitData unit)
        {
            var result = new List<TileData>();
            var front = new List<TileData> { unit.RootTile };

            while (front.Count > 0)
            {
                var tile = front[0];

                foreach (var neighbor in tile.Neighbors)
                {
                    if (HexPosition.GetMagnitude(neighbor.Hex, unit.RootTile.Hex) > unit.Preset.MoveRange)
                        continue;

                    if (tile.Region != unit.RootTile.Region)
                        continue;

                    if (PlayerUnitUtilities.IsCombatUnit(neighbor.Unit.Type))
                        if (!PlayerCombineUnitUtilities.TryCombinedUnitType(unit.Type, neighbor.Unit.Type, out _))
                            continue;

                    if (!front.Contains(neighbor) && !result.Contains(neighbor))
                        front.Add(neighbor);
                }

                front.Remove(tile);
                if (tile != unit.RootTile)
                    result.Add(tile);
            }

            return result;
        }
    }
}
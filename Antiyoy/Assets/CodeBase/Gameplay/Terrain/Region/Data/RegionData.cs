using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Region.Data
{
    public class RegionData
    {
        public RegionData(RegionType type, Color color)
        {
            Type = type;
            Color = color;
        }

        public List<TileData> Tiles { get; } = new();

        public RegionType Type { get; }

        public Color Color { get; }

        public void MoveTiles(RegionData toRegion)
        {
            foreach (var tile in Tiles)
            {
                toRegion.Tiles.Add(tile);
                tile.SetRegion(toRegion);
            }

            Tiles.Clear();
        }
    }
}
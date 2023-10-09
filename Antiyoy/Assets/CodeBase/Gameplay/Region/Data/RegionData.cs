using System.Collections.Generic;
using CodeBase.Gameplay.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Region.Data
{
    public class RegionData
    {
        public readonly List<TileData> Tiles = new();
        public readonly RegionType Type;
        public readonly Color Color;
        
        public RegionData(RegionType type, Color color)
        {
            Type = type;
            Color = color;
        }
    }
}
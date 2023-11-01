using System.Collections.Generic;
using CodeBase.Gameplay.World.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.World.Region
{
    public class RegionObject
    {
        public readonly List<TileObject> Tiles = new();
        public readonly RegionType Type;
        public readonly Color Color;
        public readonly int Id;

        public int Income;
        public int CoinsCount;

        public RegionObject(RegionType type, Color color, int id)
        {
            Type = type;
            Color = color;
            Id = id;
        }
    }
}
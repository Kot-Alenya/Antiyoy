﻿using System.Collections.Generic;
using CodeBase.Gameplay.World.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Region.Data
{
    public class RegionData
    {
        public readonly List<TileData> Tiles = new();
        public readonly RegionType Type;
        public readonly Color Color;
        public readonly int Id;

        public int Income;
        
        public RegionData(RegionType type, Color color, int id)
        {
            Type = type;
            Color = color;
            Id = id;
        }
    }
}
using System.Collections.Generic;
using _dev;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Region
{
    public class RegionObject
    {
        public RegionObject(RegionType type, Color color)
        {
            Type = type;
            Color = color;
        }

        //нужно для рекалькуляции!
        public List<TileObject> Tiles { get; private set; } = new();

        public CapitalController Capital { get; set; }

        public RegionType Type { get; private set; }

        public Color Color { get; private set; }
    }
}
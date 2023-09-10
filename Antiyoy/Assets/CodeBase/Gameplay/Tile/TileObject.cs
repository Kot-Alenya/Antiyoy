using System.Collections.Generic;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TileObject : MonoBehaviour
    {
        private TilePrefabData _instance;

        public List<TileConnection> Connections { get; private set; } = new();

        public HexCoordinates Coordinates { get; private set; }

        public RegionObject Region { get; private set; }

        public void Constructor(TilePrefabData instance, HexCoordinates coordinates)
        {
            _instance = instance;
            Coordinates = coordinates;
        }

        public void SetRegion(RegionObject region)
        {
            Region = region;

            region.Tiles.Add(this);
            _instance.SpriteRenderer.color = region.Color;
        }
    }
}
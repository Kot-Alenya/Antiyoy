using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Data.Hex;
using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.Terrain.Region.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile.Data
{
    public class TileData
    {
        private readonly TilePrefabData _instance;

        public TileData(TilePrefabData instance, HexPosition hex)
        {
            _instance = instance;
            Hex = hex;
        }

        public List<TileData> Neighbors { get; } = new();

        public HexPosition Hex { get; }

        public RegionData Region { get; private set; }

        public GameObject GameObject => _instance.gameObject;

        public TextMeshProUGUI DebugText => _instance.DebugText;

        public void SetRegion(RegionData region)
        {
            Region = region;
            _instance.SpriteRenderer.color = region.Color;
        }

        public void RemoveFromNeighbors(TileData tileData)
        {
            for (var i = 0; i < Neighbors.Count; i++)
            {
                if (Neighbors[i] == tileData)
                {
                    Neighbors.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
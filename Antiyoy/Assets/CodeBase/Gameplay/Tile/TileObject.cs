using System.Collections.Generic;
using _dev;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Tile.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TileObject : MonoBehaviour
    {
        private TilePrefabData _instance;

        public List<TileConnection> Connections { get; private set; } = new();

        public HexPosition Coordinates { get; private set; }

        public RegionObject Region { get; private set; }

        public IEntityController Entity { get; private set; }

        public TextMeshProUGUI DebugText => _instance.DebugText;

        public void Constructor(TilePrefabData instance, HexPosition coordinates)
        {
            _instance = instance;
            Coordinates = coordinates;
        }

        public void SetRegion(RegionObject region)
        {
            Region = region;
            _instance.SpriteRenderer.color = region.Color;
        }

        public void SetEntity(IEntityController entity) => Entity = entity;

        public void RemoveRegion()
        {
            Region = null;
            _instance.SpriteRenderer.color = Color.clear;
        }
    }
}
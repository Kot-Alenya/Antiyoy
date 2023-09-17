using System.Collections.Generic;
using _dev;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Tile.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public struct TileData
    {
        public RegionObject Region;
        public HexPosition Hex;
    }

    public class TileObject //model + controller or just DATA
    {
        private readonly TilePrefabData _instance;

        public List<TileObject> Connections { get; private set; } = new();
        //что делать со связями?

        public HexPosition Hex { get; private set; }

        public RegionObject Region { get; private set; }

        public TextMeshProUGUI DebugText => _instance.DebugText;

        public TileObject(TilePrefabData instance, HexPosition coordinates, RegionType regionType)
        {
            _instance = instance;
            Hex = coordinates;
            Type = regionType;
        }

        public RegionType Type { get; set; }
        public Object GameObject => _instance.gameObject;

        public void Initialize(RegionObject regionObject)
        {
            Region = regionObject;
            _instance.SpriteRenderer.color = regionObject.Color;
        }

        public void Dispose()
        {
        }

        public void RemoveFromConnections(TileObject tileObject)
        {
            for (var i = 0; i < Connections.Count; i++)
            {
                if (Connections[i] == tileObject)
                {
                    Connections.RemoveAt(i);
                    return;
                }
            }
        }

        public void SetRegion(RegionObject region)
        {
            Region = region;
            _instance.SpriteRenderer.color = region.Color;
        }
    }
}
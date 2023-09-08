using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TileObject : MonoBehaviour
    {
        private TilePrefabData _instance;

        public List<TileConnection> Connections { get; private set; }

        public HexCoordinates Coordinates { get; private set; }

        public void Constructor(TilePrefabData instance, HexCoordinates coordinates)
        {
            _instance = instance;
            Coordinates = coordinates;
            Connections = new();
        }

        public void SetColor(Color color) => _instance.SpriteRenderer.color = color;
    }
}
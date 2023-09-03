using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile
{
    public class TileObject
    {
        private readonly List<TileConnection> _connections = new();
        private readonly TileObjectStaticData _data;

        public TileObject(TileObjectStaticData data, HexCoordinates coordinates)
        {
            Coordinates = coordinates;
            _data = data;
        }

        public HexCoordinates Coordinates { get; }

        public GameObject GameObject => _data.gameObject;
        
        public TileObjectStaticData Data => _data;

        public void AddConnection(TileConnection connection) => _connections.Add(connection);

        public List<TileConnection> GetConnections() => _connections;
    }
}
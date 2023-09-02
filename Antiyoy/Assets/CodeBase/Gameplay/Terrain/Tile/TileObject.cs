using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Terrain.Tile
{
    public class TileObject
    {
        private readonly List<TileConnection> _connections = new();

        public TileObject(TileObjectStaticData data, HexCoordinates coordinates)
        {
            Data = data;
            Coordinates = coordinates;
        }

        public TileObjectStaticData Data { get; }

        public HexCoordinates Coordinates { get; }

        public void AddConnection(TileConnection connection) => _connections.Add(connection);

        public List<TileConnection> GetConnections() => _connections;
    }
}
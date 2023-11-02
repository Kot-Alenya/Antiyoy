using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Tile
{
    public class TileConnector
    {
        public void Connect(TileData rootTile, List<TileData> tiles)
        {
            foreach (var tile in tiles)
            {
                rootTile.Neighbors.Add(tile);
                tile.Neighbors.Add(rootTile);
            }
        }

        public void Disconnect(TileData rootTile, List<TileData> tiles)
        {
            foreach (var tile in tiles)
            {
                rootTile.Neighbors.Remove(tile);
                tile.Neighbors.Remove(rootTile);
            }
        }
    }
}
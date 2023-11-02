using System.Collections.Generic;
using CodeBase.Gameplay.World.Tile;

namespace CodeBase.Gameplay.World.NewTerrain
{
    public class TileConnector
    {
        public void Connect(TileObject rootTile, List<TileObject> tiles)
        {
            foreach (var tile in tiles)
            {
                rootTile.Neighbors.Add(tile);
                tile.Neighbors.Add(rootTile);
            }
        }

        public void Disconnect(TileObject rootTile, List<TileObject> tiles)
        {
            foreach (var tile in tiles)
            {
                rootTile.Neighbors.Remove(tile);
                tile.Neighbors.Remove(rootTile);
            }
        }
    }
}
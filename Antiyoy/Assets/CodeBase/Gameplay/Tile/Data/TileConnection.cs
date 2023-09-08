namespace CodeBase.Gameplay.Tile.Data
{
    public struct TileConnection
    {
        public readonly TileObject ConnectedTile;

        public TileConnection(TileObject connectedTile) => ConnectedTile = connectedTile;
    }
}
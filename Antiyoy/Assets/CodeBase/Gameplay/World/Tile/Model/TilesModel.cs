using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile.Model
{
    public class TilesModel
    {
        private readonly TileArray _tiles;
        private readonly Transform _root;
        private readonly TileFactory _tileFactory;
        private readonly TilesNeighborsTool _neighborsTool;

        public TilesModel(TileArray tiles, Transform root, TileFactory tileFactory)
        {
            _tiles = tiles;
            _root = root;
            _tileFactory = tileFactory;
            _neighborsTool = new TilesNeighborsTool(tiles);
        }

        public Vector2Int Size => _tiles.Size;

        public bool IsHexInTiles(HexPosition hex) => _tiles.IsInArraySize(hex);

        public bool TryGetTile(HexPosition hex, out TileData tile) => _tiles.TryGet(hex, out tile);

        public TileData CreateTile(HexPosition hex)
        {
            var tile = _tileFactory.Create(_root, hex);

            _tiles.Set(tile, tile.Hex);
            _neighborsTool.ConnectNeighbors(tile);

            return tile;
        }

        public void DestroyTile(TileData tile)
        {
            _neighborsTool.DisconnectNeighbors(tile);
            _tiles.Remove(tile.Hex);
            _tileFactory.DestroyInstance(tile);
        }
    }
}
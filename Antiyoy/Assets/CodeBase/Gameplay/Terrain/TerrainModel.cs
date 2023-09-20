using CodeBase.Gameplay.Terrain.Data.Hex;
using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainModel
    {
        private readonly TerrainTiles _tiles;
        private readonly TerrainRegions _regions;
        private readonly GameObject _instance;
        private readonly TileFactory _tileFactory;

        public TerrainModel(TerrainTiles tiles, TerrainRegions regions, GameObject instance, TileFactory tileFactory)
        {
            _tiles = tiles;
            _regions = regions;
            _instance = instance;
            _tileFactory = tileFactory;
        }

        public bool IsHexInTerrain(HexPosition hex) => _tiles.IsHexInArraySize(hex);

        public bool TryGetTile(HexPosition hex, out TileData tile) => _tiles.TryGet(hex, out tile);

        public void CreateTile(HexPosition hex, RegionType regionType)
        {
            var tile = _tileFactory.Create(_instance.transform, hex);

            _tiles.Set(tile, tile.Hex);
            ConnectNeighbors(tile);
            _regions.Add(tile, regionType);
        }

        public void DestroyTile(TileData tile)
        {
            DisconnectNeighbors(tile);
            _regions.Remove(tile);

            _tiles.Set(null, tile.Hex);
            _tileFactory.Destroy(tile);
        }

        public void RecalculateChangedRegions() => _regions.RecalculateChangedRegions();

        private void ConnectNeighbors(TileData tile)
        {
            var neighbours = _tiles.GetNeighbours(tile.Hex);

            foreach (var neighbour in neighbours)
            {
                neighbour.Neighbors.Add(tile);
                tile.Neighbors.Add(neighbour);
            }
        }

        private void DisconnectNeighbors(TileData tile)
        {
            foreach (var neighbour in tile.Neighbors)
                neighbour.RemoveFromNeighbors(tile);

            tile.Neighbors.Clear();
        }
    }
}
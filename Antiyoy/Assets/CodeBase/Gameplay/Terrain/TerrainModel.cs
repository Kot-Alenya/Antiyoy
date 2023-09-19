using System.Collections.Generic;
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
        private readonly List<RegionData> _changedRegions = new();
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
            ConnectNeighbours(tile);
            ConnectToRegion(tile, regionType);
        }

        public void DestroyTile(TileData tile)
        {
            DisconnectNeighbours(tile);
            DisconnectFromRegion(tile);

            _tiles.Set(null, tile.Hex);
            _tileFactory.Destroy(tile);
        }

        public void RecalculateChangedRegions()
        {
            _regions.Recalculate(_changedRegions);
            _changedRegions.Clear();
        }

        private void ConnectNeighbours(TileData tile)
        {
            var neighbours = _tiles.GetNeighbours(tile.Hex);

            foreach (var neighbour in neighbours)
            {
                neighbour.Neighbors.Add(tile);
                tile.Neighbors.Add(neighbour);
            }
        }

        private void DisconnectNeighbours(TileData tile)
        {
            foreach (var neighbour in tile.Neighbors)
                neighbour.RemoveFromNeighbors(tile);

            tile.Neighbors.Clear();
        }

        private void ConnectToRegion(TileData tile, RegionType regionType)
        {
            var region = _regions.GetOrCreateRegionFromNeighbors(tile, regionType);

            region.Tiles.Add(tile);
            tile.SetRegion(region);

            if (!_changedRegions.Contains(region))
                _changedRegions.Add(region);
        }

        private void DisconnectFromRegion(TileData tile)
        {
            var region = tile.Region;

            region.Tiles.Remove(tile);

            if (!_changedRegions.Contains(region))
                _changedRegions.Add(region);
        }
    }
}
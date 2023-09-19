using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainModel
    {
        private readonly List<RegionObject> _changedRegions = new();
        private readonly TerrainTiles _tiles;
        private readonly TerrainRegions _regions;
        private readonly GameObject _instance;
        private readonly TileFactory _tileFactory;
        private readonly Vector2Int _size;

        public TerrainModel(TerrainTiles tiles, TerrainRegions regions, GameObject instance, TileFactory tileFactory,
            Vector2Int size)
        {
            _tiles = tiles;
            _regions = regions;
            _instance = instance;
            _tileFactory = tileFactory;
            _size = size;
        }

        public bool IsHexInTerrain(HexPosition hex)
        {
            var index = HexMath.ToArrayIndex(hex);

            if (index.x < 0)
                return false;

            if (index.x >= _size.x)
                return false;

            if (index.y < 0)
                return false;

            return index.y < _size.y;
        }

        public bool TryGetTile(HexPosition hex, out TileObject tile)
        {
            if (IsHexInTerrain(hex))
            {
                tile = _tiles.Get(hex);
                return tile != default;
            }

            tile = default;
            return false;
        }

        public void CreateTile(HexPosition hex, RegionType regionType)
        {
            var tile = _tileFactory.Create(_instance.transform, hex, regionType);
            var neighbours = FindNeighbours(hex);
            var region = GetRegion(neighbours, tile.Type);

            //UnityEngine.Debug.Log(neighbours.Count);

            tile.Connections.AddRange(neighbours);

            foreach (var neighbour in neighbours)
                neighbour.Connections.Add(tile);

            region.Tiles.Add(tile);
            tile.SetRegion(region);
            _tiles.Set(tile, hex);

            if (!_changedRegions.Contains(region))
                _changedRegions.Add(region);
        }

        public void DestroyTile(TileObject tile)
        {
            var region = tile.Region;

            foreach (var neighbour in tile.Connections)
                neighbour.RemoveFromConnections(tile);

            _tiles.Set(null, tile.Hex);
            region.Tiles.Remove(tile);
            _tileFactory.Destroy(tile);

            if (!_changedRegions.Contains(region))
                _changedRegions.Add(region);
        }

        public void RecalculateChangedRegions()
        {
            //UnityEngine.Debug.Log(_changedRegions.Count);

            foreach (var region in _changedRegions)
                _regions.Recalculate(region);

            _changedRegions.Clear();
        }

        private List<TileObject> FindNeighbours(HexPosition rootHex)
        {
            var neighbours = new List<TileObject>();

            foreach (var direction in HexPositionDirections.Directions)
            {
                var neighbourHex = rootHex + direction;

                if (!IsHexInTerrain(neighbourHex))
                    continue;

                var neighbour = _tiles.Get(neighbourHex);

                if (neighbour != null)
                    neighbours.Add(neighbour);
            }

            return neighbours;
        }

        private RegionObject GetRegion(List<TileObject> tileNeighbours, RegionType regionType)
        {
            foreach (var neighbour in tileNeighbours)
                if (neighbour.Type == regionType)
                    return neighbour.Region;

            return _regions.CreateRegion(regionType); //FACTORY
        }
    }
}
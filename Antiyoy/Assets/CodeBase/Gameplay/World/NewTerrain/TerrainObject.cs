using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.World.NewTerrain
{
    public class TerrainObject
    {
        private readonly TileArray _tileArray;
        private readonly RegionFactory _regionFactory;
        private readonly RegionRebuilder _regionRebuilder;
        private RegionConnector _regionConnector;
        private TileConnector _tileConnector;

        public TerrainObject(TileArray tileArray, RegionFactory regionFactory, RegionRebuilder regionRebuilder)
        {
            _tileArray = tileArray;
            _regionFactory = regionFactory;
            _regionRebuilder = regionRebuilder;
        }

        public Vector2Int Size => _tileArray.Size;

        public bool IsInTerrain(HexPosition hex) => _tileArray.IsInArray(hex);

        public void SetTile(TileObject tile, HexPosition hex, RegionType regionType)
        {
            _tileArray.Set(tile, hex);
            _tileConnector.Connect(tile, TileUtilities.FindNeighbors(hex, _tileArray));

            if (!TileUtilities.TryGetRegionFromNeighbors(tile, regionType, out var region))
                region = _regionFactory.Create(regionType);

            _regionConnector.Connect(tile, region);
            _regionRebuilder.Rebuild(region);
        }

        public void RemoveTile(TileObject tile)
        {
            var region = tile.Region;

            _tileConnector.Disconnect(tile, new List<TileObject>(tile.Neighbors));
            _regionConnector.Disconnect(tile, region);
            _tileArray.Remove(tile.Hex);

            _regionRebuilder.Rebuild(region);
        }

        public TileObject GetTile(HexPosition hex) => _tileArray.Get(hex);

        public bool TryGetTile(HexPosition hex, out TileObject tile) => _tileArray.TryGet(hex, out tile);
    }
}
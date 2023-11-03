using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Region.Rebuilder.Geometry
{
    public class RegionSplitter
    {
        private readonly RegionFactory _regionFactory;
        private readonly RegionConnector _regionConnector;

        public RegionSplitter(RegionFactory regionFactory, RegionConnector regionConnector)
        {
            _regionFactory = regionFactory;
            _regionConnector = regionConnector;
        }

        public bool TrySplit(RegionData region, out List<RegionData> result)
        {
            result = null;

            if (region.Tiles.Count == 0)
                return false;

            var splits = GetSplits(region);

            if (splits.Count < 2)
                return false;

            result = Split(region, splits);
            return true;
        }

        private List<List<TileData>> GetSplits(RegionData regionData)
        {
            var splits = new List<List<TileData>>();
            var front = new List<TileData>();
            var unPassed = new List<TileData>(regionData.Tiles);
            var passed = new List<TileData>();

            front.Add(regionData.Tiles[0]);

            while (unPassed.Count > 0)
            {
                if (front.Count == 0)
                {
                    front.Add(unPassed[0]);
                    splits.Add(passed);
                    passed = new List<TileData>();
                }

                var tile = front[0];

                foreach (var connection in tile.Neighbors)
                {
                    if (front.Contains(connection))
                        continue;

                    if (unPassed.Contains(connection))
                        front.Add(connection);
                }

                front.Remove(tile);
                unPassed.Remove(tile);
                passed.Add(tile);
            }

            splits.Add(passed);
            return splits;
        }

        private List<RegionData> Split(RegionData region, List<List<TileData>> splits)
        {
            var regions = new List<RegionData> { region };

            splits.Remove(GetBiggestSplit(splits));

            foreach (var split in splits)
                regions.Add(CreateRegion(split, region));

            return regions;
        }

        private List<TileData> GetBiggestSplit(List<List<TileData>> splits)
        {
            var previous = splits[0];

            for (var i = 1; i < splits.Count; i++)
                if (splits[i].Count > previous.Count)
                    previous = splits[i];

            return previous;
        }

        private RegionData CreateRegion(List<TileData> splitPart, RegionData baseRegion)
        {
            var newRegion = _regionFactory.Create(baseRegion.Type);

            foreach (var tile in splitPart)
            {
                _regionConnector.Connect(tile, newRegion);
                _regionConnector.Disconnect(tile, baseRegion);
            }

            return newRegion;
        }
    }
}
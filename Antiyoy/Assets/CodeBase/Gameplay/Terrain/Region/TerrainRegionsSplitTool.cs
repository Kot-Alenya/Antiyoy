using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Terrain.Region
{
    public class TerrainRegionsSplitTool
    {
        private readonly RegionFactory _regionFactory;

        public TerrainRegionsSplitTool(RegionFactory regionFactory) => _regionFactory = regionFactory;

        public void Split(RegionData regionData)
        {
            if (regionData.Tiles.Count == 0)
                return;

            var passed = GetPassedTiles(regionData);

            if (passed.Count >= regionData.Tiles.Count)
                return;

            var splitPart = passed.Count * 2 > regionData.Tiles.Count
                ? Difference(regionData.Tiles, passed)
                : passed;

            var newRegion = CreateRegion(splitPart, regionData);

            Split(newRegion);
            Split(regionData);
        }

        private List<TileData> GetPassedTiles(RegionData regionData)
        {
            var front = new List<TileData>();
            var passed = new List<TileData>();

            front.Add(regionData.Tiles[0]);

            while (front.Count > 0)
            {
                var tile = front[0];

                foreach (var connection in tile.Neighbors)
                {
                    if (front.Contains(connection))
                        continue;

                    if (passed.Contains(connection))
                        continue;

                    if (regionData.Tiles.Contains(connection))
                        front.Add(connection);
                }

                front.Remove(tile);
                passed.Add(tile);
            }

            return passed;
        }

        private List<TileData> Difference(List<TileData> frits, List<TileData> second)
        {
            var difference = new List<TileData>(frits.Count - second.Count);

            foreach (var tileObject in frits)
            {
                if (!second.Contains(tileObject))
                    difference.Add(tileObject);
            }

            return difference;
        }

        private RegionData CreateRegion(List<TileData> splitPart, RegionData baseRegion)
        {
            var newRegion = _regionFactory.Create(baseRegion.Type);

            foreach (var tile in splitPart)
            {
                newRegion.Tiles.Add(tile);
                tile.SetRegion(newRegion);
                baseRegion.Tiles.Remove(tile);
            }

            return newRegion;
        }
    }
}
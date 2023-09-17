using System.Collections.Generic;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainRegions //to region Object?
    {
        public void Recalculate(RegionObject region)
        {
            var neighbourRegions = GetNeighborRegions(region, region.Type);
            var regionToSeparate = region;

            //UnityEngine.Debug.Log(neighbourRegions.Count);

            if (neighbourRegions.Count > 1)
                regionToSeparate = JoinRegions(neighbourRegions);

            foreach (var tile in regionToSeparate.Tiles) tile.DebugText.text = regionToSeparate.Tiles.Count.ToString();

            if (regionToSeparate.Tiles.Count > 0)
                Separate(regionToSeparate);
        }

        private List<RegionObject> GetNeighborRegions(RegionObject region, RegionType regionType)
        {
            var regions = new List<RegionObject>();

            foreach (var rootTile in region.Tiles)
            foreach (var tile in rootTile.Connections)
            {
                //bug в связях!
                UnityEngine.Debug.Log(rootTile.Connections.Count);

                if (tile.Type != regionType)
                    continue;

                if (!regions.Contains(tile.Region))
                    regions.Add(tile.Region);
            }

            return regions;
        }

        private RegionObject JoinRegions(List<RegionObject> regions)
        {
            var previous = regions[0];

            for (var i = 1; i < regions.Count; i++)
            {
                if (regions[i].Tiles.Count > previous.Tiles.Count)
                {
                    MoveTiles(previous, regions[i]);
                    previous = regions[i];
                }
                else
                    MoveTiles(regions[i], previous);
            }

            return previous;
        }

        private void MoveTiles(RegionObject fromRegion, RegionObject toRegion)
        {
            foreach (var tile in fromRegion.Tiles)
            {
                toRegion.Tiles.Add(tile);
                tile.SetRegion(toRegion);
            }

            fromRegion.Tiles.Clear();
        }

        private Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        private List<TileObject> GetPassedTiles(RegionObject regionObject)
        {
            var front = new List<TileObject>();
            var passed = new List<TileObject>();

            front.Add(regionObject.Tiles[0]);

            while (front.Count > 0)
            {
                var tile = front[0];

                foreach (var connection in tile.Connections)
                {
                    if (front.Contains(connection))
                        continue;

                    if (passed.Contains(connection))
                        continue;

                    if (regionObject.Tiles.Contains(connection))
                        front.Add(connection);
                }

                front.Remove(tile);
                passed.Add(tile);
            }

            return passed;
        }

        private List<TileObject> Difference(List<TileObject> frits, List<TileObject> second)
        {
            var difference = new List<TileObject>(frits.Count - second.Count);

            foreach (var tileObject in frits)
            {
                if (!second.Contains(tileObject))
                    difference.Add(tileObject);
            }

            return difference;
        }

        private void Separate(RegionObject regionObject)
        {
            var passed = GetPassedTiles(regionObject);

            if (passed.Count >= regionObject.Tiles.Count)
                return;

            var newRegion = new RegionObject(regionObject.Type, GetRandomColor());
            var excepted = passed.Count * 2 > regionObject.Tiles.Count
                ? Difference(regionObject.Tiles, passed)
                : passed;

            foreach (var tileObject in excepted)
            {
                newRegion.Tiles.Add(tileObject);
                tileObject.SetRegion(newRegion);
                regionObject.Tiles.Remove(tileObject);
            }

            foreach (var tile in newRegion.Tiles) tile.DebugText.text = newRegion.Tiles.Count.ToString();
            foreach (var tile in regionObject.Tiles) tile.DebugText.text = regionObject.Tiles.Count.ToString();

            Separate(newRegion);
            Separate(regionObject);
        }

        public RegionObject CreateRegion(RegionType regionType)
        {
            return new RegionObject(regionType, GetRandomColor());
        }
    }
}
using System.Collections.Generic;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainRegions
    {
        public TerrainRegions()
        {
        }

        //public void Recalculate()
        //{
        //}

        private bool IsTypesMatch(RegionObject regionObject, RegionType regionType)
            => regionObject != null && regionObject.Type == regionType;

        public void Remove(TileObject tileObject)
        {
            if (tileObject.Region == null)
                return;

            tileObject.Region.Tiles.Remove(tileObject);

            if (tileObject.Region.Tiles.Count > 0)
                Separate(tileObject.Region);

            tileObject.RemoveRegion();
        }

        public bool TryGetRegion(TileObject tileObject, RegionType regionType)
        {
            var neighbourRegions = GetRegionsInNeighbours(tileObject, regionType);

            if (neighbourRegions.Count > 0)
            {
                return true;
            }
        }

        public void Set(TileObject tileObject, RegionType regionType)
        {
            if (IsTypesMatch(tileObject.Region, regionType))
                return;

            Remove(tileObject);

            //add region.
            var neighbourRegions = GetRegionsInNeighbours(tileObject, regionType);

            if (neighbourRegions.Count > 0)
            {
                Add(tileObject, neighbourRegions[0]);

                if (neighbourRegions.Count > 1)
                    JoinRegions(neighbourRegions);
            }
            else
            {
                var regionObject = new RegionObject(regionType, GetRandomColor());
                Add(tileObject, regionObject);
            }
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
                    if (front.Contains(connection.ConnectedTile))
                        continue;

                    if (passed.Contains(connection.ConnectedTile))
                        continue;

                    if (regionObject.Tiles.Contains(connection.ConnectedTile))
                        front.Add(connection.ConnectedTile);
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

            Separate(newRegion);
            Separate(regionObject);
        }

        private void Add(TileObject tile, RegionObject regionObject)
        {
            regionObject.Tiles.Add(tile);
            tile.SetRegion(regionObject);
        }

        private void MoveTiles(RegionObject fromRegion, RegionObject toRegion)
        {
            foreach (var tile in fromRegion.Tiles)
                Add(tile, toRegion);

            fromRegion.Tiles.Clear();
        }

        private List<RegionObject> GetRegionsInNeighbours(TileObject tileObject, RegionType regionType)
        {
            var regions = new List<RegionObject>();

            foreach (var connection in tileObject.Connections)
            {
                var neighbourRegion = connection.ConnectedTile.Region;

                if (!IsTypesMatch(neighbourRegion, regionType))
                    continue;

                if (regions.Contains(neighbourRegion))
                    continue;

                regions.Add(neighbourRegion);
            }

            return regions;
        }

        private void JoinRegions(List<RegionObject> regions)
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
        }
    }
}
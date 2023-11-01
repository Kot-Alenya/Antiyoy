using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Dev.TEMPO.Region.Factory
{
    public class RegionFactory : IRegionFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private int _currentMaxId;

        public RegionFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public List<RegionData> Regions { get; } = new();

        public RegionData Create(RegionType type)
        {
            var regionId = ++_currentMaxId;
            var region = new RegionData(type, GetRandomColor(), regionId);

            Regions.Add(region);

            return region;
        }

        public void Destroy(RegionData region)
        {
            Regions.Remove(region);
            region.Tiles.Clear();
        }

        private Color GetColor(RegionType regionType)
        {
            var regionStaticData = _staticDataProvider.Get<RegionStaticData>();
            return regionType switch
            {
                RegionType.Neutral => regionStaticData.NeutralColor,
                RegionType.Red => regionStaticData.RedColor,
                RegionType.Blue => regionStaticData.BlueColor,
                _ => throw new ArgumentOutOfRangeException(nameof(regionType), regionType, null)
            };
        }

        private Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
using System;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.World.Region
{
    public class RegionFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public RegionFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public RegionData Create(RegionType type) => new(type, GetRandomColor());

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
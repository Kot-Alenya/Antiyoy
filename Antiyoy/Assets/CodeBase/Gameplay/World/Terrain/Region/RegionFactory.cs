using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Region
{
    public class RegionFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private int _currentMaxId;

        public RegionFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public RegionData Create(RegionType type)
        {
            var regionId = ++_currentMaxId;
            var region = new RegionData(type, GetRandomColor(), regionId);

            return region;
        }

        public void Destroy(RegionData region) => region.Tiles.Clear();

        private Color GetColor(RegionType regionType)
        {
            var regionStaticData = _staticDataProvider.Get<RegionsConfig>();

            return regionStaticData.Presets[regionType];
        }

        private Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
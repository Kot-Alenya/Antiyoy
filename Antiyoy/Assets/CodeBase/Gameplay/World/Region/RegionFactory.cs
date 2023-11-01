using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.World.Region
{
    public class RegionFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private int _currentMaxId;

        public RegionFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public RegionObject Create(RegionType type)
        {
            var regionId = ++_currentMaxId;
            var region = new RegionObject(type, GetRandomColor(), regionId);

            return region;
        }

        public void Destroy(RegionObject region) => region.Tiles.Clear();

        private Color GetColor(RegionType regionType)
        {
            var regionStaticData = _staticDataProvider.Get<RegionsConfig>();

            return regionStaticData.Presets[regionType];
        }

        private Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
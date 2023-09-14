using System;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Infrastructure;

namespace CodeBase.Gameplay.Region
{
    public class RegionFactory
    {
        private readonly RegionStaticData _staticData;

        public RegionFactory(StaticData staticData) => _staticData = staticData.RegionStaticData;

        public RegionObject Create(RegionType type)
        {
            return type switch
            {
                //RegionType.None => new RegionObject(RegionType.None, _staticData.NoneRegion.Color),
                RegionType.Neutral => new RegionObject(RegionType.Neutral, _staticData.NeutralRegion.Color),
                RegionType.Red => new RegionObject(RegionType.Red, _staticData.RedRegion.Color),
                RegionType.Blue => new RegionObject(RegionType.Blue, _staticData.BlueRegion.Color),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
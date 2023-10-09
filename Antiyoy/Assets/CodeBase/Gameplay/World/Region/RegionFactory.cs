using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.World.Region
{
    public class RegionFactory
    {
        private readonly RegionStaticData _staticData;

        public RegionFactory(StaticData staticData) => _staticData = staticData.RegionStaticData;

        public RegionData Create(RegionType type) => new(type, GetColor(type));

        private Color GetColor(RegionType regionType)
        {
            return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            
            /*return regionType switch
            {
                RegionType.Neutral => _staticData.NeutralColor,
                RegionType.Red => _staticData.RedColor,
                RegionType.Blue => _staticData.BlueColor,
                _ => throw new ArgumentOutOfRangeException(nameof(regionType), regionType, null)
            };*/
        }
    }
}
using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Terrain.Region.Factory
{
    public interface IRegionFactory
    {
        public List<RegionData> Regions { get; }

        public RegionData Create(RegionType type);

        public void Destroy(RegionData region);
    }
}
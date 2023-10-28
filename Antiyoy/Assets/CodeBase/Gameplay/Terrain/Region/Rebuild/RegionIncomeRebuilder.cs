using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Infrastructure.Project.Services.StaticData;

namespace CodeBase.Gameplay.Terrain.Region.Rebuild
{
    public class RegionIncomeRebuilder
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public RegionIncomeRebuilder(IStaticDataProvider staticDataProvider) =>
            _staticDataProvider = staticDataProvider;

        public void RebuildIncome(RegionData region) => region.Income = GetIncome(region);

        public void RebuildIncome(List<RegionData> regions)
        {
            foreach (var region in regions)
                region.Income = GetIncome(region);
        }

        private int GetIncome(RegionData region)
        {
            var defaultTileIncome = _staticDataProvider.Get<RegionStaticData>().DefaultTileIncome;
            var income = 0;

            foreach (var tile in region.Tiles)
            {
                income += defaultTileIncome;

                if (tile.Entity != null)
                    income += tile.Entity.Income;
            }

            return income;
        }
    }
}
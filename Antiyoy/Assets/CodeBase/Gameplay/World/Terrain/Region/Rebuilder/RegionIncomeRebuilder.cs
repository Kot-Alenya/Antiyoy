using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.Gameplay.World.Terrain.Region.Rebuilder
{
    public class RegionIncomeRebuilder
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly UnitStaticDataHelper _unitStaticDataHelper;

        public RegionIncomeRebuilder(IStaticDataProvider staticDataProvider, UnitStaticDataHelper unitStaticDataHelper)
        {
            _staticDataProvider = staticDataProvider;
            _unitStaticDataHelper = unitStaticDataHelper;
        }

        public void Rebuild(List<RegionData> regions)
        {
            foreach (var region in regions)
                Rebuild(region);
        }

        public void Rebuild(RegionData region) => region.Income = CalculateIncome(region);

        private int CalculateIncome(RegionData region)
        {
            var config = _staticDataProvider.Get<TileConfig>();
            var income = 0;

            foreach (var tile in region.Tiles)
            {
                if (tile.Unit != null)
                    income += _unitStaticDataHelper.GetPreset(tile.Unit.Type).Income;
                else
                    income += config.DefaultIncome;
            }

            return income;
        }
    }
}
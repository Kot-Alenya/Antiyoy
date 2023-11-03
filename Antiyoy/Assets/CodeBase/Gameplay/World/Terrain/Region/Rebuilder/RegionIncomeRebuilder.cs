using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.Gameplay.World.Terrain.Region.Rebuilder
{
    public class RegionIncomeRebuilder
    {
        private readonly IStaticDataProvider _staticDataProvider;
        public RegionIncomeRebuilder(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public void Rebuild(List<RegionData> regions)
        {
            foreach (var region in regions)
                Rebuild(region);
        }
        
        public void Rebuild(RegionData region) => region.Income = GetIncome(region);

        private int GetIncome(RegionData region)
        {
            var entitiesConfig = _staticDataProvider.Get<EntitiesConfig>();
            var defaultTileIncome = entitiesConfig.Presets[EntityType.None].Income;
            var income = 0;

            foreach (var tile in region.Tiles)
            {
                if (tile.Entity != null)
                    income += tile.Entity.Preset.Income;
                else
                    income += defaultTileIncome;
            }

            return income;
        }
    }
}
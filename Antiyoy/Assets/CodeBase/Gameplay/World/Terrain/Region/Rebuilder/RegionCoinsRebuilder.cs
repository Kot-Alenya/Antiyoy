using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Terrain.Region.Rebuilder
{
    public class RegionCoinsRebuilder
    {
        private readonly RegionFactory _regionFactory;
        private readonly RegionIncomeRebuilder _regionIncomeRebuilder;
        private readonly ITerrain _terrain;

        public RegionCoinsRebuilder(RegionFactory regionFactory, RegionIncomeRebuilder regionIncomeRebuilder,
            ITerrain terrain)
        {
            _regionFactory = regionFactory;
            _regionIncomeRebuilder = regionIncomeRebuilder;
            _terrain = terrain;
        }

        public void RebuildAll()
        {
            foreach (var region in _regionFactory.Regions)
                Rebuild(region);
        }

        private void Rebuild(RegionData region)
        {
            region.CoinsCount += region.Income;

            if (region.CoinsCount < 0)
            {
                region.CoinsCount = 0;
                RemoveCombatUnits(region);
                _regionIncomeRebuilder.Rebuild(region);
            }
        }

        private void RemoveCombatUnits(RegionData region)
        {
            foreach (var tile in region.Tiles)
                if (tile.Entity != null)
                    if (tile.Entity.Type == EntityType.Peasant)
                        _terrain.DestroyEntity(tile.Entity);
        }
    }
}
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

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
                KillCombatUnits(region);
                _regionIncomeRebuilder.Rebuild(region);
            }
        }

        private void KillCombatUnits(RegionData region)
        {
            foreach (var tile in region.Tiles)
            {
                switch (tile.Unit.Type)
                {
                    case UnitType.Peasant:
                    case UnitType.Spearman:
                    case UnitType.Baron:
                    case UnitType.Knight:
                        _terrain.DestroyUnit(tile.Unit);
                        _terrain.CreateUnit(tile, UnitType.Grave, false);
                        break;
                }
            }
        }
    }
}
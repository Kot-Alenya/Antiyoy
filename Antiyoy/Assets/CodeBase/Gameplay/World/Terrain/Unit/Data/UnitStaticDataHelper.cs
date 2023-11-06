using System.Linq;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.Gameplay.World.Terrain.Unit.Data
{
    public class UnitStaticDataHelper
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public UnitStaticDataHelper(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public UnitPresetData_new GetPreset(UnitType unitType)
        {
            var config = _staticDataProvider.Get<UnitsConfig>();

            if (unitType.IsCombat())
                return config.CombatUnitPresets.First(preset => preset.Type == unitType);

            if (unitType.IsConstruction())
                return config.ConstructionUnitPresets.First(preset => preset.Type == unitType);

            return config.OtherUnitPresets.FirstOrDefault(preset => preset.Type == unitType);
        }
    }
}
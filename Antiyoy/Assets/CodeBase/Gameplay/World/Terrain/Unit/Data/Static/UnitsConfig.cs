using CodeBase.Infrastructure.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Unit.Data
{
    [CreateAssetMenu(menuName = "Configurations/Units", fileName = "UnitsConfig", order = 0)]
    public class UnitsConfig : ScriptableObjectStaticData
    {
        public CombatUnitPresetData[] CombatUnitPresets;
        public ConstructionUnitPresetData[] ConstructionUnitPresets;
        public OtherUnitPresetData[] OtherUnitPresets;
    }
}
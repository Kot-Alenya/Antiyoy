using System.Collections.Generic;
using CodeBase.Infrastructure.Services.StaticData.Data;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Entity.Data
{
    [CreateAssetMenu(menuName = "Configurations/Enitities", fileName = "EntitiesConfig", order = 0)]
    public class UnitsConfig : ScriptableObjectStaticData
    {
        [OdinSerialize] public Dictionary<UnitType, UnitPresetData> Presets;
    }
}
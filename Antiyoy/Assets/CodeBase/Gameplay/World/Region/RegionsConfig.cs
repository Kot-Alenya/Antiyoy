using System.Collections.Generic;
using CodeBase.Infrastructure.Services.StaticData.Data;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Gameplay.World.Region
{
    [CreateAssetMenu(menuName = "Configurations/Region", fileName = "RegionConfig", order = 0)]
    public class RegionsConfig : ScriptableObjectStaticData
    {
        [OdinSerialize] public Dictionary<RegionType, Color> Presets;
    }
}
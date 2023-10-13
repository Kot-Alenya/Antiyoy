using CodeBase.Infrastructure.Services.StaticData;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrap
{
    [CreateAssetMenu(menuName = "Configurations/Project", fileName = "ProjectConfig", order = 0)]
    public class ProjectStaticData : SerializedScriptableObject
    {
        [OdinSerialize] public IStaticData[] StaticDataToBind;
    }
}
using CodeBase.Infrastructure.Project.Services.StaticData;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Infrastructure.Project.Data
{
    [CreateAssetMenu(menuName = "Configurations/Project/DataToBind", fileName = "ProjectDataToBindConfig", order = 0)]
    public class ProjectDataToBindConfig : SerializedScriptableObject
    {
        [OdinSerialize] public IStaticData[] Value;
    }
}
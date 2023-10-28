using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine;

namespace CodeBase.Dev.DebugWindow.Data
{
    [CreateAssetMenu(menuName = "Configurations/DebugWindow", fileName = "DebugWindowConfig", order = 0)]
    public class DebugWindowStaticData : ScriptableObject, IStaticData
    {
        public DebugWindowPrefabData Prefab;
    }
}
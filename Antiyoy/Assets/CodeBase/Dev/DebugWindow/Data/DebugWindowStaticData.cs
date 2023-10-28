using CodeBase.Infrastructure.Project.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Dev.DebugWindow.Data
{
    [CreateAssetMenu(menuName = "Configurations/DebugWindow", fileName = "DebugWindowConfig", order = 0)]
    public class DebugWindowStaticData : ScriptableObjectStaticData
    {
        public DebugWindowPrefabData Prefab;
    }
}
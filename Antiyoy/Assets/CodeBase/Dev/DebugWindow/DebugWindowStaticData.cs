using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Dev.DebugWindow
{
    [CreateAssetMenu(menuName = "Configurations/DebugWindow", fileName = "DebugWindowConfig", order = 0)]
    public class DebugWindowStaticData : ScriptableObject, IStaticData
    {
        public DebugWindowPrefabData Prefab;
    }
}
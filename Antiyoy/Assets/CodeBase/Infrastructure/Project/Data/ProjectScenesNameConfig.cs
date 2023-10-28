using CodeBase.Infrastructure.Project.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Project.Data
{
    [CreateAssetMenu(menuName = "Configurations/Project/ScenesName", fileName = "ProjectScenesNameConfig", order = 0)]
    public class ProjectScenesNameConfig : ScriptableObjectStaticData
    {
        public string GameLoading;
        public string GameHub;
        public string Gameplay;
        public string WorldEditor;
    }
}
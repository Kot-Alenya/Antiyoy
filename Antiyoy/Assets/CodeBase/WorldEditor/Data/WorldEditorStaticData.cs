using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine;

namespace CodeBase.WorldEditor.Data
{
    [CreateAssetMenu(menuName = "Configurations/MapEditor", fileName = "MapEditorConfig", order = 0)]
    public class WorldEditorStaticData : ScriptableObject, IStaticData
    {
        public WorldEditorPrefabData Prefab;
        public KeyCode ReturnWorldFirstKey;
        public KeyCode ReturnWorldBackSecondKey;
        public KeyCode ReturnWorldNextSecondKey;
    }
}
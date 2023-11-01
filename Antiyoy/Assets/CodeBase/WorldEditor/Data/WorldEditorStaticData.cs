using CodeBase.Infrastructure.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.WorldEditor.Data
{
    [CreateAssetMenu(menuName = "Configurations/MapEditor", fileName = "MapEditorConfig", order = 0)]
    public class WorldEditorStaticData : ScriptableObjectStaticData
    {
        public WorldEditorPrefabData Prefab;
        public KeyCode ReturnWorldFirstKey;
        public KeyCode ReturnWorldBackSecondKey;
        public KeyCode ReturnWorldNextSecondKey;
    }
}
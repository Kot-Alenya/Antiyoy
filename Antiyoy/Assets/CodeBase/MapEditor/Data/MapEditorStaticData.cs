using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.MapEditor.Data
{
    [CreateAssetMenu(menuName = "Configurations/MapEditor", fileName = "MapEditorConfig", order = 0)]
    public class MapEditorStaticData : ScriptableObject, IStaticData
    {
        public MapEditorPrefabData Prefab;        
        public KeyCode ReturnWorldFirstKey;
        public KeyCode ReturnWorldBackSecondKey;
        public KeyCode ReturnWorldNextSecondKey;
    }
}
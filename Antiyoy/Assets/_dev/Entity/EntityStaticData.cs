using UnityEngine;

namespace _dev
{
    [CreateAssetMenu(menuName = "Configurations/Entity", fileName = "EntityConfig", order = 0)]
    public class EntityStaticData : ScriptableObject
    {
        public CapitalPrefabData CapitalPrefab;
    }
}
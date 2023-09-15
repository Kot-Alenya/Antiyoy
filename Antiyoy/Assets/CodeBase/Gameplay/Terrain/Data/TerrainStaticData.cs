using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Data
{
    [CreateAssetMenu(menuName = "Configurations/Terrain", fileName = "TerrainConfig", order = 0)]
    public class TerrainStaticData : ScriptableObject
    {
        public TerrainBackgroundPrefabData BackgroundPrefabData;
        public Vector2Int Size;
    }
}
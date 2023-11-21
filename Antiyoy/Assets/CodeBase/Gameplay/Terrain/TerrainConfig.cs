using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    [CreateAssetMenu(menuName = "Configurations/Terrain", fileName = "TerrainConfig", order = 0)]
    public class TerrainConfig : ScriptableObject
    {
        public Vector2Int Size;
        public TilePrefabData TilePrefab;
    }
}
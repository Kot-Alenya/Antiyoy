using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Data
{
    [CreateAssetMenu(menuName = "Configurations/Terrain", fileName = "TerrainConfig", order = 0)]
    public class TerrainStaticData : ScriptableObject, IStaticData
    {
        public TerrainBackgroundPrefabData BackgroundPrefabData;
        public Vector2Int Size;
    }
}
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile.Factory
{
    [CreateAssetMenu(menuName = "Configurations/Tile", fileName = "TilesConfig", order = 0)]
    public class TilesConfig : ScriptableObjectStaticData
    {
        public TilePrefabData Prefab;
    }
}
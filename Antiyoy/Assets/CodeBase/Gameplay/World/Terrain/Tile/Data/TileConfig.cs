using CodeBase.Infrastructure.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Tile.Data
{
    [CreateAssetMenu(menuName = "Configurations/Tile", fileName = "TileConfig", order = 0)]
    public class TileConfig : ScriptableObjectStaticData
    {
        public TilePrefabData Prefab;
        public int DefaultIncome = 1;
    }
}
using CodeBase.Infrastructure.Project.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile.Data
{
    [CreateAssetMenu(menuName = "Configurations/Tile", fileName = "TileConfig", order = 0)]
    public class TileStaticData : ScriptableObjectStaticData
    {
        public TilePrefabData Prefab;
    }
}
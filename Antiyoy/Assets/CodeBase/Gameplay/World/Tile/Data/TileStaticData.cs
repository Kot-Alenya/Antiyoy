using UnityEngine;

namespace CodeBase.Gameplay.World.Tile.Data
{
    [CreateAssetMenu(menuName = "Configurations/Tile", fileName = "TileConfig", order = 0)]
    public class TileStaticData : ScriptableObject
    {
        public TilePrefabData Prefab;
    }
}
using UnityEngine;

namespace CodeBase.Gameplay.Tile.Data
{
    [CreateAssetMenu(menuName = "Configurations/Tile", fileName = "TileConfig", order = 0)]
    public class TileStaticData : ScriptableObject
    {
        public TilePrefabData Prefab;
        public Color NeutralColor;
        public Color RedColor;
        public Color BlueColor;
    }
}
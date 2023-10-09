using UnityEngine;

namespace CodeBase.Gameplay.World.Region.Data
{
    [CreateAssetMenu(menuName = "Configurations/Region", fileName = "RegionConfig", order = 0)]
    public class RegionStaticData : ScriptableObject
    {
        public Color NeutralColor;
        public Color RedColor;
        public Color BlueColor;
    }
}
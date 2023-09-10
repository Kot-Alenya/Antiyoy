using UnityEngine;

namespace CodeBase.Gameplay.Region.Data
{
    [CreateAssetMenu(menuName = "Configurations/Region", fileName = "RegionConfig", order = 0)]
    public class RegionStaticData : ScriptableObject
    {
        public RegionPresetData NoneRegion;
        public RegionPresetData NeutralRegion;
        public RegionPresetData RedRegion;
        public RegionPresetData BlueRegion;
    }
}
using CodeBase.Gameplay.World.Region;
using CodeBase.Infrastructure.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Player.Data
{
    [CreateAssetMenu(menuName = "Configurations/Player", fileName = "PlayerStaticData", order = 0)]
    public class PlayerStaticData : ScriptableObjectStaticData
    {
        public RegionType DefaultRegionType;
        public int DefaultCoinsCount;
        public Color SelectedRegionBorderColor;
        public Color UnSelectedRegionBorderColor;
    }
}
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Infrastructure.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Player.Data
{
    [CreateAssetMenu(menuName = "Configurations/Player", fileName = "PlayerStaticData", order = 0)]
    public class PlayerConfig : ScriptableObjectStaticData
    {
        public RegionType DefaultRegionType;
    }
}
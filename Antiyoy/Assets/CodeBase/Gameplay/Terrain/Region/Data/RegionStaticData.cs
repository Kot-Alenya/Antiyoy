using CodeBase.Infrastructure.Project.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Region.Data
{
    [CreateAssetMenu(menuName = "Configurations/Region", fileName = "RegionConfig", order = 0)]
    public class RegionStaticData : ScriptableObjectStaticData
    {
        public Color NeutralColor;
        public Color RedColor;
        public Color BlueColor;
        public int DefaultTileIncome;
    }
}
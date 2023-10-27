﻿using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Region.Data
{
    [CreateAssetMenu(menuName = "Configurations/Region", fileName = "RegionConfig", order = 0)]
    public class RegionStaticData : ScriptableObject, IStaticData
    {
        public Color NeutralColor;
        public Color RedColor;
        public Color BlueColor;
        public int DefaultTileIncome;
    }
}
﻿using CodeBase.Infrastructure.Project.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Data
{
    [CreateAssetMenu(menuName = "Configurations/Terrain", fileName = "TerrainConfig", order = 0)]
    public class TerrainStaticData : ScriptableObjectStaticData
    {
        public TerrainPrefabData Prefab;
        public Vector2Int Size;
    }
}
﻿using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Data
{
    [CreateAssetMenu(menuName = "Configurations/Terrain", fileName = "TerrainConfig", order = 0)]
    public class TerrainStaticData : ScriptableObject, IStaticData
    {
        public TerrainPrefabData Prefab;
        public Vector2Int Size;

        public TerrainPrefabData Instance { get; private set; }

        public void Initialize(TerrainPrefabData instance) => Instance = instance;
    }
}
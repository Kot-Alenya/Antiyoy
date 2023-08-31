﻿using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    [CreateAssetMenu(menuName = "Configurations/Terrain", fileName = "TerrainConfig", order = 0)]
    public class TerrainStaticData : ScriptableObject
    {
        public Vector2Int Size;
    }
}
using System;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    [Serializable]
    public class TerrainConfig
    {
        public Vector2Int Size;
        public TerrainController Controller;
    }
}
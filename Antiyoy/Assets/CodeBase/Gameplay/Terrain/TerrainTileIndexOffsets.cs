using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public struct TerrainTileIndexOffsets
    {
        public Vector2Int[] Offsets;
        public Vector2Int Up;
        public Vector2Int Down;
        public Vector2Int RightUp;
        public Vector2Int RightDown;
        public Vector2Int LeftUp;
        public Vector2Int LeftDown;
    }
}
using UnityEngine;

namespace CodeBase.Gameplay.Hex
{
    public static class HexCoordinatesUtilities
    {
        public static HexCoordinates FromArrayIndex(Vector2Int index)
        {
            return new HexCoordinates
            {
                Q = index.x - index.y / 2,
                R = index.y
            };
        }
    }
}
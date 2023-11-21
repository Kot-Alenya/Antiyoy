using UnityEngine;

namespace CodeBase.Gameplay.Hex
{
    public static class HexCoordinatesUtilities
    {
        private static readonly float TileBiggestRadius = 1 / 2f;
        private static readonly float TileSmallerRadius = Mathf.Sqrt(3) * TileBiggestRadius / 2;
        
        public static HexCoordinates FromArrayIndex(Vector2Int index)
        {
            return new HexCoordinates
            {
                Q = index.x - index.y / 2,
                R = index.y
            };
        }

        public static Vector3 ToWorldPosition(this HexCoordinates hex)
        {
            var x = hex.X * TileSmallerRadius * 2;
            var y = hex.Y * TileBiggestRadius * 3 / 2;
            var result = hex.Y % 2 == 0 ? new Vector2(x - TileSmallerRadius, y) : new Vector2(x, y);
            
            return result;
        }
    }
}
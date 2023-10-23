using UnityEngine;

namespace CodeBase.Gameplay.World.Hex
{
    public static class HexMath
    {
        private const float HexSize = 1f;

        public static readonly float OuterRadius = HexSize / 2f;
        public static readonly float InnerRadius = Mathf.Sqrt(3) * OuterRadius / 2;

        public static HexPosition FromArrayIndex(Vector2Int index)
        {
            return new HexPosition
            {
                Q = index.x - index.y / 2,
                R = index.y
            };
        }

        public static Vector2Int ToArrayIndex(HexPosition hex)
        {
            return new Vector2Int
            {
                x = hex.Q + hex.R / 2,
                y = hex.R
            };
        }

        public static HexPosition FromWorldPosition(Vector2 position)
        {
            var yTileOffset = OuterRadius - InnerRadius / 2f;
            var threeTilesHeight = OuterRadius * 6f - yTileOffset * 2f;
            var averageTileHeight = threeTilesHeight / 3f;
            var yNormalized = position.y / averageTileHeight;
            if (yNormalized < 0)
                yNormalized -= 1;

            var roundY = (int)yNormalized;

            var averageTileWidth = InnerRadius * 2f;
            var xTileOffset = roundY % 2f == 0 ? InnerRadius : 0;
            var xNormalized = (position.x + xTileOffset) / averageTileWidth;

            if (xNormalized < 0)
                xNormalized -= 1;

            var roundX = (int)xNormalized;

            return FromArrayIndex(new Vector2Int(roundX, roundY));
        }

        public static Vector2 ToWorldPosition(HexPosition hex)
        {
            var index = ToArrayIndex(hex);
            var x = index.x * InnerRadius * 2f;
            var y = index.y * OuterRadius * 3f / 2f;

            x = index.y % 2f == 0 ? x - InnerRadius : x;

            x += InnerRadius;
            y += OuterRadius;

            return new Vector2(x, y);
        }
    }
}
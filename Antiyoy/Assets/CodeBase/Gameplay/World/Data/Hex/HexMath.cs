using UnityEngine;

namespace CodeBase.Gameplay.World.Data.Hex
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
            var yOffset = OuterRadius * 3f / 2f;
            var xOffset = InnerRadius * 2f;
            var y = position.y / yOffset;
            var x = position.x / xOffset;

            var roundY = (int)(y + 0.5f);

            x = roundY % 2f == 0 ? x + InnerRadius : x;

            var roundX = (int)(x + 0.5f);

            return FromArrayIndex(new Vector2Int(roundX, roundY));
        }

        public static Vector2 ToWorldPosition(HexPosition position)
        {
            var index = ToArrayIndex(position);
            var x = index.x * InnerRadius * 2f;
            var y = index.y * OuterRadius * 3f / 2f;

            x = index.y % 2f == 0 ? x - InnerRadius : x;

            return new Vector2(x, y);
        }
    }
}
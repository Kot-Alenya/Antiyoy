using UnityEngine;

namespace CodeBase.Gameplay
{
    public struct HexCoordinates
    {
        public int Q;
        public int R;
        public int S;

        private HexCoordinates(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public override string ToString() => $"{Q};{R};{S}";

        public static HexCoordinates FromOffset(Vector2Int offsetCoordinates)
        {
            var r = offsetCoordinates.y;
            var q = offsetCoordinates.x - r / 2;
            var s = -q - r;

            return new HexCoordinates(q, r, s);
        }
    }
}
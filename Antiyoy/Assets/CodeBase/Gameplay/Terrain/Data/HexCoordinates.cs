namespace CodeBase.Gameplay.Terrain.Data
{
    public struct HexCoordinates
    {
        public int Q;
        public int R;
        public int S;

        public int X => Q + R / 2;

        public int Y => R;

        public HexCoordinates(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public HexCoordinates(int x, int y)
        {
            Q = x - y / 2;
            R = y;
            S = -Q - y;
        }

        public override string ToString() => $"Q R S\n{Q};{R};{S}";

        public static HexCoordinates operator +(HexCoordinates a, HexCoordinates b)
        {
            a.Q += b.Q;
            a.R += b.R;
            a.S += b.S;

            return a;
        }
    }
}
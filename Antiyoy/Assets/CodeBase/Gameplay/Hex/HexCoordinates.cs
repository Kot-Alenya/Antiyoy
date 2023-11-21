namespace CodeBase.Gameplay.Hex
{
    public struct HexCoordinates
    {
        public int Q;
        public int R;

        public int S => -Q - R;

        public int X => Q + R / 2;

        public int Y => R;

        public HexCoordinates(int q, int r)
        {
            Q = q;
            R = r;
        }

        public override string ToString() => $"Q R S\n{Q};{R};{S}";

        public static HexCoordinates operator +(HexCoordinates a, HexCoordinates b)
        {
            a.Q += b.Q;
            a.R += b.R;

            return a;
        }
    }
}
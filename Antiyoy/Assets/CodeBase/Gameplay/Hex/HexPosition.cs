namespace CodeBase.Gameplay.Hex
{
    public struct HexPosition
    {
        public int Q;
        public int R;

        public int S => -Q - R;

        public HexPosition(int q, int r)
        {
            Q = q;
            R = r;
        }

        public static HexPosition operator +(HexPosition a, HexPosition b)
        {
            a.Q += b.Q;
            a.R += b.R;

            return a;
        }

        public override string ToString() => $"{Q};{R};{S}";
    }
}
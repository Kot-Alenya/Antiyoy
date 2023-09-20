using System;

namespace CodeBase.Gameplay.Terrain.Data.Hex
{
    public struct HexPosition : IEquatable<HexPosition>
    {
        public int Q;
        public int R;

        public int S => -Q - R;

        public HexPosition(int q, int r)
        {
            Q = q;
            R = r;
        }

        public static HexPosition operator +(HexPosition a, HexPosition b) => new(a.Q + b.Q, a.R + b.R);

        public static bool operator ==(HexPosition a, HexPosition b) => a.Equals(b);

        public static bool operator !=(HexPosition a, HexPosition b) => !a.Equals(b);

        public bool Equals(HexPosition other) => Q == other.Q && R == other.R;

        public override bool Equals(object obj) => obj is HexPosition other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Q, R);

        public override string ToString() => $"{Q};{R};{S}";
    }
}
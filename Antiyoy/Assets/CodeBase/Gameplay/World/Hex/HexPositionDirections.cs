using System;

namespace CodeBase.Gameplay.World.Hex
{
    public struct HexPositionDirections
    {
        private static readonly HexPosition Northeast = new(0, 1);
        private static readonly HexPosition East = new(1, 0);
        private static readonly HexPosition Southeast = new(1, -1);
        private static readonly HexPosition Southwest = new(0, -1);
        private static readonly HexPosition West = new(-1, 0);
        private static readonly HexPosition Northwest = new(-1, 1);

        public static readonly HexPosition[] Directions =
        {
            Northeast,
            East,
            Southeast,
            Southwest,
            West,
            Northwest
        };

        public static HexDirectionType GetDirectionType(HexPosition direction)
        {
            if (direction == Northeast)
                return HexDirectionType.Northeast;
            if (direction == East)
                return HexDirectionType.East;
            if (direction == Southeast)
                return HexDirectionType.Southeast;
            if (direction == Southwest)
                return HexDirectionType.Southwest;
            if (direction == West)
                return HexDirectionType.West;
            if (direction == Northwest)
                return HexDirectionType.Northwest;

            throw new SystemException("Invalid hex direction!");
        }
    }
}
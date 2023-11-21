namespace CodeBase.Gameplay.Hex
{
    public struct HexCoordinatesDirections
    {
        private static readonly HexCoordinates Northeast = new(0, 1);
        private static readonly HexCoordinates East = new(1, 0);
        private static readonly HexCoordinates Southeast = new(1, -1);
        private static readonly HexCoordinates Southwest = new(0, -1);
        private static readonly HexCoordinates West = new(-1, 0);
        private static readonly HexCoordinates Northwest = new(-1, 1);

        public static readonly HexCoordinates[] Directions =
        {
            Northeast,
            East,
            Southeast,
            Southwest,
            West,
            Northwest
        };
    }
}
namespace CodeBase.Gameplay.Terrain.Data.Hex
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
    }
}
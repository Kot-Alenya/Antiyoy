namespace CodeBase.Gameplay.World.Terrain.Unit.Data
{
    public static class UnitTypeExtensions
    {
        public static bool IsCombat(this UnitType type)
        {
            return type switch
            {
                UnitType.Peasant => true,
                UnitType.Spearman => true,
                UnitType.Baron => true,
                UnitType.Knight => true,
                _ => false
            };
        }

        public static bool IsConstruction(this UnitType type)
        {
            return type switch
            {
                UnitType.Farm => true,
                UnitType.Tower => true,
                UnitType.SuperTower => true,
                _ => false
            };
        }
    }
}
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.Player.States.Unit.Create
{
    public static class PlayerUnitUtilities
    {
        public static bool IsCombatUnit(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Peasant:
                case UnitType.Spearman:
                case UnitType.Baron:
                case UnitType.Knight:
                    return true;
                default:
                    return false;
            }
        }
    }
}
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.Player.States.Unit
{
    public static class PlayerCombineUnitUtilities
    {
        private const int MaxCombatUnitRang = 4;

        public static bool TryCombinedUnitType(UnitType type1, UnitType type2, out UnitType combinedType)
        {
            var combinedRang = ToRang(type1) + ToRang(type2);

            combinedType = FromRang(combinedRang);

            return combinedRang <= MaxCombatUnitRang;
        }

        private static int ToRang(UnitType type)
        {
            return type switch
            {
                UnitType.None => 0,
                UnitType.Peasant => 1,
                UnitType.Spearman => 2,
                UnitType.Baron => 3,
                _ => MaxCombatUnitRang + 1
            };
        }

        private static UnitType FromRang(int rang)
        {
            return rang switch
            {                
                1 => UnitType.Peasant,
                2 => UnitType.Spearman,
                3 => UnitType.Baron,
                4 => UnitType.Knight,
                _ => UnitType.None
            };
        }
    }
}
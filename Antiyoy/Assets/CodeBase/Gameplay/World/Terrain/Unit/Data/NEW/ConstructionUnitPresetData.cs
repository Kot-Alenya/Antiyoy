using System;

namespace CodeBase.Gameplay.World.Terrain.Unit.Data
{
    [Serializable]
    public class ConstructionUnitPresetData : UnitPresetData_new
    {
        public int Cost;
        public int CostIncreaseFactor;
        public int ProtectionLevel;
        public int ProtectionRange;
    }
}
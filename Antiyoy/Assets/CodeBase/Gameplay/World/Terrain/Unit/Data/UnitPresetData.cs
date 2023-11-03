using System;

namespace CodeBase.Gameplay.World.Terrain.Unit.Data
{
    [Serializable]
    public class UnitPresetData
    {
        public UnitPrefabData Prefab;
        public int Income;
        public int Cost;
        public int CostIncreaseFactor;
    }
}
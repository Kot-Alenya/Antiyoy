using System;

namespace CodeBase.Gameplay.World.Terrain.Entity.Data
{
    [Serializable]
    public class EntityPresetData
    {
        public EntityPrefabData Prefab;
        public int Income;
        public int Cost;
        public int CostIncreaseFactor;
    }
}
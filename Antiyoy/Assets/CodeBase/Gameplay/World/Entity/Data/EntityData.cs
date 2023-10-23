namespace CodeBase.Gameplay.World.Entity.Data
{
    public class EntityData
    {
        public readonly EntityPrefabData Instance;
        public readonly EntityType Type;
        public readonly int Income;

        public EntityData(EntityPrefabData instance, EntityType type, int income)
        {
            Instance = instance;
            Type = type;
            Income = income;
        }
    }
}
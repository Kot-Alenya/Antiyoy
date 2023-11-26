using CodeBase.Gameplay.CommonEcs;

namespace CodeBase.Gameplay.Region
{
    public class RegionController
    {
        private readonly GameplayEcsWorld _world;

        public RegionController(RegionType type, RegionEntitiesCollection entities)
        {
            Type = type;
            Entities = entities;
        }

        public RegionEntitiesCollection Entities { get; }

        public RegionType Type { get; }
    }
}
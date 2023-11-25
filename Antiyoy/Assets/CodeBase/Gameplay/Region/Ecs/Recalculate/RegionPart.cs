using System.Collections.Generic;

namespace CodeBase.Gameplay.Region.Ecs.Recalculate
{
    public readonly struct RegionPart
    {
        public readonly List<int> Entities;

        public RegionPart(List<int> entities) => Entities = entities;
    }
}
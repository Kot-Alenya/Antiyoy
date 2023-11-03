using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Ecs.Systems
{
    public class SetUnitCanMoveSystem : IEcsRunSystem
    {
        private readonly ITerrain _terrain;

        public SetUnitCanMoveSystem(ITerrain terrain) => _terrain = terrain;

        public void Run(IEcsSystems systems)
        {
            foreach (var tile in _terrain)
            {
                switch (tile.Unit.Type)
                {
                    case UnitType.Peasant:
                    case UnitType.Spearman:
                    case UnitType.Baron:
                    case UnitType.Knight:
                        tile.Unit.IsCanMove = true;
                        tile.Unit.Instance.Animator.CanMove();
                        break;
                }
            }
        }
    }
}
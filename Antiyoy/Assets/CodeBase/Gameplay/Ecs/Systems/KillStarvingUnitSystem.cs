using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Ecs.Systems
{
    public class KillStarvingUnitSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly GameplayEcsWorld _world;
        private readonly ITerrain _terrain;
        private EcsFilter _eventFilter;
        private EcsPool<RegionStarvationEvent> _eventPool;

        public KillStarvingUnitSystem(GameplayEcsWorld world, ITerrain terrain)
        {
            _world = world;
            _terrain = terrain;
        }

        public void Init(IEcsSystems systems)
        {
            _eventFilter = _world.Filter<RegionStarvationEvent>().End();
            _eventPool = _world.GetPool<RegionStarvationEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _eventFilter)
            {
                ref var component = ref _eventPool.Get(entity);

                KillCombatUnits(component.Region);
            }
        }

        private void KillCombatUnits(RegionData region)
        {
            foreach (var tile in region.Tiles)
            {
                if (tile.Unit != null && tile.Unit.Type.IsCombat())
                {
                    _terrain.DestroyUnit(tile.Unit);
                    _terrain.CreateUnit(tile, UnitType.Grave, false);
                }
            }
        }
    }
}
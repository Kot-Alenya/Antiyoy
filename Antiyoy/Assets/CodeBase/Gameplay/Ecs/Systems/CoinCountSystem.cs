using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Ecs.Systems
{
    public class CoinCountSystem : IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private readonly RegionFactory _regionFactory;

        public CoinCountSystem(GameplayEcsWorld world, RegionFactory regionFactory)
        {
            _world = world;
            _regionFactory = regionFactory;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var region in _regionFactory.Regions)
                Rebuild(region);
        }

        private void Rebuild(RegionData region)
        {
            region.CoinsCount += region.Income;

            if (region.CoinsCount < 0)
            {
                region.CoinsCount = 0;
                SendStarvationEvent(region);
            }
        }

        private void SendStarvationEvent(RegionData region)
        {
            var entity = _world.NewEntity();
            var pool = _world.GetPool<RegionStarvationEvent>();

            ref var component = ref pool.Add(entity);

            component.Region = region;
        }
    }
}
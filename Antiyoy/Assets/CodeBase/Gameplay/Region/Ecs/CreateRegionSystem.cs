using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Infrastructure;
using CodeBase.Gameplay.Region.Ecs.Components;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Region.Ecs
{
    public class CreateRegionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private EcsFilter _regionCreateRequestFilter;
        private EcsPool<RegionCreateRequest> _regionCreateRequestPool;
        private EcsPool<RegionComponent> _regionPool;
        private RegionsConfig _regionsConfig;
        private EcsPool<TileComponent> _tilePool;

        public CreateRegionSystem(GameplayEcsWorld world, GameplayStaticDataProvider staticDataProvider)
        {
            _world = world;
            _staticDataProvider = staticDataProvider;
        }

        public void Init(IEcsSystems systems)
        {
            _regionCreateRequestFilter = _world.Filter<RegionCreateRequest>().End();
            _regionCreateRequestPool = _world.GetPool<RegionCreateRequest>();
            _regionPool = _world.GetPool<RegionComponent>();
            _tilePool = _world.GetPool<TileComponent>();
            _regionsConfig = _staticDataProvider.GetRegionsConfig();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _regionCreateRequestFilter)
                CreateRegion(entity);
        }

        private void CreateRegion(int entity)
        {
            var request = _regionCreateRequestPool.Get(entity);

            ref var region = ref _regionPool.Add(entity);
            var color = _regionsConfig.Colors[request.Type];

            region.Type = request.Type;
            _tilePool.Get(entity).Controller.SetColor(color);
        }
    }
}
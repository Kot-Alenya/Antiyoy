using CodeBase.Gameplay.CommonEcs;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Tile.Ecs
{
    public class CreateTileSystem : IEcsInitSystem,IEcsRunSystem
    {
        private readonly GameplayEcsWorld _world;
        private EcsFilter _requestFilter;

        public CreateTileSystem(GameplayEcsWorld world)
        {
            _world = world;
        }

        public void Init(IEcsSystems systems)
        {
            _requestFilter = _world.Filter<TileCreateRequest>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var request in _requestFilter)
            {
                
            }
        }
    }
}
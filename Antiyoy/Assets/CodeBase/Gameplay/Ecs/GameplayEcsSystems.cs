using CodeBase.Gameplay.Ecs.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace CodeBase.Gameplay.Ecs
{
    public class GameplayEcsSystems
    {
        private readonly GameplayEcsSystemFactory _systemFactory;
        private readonly GameplayEcsWorld _world;
        private IEcsSystems _systems;

        public GameplayEcsSystems(GameplayEcsSystemFactory systemFactory, GameplayEcsWorld world)
        {
            _systemFactory = systemFactory;
            _world = world;
        }

        public void Initialize()
        {
            _systems = new EcsSystems(_world);
            _systems
                .AddWorld(_world, nameof(GameplayEcsWorld))
                .Add(_systemFactory.Create<ReplaceGraveSystem>())
                .Add(_systemFactory.Create<CoinCountSystem>())
                .Add(_systemFactory.Create<KillStarvingUnitSystem>())
                .DelHere<RegionStarvationEvent>()
                .Add(_systemFactory.Create<SetUnitCanMoveSystem>())
                .Init();
        }

        public void Dispose() => _systems?.Destroy();

        public void Run() => _systems?.Run();
    }
}
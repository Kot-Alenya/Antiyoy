using CodeBase.Gameplay.Ecs.Systems;
using Leopotam.EcsLite;

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
                .Add(_systemFactory.Create<ReplaceGraveSystem>())
                .Add(_systemFactory.Create<CoinCountSystem>())
                .Add(_systemFactory.Create<KillStarvingUnitSystem>())
                .Add(_systemFactory.Create<SetUnitCanMoveSystem>())
                //.DelHere<RegionStarvationEvent>()
                .Init();
        }

        public void Dispose() => _systems?.Destroy();

        public void Run() => _systems?.Run();
    }
}
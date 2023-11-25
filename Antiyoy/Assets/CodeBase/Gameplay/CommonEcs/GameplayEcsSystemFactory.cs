using CodeBase.Gameplay.Region.Ecs;
using CodeBase.Gameplay.Region.Ecs.Components;
using CodeBase.Gameplay.Tile.Ecs;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Zenject;

namespace CodeBase.Gameplay.CommonEcs
{
    public class GameplayEcsSystemFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly GameplayEcsWorld _world;

        private GameplayEcsSystemFactory(IInstantiator instantiator, GameplayEcsWorld world)
        {
            _instantiator = instantiator;
            _world = world;
        }

        public IEcsSystems CreateMainSystems()
        {
            var systems = new EcsSystems(_world);

            AddDebugSystems(systems);
            
            systems.Add(Create<DestroyRegionLinkSystem>());
            systems.Add(Create<DestroyTileSystem>());

            systems.Add(Create<CreateTileSystem>());
            systems.Add(Create<CreateRegionLinkSystem>());

            systems.DelHere<RegionLinkCreateRequest>();
            systems.DelHere<RegionLinkDestroyRequest>();

            systems.DelHere<TileCreateRequest>();
            systems.DelHere<TileDestroyRequest>();

            return systems;
        }

        private void AddDebugSystems(IEcsSystems systems)
        {
#if UNITY_EDITOR
            // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
            systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            // Регистрируем отладочные системы по контролю за текущей группой систем. 
            systems.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem());
#endif
        }

        public IEcsSystems CreateTurnNextSystems() => throw new System.NotImplementedException();

        private T Create<T>() where T : IEcsSystem => _instantiator.Instantiate<T>();
    }
}
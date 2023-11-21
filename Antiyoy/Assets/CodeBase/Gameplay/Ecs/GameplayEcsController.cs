using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Ecs
{
    public class GameplayEcsController : MonoBehaviour
    {
        private IEcsSystems _systems;
        private EcsWorld _world;

        [Inject]
        public void Construct(GameplayEcsWorld world) => _world = world;

        public void Initialize(IEcsSystems systems)
        {
            _systems = systems;
            _systems.Init();
        }

        private void Dispose()
        {
            _systems?.Destroy();
            _world?.Destroy();
        }

        private void Update() => _systems.Run();
    }
}
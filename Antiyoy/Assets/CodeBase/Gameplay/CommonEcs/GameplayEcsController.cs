using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.CommonEcs
{
    public class GameplayEcsController : MonoBehaviour
    {
        private EcsWorld _world;
        private GameplayEcsEventsBus _eventsBus;
        private IEcsSystems[] _gameplaySystems;

        [Inject]
        public void Construct(GameplayEcsWorld world, GameplayEcsEventsBus eventsBus)
        {
            _world = world;
            _eventsBus = eventsBus;
        }

        public void Initialize(IEcsSystems[] gameplaySystems)
        {
            _gameplaySystems = gameplaySystems;

            foreach (var system in _gameplaySystems)
                system.Init();
        }

        public void Dispose()
        {
            foreach (var system in _gameplaySystems)
                system.Destroy();

            _world.Destroy();
            _eventsBus.Destroy();
        }

        private void Update()
        {
            foreach (var system in _gameplaySystems)
                system.Run();
        }
    }
}
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.CommonEcs
{
    public class GameplayEcsController : MonoBehaviour
    {
        private EcsWorld _world;
        private GameplayEcsEventsBus _eventsBus;
        private IEcsSystems _mainSystems;
        private IEcsSystems _mainDebugSystems;
        private IEcsSystems _eventsDebugSystems;

        [Inject]
        public void Construct(GameplayEcsWorld world, GameplayEcsEventsBus eventsBus)
        {
            _world = world;
            _eventsBus = eventsBus;
        }

        public void Initialize(IEcsSystems mainSystems, IEcsSystems mainDebugSystems, IEcsSystems eventsDebugSystems)
        {
            _mainSystems = mainSystems;
            _mainDebugSystems = mainDebugSystems;
            _eventsDebugSystems = eventsDebugSystems;

            _mainSystems.Init();
            _mainDebugSystems.Init();
            _eventsDebugSystems.Init();
        }

        private void Dispose()
        {
            _mainSystems.Destroy();
            _mainDebugSystems.Destroy();
            _eventsDebugSystems.Destroy();
            _world.Destroy();
            _eventsBus.Destroy();
        }

        private void Update()
        {
            _mainSystems.Run();
            _mainDebugSystems.Run();
            _eventsDebugSystems.Run();
        }
    }
}
using CodeBase.Gameplay.Ecs;
using CodeBase.Gameplay.PlayerCamera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Infrastructure.ProjectStateMachine;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayStaticDataProvider _gameplayStaticDataProvider;

        public override void InstallBindings()
        {
            BindStateMachine();
            BindEcsWorld();

            Container.Bind<GameplayStaticDataProvider>().FromInstance(_gameplayStaticDataProvider).AsSingle();
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<TerrainFactory>().AsSingle();
        }

        private void BindEcsWorld()
        {
            Container.Bind<GameplayEcsWorld>().AsSingle();
            Container.Bind<GameplayEcsFactory>().AsSingle();
            Container.Bind<GameplayEcsControllerProvider>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<StateMachine>().AsSingle();
            Container.Bind<StateFactory>().AsSingle();
        }
    }
}
using CodeBase.Gameplay.GameplayCamera;
using CodeBase.ProjectInfrastructure.ProjectStateMachine;
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

            Container.Bind<GameplayStaticDataProvider>().FromInstance(_gameplayStaticDataProvider).AsSingle();
            Container.Bind<CameraFactory>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<StateMachine>().AsSingle();
            Container.Bind<StateFactory>().AsSingle();
        }
    }
}
using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.GameplayCamera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Tile;
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
            BindTerrain();
            BindCamera();

            Container.Bind<GameplayStaticDataProvider>().FromInstance(_gameplayStaticDataProvider).AsSingle();
            Container.Bind<TileFactory>().AsSingle();
        }

        private void BindCamera()
        {
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<CameraProvider>().AsSingle();
        }

        private void BindTerrain()
        {
            Container.Bind<TerrainFactory>().AsSingle();
            Container.Bind<TerrainControllerProvider>().AsSingle();
        }

        private void BindEcsWorld()
        {
            Container.Bind<GameplayEcsWorld>().AsSingle();
            Container.Bind<GameplayEcsSystemFactory>().AsSingle();
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
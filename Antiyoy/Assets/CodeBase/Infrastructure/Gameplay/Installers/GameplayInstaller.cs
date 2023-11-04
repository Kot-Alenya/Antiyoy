using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Ecs;
using CodeBase.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayUIPrefabData _gameplayUIPrefab;

        public override void InstallBindings()
        {
            BindEcs();

            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<GameplayUIFactory>().AsSingle().WithArguments(_gameplayUIPrefab);
            Container.BindInterfacesTo<GameplayStartup>().AsSingle();
        }

        private void BindEcs()
        {
            Container.Bind<GameplayEcsSystemFactory>().AsSingle();
            Container.Bind<GameplayEcsSystems>().AsSingle();
            Container.Bind<GameplayEcsWorld>().AsSingle();
        }
    }
}
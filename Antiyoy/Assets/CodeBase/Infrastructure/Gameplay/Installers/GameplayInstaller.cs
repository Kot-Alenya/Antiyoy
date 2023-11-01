using CodeBase.Gameplay.Camera;
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
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<GameplayUIFactory>().AsSingle().WithArguments(_gameplayUIPrefab);
            Container.BindInterfacesTo<GameplayStartup>().AsSingle();
        }
    }
}
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayUIPrefabData _gameplayUIPrefab;
        [SerializeField] private PlayerUIPrefabData _playerUIPrefab;

        public override void InstallBindings()
        {
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<GameplayUIFactory>().AsSingle().WithArguments(_gameplayUIPrefab);
            Container.Bind<PlayerUIFactory>().AsSingle().WithArguments(_playerUIPrefab);

            Container.BindInterfacesTo<GameplayStartup>().AsSingle();
        }
    }
}
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.States;
using CodeBase.Gameplay.Player.States.Entity;
using CodeBase.Gameplay.Player.States.Region;
using CodeBase.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayUIPrefabData _gameplayUIPrefab;
        [SerializeField] private PlayerPrefabData _playerPrefabData;

        public override void InstallBindings()
        {
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<GameplayUIFactory>().AsSingle().WithArguments(_gameplayUIPrefab);
            Container.BindInterfacesTo<GameplayStartup>().AsSingle();

            BindPlayer();
        }

        private void BindPlayer()
        {
            Container.Bind<PlayerRegionFocusView>().AsSingle();
            Container.Bind<PlayerTileFocusView>().AsSingle();
            Container.Bind<PlayerStateMachine>().AsSingle();
            Container.Bind<PlayerFactory>().AsSingle().WithArguments(_playerPrefabData);
        }
    }
}
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerPrefabData _playerPrefabData;

        public override void InstallBindings()
        {
            Container.Bind<PlayerTerrainFocus>().AsSingle();
            Container.Bind<PlayerStateMachine>().AsSingle();
            Container.Bind<PlayerFactory>().AsSingle().WithArguments(_playerPrefabData);
        }
    }
}
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.Player.States;
using CodeBase.Gameplay.Player.UI;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class PlayerFactory
    {
        private readonly DiContainer _container;
        private readonly PlayerPrefabData _playerPrefabData;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly PlayerStateMachine _playerStateMachine;

        public PlayerFactory(DiContainer container, PlayerPrefabData playerPrefabData,
            IStaticDataProvider staticDataProvider, PlayerStateMachine playerStateMachine)
        {
            _container = container;
            _playerPrefabData = playerPrefabData;
            _staticDataProvider = staticDataProvider;
            _playerStateMachine = playerStateMachine;
        }

        public void Create()
        {
            var instance = Object.Instantiate(_playerPrefabData);
            var data = CreatePlayerData();

            instance.PlayerUIWindow.Initialize();

            _container.Bind<IPlayerUIMediator>().FromInstance(instance.PlayerUIWindow).AsSingle();
            _container.Bind<IPlayerInput>().FromInstance(instance.PlayerInput).AsSingle();
            _container.Bind<PlayerData>().FromInstance(data).AsSingle();
            _container.InjectGameObject(instance.gameObject);

            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }

        private PlayerData CreatePlayerData()
        {
            var preset = _staticDataProvider.Get<PlayerStaticData>();
            var data = new PlayerData
            {
                RegionType = preset.DefaultRegionType,
                CoinsCount = preset.DefaultCoinsCount
            };

            return data;
        }
    }
}
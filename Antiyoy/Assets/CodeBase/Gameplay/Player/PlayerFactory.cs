using CodeBase.Gameplay.Player.Controller;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class PlayerFactory
    {
        private readonly DiContainer _container;
        private readonly PlayerPrefabData _playerPrefabData;
        private readonly IStaticDataProvider _staticDataProvider;

        public PlayerFactory(DiContainer container, PlayerPrefabData playerPrefabData,
            IStaticDataProvider staticDataProvider)
        {
            _container = container;
            _playerPrefabData = playerPrefabData;
            _staticDataProvider = staticDataProvider;
        }

        public void Create()
        {
            var model = CreateModel();
            var instance = Object.Instantiate(_playerPrefabData);

            _container.Bind<IPlayerUIMediator>().FromInstance(instance.PlayerUIWindow).AsSingle();
            _container.InjectGameObject(instance.gameObject);

            instance.PlayerUIWindow.Initialize();
            model.Initialize(instance.PlayerUIWindow);
        }

        private PlayerModel CreateModel()
        {
            var preset = _staticDataProvider.Get<PlayerStaticData>();
            var data = new PlayerData
            {
                RegionType = preset.DefaultRegionType,
                CoinsCount = preset.DefaultCoinsCount
            };
            var model = _container.Instantiate<PlayerModel>(new object[] { data });

            _container.Bind<PlayerModel>().FromInstance(model).AsSingle();

            return model;
        }
    }
}
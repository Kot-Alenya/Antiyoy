using CodeBase.Gameplay.Player.Controller;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Infrastructure.Project.Services.StaticData;
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
            var controller = CreateController();
            var instance = _container.InstantiatePrefabForComponent<PlayerPrefabData>(_playerPrefabData);

            instance.PlayerUIWindow.Initialize();
            controller.Initialize(instance.PlayerUIWindow);
        }

        private IPlayerController CreateController()
        {
            var preset = _staticDataProvider.Get<PlayerStaticData>();
            var data = new PlayerData
            {
                RegionType = preset.DefaultRegionType,
                CoinsCount = preset.DefaultCoinsCount
            };
            var controller = _container.Instantiate<PlayerController>(new object[] { data });

            _container.Bind<IPlayerController>().FromInstance(controller).AsSingle();

            return controller;
        }
    }
}
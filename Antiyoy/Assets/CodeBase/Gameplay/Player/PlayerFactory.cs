using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class PlayerFactory
    {
        private readonly DiContainer _container;
        private readonly PlayerPrefabData _playerPrefabData;
        private readonly IPlayerController _playerController;

        public PlayerFactory(DiContainer container, PlayerPrefabData playerPrefabData,
            IPlayerController playerController)
        {
            _container = container;
            _playerPrefabData = playerPrefabData;
            _playerController = playerController;
        }

        public void Create()
        {
            var instance = _container.InstantiatePrefabForComponent<PlayerPrefabData>(_playerPrefabData);
            
            _playerController.Initialize(instance.PlayerUIWindow);
        }
    }
}
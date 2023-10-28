using Zenject;

namespace CodeBase.Infrastructure.GameHub.UI
{
    public class GameHubUIFactory
    {
        private readonly DiContainer _container;
        private readonly GameHubUIPrefabData _prefab;

        public GameHubUIFactory(DiContainer container, GameHubUIPrefabData prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public void Create() => _container.InstantiatePrefab(_prefab);
    }
}
using Zenject;

namespace CodeBase.Gameplay.UI
{
    public class GameplayUIFactory
    {
        private readonly DiContainer _container;
        private readonly GameplayUIPrefabData _prefab;

        public GameplayUIFactory(DiContainer container, GameplayUIPrefabData prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public void Create() => _container.InstantiatePrefab(_prefab);
    }
}
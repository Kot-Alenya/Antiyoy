using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class PlayerUIFactory
    {
        private readonly DiContainer _container;
        private readonly PlayerUIPrefabData _prefab;

        public PlayerUIFactory(DiContainer container, PlayerUIPrefabData prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public void Create() => _container.InstantiatePrefab(_prefab);
    }
}
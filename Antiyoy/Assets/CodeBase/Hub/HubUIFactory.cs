using Zenject;

namespace CodeBase.Hub
{
    public class HubUIFactory
    {
        private readonly DiContainer _container;
        private readonly HubUIPrefabData _prefab;

        public HubUIFactory(DiContainer container, HubUIPrefabData prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public void Create() => _container.InstantiatePrefab(_prefab);
    }
}
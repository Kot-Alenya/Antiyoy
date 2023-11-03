using Leopotam.EcsLite;
using Zenject;

namespace CodeBase.Gameplay.Ecs
{
    public class GameplayEcsSystemFactory
    {
        private readonly IInstantiator _instantiator;

        public GameplayEcsSystemFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public IEcsSystem Create<T>() where T : IEcsSystem => _instantiator.Instantiate<T>();
    }
}
using CodeBase.Gameplay.World.Version.Modules;
using Zenject;

namespace CodeBase.Gameplay.World.Version
{
    public class WorldVersionControllerFactory
    {
        private readonly DiContainer _container;

        public WorldVersionControllerFactory(DiContainer container) => _container = container;

        public void Create()
        {
            var recorder = _container.Instantiate<VersionRecorder>();
            var handler = _container.Instantiate<VersionHandler>();

            _container.Bind<IWorldVersionController>().To<WorldVersionController>().AsSingle()
                .WithArguments(recorder, handler);
        }
    }
}
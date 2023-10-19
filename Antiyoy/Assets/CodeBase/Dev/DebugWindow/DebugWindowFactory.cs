using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Dev.DebugWindow
{
    public class DebugWindowFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataProvider _staticDataProvider;

        public DebugWindowFactory(DiContainer container, IStaticDataProvider staticDataProvider)
        {
            _container = container;
            _staticDataProvider = staticDataProvider;
        }

        public void Create()
        {
            var staticData = _staticDataProvider.Get<DebugWindowStaticData>();
            var instance = Object.Instantiate(staticData.Prefab);

            _container.Bind<IDebugWindowController>().To<DebugWindowController>().AsSingle()
                .WithArguments(instance.Window);
            _container.InjectGameObject(instance.gameObject);

            instance.Window.Close();
        }
    }
}
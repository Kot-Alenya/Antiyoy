using CodeBase.Infrastructure.Project.Services.StaticData;
using CodeBase.WorldEditor.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.WorldEditor
{
    public class WorldEditorFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataProvider _staticDataProvider;

        public WorldEditorFactory(DiContainer container, IStaticDataProvider staticDataProvider)
        {
            _container = container;
            _staticDataProvider = staticDataProvider;
        }

        public void Create()
        {
            var mapEditorStaticData = _staticDataProvider.Get<WorldEditorStaticData>();
            var instance = Object.Instantiate(mapEditorStaticData.Prefab);
            var model = _container.Instantiate<WorldEditorModel>();
            var controller = _container.Instantiate<WorldEditorController>(new object[] { model });

            _container.Bind<IWorldEditorController>().FromInstance(controller).AsSingle();
            _container.InjectGameObject(instance.gameObject);
        }
    }
}
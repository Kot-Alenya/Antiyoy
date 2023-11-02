using CodeBase.Infrastructure.Services.StaticData;
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

            _container.InjectGameObject(instance.gameObject);
        }
    }
}
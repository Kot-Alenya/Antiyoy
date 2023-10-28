using CodeBase.Infrastructure.Project.Services.StaticData;
using CodeBase.MapEditor.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.MapEditor
{
    public class MapEditorFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataProvider _staticDataProvider;

        public MapEditorFactory(DiContainer container, IStaticDataProvider staticDataProvider)
        {
            _container = container;
            _staticDataProvider = staticDataProvider;
        }

        public void Create()
        {
            var mapEditorStaticData = _staticDataProvider.Get<MapEditorStaticData>();
            var instance = Object.Instantiate(mapEditorStaticData.Prefab);
            var model = _container.Instantiate<MapEditorModel>();
            var controller = _container.Instantiate<MapEditorController>(new object[] { model });

            _container.Bind<IMapEditorController>().FromInstance(controller).AsSingle();
            _container.InjectGameObject(instance.gameObject);
        }
    }
}
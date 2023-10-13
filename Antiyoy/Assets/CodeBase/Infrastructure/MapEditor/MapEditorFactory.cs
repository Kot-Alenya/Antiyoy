using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Change;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.MapEditor.Data;
using CodeBase.Infrastructure.MapEditor.UI;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public MapEditorFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public void Create(ICameraController camera, IWorldController world)
        {
            var mapEditorStaticData = _staticDataProvider.Get<MapEditorStaticData>();
            var prefab = mapEditorStaticData.Prefab;
            var instance = Object.Instantiate(prefab);
            var model = new MapEditorModel(world);
            var controller = new MapEditorController(model, world);

            CreateInput(instance, controller, camera);
            ConstructUI(instance, controller);
        }

        private void CreateInput(MapEditorPrefabData instance, MapEditorController controller, ICameraController camera)
        {
            var input = instance.gameObject.AddComponent<MapEditorInput>();

            input.Construct(camera, controller);
        }

        private void ConstructUI(MapEditorPrefabData instance, MapEditorController controller)
        {
            foreach (var monoBehaviour in instance.UIElements)
            {
                var element = (IMapEditorUIElement)monoBehaviour;
                element.Construct(controller);
            }
        }
    }
}
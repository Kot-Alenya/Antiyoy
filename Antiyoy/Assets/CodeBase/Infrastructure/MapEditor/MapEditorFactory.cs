using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Map;
using CodeBase.Gameplay.Terrain;
using CodeBase.Infrastructure.MapEditor.Data;
using CodeBase.Infrastructure.MapEditor.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorFactory
    {
        private readonly MapEditorPrefabData _prefabData;
        private readonly MapRecorder _recorder;

        public MapEditorFactory(MapEditorPrefabData prefabData, MapRecorder recorder)
        {
            _prefabData = prefabData;
            _recorder = recorder;
        }

        public void Create(MapController terrainController, CameraObject cameraObject)
        {
            var instance = Object.Instantiate(_prefabData);
            var model = new MapEditorModel(terrainController, _recorder);
            var controller = new MapEditorController(model);

            CreateInput(instance, cameraObject, controller);
            ConstructUI(instance, controller);
        }

        private void CreateInput(MapEditorPrefabData instance, CameraObject cameraObject,
            MapEditorController controller)
        {
            var input = instance.gameObject.AddComponent<MapEditorInput>();

            input.Constructor(cameraObject, controller);
        }

        private void ConstructUI(MapEditorPrefabData instance, MapEditorController controller)
        {
            foreach (var monoBehaviour in instance.UIElements)
            {
                var element = (IMapEditorUIElement)monoBehaviour;
                element.Constructor(controller);
            }
        }
    }
}
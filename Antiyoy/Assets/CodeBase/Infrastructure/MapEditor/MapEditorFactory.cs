using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Infrastructure.MapEditor.Data;
using CodeBase.Infrastructure.MapEditor.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorFactory
    {
        private readonly MapEditorPrefabData _prefabData;

        public MapEditorFactory(MapEditorPrefabData prefabData) => _prefabData = prefabData;

        public void Create(TerrainController terrainController, CameraObject cameraObject)
        {
            var instance = Object.Instantiate(_prefabData);
            var model = new MapEditorModel(terrainController);
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
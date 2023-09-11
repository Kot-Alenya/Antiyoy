using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Infrastructure.MapEditor.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorFactory
    {
        private readonly MapEditorPrefabData _prefabData;

        public MapEditorFactory(MapEditorPrefabData prefabData) => _prefabData = prefabData;

        public void Create(TerrainObject terrainObject, CameraObject cameraObject)
        {
            var instance = Object.Instantiate(_prefabData);
            var model = new MapEditorModel(terrainObject);
            var controller = new MapEditorController(model);

            CreateInput(instance, cameraObject, controller);

            foreach (var button in instance.SetRegionButtons)
                button.Constructor(controller);
        }

        private void CreateInput(MapEditorPrefabData instance, CameraObject cameraObject,
            MapEditorController controller)
        {
            var input = instance.gameObject.AddComponent<MapEditorInput>();

            input.Constructor(cameraObject, controller);
        }
    }
}
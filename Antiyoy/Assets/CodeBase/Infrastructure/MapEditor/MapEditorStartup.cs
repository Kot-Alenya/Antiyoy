using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Map;
using CodeBase.Gameplay.Terrain;
using CodeBase.Infrastructure.MapEditor.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorStartup : MonoBehaviour
    {
        [SerializeField] private MapEditorPrefabData _prefabData;
        private MapFactory _terrainFactory;
        private CameraFactory _cameraFactory;

        [Inject]
        private void Constructor(MapFactory terrainFactory, CameraFactory cameraFactory)
        {
            _terrainFactory = terrainFactory;
            _cameraFactory = cameraFactory;
        }

        private void Start()
        {
            var terrainObject = _terrainFactory.Create();
            var cameraObject = _cameraFactory.Create();
            var recorder = new MapRecorder();
            var mapEditorFactory = new MapEditorFactory(_prefabData, recorder);

            mapEditorFactory.Create(terrainObject, cameraObject);
        }
    }
}
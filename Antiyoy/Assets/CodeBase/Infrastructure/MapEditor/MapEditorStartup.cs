using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.World;
using CodeBase.Infrastructure.MapEditor.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorStartup : MonoBehaviour
    {
        [SerializeField] private MapEditorPrefabData _prefabData;
        private WorldFactory _worldFactory;
        private CameraFactory _cameraFactory;

        [Inject]
        private void Constructor(WorldFactory terrainFactory, CameraFactory cameraFactory)
        {
            _worldFactory = terrainFactory;
            _cameraFactory = cameraFactory;
        }

        private void Start()
        {
            var terrainObject = _worldFactory.Create();
            var cameraObject = _cameraFactory.Create();
            var recorder = new WorldRecorder();
            var mapEditorFactory = new MapEditorFactory(_prefabData, recorder);

            mapEditorFactory.Create(terrainObject, cameraObject);
        }
    }
}
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Infrastructure.MapEditor.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorStartup : MonoBehaviour
    {
        [SerializeField] private MapEditorPrefabData _prefabData;
        private TerrainFactory _terrainFactory;
        private CameraFactory _cameraFactory;

        [Inject]
        private void Constructor(TerrainFactory terrainFactory, CameraFactory cameraFactory)
        {
            _terrainFactory = terrainFactory;
            _cameraFactory = cameraFactory;
        }

        private void Start()
        {
            var terrainObject = _terrainFactory.Create();
            var cameraObject = _cameraFactory.Create();
            var mapEditorFactory = new MapEditorFactory(_prefabData);

            mapEditorFactory.Create(terrainObject, cameraObject);
        }
    }
}
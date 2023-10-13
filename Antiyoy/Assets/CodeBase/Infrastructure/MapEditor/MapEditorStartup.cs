using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorStartup : MonoBehaviour
    {
        private WorldFactory _worldFactory;
        private CameraFactory _cameraFactory;
        private MapEditorFactory _mapEditorFactory;

        [Inject]
        private void Constructor(WorldFactory worldFactory, CameraFactory cameraFactory,
            MapEditorFactory mapEditorFactory)
        {
            _worldFactory = worldFactory;
            _cameraFactory = cameraFactory;
            _mapEditorFactory = mapEditorFactory;
        }

        private void Start()
        {
            var camera = _cameraFactory.Create();
            var world = _worldFactory.Create();
            _mapEditorFactory.Create(camera, world);
        }
    }
}
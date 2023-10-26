using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Version;
using CodeBase.MapEditor;
using Zenject;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindWorld();
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<MapEditorFactory>().AsSingle();
            Container.Bind<DebugWindowFactory>().AsSingle();
        }

        private void BindWorld()
        {
            Container.Bind<TerrainFactory>().AsSingle();
            Container.Bind<WorldFactory>().AsSingle();
            Container.Bind<WorldVersionControllerFactory>().AsSingle();
        }
    }
}
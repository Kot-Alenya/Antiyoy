using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Version;
using CodeBase.Infrastructure.Services.StateMachine;
using CodeBase.Infrastructure.Services.StateMachine.Factory;
using CodeBase.MapEditor;
using Zenject;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStateMachine();
            BindWorld();

            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<MapEditorFactory>().AsSingle();
            Container.Bind<DebugWindowFactory>().AsSingle();
        }

        private void BindWorld()
        {
            Container.Bind<ITerrainFactory>().To<TerrainFactory>().AsSingle();
            Container.Bind<WorldVersionControllerFactory>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
        }
    }
}
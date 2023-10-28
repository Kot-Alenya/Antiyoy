using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.WorldEditor;
using Zenject;

namespace CodeBase.Infrastructure.WorldEditor
{
    public class WorldEditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<WorldEditorFactory>().AsSingle();
            Container.Bind<DebugWindowFactory>().AsSingle();
            Container.BindInterfacesTo<WorldEditorStartup>().AsSingle();
        }
    }
}
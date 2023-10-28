using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Player;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DebugWindowFactory>().AsSingle();
            
            
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<PlayerUIFactory>().AsSingle();
            Container.BindInterfacesTo<GameplayStartup>().AsSingle();
        }
    }
}
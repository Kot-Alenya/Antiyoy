using CodeBase.Gameplay.World.Progress;
using CodeBase.Gameplay.World.Version;

namespace CodeBase.Infrastructure.Gameplay.Installers
{
    public class WorldInstaller : TerrainInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();

            BindVersion();
            //BindProgress();
        }

        private void BindProgress()
        {
            Container.Bind<WorldProgressSaver>().AsSingle();
            Container.Bind<WorldProgressLoader>().AsSingle();
        }

        private void BindVersion()
        {
            Container.Bind<WorldVersionManager>().AsSingle();
            Container.Bind<WorldVersionRecorder>().AsSingle();
        }
    }
}
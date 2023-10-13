using CodeBase.Infrastructure.Services.StaticData;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class ProjectInstaller : MonoInstaller
    {
        public ProjectStaticData StaticData;

        public override void InstallBindings()
        {
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle()
                .WithArguments(StaticData.StaticDataToBind);
        }
    }
}
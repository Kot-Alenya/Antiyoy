using System;
using CodeBase.Utilities.Zenject;
using Sirenix.Serialization;

namespace CodeBase.Infrastructure.Project.Services.StaticData
{
    public class StaticDataProviderInstaller : SerializedMonoInstaller
    {
        [NonSerialized, OdinSerialize] private IStaticData[] _dataToProvide;

        public override void InstallBindings()
        {
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle()
                .WithArguments(_dataToProvide);
        }
    }
}
using System;
using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Project.Services.SceneLoader;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.Project.Services.StateMachine.Factory;
using CodeBase.Infrastructure.Project.Services.StaticData;
using CodeBase.Utilities.Zenject;
using Sirenix.Serialization;

namespace CodeBase.Infrastructure.Project
{
    public class ProjectInstaller : SerializedMonoInstaller
    {
        [NonSerialized, OdinSerialize] private IStaticData[] _dataToProvide;

        public override void InstallBindings()
        {
            BindStateMachine();

            Container.Bind<IProgressSaveLoader>().To<ProgressSaveLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle()
                .WithArguments(_dataToProvide);
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
        }
    }
}
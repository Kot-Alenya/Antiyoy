using CodeBase.Infrastructure.Project.Data;
using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Project.Services.SceneLoader;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.Project.Services.StateMachine.Factory;
using CodeBase.Infrastructure.Project.Services.StaticData;
using Zenject;

namespace CodeBase.Infrastructure.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public ProjectDataToBindConfig DataToBind;

        public override void InstallBindings()
        {
            BindStateMachine();

            Container.Bind<IProgressSaveLoader>().To<ProgressSaveLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle()
                .WithArguments(DataToBind.Value);
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
        }
    }
}
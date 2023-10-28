using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Project.Services.SceneLoader;
using CodeBase.Utilities.Zenject;

namespace CodeBase.Infrastructure.Project
{
    public class ProjectInstaller : SerializedMonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IProgressSaveLoader>().To<ProgressSaveLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}
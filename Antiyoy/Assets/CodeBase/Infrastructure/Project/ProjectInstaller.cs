using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Project.Services.SceneLoader;
using CodeBase.Infrastructure.Project.Services.StaticData;
using CodeBase.Infrastructure.Project.Services.StaticData.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ScriptableObjectStaticData[] _dataToProvide;

        public override void InstallBindings()
        {
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle().WithArguments(_dataToProvide);
            Container.Bind<IProgressSaveLoader>().To<ProgressSaveLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}
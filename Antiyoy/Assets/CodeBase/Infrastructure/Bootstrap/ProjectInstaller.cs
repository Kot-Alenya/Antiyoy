using System.Linq;
using AYellowpaper;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class ProjectInstaller : MonoInstaller
    {
        [RequireInterface(typeof(IStaticData))] [SerializeField]
        private ScriptableObject[] _staticData;

        public override void InstallBindings()
        {
            BindStaticData();
        }

        private void BindStaticData()
        {
            var requiredStaticData = _staticData.Cast<IStaticData>().ToArray();
            
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle().WithArguments(requiredStaticData);
        }
    }
}
using CodeBase.Infrastructure.Hub.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Hub
{
    public class HubInstaller : MonoInstaller
    {
        [SerializeField] private HubUIPrefabData _uiPrefab;

        public override void InstallBindings()
        {
            Container.Bind<HubUIFactory>().AsSingle().WithArguments(_uiPrefab);
            Container.BindInterfacesTo<HubStartup>().AsSingle();
        }
    }
}
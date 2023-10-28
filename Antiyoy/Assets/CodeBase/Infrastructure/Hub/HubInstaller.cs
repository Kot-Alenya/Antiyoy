using CodeBase.Infrastructure.Hub.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Hub
{
    public class GameHubInstaller : MonoInstaller
    {
        [SerializeField] private GameHubUIPrefabData _uiPrefab;

        public override void InstallBindings()
        {
            Container.Bind<GameHubUIFactory>().AsSingle().WithArguments(_uiPrefab);
            Container.BindInterfacesTo<GameHubStartup>().AsSingle();
        }
    }
}
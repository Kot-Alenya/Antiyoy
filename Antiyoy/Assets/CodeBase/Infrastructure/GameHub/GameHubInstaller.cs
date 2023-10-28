using CodeBase.Infrastructure.GameHub.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameHub
{
    public class GameHubInstaller : MonoInstaller
    {
        [SerializeField] private GameHubUIPrefabData _uiPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameHubStartup>().AsSingle();
            Container.Bind<GameHubUIFactory>().AsSingle().WithArguments(_uiPrefab);
        }
    }
}
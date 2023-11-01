using CodeBase.Hub.UI;
using Zenject;

namespace CodeBase.Infrastructure.Hub
{
    public class HubStartup : IInitializable
    {
        private readonly HubUIFactory _uiFactory;

        private HubStartup(HubUIFactory uiFactory) => _uiFactory = uiFactory;

        public void Initialize() => _uiFactory.Create();
    }
}
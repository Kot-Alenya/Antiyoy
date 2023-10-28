using CodeBase.Infrastructure.Hub.UI;
using Zenject;

namespace CodeBase.Infrastructure.Hub
{
    public class GameHubStartup : IInitializable
    {
        private readonly GameHubUIFactory _uiFactory;

        private GameHubStartup(GameHubUIFactory uiFactory) => _uiFactory = uiFactory;

        public void Initialize() => _uiFactory.Create();
    }
}
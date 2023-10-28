using CodeBase.Infrastructure.GameHub.UI;
using Zenject;

namespace CodeBase.Infrastructure.GameHub
{
    public class GameHubStartup : IInitializable
    {
        private readonly GameHubUIFactory _uiFactory;

        private GameHubStartup(GameHubUIFactory uiFactory) => _uiFactory = uiFactory;

        public void Initialize() => _uiFactory.Create();
    }
}
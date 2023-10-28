using CodeBase.Gameplay.Version.Handler;
using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.Gameplay.UI
{
    public class GameplayReturnBackButton : ButtonBase
    {
        private IWorldVersionHandler _versionHandler;

        [Inject]
        private void Construct(IWorldVersionHandler versionHandler) => _versionHandler = versionHandler;

        private protected override void OnClick() => _versionHandler.ReturnBack();
    }
}
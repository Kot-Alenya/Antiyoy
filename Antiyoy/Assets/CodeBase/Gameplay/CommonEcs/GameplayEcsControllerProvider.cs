namespace CodeBase.Gameplay.CommonEcs
{
    public class GameplayEcsControllerProvider
    {
        private GameplayEcsController _controller;

        public void Initialize(GameplayEcsController controller) => _controller = controller;

        public GameplayEcsController GetController() => _controller;
    }
}
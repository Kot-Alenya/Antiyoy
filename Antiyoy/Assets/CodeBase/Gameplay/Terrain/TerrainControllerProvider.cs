namespace CodeBase.Gameplay.Terrain
{
    public class TerrainControllerProvider
    {
        private TerrainController _controller;
        
        public void Initialize(TerrainController controller) => _controller = controller;

        public TerrainController GetController() => _controller;
    }
}
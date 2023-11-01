namespace CodeBase.Gameplay.World.Version
{
    public interface IWorldVersionOperation
    {
        public void Apply();

        public void Revert();
    }
}
namespace CodeBase.Gameplay.World.Version
{
    public interface IWorldVersionOperationHandler
    {
        public void Apply(IWorldVersionOperationData data);

        public void Revert(IWorldVersionOperationData data);
    }
}
namespace CodeBase.Gameplay.World
{
    public interface ICloneable<out T>
    {
        public T Clone();
    }
}
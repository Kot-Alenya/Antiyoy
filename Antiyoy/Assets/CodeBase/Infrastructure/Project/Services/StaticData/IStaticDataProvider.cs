namespace CodeBase.Infrastructure.Project.Services.StaticData
{
    public interface IStaticDataProvider
    {
        public T Get<T>() where T : IStaticData;
    }
}
using CodeBase.Infrastructure.Services.StaticData.Data;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataProvider
    {
        public T Get<T>() where T : IStaticData;
    }
}
namespace CodeBase.Infrastructure.Project.Services.StaticData
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private readonly IStaticData[] _data;

        public StaticDataProvider(params IStaticData[] data) => _data = data;

        public T Get<T>() where T : IStaticData
        {
            foreach (var data in _data)
                if (data is T requiredData)
                    return requiredData;

            return default;
        }
    }
}
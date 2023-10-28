using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Project.Services.StaticData.Data;

namespace CodeBase.Infrastructure.Project.Services.StaticData
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private readonly Dictionary<Type, IStaticData> _data = new();

        public StaticDataProvider(params IStaticData[] dataToProvide) => SetOrReplace(dataToProvide);

        public T Get<T>() where T : IStaticData => (T)_data[typeof(T)];

        private void SetOrReplace(params IStaticData[] dataToProvide)
        {
            foreach (var data in dataToProvide)
                _data[data.GetType()] = data;
        }
    }
}
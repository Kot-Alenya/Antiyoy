using CodeBase.Infrastructure.Services.ProgressSaveLoader.Watcher;

namespace CodeBase.Infrastructure.Services.ProgressSaveLoader
{
    public interface IProgressSaveLoader
    {
        public void RegisterWatcher(IProgressWatcher watcher);

        public void UnRegisterWatcher(IProgressWatcher watcher);

        public void ClearWatchers();

        public void Save<T>(string key) where T : ProgressData, new();

        public void Load<T>(string key) where T : ProgressData, new();
    }
}
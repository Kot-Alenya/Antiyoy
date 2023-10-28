namespace CodeBase.Infrastructure.Project.Services.ProgressSaveLoader.Watcher
{
    public interface IProgressReader<in T> : IProgressWatcher where T : ProgressData
    {
        public void OnProgressLoad(T progress);
    }
}
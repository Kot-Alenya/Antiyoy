namespace CodeBase.Infrastructure.Services.ProgressSaveLoader.Watcher
{
    public interface IProgressWriter<in T> : IProgressWatcher where T : ProgressData
    {
        public void OnProgressSave(T progress);
    }
}
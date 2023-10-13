using CodeBase.Gameplay.World.Data.Operation;

namespace CodeBase.Gameplay.World.Change.Recorder
{
    public interface IWorldChangeRecorder
    {
        public void Record(params IWorldOperationData[] changes);

        public void AddToBuffer(IWorldOperationData change);

        public void RecordFromBuffer();

        public void Clear();
    }
}
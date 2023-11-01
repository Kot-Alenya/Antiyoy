using CodeBase.Gameplay.World.Version.Operation;

namespace CodeBase.Gameplay.World.Version.Recorder
{
    public interface IWorldVersionRecorder
    {
        void Record(params IOperationData[] changes);
        void AddToBuffer(IOperationData change);
        void RecordFromBufferAndClearBuffer();
        void ClearRecords();
        bool TryGetPreviousRecord(out IOperationData[] record);
        bool TryGetNextRecord(out IOperationData[] record);
    }
}
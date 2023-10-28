using CodeBase.Gameplay.Version.Operation;

namespace CodeBase.Gameplay.Version.Recorder
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
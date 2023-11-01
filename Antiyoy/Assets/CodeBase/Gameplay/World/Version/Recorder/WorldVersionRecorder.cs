using System;
using System.Collections.Generic;
using CodeBase.Gameplay.World.Version.Operation;

namespace CodeBase.Gameplay.World.Version.Recorder
{
    public class WorldVersionRecorder : IWorldVersionRecorder
    {
        private readonly List<IOperationData[]> _records = new();
        private int _readIndex = -1;

        public readonly List<IOperationData> Buffer = new();

        public void Record(params IOperationData[] operations)
        {
            _readIndex++;
            _records.RemoveRange(_readIndex, _records.Count - _readIndex);
            _records.Add(operations);
        }

        public void AddToBuffer(IOperationData operation) => Buffer.Add(operation);

        public void RecordFromBufferAndClearBuffer()
        {
            Record(Buffer.ToArray());
            Buffer.Clear();
        }

        public void ClearRecords()
        {
            _records.Clear();
            _readIndex = -1;
        }

        public bool TryGetPreviousRecord(out IOperationData[] record)
        {
            if (_readIndex >= 0)
            {
                record = _records[_readIndex];
                _readIndex--;
                return true;
            }

            record = Array.Empty<IOperationData>();
            return false;
        }

        public bool TryGetNextRecord(out IOperationData[] record)
        {
            if (_readIndex < _records.Count - 1)
            {
                _readIndex++;
                record = _records[_readIndex];
                return true;
            }

            record = Array.Empty<IOperationData>();
            return false;
        }
    }
}
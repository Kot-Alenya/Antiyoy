using System;
using System.Collections.Generic;

namespace CodeBase.Gameplay.World.Version
{
    public class WorldVersionRecorder
    {
        private readonly List<IWorldVersionOperationData[]> _records = new();
        private int _readIndex = -1;

        private readonly List<IWorldVersionOperationData> _buffer = new();

        private void Record(params IWorldVersionOperationData[] operations)
        {
            _readIndex++;
            _records.RemoveRange(_readIndex, _records.Count - _readIndex);
            _records.Add(operations);
        }

        public void AddToBuffer(IWorldVersionOperationData operationHandler) => _buffer.Add(operationHandler);

        public void RecordFromBufferAndClearBuffer()
        {
            Record(_buffer.ToArray());
            _buffer.Clear();
        }

        public void ClearRecords()
        {
            _records.Clear();
            _readIndex = -1;
        }

        public bool TryGetPreviousRecord(out IWorldVersionOperationData[] record)
        {
            if (_readIndex >= 0)
            {
                record = _records[_readIndex];
                _readIndex--;
                return true;
            }

            record = Array.Empty<IWorldVersionOperationData>();
            return false;
        }

        public bool TryGetNextRecord(out IWorldVersionOperationData[] record)
        {
            if (_readIndex < _records.Count - 1)
            {
                _readIndex++;
                record = _records[_readIndex];
                return true;
            }

            record = Array.Empty<IWorldVersionOperationData>();
            return false;
        }
    }
}
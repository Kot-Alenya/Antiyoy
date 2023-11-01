using System;
using System.Collections.Generic;

namespace CodeBase.Gameplay.World.Version
{
    public class WorldVersionRecorder
    {
        private readonly List<IWorldVersionOperation[]> _records = new();
        private int _readIndex = -1;

        private readonly List<IWorldVersionOperation> _buffer = new();

        private void Record(params IWorldVersionOperation[] operations)
        {
            _readIndex++;
            _records.RemoveRange(_readIndex, _records.Count - _readIndex);
            _records.Add(operations);
        }

        public void AddToBuffer(IWorldVersionOperation operation) => _buffer.Add(operation);

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

        public bool TryGetPreviousRecord(out IWorldVersionOperation[] record)
        {
            if (_readIndex >= 0)
            {
                record = _records[_readIndex];
                _readIndex--;
                return true;
            }

            record = Array.Empty<IWorldVersionOperation>();
            return false;
        }

        public bool TryGetNextRecord(out IWorldVersionOperation[] record)
        {
            if (_readIndex < _records.Count - 1)
            {
                _readIndex++;
                record = _records[_readIndex];
                return true;
            }

            record = Array.Empty<IWorldVersionOperation>();
            return false;
        }
    }
}
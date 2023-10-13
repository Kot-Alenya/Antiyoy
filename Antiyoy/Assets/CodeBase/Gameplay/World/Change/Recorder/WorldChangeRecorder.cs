using System;
using System.Collections.Generic;
using CodeBase.Gameplay.World.Data.Operation;

namespace CodeBase.Gameplay.World.Change.Recorder
{
    public class WorldChangeRecorder : IWorldChangeRecorder
    {
        private readonly List<IWorldOperationData[]> _records = new();
        private readonly List<IWorldOperationData> _buffer = new();
        private int _readIndex = -1;

        public void Record(params IWorldOperationData[] operations)
        {
            _readIndex++;
            _records.RemoveRange(_readIndex, _records.Count - _readIndex);
            _records.Add(operations);
        }

        public void AddToBuffer(IWorldOperationData operation) => _buffer.Add(operation);

        public void RecordFromBuffer()
        {
            Record(_buffer.ToArray());
            _buffer.Clear();
        }

        public void Clear()
        {
            _records.Clear();
            _readIndex = -1;
        }

        public bool TryBack(out IWorldOperationData[] record)
        {
            if (_readIndex >= 0)
            {
                record = _records[_readIndex];
                _readIndex--;
                return true;
            }
            
            record = Array.Empty<IWorldOperationData>();
            return false;
        }

        public bool TryNext(out IWorldOperationData[] record)
        {
            if (_readIndex < _records.Count - 1)
            {
                _readIndex++;
                record = _records[_readIndex];
                return true;
            }
            
            record = Array.Empty<IWorldOperationData>();
            return false;
        }
    }
}
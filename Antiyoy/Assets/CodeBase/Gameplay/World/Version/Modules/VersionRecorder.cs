using System;
using System.Collections.Generic;
using CodeBase.Gameplay.World.Version.Operation;

namespace CodeBase.Gameplay.World.Version.Modules
{
    public class VersionRecorder
    {
        private readonly List<IWorldOperationData[]> _records = new();
        private int _readIndex = -1;

        public readonly List<IWorldOperationData> Buffer = new();

        public void Record(params IWorldOperationData[] operations)
        {
            _readIndex++;
            _records.RemoveRange(_readIndex, _records.Count - _readIndex);
            _records.Add(operations);
        }

        public void AddToBuffer(IWorldOperationData operation) => Buffer.Add(operation);

        public void RecordFromBuffer()
        {
            Record(Buffer.ToArray());
            Buffer.Clear();
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
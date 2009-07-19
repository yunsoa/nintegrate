using System.Collections.Generic;
using System.Threading;

namespace NIntegrate.ServiceModel.Collections.Generic
{
    public abstract class Registry<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dict;
        private readonly ReaderWriterLock _dictLock;

        internal Registry()
        {
            _dict = new Dictionary<TKey, TValue>();
            _dictLock = new ReaderWriterLock();
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                return RWLock.GetReadLock(
                    _dictLock,
                    RWLock.DEFAULT_RWLOCK_TIMEOUT,
                    () =>
                        {
                            TValue value;
                            if (_dict.TryGetValue(key, out value))
                                return value;
                            return default(TValue);
                        });
            }
        }

        public virtual bool AddItem(TKey key, TValue value)
        {
            if (!_dict.ContainsKey(key))
            {
                return RWLock.GetWriteLock(
                    _dictLock,
                    RWLock.DEFAULT_RWLOCK_TIMEOUT,
                    () =>
                        {
                            if (!_dict.ContainsKey(key))
                            {
                                _dict.Add(key, value);
                                return true;
                            }
                            return false;
                        }
                    );
            }

            return false;
        }
    }
}
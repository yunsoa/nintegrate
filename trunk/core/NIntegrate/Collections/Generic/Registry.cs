using System.Collections.Generic;
using System.Threading;
using NIntegrate.Threading;

namespace NIntegrate.Collections.Generic
{
    public abstract class Registry<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
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

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        #endregion
    }
}
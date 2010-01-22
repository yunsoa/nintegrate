using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using NIntegrate.Threading;

namespace NIntegrate.Collections.Generic
{
    /// <summary>
    /// A thread safe Lru generic IDictionary implementation
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Lru")]
    public class LruDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Non-Public Fields

        private readonly int _capacity;
        private readonly Dictionary<TKey, Node> _data;
        private Node _head;
        private Node _tail;
        private readonly ReaderWriterLock _lock = new ReaderWriterLock();
        private readonly int _lockTimeout = 30000;

        #endregion

        #region Non-Public Properties

        protected ReaderWriterLock Lock
        {
            get
            {
                return _lock;
            }
        }

        protected int LockTimeout
        {
            get
            {
                return _lockTimeout;
            }
        }

        #endregion

        #region Private Nested Classes

        private class Node
        {
            #region Private Fields

            private readonly TKey _key;
            private Node _prev;
            private Node _next;
            private readonly TValue _value;

            #endregion

            #region Public Properties

            public TKey Key
            {
                get
                {
                    return _key;
                }
            }

            public Node Prev
            {
                get
                {
                    return _prev;
                }
                set
                {
                    _prev = value;
                }
            }

            public Node Next
            {
                get
                {
                    return _next;
                }
                set
                {
                    _next = value;
                }
            }

            public TValue Value
            {
                get
                {
                    return _value;
                }
            }

            #endregion

            #region Constructors

            public Node(TKey key, TValue value)
            {
                _key = key;
                _value = value;
            }

            #endregion
        }

        #endregion

        #region Constructors

        public LruDictionary(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("capacity MUST > 0");

            this._capacity = capacity;
            this._data = new Dictionary<TKey, Node>();
        }

        #endregion

        #region Private Methods

        private void InsertNode(Node node)
        {
            node.Next = _head;
            node.Prev = null;
            if (_head != null)
                _head.Prev = node;
            _head = node;
            if (_tail == null)
                _tail = node;
        }

        private void DeleteNode(Node node)
        {
            if (node.Prev != null)
                node.Prev.Next = node.Next;
            else
                _head = node.Next;
            if (node.Next != null)
                node.Next.Prev = node.Prev;
            else
                _tail = node.Prev;
        }

        private bool RemoveLru()
        {
            return RWLock.GetWriteLock(_lock, _lockTimeout, delegate
            {
                if (_tail == null)
                    return false;
                _data.Remove(_tail.Key);
                DeleteNode(_tail);
                return true;
            });
        }

        protected bool InternalRemove(TKey key)
        {
            Node node;
            if (_data.TryGetValue(key, out node))
            {
                _data.Remove(key);
                DeleteNode(node);
                return true;
            }
            return false;
        }

        #endregion

        #region Public Methods

        public void Add(TKey key, TValue value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            RWLock.GetWriteLock(_lock, _lockTimeout, delegate
            {
                // Throw out old one if reusing a key.
                if (_data.ContainsKey(key))
                    Remove(key);
                // Keep throwing out old items to make space. Stop if empty.
                while (_data.Count >= _capacity)
                {
                    if (!RemoveLru())
                        break;
                }
                if (_data.Count >= _capacity)
                    // Just not enough room.
                    return false;
                Node node = new Node(key, value);
                _data.Add(key, node);
                InsertNode(node);
                return true;
            });
        }

        public bool Remove(TKey key)
        {
            return RWLock.GetWriteLock(_lock, _lockTimeout, delegate
            {
                return InternalRemove(key);
            });
        }

        public void Clear()
        {
            RWLock.GetWriteLock(_lock, _lockTimeout, delegate
            {
                _data.Clear();
                _head = null;
                _tail = null;
                return true;
            });
        }

        public bool ContainsKey(TKey key)
        {
            return _data.ContainsKey(key);
        }

        public TValue this[TKey key]
        {
            get
            {
                return RWLock.GetReadLock(_lock, _lockTimeout, delegate
                {
                    Node node;
                    if (_data.TryGetValue(key, out node))
                    {
                        DeleteNode(node);
                        InsertNode(node);
                        return node.Value;
                    }
                    return default(TValue);
                });
            }
            set
            {
                Add(key, value);
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return _data.Keys;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = this[key];
            return (value != null);
        }

        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> retList = new List<TValue>();
                foreach (Node node in _data.Values)
                    retList.Add(node.Value);
                return retList;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _data.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null || arrayIndex >= _data.Count - 1)
                return;

            if (arrayIndex < 0)
                arrayIndex = 0;

            IEnumerator<KeyValuePair<TKey, TValue>> en = this.GetEnumerator();
            int i = 0;
            while (i < arrayIndex)
                en.MoveNext();
            for (int j = 0; i < _data.Count && j < array.Length; ++i, ++j)
            {
                en.MoveNext();
                array[j] = en.Current;
            }
        }

        public int Count
        {
            get
            {
                return _data.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            Dictionary<TKey, Node>.Enumerator en = _data.GetEnumerator();
            while (en.MoveNext())
                yield return new KeyValuePair<TKey, TValue>(en.Current.Key, en.Current.Value.Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Dictionary<TKey, Node>.Enumerator en = _data.GetEnumerator();
            while (en.MoveNext())
                yield return new KeyValuePair<TKey, TValue>(en.Current.Key, en.Current.Value.Value);
        }

        #endregion
    }

    /// <summary>
    /// The LruDependingDictionary class is an enhanced version of LruDictionary, providing additional caching dependency ability.
    /// </summary>
    /// <typeparam name="TKey">The type of cached item key.</typeparam>
    /// <typeparam name="TValue">The type of cached item value.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Lru")]
    public class LruDependingDictionary<TKey, TValue> 
        : LruDictionary<DependingKey<TKey>, TValue>
    {
        #region Constructors

        public LruDependingDictionary(int capacity)
            : base(capacity)
        {
        }

        #endregion

        #region Public Methods

        public TValue this[TKey key]
        {
            get
            {
                return this[new DependingKey<TKey>(key)];
            }
        }

        public void Remove(TKey key)
        {
            Remove(new DependingKey<TKey>(key));
        }

        public void NotifyDependencyChanged(string dependency, bool ignoreCase, bool partiallyMatch)
        {
            List<DependingKey<TKey>> list = new List<DependingKey<TKey>>();
            IEnumerator<DependingKey<TKey>> en = this.Keys.GetEnumerator();
            RWLock.GetWriteLock(Lock, LockTimeout, delegate
            {
                while (en.MoveNext())
                    if (en.Current.Depends(dependency, ignoreCase, partiallyMatch))
                        list.Add(en.Current);
                if (list.Count > 0)
                    for (int i = 0; i < list.Count; ++i)
                        this.InternalRemove(list[i]);
                return true;
            });
        }

        public void NotifyDependencyChanged(string dependency)
        {
            NotifyDependencyChanged(dependency, false, false);
        }

        #endregion
    }
}
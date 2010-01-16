using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NIntegrate.Threading
{
    public abstract class SingleThreadQueue<TItem> where TItem: class
    {
        private Mutex _entryLock;
        private ManualResetEvent _entryWait;
        private Queue<TItem> _itemQueue;
        private AutoResetEvent _processWait;
        private Thread _workerThread;
        private const int _defaultSleepMilliseconds = 100;

        #region Constructors

        protected SingleThreadQueue(int maxQueueLength)
            : this(maxQueueLength, _defaultSleepMilliseconds)
        {
        }

        protected SingleThreadQueue(int maxQueueLength, int threadSleepMilliseconds)
        {
            _itemQueue = new Queue<TItem>();
            ThreadSleepMilliseconds = 100;
            _processWait = new AutoResetEvent(false);
            _entryWait = new ManualResetEvent(true);
            _entryLock = new Mutex();
            if (maxQueueLength < -1)
            {
                throw new ArgumentException(SR.VALID_MAX_QUEUE_LENGTH);
            }
            if (threadSleepMilliseconds <= 0)
            {
                throw new ArgumentException(SR.VALID_THREAD_SLEEP_TIME);
            }
            MaxQueueLength = maxQueueLength;
            ThreadSleepMilliseconds = threadSleepMilliseconds;
        }

        #endregion

        #region Properties

        protected int MaxQueueLength { get; private set; }

        protected int ThreadSleepMilliseconds { get; private set; }

        protected int Count
        {
            get
            {
                lock (_itemQueue)
                {
                    return _itemQueue.Count;
                }
            }
        }

        #endregion

        #region Public Methods

        public virtual void Enqueue(TItem item)
        {
            if (MaxQueueLength == 0)
            {
                Process(item);
            }
            else if (Count < MaxQueueLength)
            {
                AddToQueue(item);
                InvokeThreadStart();
            }
            else
            {
                OnQueueOverflow(item);
            }
        }

        #endregion

        #region Non-Public Methods

        protected void AddToQueue(TItem item)
        {
            lock (_itemQueue)
            {
                _itemQueue.Enqueue(item);
            }
        }

        protected TItem Dequeue()
        {
            lock (_itemQueue)
            {
                return _itemQueue.Dequeue();
            }
        }

        protected void InvokeThreadStart()
        {
            lock (_itemQueue)
            {
                if ((_workerThread == null) && (_workerThread == null))
                {
                    _workerThread = new Thread(new ThreadStart(ProcessQueue));
                    _workerThread.Name = GetType().Name;
                    _workerThread.Start();
                }
                _workerThread.IsBackground = false;
                _processWait.Set();
            }
        }

        protected virtual void OnAfterProcess()
        {
            if (Count < MaxQueueLength)
            {
                _entryWait.Set();
            }
        }

        protected virtual void OnQueueOverflow(TItem item)
        {
            while (item != null)
            {
                _entryWait.WaitOne();
                _entryLock.WaitOne();
                try
                {
                    if (Count < MaxQueueLength)
                    {
                        AddToQueue(item);
                        item = default(TItem);
                    }
                }
                finally
                {
                    _entryLock.ReleaseMutex();
                }
            }
        }

        protected TItem Peek()
        {
            lock (_itemQueue)
            {
                return _itemQueue.Peek();
            }
        }

        protected abstract void Process(TItem item);

        protected bool TryPeek(ref TItem item_out)
        {
            lock (_itemQueue)
            {
                if (_itemQueue.Count > 0)
                {
                    item_out = _itemQueue.Peek();
                    return true;
                }
                item_out = default(TItem);
                return false;
            }
        }

        private void ProcessQueue()
        {
            TItem item = default(TItem);
            while (true)
            {
                _processWait.WaitOne(ThreadSleepMilliseconds, false);
                Thread.CurrentThread.IsBackground = false;
                while (TryPeek(ref item))
                {
                    Process(item);
                    Dequeue();
                    OnAfterProcess();
                }
                lock (_itemQueue)
                {
                    if (Count == 0)
                    {
                        Thread.CurrentThread.IsBackground = true;
                    }
                }
            }
        }

        #endregion
    }
}
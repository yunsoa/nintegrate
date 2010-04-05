using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NIntegrate.Threading
{
    /// <summary>
    /// The SingleThreadQueue class is a queue attached with a thread &amp; a process handler. Items added to the queue are always processed by specified process handler in a single thread.
    /// </summary>
    /// <typeparam name="TItem">The type of the item to be added &amp; processed in the queue.</typeparam>
    public abstract class SingleThreadQueue<TItem> where TItem: class
    {
        private Mutex _entryLock;
        private ManualResetEvent _entryWait;
        private Queue<TItem> _itemQueue;
        private AutoResetEvent _processWait;
        private Thread _workerThread;
        private const int _defaultSleepMilliseconds = 100;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleThreadQueue&lt;TItem&gt;"/> class.
        /// </summary>
        /// <param name="maxQueueLength">Length of the max queue.</param>
        protected SingleThreadQueue(int maxQueueLength)
            : this(maxQueueLength, _defaultSleepMilliseconds)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleThreadQueue&lt;TItem&gt;"/> class.
        /// </summary>
        /// <param name="maxQueueLength">Length of the max queue.</param>
        /// <param name="threadSleepMilliseconds">The thread sleep milliseconds.</param>
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

        /// <summary>
        /// Gets or sets the length of the max queue.
        /// </summary>
        /// <value>The length of the max queue.</value>
        protected int MaxQueueLength { get; private set; }

        /// <summary>
        /// Gets or sets the thread sleep milliseconds.
        /// </summary>
        /// <value>The thread sleep milliseconds.</value>
        protected int ThreadSleepMilliseconds { get; private set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
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

        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
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

        /// <summary>
        /// Adds to queue.
        /// </summary>
        /// <param name="item">The item.</param>
        protected void AddToQueue(TItem item)
        {
            lock (_itemQueue)
            {
                _itemQueue.Enqueue(item);
            }
        }

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <returns></returns>
        protected TItem Dequeue()
        {
            lock (_itemQueue)
            {
                return _itemQueue.Dequeue();
            }
        }

        /// <summary>
        /// Invokes the thread start.
        /// </summary>
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

        /// <summary>
        /// Called when after process.
        /// </summary>
        protected virtual void OnAfterProcess()
        {
            if (Count < MaxQueueLength)
            {
                _entryWait.Set();
            }
        }

        /// <summary>
        /// Called when queue overflow.
        /// </summary>
        /// <param name="item">The item.</param>
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

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns></returns>
        protected TItem Peek()
        {
            lock (_itemQueue)
            {
                return _itemQueue.Peek();
            }
        }

        /// <summary>
        /// Processes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected abstract void Process(TItem item);

        /// <summary>
        /// Tries the peek.
        /// </summary>
        /// <param name="item_out">The item_out.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Processes the queue.
        /// </summary>
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
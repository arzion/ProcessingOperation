using System;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace ProcessingOperations
{
    /// <summary>
    /// The manager that can process some work.
    /// </summary>
    /// <seealso cref="IProcessingManager" />
    public sealed class RepeatingOperationProcessingManager : IProcessingManager
    {
        private readonly IOperation _operation;

        private bool _isDisposed;
        private bool _isRunning;

        private readonly Timer _timer;
        private readonly object _syncObject = new object();
        private readonly CancellationTokenSource _operationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatingOperationProcessingManager" /> class.
        /// </summary>
        /// <param name="operation">The assotiated operation that should be executed.</param>
        /// <param name="settings">The settings of the repeating operation.</param>
        public RepeatingOperationProcessingManager(
            IOperation operation,
            RepeatingOperationSettings settings)
        {
            _operation = operation;
            var intervalMs = settings.EverySeconds * 1000;
            _timer = new Timer { Interval = intervalMs };
        }

        /// <summary>
        /// Starts processing of the operation.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">Throws when object was disposed.</exception>
        /// <exception cref="System.InvalidOperationException">Throws if operation is already running.</exception>
        public void Start()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (_isRunning)
            {
                throw new InvalidOperationException("Process is already running.");
            }
            _isRunning = true;

            new TaskFactory()
                .StartNew(() => DoExecute(_operationTokenSource.Token))
                .ContinueWith(task =>
                {
                    lock (_syncObject)
                    {
                        if (_isDisposed)
                        {
                            return;
                        }

                        _timer.Start();

                        _timer.Elapsed += (sender, args) =>
                        {
                            _timer.Stop();

                            lock (_syncObject)
                            {
                                if (_isDisposed)
                                {
                                    return;
                                }
                                DoExecute(_operationTokenSource.Token);
                                _timer.Start();
                            }
                        };
                    }
                });
        }

        /// <summary>
        /// Stops processing of the operation.
        /// </summary>
        public void Stop()
        {
            Dispose();
        }

        /// <summary>
        /// Occurs when error happens during execution of operation.
        /// </summary>
        public event Action<Exception> OnExecutionError;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _operationTokenSource.Cancel();

            lock (_syncObject)
            {
                _timer.Stop();
                _isDisposed = true;
            }
        }

        private void DoExecute(CancellationToken token)
        {
            try
            {
                _operation.Execute(token);
            }
            catch (Exception ex)
            {
                OnExecutionError?.Invoke(ex);
            }
        }
    }
}
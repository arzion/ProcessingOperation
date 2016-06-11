using System;

namespace ProcessingOperations
{
    /// <summary>
    /// The manager that can process some work.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IProcessingManager : IDisposable
    {
        /// <summary>
        /// Starts processing of the operation.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops processing of the operation.
        /// </summary>
        void Stop();

        /// <summary>
        /// Occurs when error happens during execution of operation.
        /// </summary>
        event Action<Exception> OnExecutionError;
    }
}
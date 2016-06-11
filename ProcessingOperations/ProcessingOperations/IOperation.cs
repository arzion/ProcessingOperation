using System.Threading;

namespace ProcessingOperations
{
    /// <summary>
    /// Handler of the operation.
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        void Execute(CancellationToken token);
    }
}
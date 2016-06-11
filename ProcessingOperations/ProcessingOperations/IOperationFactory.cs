namespace ProcessingOperations
{
    /// <summary>
    /// The factory that creates Operations by specified key.
    /// </summary>
    public interface IOperationFactory
    {
        /// <summary>
        /// Creates the operation instance by operation key.
        /// </summary>
        IOperation Create(string operationKey);
    }
}
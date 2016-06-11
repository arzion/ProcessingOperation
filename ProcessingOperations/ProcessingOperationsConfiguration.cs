namespace ProcessingOperations
{
    /// <summary>
    /// The global configuration of the processing operations.
    /// </summary>
    public static class ProcessingOperationsConfiguration
    {
        internal static IOperationFactory OperationFactory { get; private set; }

        /// <summary>
        /// Sets the operation factory to create the operations.
        /// </summary>
        /// <param name="operationFactory">The operation factory.</param>
        public static void SetOperationFactory(IOperationFactory operationFactory)
        {
            OperationFactory = operationFactory;
        }
    }
}
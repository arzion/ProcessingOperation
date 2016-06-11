namespace ProcessingOperations.Configuration
{
    /// <summary>
    /// The setting of repeating operation processing manager.
    /// </summary>
    internal interface IRepeatingOperationProcessingManagerSettings
    {
        /// <summary>
        /// Gets the name of the processing manager.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the key of the operation associated with operation.
        /// </summary>
        string OperationKey { get; }

        /// <summary>
        /// Gets the interval of execution of processing manager associated operation in seconds.
        /// </summary>
        double EverySeconds { get; }
    }
}
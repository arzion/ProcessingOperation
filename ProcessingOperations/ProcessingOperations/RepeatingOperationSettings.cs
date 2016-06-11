namespace ProcessingOperations
{
    /// <summary>
    /// The settings of the <see cref="RepeatingOperationProcessingManager"/>.
    /// </summary>
    public class RepeatingOperationSettings
    {
        /// <summary>
        /// Gets or sets the interval of operation execution by <see cref="RepeatingOperationProcessingManager"/> in seconds.
        /// </summary>
        public double EverySeconds { get; set; }
    }
}
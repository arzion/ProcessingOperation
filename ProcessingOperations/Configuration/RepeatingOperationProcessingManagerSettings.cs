using System.Configuration;

namespace ProcessingOperations.Configuration
{
    /// <summary>
    /// The settings of the repeating operation processing managers.
    /// </summary>
    internal class RepeatingOperationProcessingManagerSettings : ConfigurationElement, IRepeatingOperationProcessingManagerSettings
    {
        /// <summary>
        /// Gets the name of the manager.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name => base["name"].ToString();

        /// <summary>
        /// Gets the interval of execution of processing manager associated operation in seconds.
        /// </summary>
        [ConfigurationProperty("everySeconds", IsRequired = true)]
        public double EverySeconds => double.Parse(base["everySeconds"].ToString());

        /// <summary>
        /// Gets the key of the handler registered in DI container.
        /// </summary>
        [ConfigurationProperty("operationKey", IsRequired = true)]
        public string OperationKey => base["operationKey"].ToString();
    }
}
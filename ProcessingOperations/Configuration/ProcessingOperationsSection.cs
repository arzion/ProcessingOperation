using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ProcessingOperations.Configuration
{
    /// <summary>
    /// Represents the section of the processing manager in configuration file.
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationSection" />
    internal class ProcessingOperationsSection : ConfigurationSection, IProcessingOperationsSettings
    {
        /// <summary>
        /// Gets the message processing managers element.
        /// </summary>
        [ConfigurationProperty("repeatingOperations")]
        private RepeatingOperationProcessingManagersCollection RepeatingOperationsProcessingManagersElement
            => (RepeatingOperationProcessingManagersCollection)base["repeatingOperations"];

        /// <summary>
        /// Gets the repeating operations processing managers settings.
        /// </summary>
        [ConfigurationProperty("repeatingOperations")]
        public IEnumerable<IRepeatingOperationProcessingManagerSettings> RepeatingOperationProcessingManagers
            => RepeatingOperationsProcessingManagersElement.Cast<IRepeatingOperationProcessingManagerSettings>();
    }
}
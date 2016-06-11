using System.Collections.Generic;

namespace ProcessingOperations.Configuration
{
    /// <summary>
    /// The settings of the processing managers.
    /// </summary>
    internal interface IProcessingOperationsSettings
    {
        /// <summary>
        /// Gets the repeating operation processing managers settings.
        /// </summary>
        /// <value>
        /// The repeating operation processing managers settings.
        /// </value>
        IEnumerable<IRepeatingOperationProcessingManagerSettings> RepeatingOperationProcessingManagers { get; }
    }
}
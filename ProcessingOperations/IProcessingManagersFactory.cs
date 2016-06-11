using System.Collections.Generic;

namespace ProcessingOperations
{
    /// <summary>
    /// The factory to create processing managers.
    /// </summary>
    public interface IProcessingManagersFactory
    {
        /// <summary>
        /// Creates list of the processing managers based on the configuration file.
        /// </summary>
        /// <returns>The list of processing managers.</returns>
        IList<IProcessingManager> Create();

        /// <summary>
        /// Creates the <see cref="IProcessingManager"/> by specified name.
        /// </summary>
        /// <param name="name">The name of the manager.</param>
        /// <returns>Created processing manager.</returns>
        IProcessingManager Create(string name);
    }
}
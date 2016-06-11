using System.Configuration;

namespace ProcessingOperations.Configuration
{
    /// <summary>
    /// The provider of the settings for processing managers.
    /// </summary>
    internal class ProcessingOperationsSettingsProvider : IProcessingOperationsSettingsProvider
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The processing manager settings.</returns>
        public IProcessingOperationsSettings GetSettings()
        {
            return ConfigurationManager.GetSection("processingOperations") as ProcessingOperationsSection;
        }
    }
}
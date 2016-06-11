using System.Configuration;

namespace ProcessingOperations.Configuration
{
    /// <summary>
    /// The provider of the settings for processing managers.
    /// </summary>
    internal class ProcessingManagersSettingsProvider : IProcessingManagersSettingsProvider
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The processing manager settings.</returns>
        public IProcessingManagersSettings GetSettings()
        {
            return ConfigurationManager.GetSection("processingManagers") as ProcessingManagersSection;
        }
    }
}
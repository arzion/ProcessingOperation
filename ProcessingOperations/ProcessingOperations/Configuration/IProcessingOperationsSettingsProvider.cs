namespace ProcessingOperations.Configuration
{
    /// <summary>
    /// The provider of the settings for processing managers.
    /// </summary>
    internal interface IProcessingOperationsSettingsProvider
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The processing managers settings.</returns>
        IProcessingOperationsSettings GetSettings();
    }
}
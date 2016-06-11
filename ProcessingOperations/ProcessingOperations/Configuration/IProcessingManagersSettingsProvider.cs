namespace ProcessingOperations.Configuration
{
    /// <summary>
    /// The provider of the settings for processing managers.
    /// </summary>
    internal interface IProcessingManagersSettingsProvider
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The processing managers settings.</returns>
        IProcessingManagersSettings GetSettings();
    }
}
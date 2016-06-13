using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ProcessingOperations.Configuration;

namespace ProcessingOperations
{
    /// <summary>
    /// The factory to create processing managers.
    /// </summary>
    /// <seealso cref="IProcessingManagersFactory" />
    public class ProcessingManagersFactory : IProcessingManagersFactory
    {
        private readonly IProcessingOperationsSettingsProvider _settingsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingManagersFactory" /> class.
        /// </summary>
        /// <exception cref="InvalidOperationException">IOperation factory should be configured.</exception>
        public ProcessingManagersFactory()
        {
            _settingsProvider = new ProcessingOperationsSettingsProvider();
        }

        /// <summary>
        /// Creates list of the processing managers based on the configuration file.
        /// </summary>
        /// <returns>The list of processing managers.</returns>
        /// <exception cref="ConfigurationErrorsException">There were no components found associated with the processing manager.</exception>
        public IList<IProcessingManager> Create()
        {
            var settings = _settingsProvider.GetSettings();

            var processingManagers = new List<IProcessingManager>();
            if (settings.RepeatingOperationProcessingManagers != null)
            {
                processingManagers.AddRange(settings.RepeatingOperationProcessingManagers.Select(BuildProcessingManager));
            }

            return processingManagers;
        }

        /// <summary>
        /// Creates the <see cref="IProcessingManager" /> by specified name.
        /// </summary>
        /// <param name="name">The name of the manager.</param>
        /// <returns>
        /// Created processing manager.
        /// </returns>
        /// <exception cref="ConfigurationErrorsException">There was no processing manager in configuration with specified name.</exception>
        public IProcessingManager Create(string name)
        {
            var settings = _settingsProvider.GetSettings();
            
            var operationProcessingManagerSettings = settings
                .RepeatingOperationProcessingManagers?.FirstOrDefault(s => s.Name == name);
            if (operationProcessingManagerSettings != null)
            {
                return BuildProcessingManager(operationProcessingManagerSettings);
            }

            throw new ConfigurationErrorsException($"There is no Processing manager in the configuration with name: {name}");
        }

        private IProcessingManager BuildProcessingManager(IRepeatingOperationProcessingManagerSettings settings)
        {
            var operation = CreateOperation(settings);
            if (operation == null)
            {
                throw new ConfigurationErrorsException($"There were no components found associated with the processing manager. Operation key: {settings.OperationKey}");
            }
            return CreateRepeatingOperationProcessingManager(operation, settings);
        }

        private IOperation CreateOperation(IRepeatingOperationProcessingManagerSettings settings)
        {
            if (ProcessingOperationsConfiguration.OperationFactory != null)
            {
                return ProcessingOperationsConfiguration.OperationFactory.Create(settings.OperationKey);
            }
            if (ProcessingOperationsConfiguration.OperationFactoryFunc != null)
            {
                return ProcessingOperationsConfiguration.OperationFactoryFunc(settings.OperationKey);
            }
            throw new InvalidOperationException(
                    "Operation factory should be configured through the ProcessingOperationsConfiguration");
        }

        private IProcessingManager CreateRepeatingOperationProcessingManager(
            IOperation operation,
            IRepeatingOperationProcessingManagerSettings settings)
        {
            return new RepeatingOperationProcessingManager(
                operation,
                new RepeatingOperationSettings
                {
                    EverySeconds = settings.EverySeconds
                });
        }
    }
}
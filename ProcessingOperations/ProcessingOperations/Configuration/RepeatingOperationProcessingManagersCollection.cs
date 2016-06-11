using System.Configuration;

namespace ProcessingOperations.Configuration
{
    /// <summary>
    /// Represents the collection of the processing manager in configuration file.
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationElementCollection" />
    [ConfigurationCollection(typeof(ProcessingManagersSection))]
    internal class RepeatingOperationProcessingManagersCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType =>
            ConfigurationElementCollectionType.BasicMapAlternate;

        /// <summary>
        /// Gets the name used to identify this collection of elements in the configuration file when overridden in a derived class.
        /// </summary>
        protected override string ElementName => "manager";

        /// <summary>
        /// Indicates whether the specified <see cref="T:System.Configuration.ConfigurationElement" /> exists in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <param name="elementName">The name of the element to verify.</param>
        /// <returns>
        /// true if the element exists in the collection; otherwise, false. The default is false.
        /// </returns>
        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(ElementName);
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A newly created <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new RepeatingOperationProcessingManagerSettings();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RepeatingOperationProcessingManagerSettings)element).Name;
        }
    }
}
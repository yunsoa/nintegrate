using System;
using System.Runtime.Serialization;
using System.Globalization;

namespace NIntegrate.ServiceModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ChannelFactoryCreationException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelFactoryCreationException"/> class.
        /// </summary>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <param name="innerException">The inner exception.</param>
        public ChannelFactoryCreationException(Type serviceContractType, Exception innerException)
            : base(CreateMessage(serviceContractType), innerException)
        {
            ServiceContractType = serviceContractType;
        }

        private ChannelFactoryCreationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of the service contract.
        /// </summary>
        /// <value>The type of the service contract.</value>
        public Type ServiceContractType { get; private set; }

        #endregion

        #region Non-Public Methods

        private static string CreateMessage(Type serviceContractType)
        {
            return string.Format(CultureInfo.InvariantCulture, SR.FAILED_TO_CREATE_CHANNELFACTORY_FOR_SERVICECONTRACT, serviceContractType);
        }

        #endregion
    }
}

using System;
using System.Runtime.Serialization;
using System.Globalization;

namespace NIntegrate.ServiceModel
{
    [Serializable]
    public sealed class ChannelFactoryCreationException : Exception
    {
        #region Constructors

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

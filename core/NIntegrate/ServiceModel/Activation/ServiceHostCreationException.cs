using System;
using System.Runtime.Serialization;
using System.Text;

namespace NIntegrate.ServiceModel.Activation
{
    [Serializable]
    public sealed class ServiceHostCreationException : Exception
    {
        #region Constructors

        public ServiceHostCreationException(Type serviceType, Uri[] baseAddresses, Exception innerException)
            : base(CreateMessage(serviceType, baseAddresses), innerException)
        {
        }

        public ServiceHostCreationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Non-Public Methods

        private static string CreateMessage(Type serviceType, Uri[] baseAddresses)
        {
            var sb = new StringBuilder();
            sb.Append(serviceType.ToString());
            if (baseAddresses != null)
            {
                foreach (var uri in baseAddresses)
                {
                    sb.Append(',');
                    sb.Append(uri.ToString());
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}
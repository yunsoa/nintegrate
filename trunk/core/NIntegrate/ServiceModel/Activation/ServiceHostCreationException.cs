using System;
using System.Runtime.Serialization;
using System.Text;

namespace NIntegrate.ServiceModel.Activation
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ServiceHostCreationException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHostCreationException"/> class.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        /// <param name="innerException">The inner exception.</param>
        public ServiceHostCreationException(Type serviceType, Uri[] baseAddresses, Exception innerException)
            : base(CreateMessage(serviceType, baseAddresses), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHostCreationException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
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
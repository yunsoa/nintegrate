using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Configuration;
using NIntegrate.ServiceModel.Description;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NIntegrateLoggingElement : BehaviorExtensionElement
    {
        /// <summary>
        /// Gets the type of behavior.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Type"/>.</returns>
        public override Type BehaviorType
        {
            get { return typeof(NIntegrateLoggingBehavior); }
        }

        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>The behavior extension.</returns>
        protected override object CreateBehavior()
        {
            return new NIntegrateLoggingBehavior();
        }
    }
}

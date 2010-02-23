//===============================================================================
// Microsoft Aspiring Software Architects Program
// E-Appointments - Case Study Implementation
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace EAppointments.BMS.ServiceContracts
{
    /// <summary>
    /// Fault contract that will send all the unhandled exception to the client.
    /// </summary>
    [DataContract]
    public partial class SystemFault
    {
        private Guid IdField;

        /// <summary>
        /// Gets the system defined message.
        /// </summary>
        /// <value>The message.</value>
        [DataMember(IsRequired = false, Name = "Message", Order = 0)]
        public String Message
        {
            // TODO: Move to a resource file.
            get { return "An error has occurred while consuming this service. Please contact your system administrator for more information."; }
                //Properties.Resources.SystemFaultMessage; }
            set { }
        }

        /// <summary>
        /// Gets or sets the error id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember(IsRequired = false, Name = "Id", Order = 1)]
        public Guid Id
        {
            get { return IdField; }
            set { IdField = value; }
        }
    }
}

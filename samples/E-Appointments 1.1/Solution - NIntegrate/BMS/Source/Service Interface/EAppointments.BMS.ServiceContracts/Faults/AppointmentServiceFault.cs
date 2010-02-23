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
    [DataContract]
    public class AppointmentServiceFault
    {
        private string message;
        private Guid id;

        /// <summary>
        /// Gets or sets the fault message.
        /// </summary>
        /// <value>The fault message.</value>
        [DataMember(IsRequired = false, Name = "Message", Order = 0)]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// Gets or sets the fault Id.
        /// </summary>
        /// <value>The fault id.</value>
        [DataMember(IsRequired = false, Name = "Id", Order = 1)]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}

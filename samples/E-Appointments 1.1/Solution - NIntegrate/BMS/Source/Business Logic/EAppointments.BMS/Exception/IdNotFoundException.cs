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
using System.Globalization;
using System.Runtime.Serialization;

namespace EAppointments.BMS
{
    [Serializable]
    public class IdNotFoundException : BusinessException
    {
        public IdNotFoundException() : base() { }

        public IdNotFoundException(string message) : base(message) { }

        public IdNotFoundException(string message, Exception ex) : base(message, ex) { }
        
        public IdNotFoundException(String entityName, Object id)
            : base(String.Format(CultureInfo.CurrentCulture, Properties.Resource.Id_Not_Found_Message, entityName, id)) { }

        protected IdNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}

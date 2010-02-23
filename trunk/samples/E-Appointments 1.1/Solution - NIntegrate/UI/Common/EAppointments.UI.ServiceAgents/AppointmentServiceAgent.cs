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
using EAppointments.UI.ServiceAgents.Interfaces;
using EAppointments.UI.ServiceAgents.AppointmentService;
using NIntegrateExtensions;
using NIntegrate.ServiceModel;
using NIntegrate.Data;

namespace EAppointments.UI.ServiceAgents
{
    public class AppointmentServiceAgent : IAppointmentServiceAgent
    {
        private WcfChannelWrapper<IAppointmentService> GetProxy()
        {
            return EAWcfClientChannelFactory.CreateWcfChannel<IAppointmentService>();
        }
      
        #region IAppointmentServiceAgent Members

        public Appointment[] Query(QueryCriteria criteria)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.Query(criteria).ToArray();
            }
        }

        public Appointment[] Find(AppointmentSearchCriteria criteria)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.Find(criteria).ToArray();
            }
        }

        public Appointment Save(Appointment appointment)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.Save(appointment);
            }
        }

        public void Book(int ubrn, Guid slotId)
        {
            using (var proxy = GetProxy())
            {
                proxy.Channel.Book(ubrn, slotId);
            }
        }

        public void Cancel(int ubrn, string cancellationReason)
        {
            using (var proxy = GetProxy())
            {
                proxy.Channel.Cancel(ubrn, cancellationReason);
            }
        }

        public void Rebook(int ubrn, Guid slotId)
        {
            using (var proxy = GetProxy())
            {
               proxy.Channel.Rebook(ubrn, slotId);
            }
        }

        public void Approve(int ubrn)
        {
            using (var proxy = GetProxy())
            {
                proxy.Channel.Approve(ubrn);
            }
        }

        public void Reject(int ubrn)
        {
            using (var proxy = GetProxy())
            {
                proxy.Channel.Reject(ubrn);
            }
        }

        public void Delete(int ubrn)
        {
            using (var proxy = GetProxy())
            {
                proxy.Channel.Delete(ubrn);
            }
        }

        #endregion
    }
}

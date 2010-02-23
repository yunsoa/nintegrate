using System;
using System.Collections.Generic;
using System.Text;
using EAppointments.UI.ServiceAgents.AppointmentService;


namespace EAppointments.UI.Modules.Appointments.Views
{
    public interface IAppointmentListView
    {
        AppointmentSearchCriteria GetSearchCriteria();
        Appointment[] Appointments { set;}
    }
}

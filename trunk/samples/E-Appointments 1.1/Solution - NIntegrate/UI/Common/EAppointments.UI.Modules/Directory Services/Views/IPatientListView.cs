using System;
using System.Collections.Generic;
using System.Text;
using EAppointments.UI.ServiceAgents.DirectoryService;

namespace EAppointments.UI.Modules.DirectoryServices
{
    public interface IPatientListView
    {
        PatientSearchCriteria GetSearchCriteria();
        Patient[] Patients { set; }
    }
} 

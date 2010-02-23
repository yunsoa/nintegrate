using System;
using System.Collections.Generic;
using System.Text;
using EAppointments.UI.ServiceAgents.DirectoryService;

namespace EAppointments.UI.Modules.Directory_Services.Views
{
    public interface IPatientDetailsView
    {
       Guid GetPatientId();
       Patient CurrentPatient { set; }
    }
}

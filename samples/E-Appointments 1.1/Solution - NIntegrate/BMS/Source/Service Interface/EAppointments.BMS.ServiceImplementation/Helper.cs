using System;
using System.Collections.Generic;
using System.Text;
using EAppointments.BMS;

namespace EAppointments.BMS.ServiceImplementation
{
    public class Helper
    {
        public static Application GetApplication()
        {
            Session session = new Session();
            // HACK: Only for this release (until security is implemented)
            session.Login("jdoe","password");
            return session.Application;
        }
    }
}

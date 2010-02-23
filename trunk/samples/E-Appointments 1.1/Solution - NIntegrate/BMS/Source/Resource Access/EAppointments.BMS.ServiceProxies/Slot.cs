using System;
using System.Collections.Generic;
using System.Text;

namespace EAppointments.BMS.ServiceProxies
{
    public class Slot
    {

    }

    public class SlotSearchCriteria
    {
        public Guid ProviderId;
        public Guid SlotId;

        public SlotSearchCriteria(Guid providerId, Guid slotId)
        {
            this.ProviderId = providerId;
            this.SlotId = slotId;
        }
    }
}

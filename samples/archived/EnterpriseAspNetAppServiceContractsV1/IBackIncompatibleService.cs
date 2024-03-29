﻿using System.Runtime.InteropServices;
using System.ServiceModel;

namespace EnterpriseAspNetAppServiceContracts
{
    [ServiceContract(Namespace = "http://nintegrate.com/Samples")]
    [ComVisible(true)]
    [Guid("B3ABD0DA-F366-4225-B6F4-4C53D5090F5F")]
    public interface IBackIncompatibleService
    {
        [OperationContract]
        BackIncompatibleResult GetIncompatibleResult();
    }
}

﻿using System.Runtime.InteropServices;
using System.ServiceModel;

namespace EnterpriseAspNetAppServiceContracts
{
    [ServiceContract(Namespace = "http://nintegrate.com/Samples")]
    [ComVisible(true)]
    [Guid("11912B4A-31E1-4a5b-A8B5-CE7E92E69B2E")]
    public interface IBackIncompatibleServiceV2
    {
        [OperationContract]
        BackIncompatibleResultV2 GetIncompatibleResultV2();
    }
}

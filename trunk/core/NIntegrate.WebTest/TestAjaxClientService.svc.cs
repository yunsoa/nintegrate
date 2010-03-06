using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

namespace NIntegrate.WebTest
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class TestAjaxClientService
    {
        [OperationContract]
        [WebGet]
        public string Hello()
        {
            return "Hello Teddy";
        }
    }
}

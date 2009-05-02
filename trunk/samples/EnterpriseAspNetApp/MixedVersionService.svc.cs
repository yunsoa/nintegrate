using EnterpriseAspNetAppServiceContractsV1;
using EnterpriseAspNetAppServiceContractsV2;

namespace EnterpriseAspNetApp
{
    public class MixedVersionService : EnterpriseAspNetAppServiceContractsV2.IBackCompatibleService, IBackIncompatibleService, IBackIncompatibleServiceV2
    {
        #region IBackCompatibleServiceV2 Members

        public BackCompatibleResultV2 GetCompatibleResult()
        {
            return new BackCompatibleResultV2 { Value = "CompatibleResult Value", Value2 = 2 };
        }

        public string SayHello()
        {
            return "hello";
        }

        #endregion

        #region IBackIncompatibleService Members

        public BackIncompatibleResult GetIncompatibleResult()
        {
            return new BackIncompatibleResult { Value = "IncompatibleResult Value" };
        }

        #endregion

        #region IBackIncompatibleServiceV2 Members

        public BackIncompatibleResultV2 GetIncompatibleResultV2()
        {
            return new BackIncompatibleResultV2 { Value = 3 };
        }

        #endregion
    }

}

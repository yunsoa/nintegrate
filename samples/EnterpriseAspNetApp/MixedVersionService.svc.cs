using EnterpriseAspNetAppServiceContracts;

namespace EnterpriseAspNetApp
{
    public class MixedVersionService : IBackCompatibleService, IBackIncompatibleServiceV2
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

        #region IBackIncompatibleServiceV2 Members

        public BackIncompatibleResultV2 GetIncompatibleResultV2()
        {
            return new BackIncompatibleResultV2 { Value = 3 };
        }

        #endregion
    }
}

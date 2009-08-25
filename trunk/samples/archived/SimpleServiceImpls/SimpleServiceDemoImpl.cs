using SimpleServiceContracts;

namespace SimpleServiceImpls
{
    public class SimpleServiceDemoImpl : ISimpleServiceDemo
    {
        #region ISimpleServiceDemo Members

        public string SayHellod()
        {
            return "Hello Friend!";
        }

        #endregion
    }
}

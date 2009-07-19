using System.ServiceModel.Channels;

namespace NIntegrate.Test.TestClasses
{
    public class TestBindingElement : BindingElement
    {
        public override BindingElement Clone()
        {
            return this;
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(MessageVersion))
            {
                return (T)(object)MessageVersion.Soap12WSAddressing10;
            }

            return default(T);
        }
    }
}

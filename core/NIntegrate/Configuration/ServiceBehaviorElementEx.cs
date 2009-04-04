using System.Collections.Generic;
using System.ServiceModel.Configuration;
using System.Reflection;

namespace NIntegrate.Configuration
{
    public class ServiceBehaviorElementEx : ServiceBehaviorElement
    {
        private static readonly PropertyInfo itemsProperty = typeof(ServiceModelExtensionCollectionElement<>).MakeGenericType(typeof(BehaviorExtensionElement))
            .GetProperty("Items", BindingFlags.NonPublic | BindingFlags.Instance);

        public override void Add(BehaviorExtensionElement element)
        {
            if (element == null)
                return;

            foreach (var existingExtension in this)
            {
                if (existingExtension.BehaviorType == element.BehaviorType)
                    return;
            }

            var list = (IList<BehaviorExtensionElement>)itemsProperty.GetValue(this, null);
            list.Add(element);
        }
    }
}

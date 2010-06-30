using System;
using ScriptFX;

namespace NIntegrate.Scripts.Plugins
{
    [Imported]
    [IgnoreNamespace]
    public class JTemplatePlugin : JQuerySharp.jQuery
    {
        public JTemplatePlugin SetParam(string name, object value) { return null; }
        public JTemplatePlugin SetTemplate(string template) { return null; }
        public JTemplatePlugin SetTemplateUrl(string templateUrl) { return null; }
        public JTemplatePlugin SetTemplateElement(string elementName) { return null; }
        public void ProcessTemplate(object data) { }
    }
}

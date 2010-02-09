using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.ServiceModel.Configuration;
using System.Drawing;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace NIntegrate.CodeGenerator
{
    public partial class WcfConfigurationValidator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (KeyValuePair<string, BindingTypeDescription> item in BindingTypeRegistry.Instance)
                {
                    ddlBindingTypes.Items.Add(item.Key);
                }
            }
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbXml.Text))
            {
                lblResults.Text = string.Empty;
            }
            else
            {
                var typeName = ddlConfigXml.SelectedItem.Text;
                var xml = tbXml.Text.Trim();
                tbXml.Text = xml;

                try
                {
                    var type = typeof(BindingXml).Assembly.GetType("NIntegrate.ServiceModel.Configuration." + typeName);
                    var obj = Activator.CreateInstance(type, typeName == "BindingXml" ? new object[] { ddlBindingTypes.SelectedValue, xml } : new object[] { xml });

                    if (typeName == "BindingXml")
                    {
                        (obj as BindingXml).CreateBinding();
                    }
                    else if (typeName == "EndpointBehaviorXml")
                    {
                        var contract = new ContractDescription("test");
                        var endpoint = new ServiceEndpoint(contract);
                        (obj as EndpointBehaviorXml).ApplyEndpointBehaviorConfiguration(endpoint);
                    }
                    else if (typeName == "HeadersXml")
                    {
                        (obj as HeadersXml).CreateAddressHeaders();
                    }
                    else if (typeName == "HostXml")
                    {
                        var servcieHost = new ServiceHost(typeof(TestService));
                        (obj as HostXml).ApplyHostTimeoutsConfiguration(servcieHost);
                    }
                    else if (typeName == "IdentityXml")
                    {
                        (obj as IdentityXml).CreateEndpointIdentity();
                    }
                    else if (typeName == "MetadataXml")
                    {
                        var metadataSet = new MetadataSet();
                        var importer = new WsdlImporter(metadataSet);
                        (obj as MetadataXml).ApplyPolicyImportersConfiguration(importer);
                        (obj as MetadataXml).ApplyWsdlImportersConfiguration(importer);
                    }
                    else if (typeName == "ServiceBehaviorXml")
                    {
                        var servcieHost = new ServiceHost(typeof(TestService));
                        (obj as ServiceBehaviorXml).ApplyServiceBehaviorConfiguration(servcieHost);
                    }

                    lblResults.ForeColor = Color.Green;
                    lblResults.Text = "OK";
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    lblResults.ForeColor = Color.Red;
                    lblResults.Text = ex.Message.Replace("\r\n", "<br />");
                }
            }
        }
    }

    #region Test Classes

    [ServiceContract]
    public class TestService
    {
        [OperationContract]
        public void Hello() { }
    }

    #endregion
}

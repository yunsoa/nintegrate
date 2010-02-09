<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WcfConfigurationValidator.aspx.cs"
    Inherits="NIntegrate.CodeGenerator.WcfConfigurationValidator" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCF Configuration Validator</title>
    <script type="text/javascript" language="javascript">
        function onConfigXmlChanged() {
            var ddl = document.getElementById('ddlConfigXml');
            document.getElementById('ddlBindingTypes').style.display = (ddl.selectedIndex == 0 ? 'inline' : 'none');
            document.getElementById('helptext').innerHTML = '<b>Help:</b> ' + ddl.options.item(ddl.selectedIndex).text + ' maps to the &lt;' + ddl.options.item(ddl.selectedIndex).value + '&gt; configuration element';
        }
    </script>
</head>
<body onload="onConfigXmlChanged()">
    <form id="form1" runat="server">
    <a href="./">Home</a><br />
    <br />
    <asp:DropDownList ID="ddlConfigXml" runat="server" onchange="onConfigXmlChanged()">
        <asp:ListItem Text="BindingXml" Value="binding"></asp:ListItem>
        <asp:ListItem Text="EndpointBehaviorXml" Value="behavior"></asp:ListItem>
        <asp:ListItem Text="HeadersXml" Value="headers"></asp:ListItem>
        <asp:ListItem Text="HostXml" Value="host"></asp:ListItem>
        <asp:ListItem Text="IdentityXml" Value="identity"></asp:ListItem>
        <asp:ListItem Text="MetadataXml" Value="metadata"></asp:ListItem>
        <asp:ListItem Text="ServiceBehaviorXml" Value="behavior&lt;!----&gt;"></asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlBindingTypes" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnValidate" runat="server" Text="Validate" 
        onclick="btnValidate_Click" /><br />
    <br />
    <div id="helptext"></div>
    <asp:TextBox ID="tbXml" runat="server" TextMode="MultiLine" Width="750" Height="300"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblResults" runat="server"></asp:Label>
    </form>
</body>
</html>

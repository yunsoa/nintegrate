<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EnterpriseAspNetApp.Default" %>

<%@ Register assembly="NIntegrate.Web" namespace="NIntegrate.Web" tagprefix="ni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" DataSourceID="QueryDataSource1">
        </asp:GridView>
        <ni:QueryDataSource ID="QueryDataSource1" runat="server" 
            onselecting="QueryDataSource1_Selecting" 
            onselected="QueryDataSource1_Selected">
        </ni:QueryDataSource>
    
    </div>
    </form>
</body>
</html>

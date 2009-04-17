<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SimpleServiceConsumer._Default" %>

<%@ Register assembly="NIntegrate.Web" namespace="NIntegrate.Web" tagprefix="ni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server"
            DataSourceID="QueryDataSource1" AllowSorting="true"
            DataKeyNames="Binding_id" AutoGenerateColumns="false"
            AutoGenerateEditButton="true">
            <Columns>
                <asp:BoundField HeaderText="Binding_id" DataField="Binding_id" ReadOnly="true" SortExpression="Binding_id" />
                <asp:TemplateField SortExpression="BindingType_id" HeaderText="BindingType">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlBindingType_id" runat="server" SelectedValue='<%#Bind("BindingType_id") %>' DataTextField="BindingTypeFriendlyName" DataValueField="BindingType_id" DataSourceID="QueryDataSource2"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlBindingType_id" Enabled="false" runat="server" SelectedValue='<%#Bind("BindingType_id") %>' DataTextField="BindingTypeFriendlyName" DataValueField="BindingType_id" DataSourceID="QueryDataSource2"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="BindingName" DataField="BindingName" SortExpression="BindingName" />
                <asp:BoundField HeaderText="BindingXML" DataField="BindingXML" SortExpression="BindingXML" />
                <asp:CheckBoxField HeaderText="MexBindingEnabled" DataField="MexBindingEnabled" SortExpression="MexBindingEnabled" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="GridView2" runat="server" DataSourceID="QueryDataSource3" AllowSorting="true"
            DataKeyNames="Endpoint_id" AutoGenerateColumns="false"
            AutoGenerateEditButton="true">
            <Columns>
                <asp:BoundField DataField="Binding_id" SortExpression="Binding_id" ReadOnly="true" HeaderText="Binding_id" />
                <asp:BoundField DataField="EndpointAddress" SortExpression="EndpointAddress" HeaderText="EndpointAddress" />
            </Columns>
        </asp:GridView>
        <ni:QueryDataSource ID="QueryDataSource1" runat="server">
        </ni:QueryDataSource>
        <ni:QueryDataSource ID="QueryDataSource2" runat="server">
        </ni:QueryDataSource>
        <ni:QueryDataSource ID="QueryDataSource3" runat="server">
        </ni:QueryDataSource>
    </div>
    </form>
</body>
</html>

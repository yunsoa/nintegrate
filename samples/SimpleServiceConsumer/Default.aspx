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
        <asp:GridView ID="gvService" runat="server" DataSourceID="dsService"
            DataKeyNames="Service_id" AutoGenerateColumns="false"
            AutoGenerateEditButton="true" AllowSorting="true">
            <Columns>
                <asp:BoundField DataField="Service_id" ReadOnly="true" HeaderText="Service_id" SortExpression="Service_id" />
                <asp:BoundField DataField="ServiceName" HeaderText="ServiceName" SortExpression="ServiceName" />
                <asp:BoundField DataField="HostXML" HeaderText="HostXML" />
            </Columns>
        </asp:GridView>    
        <br />
        <asp:GridView ID="gvBinding" runat="server"
            DataSourceID="dsBinding" AllowSorting="true"
            DataKeyNames="Binding_id" AutoGenerateColumns="false"
            AutoGenerateEditButton="true">
            <Columns>
                <asp:BoundField HeaderText="BindingName" DataField="BindingName" SortExpression="BindingName" />
                <asp:TemplateField HeaderText="BindingType" SortExpression="BindingType_id">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlBindingType_id" runat="server" SelectedValue='<%#Bind("BindingType_id") %>' DataTextField="BindingTypeFriendlyName" DataValueField="BindingType_id" DataSourceID="dsBindingType"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlBindingType_id" Enabled="false" runat="server" SelectedValue='<%#Bind("BindingType_id") %>' DataTextField="BindingTypeFriendlyName" DataValueField="BindingType_id" DataSourceID="dsBindingType"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="BindingXML" DataField="BindingXML" />
                <asp:CheckBoxField HeaderText="MexBindingEnabled" DataField="MexBindingEnabled" SortExpression="MexBindingEnabled" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="gvEndpoint" runat="server" DataSourceID="dsEndpoint"
            DataKeyNames="Endpoint_id" AutoGenerateColumns="false"
            AutoGenerateEditButton="true" AllowSorting="true" 
            onrowcommand="gvEndpoint_RowCommand">
            <Columns>
                <asp:BoundField DataField="EndpointName" ReadOnly="true" HeaderText="EndpointName" SortExpression="EndpointName" />
                <asp:BoundField DataField="EndpointAddress" HeaderText="EndpointAddress" SortExpression="EndpointAddress" />
                <asp:TemplateField HeaderText="BindingType" SortExpression="BindingType_id">
                    <ItemTemplate>
                        <asp:Button ID="btnActive" runat="server" CommandName="ChangeActive" CommandArgument='<%# (int)Eval("Endpoint_id") + "|" + IsEndpointActive((int)Eval("Endpoint_id")) %>' Text='<%# (IsEndpointActive((int)Eval("Endpoint_id")) ? "Inactive" : "Active") %>' />
                    </ItemTemplate>
                </asp:TemplateField>                
            </Columns>
        </asp:GridView>
        <ni:QueryDataSource ID="dsService" runat="server">
        </ni:QueryDataSource>
        <ni:QueryDataSource ID="dsBinding" runat="server">
        </ni:QueryDataSource>
        <ni:QueryDataSource ID="dsBindingType" runat="server">
        </ni:QueryDataSource>
        <ni:QueryDataSource ID="dsEndpoint" runat="server">
        </ni:QueryDataSource>
    </div>
    <br />
    <div><asp:Literal ID="litSayHello" runat="server"></asp:Literal></div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DistributedEnterpriseIntegration.ConfigurationWeb._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvFarms" runat="server" DataSourceID="dsFarms" AutoGenerateColumns="false" AllowSorting="true">
            <Columns>
                <asp:BoundField DataField="FarmAddress" HeaderText="FarmAddress" SortExpression="FarmAddress" />
                <asp:BoundField DataField="LoadBalancePath" HeaderText="LoadBalancePath" SortExpression="LoadBalancePath" />
                <asp:TemplateField HeaderText="Servers">
                    <ItemTemplate>
                        <asp:Repeater runat="server" DataSource='<%# GetServers((int)Eval("Farm_id")) %>'>
                            <ItemTemplate>
                                <%# Eval("ServerName") %><br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Services">
                    <ItemTemplate>
                        <asp:Repeater runat="server" DataSource='<%# GetServices((int)Eval("Farm_id")) %>'>
                            <ItemTemplate>
                                <%# Eval("ServiceName") %><br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Accessible">
                    <ItemTemplate>
                        <asp:Repeater runat="server" DataSource='<%# GetVisibility((int)Eval("Farm_id")) %>'>
                            <ItemTemplate>
                                from <%# Eval("FarmAddress") %>[<%# Eval("LoadBalancePath")%>] by <%# Eval("BindingTypeCode") %><br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <ni:QueryDataSource ID="dsFarms" runat="server" QueryTableType="DistributedEnterpriseIntegration.ConfigurationWeb.QueryObjects.Farm"></ni:QueryDataSource>
    </div>
    </form>
</body>
</html>

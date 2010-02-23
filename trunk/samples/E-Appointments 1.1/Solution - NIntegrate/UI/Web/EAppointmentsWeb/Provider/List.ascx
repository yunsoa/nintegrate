<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Provider_List" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<pp:ObjectContainerDataSource DataObjectTypeName="EAppointments.UI.ServiceAgents.ProviderService.Provider"
			ID="ProviderDataSource" runat="server" />
			
<asp:GridView runat="server" ID="ProviderGrid" DataSourceID="ProviderDataSource" AutoGenerateColumns="False"
    AllowPaging="True" AllowSorting="True"  DataKeyNames="Id" EmptyDataText="Not Found">    
    <Columns>    
        <asp:CommandField ShowSelectButton="True" />
        <asp:TemplateField HeaderText="Distance (Kms)">
            <ItemTemplate>
                <%# Eval("Proximity", "{0:0.000}") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Name" DataField="Name" HtmlEncode="false" />
        <asp:BoundField HeaderText="Organization" DataField="Organization"/>
    </Columns>   
    <EmptyDataTemplate>
        
    
    </EmptyDataTemplate>
</asp:GridView>
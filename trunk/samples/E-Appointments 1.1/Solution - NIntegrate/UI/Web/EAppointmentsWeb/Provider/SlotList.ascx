<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SlotList.ascx.cs" Inherits="Provider_SlotList" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<pp:ObjectContainerDataSource DataObjectTypeName="EAppointments.UI.ServiceAgents.ProviderService.Slot"
    ID="SlotDataSource" runat="server" />
<asp:GridView runat="server" ID="SlotGrid" DataSourceID="SlotDataSource" AutoGenerateColumns="False"
    AllowPaging="True" AllowSorting="True"  DataKeyNames="Id">
    <Columns>
        <asp:CommandField ShowSelectButton="True" />        
        <asp:TemplateField HeaderText="Slot Date">
            <ItemTemplate>
                <%# Eval("StartDateTime", "{0:d}") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Start Time">
            <ItemTemplate>
                <%# Eval("StartDateTime", "{0:hh:mm tt}") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="End Time">
            <ItemTemplate>
                <%# Eval("EndDateTime", "{0:hh:mm tt}") %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

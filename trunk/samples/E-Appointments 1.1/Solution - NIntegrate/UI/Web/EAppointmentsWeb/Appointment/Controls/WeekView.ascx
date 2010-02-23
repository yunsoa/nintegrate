<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WeekView.ascx.cs" Inherits="Appointment_WeekView" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<pp:ObjectContainerDataSource DataObjectTypeName="System.DateTime"	ID="AppointmentDataSource" runat="server" />

<asp:Repeater ID="WeekRepeater" runat="server" DataSourceID="AppointmentDataSource" OnItemDataBound="WeekRepeater_ItemDataBound">
    <HeaderTemplate>
        <div class='weekcontainer'>
    </HeaderTemplate>
    <ItemTemplate>
        <div class='weekheader'>
            <%# DataBinder.Eval(Container.DataItem, "Date", "{0:dddd, dd MMMM yyyy}") %>
        </div>
        <div class='weekcontent'>
            <asp:Label ID="lblNoAppointments" runat="Server" Visible="false">
                  <p><p><p><p>There are no appointments for this day
            </asp:Label>  
            <asp:Panel ID="pnlAppointments" runat="server"  Visible="false"/>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>

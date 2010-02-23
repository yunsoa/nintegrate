<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Patient_List" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<pp:ObjectContainerDataSource DataObjectTypeName="EAppointments.UI.ServiceAgents.DirectoryService.Patient"
			ID="PatientDataSource" runat="server" />
			
<asp:GridView runat="server" ID="PatientGrid" DataSourceID="PatientDataSource" AutoGenerateColumns="False"
    AllowPaging="True" AllowSorting="True"  DataKeyNames="Id">    
    <Columns>    
        <asp:CommandField ShowSelectButton="True" />      
        <asp:BoundField HeaderText="First Name" DataField="FirstName" HtmlEncode="false" />
        <asp:BoundField HeaderText="Last Name" DataField="LastName" HtmlEncode="false"/>
        <asp:BoundField HeaderText="Email Address" DataField="Email" HtmlEncode="false"/>
        <asp:BoundField HeaderText="City" DataField="City"/>
        <asp:BoundField HeaderText="State" DataField="State"/>
    </Columns>   
</asp:GridView>
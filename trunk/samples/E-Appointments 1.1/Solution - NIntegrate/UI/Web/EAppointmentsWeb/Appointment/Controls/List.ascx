<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Appointment_List" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<pp:ObjectContainerDataSource DataObjectTypeName="EAppointments.UI.ServiceAgents.AppointmentService.Appointment"
			ID="AppointmentDataSource" runat="server" />
			
<asp:GridView runat="server" ID="AppointmentGrid" DataSourceID="AppointmentDataSource" AutoGenerateColumns="False"
    AllowPaging="True" AllowSorting="True"  DataKeyNames="Ubrn" OnSelectedIndexChanged="AppointmentGrid_SelectedIndexChanged">    
    <Columns>    
        <asp:CommandField ShowSelectButton="True" />
        <asp:ImageField HeaderText="Status" DataImageUrlFormatString="~/App_Themes/Default/Images/{0}.bmp" DataImageUrlField="Status"
           DataAlternateTextFormatString="{0}" DataAlternateTextField="Status" ItemStyle-HorizontalAlign="Center"/>
        <asp:BoundField HeaderText="Status" DataField="Status" />
        <asp:BoundField HeaderText="UBRN" DataField="Ubrn" />
        <asp:TemplateField HeaderText="Date">
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
        <asp:TemplateField HeaderText="Patient Name">
            <ItemTemplate>
                <%# String.Format("{0} {1}", Eval("Patient.FirstName"), Eval("Patient.LastName")) %>
            </ItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField HeaderText="Referrer Name">
            <ItemTemplate>
                <%# String.Format("{0} {1}", Eval("Referrer.FirstName") , Eval("Referrer.LastName"))%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>   
</asp:GridView>
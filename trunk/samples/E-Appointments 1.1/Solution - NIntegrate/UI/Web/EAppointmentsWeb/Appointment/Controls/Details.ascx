<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Details.ascx.cs" Inherits="Appointment_Details" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<pp:ObjectContainerDataSource DataObjectTypeName="EAppointments.UI.ServiceAgents.AppointmentService.Appointment"
			ID="AppointmentDataSource" runat="server" />

<asp:FormView runat="server" ID="AppointmentDetails" DataSourceID="AppointmentDataSource" HorizontalAlign="Center">
    <ItemTemplate>
         <div class="editor_panel clearfix">
                <table cellspacing="0" cellpadding="6" border="0" class="editor">
                    <tbody>
                        <tr class="row">
                            <td class="label">
                                UBRN :
                            </td>
                            <td>
                                <%# Eval("Ubrn") %>
                            </td>
                        </tr>
                         <tr class="row">
                            <td class="label">
                                Status :
                            </td>
                            <td>
                               <%# Eval("Status") %>
                               &nbsp;
                               <asp:Image AlternateText="Patient Picture" 
                               ImageUrl='<%# String.Format("~/App_Themes/Default/Images/{0}.bmp", Eval("Status")) %>'
                                      runat="server" ID="imgPatient" ImageAlign="AbsMiddle"/>                               
                            </td>
                        </tr>    
                        <tr class="row">
                            <td class="label">
                                Patient Name :
                            </td>
                            <td>
                                <%# String.Format("{0} {1}", Eval("Patient.FirstName"), Eval("Patient.LastName")) %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                                Referring Clinician :
                            </td>
                            <td>
                                <%# String.Format("{0} {1}", Eval("Referrer.FirstName"), Eval("Referrer.LastName")) %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                                Service Provider :
                            </td>
                            <td>
                               <%# Eval("Provider.Name") %>
                            </td>
                        </tr>
                        <tr class="row" id="TRAppointmentDate" runat="server">
                            <td class="label">
                                Appointment Date :
                            </td>
                            <td>
                                <%# Eval("StartDateTime", "{0:d}") %>
                            </td>
                        </tr>
                        <tr class="row" id="TRAppointmentTime" runat="server">
                            <td class="label">
                                Appointment Time :
                            </td>
                            <td>
                                <%# String.Format("{0:hh:mm tt} - {1:hh:mm tt}", Eval("StartDateTime"), Eval("EndDateTime")) %> 
                            </td>
                        </tr>   
                        <tr class="row" id="TRCancellationDate" runat="server" visible="false">
                            <td class="label">
                                Cancellation Date :
                            </td>
                            <td>
                                 <%# Eval("CancellationDateTime") %>
                            </td>
                        </tr>
                        <tr class="row" id="TRCancellationReason" runat="server" visible="false">
                            <td class="label">
                                Cancellation Reason :
                            </td>                            
                            <td>
                                 <%# Eval("CancellationReason") %>
                            </td>
                        </tr>                                                 
                        <tr class="row">
                            <td class="label">
                                Created :
                            </td>
                            <td>
                                <%# Eval("CreatedDateTime") %>
                            </td>
                        </tr>     
                        <tr class="row">
                            <td class="label">
                                Last Updated :
                            </td>
                            <td>
                               <%# Eval("UpdatedDateTime") %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                                Clinician Comments :
                            </td>                            
                        </tr>
                        <tr class="row">
                            <td colspan="2" align="right">
                                <asp:TextBox ID="Comments" runat="server" TextMode="MultiLine" Columns="20" Rows="5" Text='<%# Eval("Comments") %>' ReadOnly="true"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
    </ItemTemplate>
</asp:FormView>

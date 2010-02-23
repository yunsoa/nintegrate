<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Details.ascx.cs" Inherits="Patient_Details" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<pp:ObjectContainerDataSource DataObjectTypeName="EAppointments.UI.ServiceAgents.DirectoryService.Patient"
    ID="PatientDataSource" runat="server" />
<asp:FormView runat="server" ID="PatientDetails" DataSourceID="PatientDataSource"
    HorizontalAlign="Center">
    <ItemTemplate>
        <div class="editor_panel clearfix">
            <table cellspacing="4" cellpadding="4" border="0" class="editor">
                <tbody>
                    <tr class="row">
                        <td>
                            <div style="float: left">
                                    <asp:Image AlternateText="Patient Picture" ImageUrl='<%# SetPicturePath(Eval("Id"))%>'
                                        runat="server" ID="imgPatient" Width="95" Height="95" />
                                </div>
                        </td>
                        <td>
                            <table border="0" cellpadding="4">
                                <tbody>
                                    <tr>
                                        <td class="label"> First Name </td>
                                    </tr>                                   
                                    <tr >
                                        <td align="center"> <%# Eval("FirstName") %></td>
                                    </tr>
                                     <tr>    
                                        <td class="label"> Last Name </td>
                                    </tr>
                                    <tr>
                                        <td align="center"><%# Eval("LastName") %></td>
                                    </tr>
                                </tbody>
                        
                             </table>                           
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
                    <tr class="row" style="clear:both;">
                        <td class="label">
                            Email Address :
                        </td>
                        <td>
                            <%# Eval("Email") %>
                        </td>
                    </tr>
                    <tr class="row">
                        <td class="label">
                            City :
                        </td>
                        <td>
                            <%# Eval("City") %>
                        </td>
                    </tr>
                    <tr class="row">
                        <td class="label">
                            State :
                        </td>
                        <td>
                            <%# Eval("State") %>
                        </td>
                    </tr>
                    <tr class="row">
                        <td class="label">
                            Zip Code :
                        </td>
                        <td>
                            <%# Eval("ZipCode") %>
                        </td>
                    </tr>
                    <tr class="row">
                        <td class="label">
                            Date of Birth :
                        </td>
                        <td>
                            <%# Eval("DateOfBirth", "{0:d}") %>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="patientdetails" style="padding: 5px">
        </div>
    </ItemTemplate>
</asp:FormView>

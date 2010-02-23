<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SlotDetails.ascx.cs" Inherits="Provider_SlotDetails" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<pp:ObjectContainerDataSource DataObjectTypeName="EAppointments.UI.ServiceAgents.ProviderService.Slot"
			ID="SlotDataSource" runat="server" />

<asp:FormView runat="server" ID="SlotDetails" DataSourceID="SlotDataSource" HorizontalAlign="Center">
    <ItemTemplate>
         <div class="editor_panel clearfix">
                <table cellspacing="4" cellpadding="4" border="0" class="editor">
                    <tbody>
                        <tr class="row">
                            <td class="label">
                                Provider Name :
                            </td>
                            <td>
                                <%# Eval("Provider.Name") %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                                Specialty :
                            </td>
                            <td>
                                <%# Eval("ClinicType.Specialty.Name") %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                               Sub-Specialty :
                            </td>
                            <td>
                                <%# Eval("ClinicType.Name") %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                               Booking Date :
                            </td>
                            <td>
                               <%# Eval("StartDateTime", "{0:d}") %>
                            </td>
                        </tr>                                                     
                        <tr class="row">
                            <td class="label">
                               Start Time :
                            </td>
                            <td>
                               <%# Eval("StartDateTime", "{0:hh:mm tt}") %>
                            </td>
                        </tr>   
                        <tr class="row">
                            <td class="label">
                               Booking Date :
                            </td>
                            <td>
                               <%# Eval("EndDateTime", "{0:hh:mm tt}") %>
                            </td>
                        </tr>   
                    </tbody>
                </table>
            </div>
    </ItemTemplate>
</asp:FormView>

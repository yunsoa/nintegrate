<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Details.ascx.cs" Inherits="Provider_Details" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>

<pp:ObjectContainerDataSource DataObjectTypeName="EAppointments.UI.ServiceAgents.ProviderService.Provider"
			ID="ProviderDataSource" runat="server" />

<asp:FormView runat="server" ID="ProviderDetails" DataSourceID="ProviderDataSource" HorizontalAlign="Center">
    <ItemTemplate>
         <div class="editor_panel clearfix">
                <table cellspacing="4" cellpadding="4" border="0" class="editor">
                    <tbody>
                        <tr class="row">
                            <td class="label">
                                Name :
                            </td>
                            <td>
                                <%# Eval("Name") %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                                Location :
                            </td>
                            <td>
                                <%# Eval("Location") %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                               Organization :
                            </td>
                            <td>
                                <%# Eval("Organization") %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                               Email :
                            </td>
                            <td>
                               <%# Eval("Email") %>
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                                Conditions Treated :
                            </td>
                            <td>
                               <asp:TextBox ID="ConditionsTreated" runat="server" TextMode="MultiLine" ReadOnly="True" Columns="30" Rows="5" Text='<%# Eval("ConditionsTreated") %>' />
                            </td>
                        </tr>
                        <tr class="row">
                            <td class="label">
                                Procedures Performed :
                            </td>
                            <td>
                                <asp:TextBox ID="ProceduresPerformed" runat="server" TextMode="MultiLine" ReadOnly="True" Columns="30" Rows="5" Text='<%# Eval("ConditionsTreated") %>' />
                            </td>
                        </tr>                                          
                    </tbody>
                </table>
            </div>
    </ItemTemplate>
</asp:FormView>

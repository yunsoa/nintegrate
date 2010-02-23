<%@ Page Language="C#" MasterPageFile="~/Shared/Site.master" AutoEventWireup="true"
    CodeFile="Action.aspx.cs" Inherits="Appointment_Action" Title="Appointment Actions" %>

<asp:Content ID="Action" ContentPlaceHolderID="SiteContent" runat="Server">
    <div id="middle">
        <div id="content" class="clearfix">
            <div id="appointmentsummary">
                <ucl:AppointmentDetails ID="apptDetails" runat="server" />
            </div>
            <div class="editor_panel clearfix" id="CancellationDiv" runat="server" visible="false">
                <table cellspacing="0" cellpadding="6" border="0" class="editor">
                    <tbody>
                        <tr class="row">
                            <td class="label">
                                Cancellation Reason :
                            </td>
                            <td align="right">
                                <asp:TextBox ID="CancellationReason" runat="server" TextMode="MultiLine" Columns="12" Rows="5"/>
                                <asp:RequiredFieldValidator runat="server" ID="requireCancellationReason" Display="none"
                                            ControlToValidate="CancellationReason" EnableClientScript="true" ErrorMessage="Cancellation Reason is required." />
                                <asp:ValidationSummary runat="server" ID="validation" ShowSummary="false" ShowMessageBox="true" HeaderText="Error(s):"/>            
                            </td>
                        </tr>
                    </tbody>
                 </table>
            </div>
            <div id="nextprevpanel">
                <div style="float: right; padding: 10px 0px 0px 10px">
                    <asp:ImageButton ID="Cancel" runat="server" SkinID="CancelButton" OnClick="Cancel_Click" Visible="false"/>
                    <asp:ImageButton ID="Delete" runat="server" SkinID="DeleteButton" OnClick="Delete_Click" Visible="false"/>
                    <asp:ImageButton ID="Approve" runat="server" SkinID="ApproveButton" OnClick="Approve_Click" Visible="false"/>
                    <asp:ImageButton ID="Reject" runat="server" SkinID="RejectButton" OnClick="Reject_Click" Visible="false"/>
                </div>
                <div style="float: right; padding: 10px 0px 0px 10px">
                    <asp:ImageButton ID="Back" runat="server" SkinID="BackButton" OnClick="Back_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Shared/Site.master" AutoEventWireup="true"
    CodeFile="SelectPatient.aspx.cs" Inherits="Appointment_SelectPatient" Title="Select a Patient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Default" ContentPlaceHolderID="SiteContent" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="scriptManager" />
    <div id="middle">
        <div class="clearfix">
            <div class="dashboard_header">
                <div class="dh_titlebar clearfix">
                    <h2>
                        New Appointment</h2>
                </div>
            </div>
            <div class="bar clearfix tab_bar">
                <div id="tabs">
                    <div class="activetab">
                        <a href="SelectPatient.aspx">Step 1: Select Patient</a></div>
                    <div class="disabled">
                        Step 2: Select Provider</div>
                    <div class="disabled">
                        Step 3: Book Slot</div>
                    <div class="disabled">
                        Step 4: Summary</div>
                </div>
            </div>
            <asp:Panel ID="pnlSelectPatient" runat="server">
            <div class="editor_panel clearfix">
                <table cellspacing="0" border="0" class="editor">
                    <tbody>
                        <tr class="row">
                            <td class="label">
                                First Name:
                            </td>
                            <td>
                                <asp:TextBox ID="FirstName" runat="server" />
                            </td>
                            <td class="label">
                                Last Name:
                            </td>
                            <td>
                                <asp:TextBox ID="LastName" runat="server" />
                            </td>
                            <td>
                                <asp:ImageButton ID="Search" runat="server" SkinID="SearchButton" OnClick="Search_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div>
                    <asp:UpdatePanel ID="pnlList" runat="server">
                       <ContentTemplate>
                            <ucl:PatientList ID="patientList" runat="Server" Visible="false"/>
                       </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            </asp:Panel>
            <asp:Panel ID="pnlPatientDetails" runat="Server"  Visible="false">
                 <ucl:PatientDetails ID="patientDetails" runat="server" />
            </asp:Panel>
            <div id="nextprevpanel">
                <div style="float: right; padding: 10px 0px 0px 10px">
                    <asp:ImageButton ID="Next" runat="server" SkinID="NextButton" OnClick="Next_Click" />
                </div>
                <div style="float: right; padding: 10px 0px 0px 10px">
                    <asp:ImageButton ID="Back" runat="server" SkinID="BackButton" OnClick="Back_Click" />
                </div>
                <div style="float:right;padding:10px 0px 0px 10px">
                    <asp:ImageButton ID="Change" runat="server" SkinID="EditButton" OnClick="Edit_Click" Visible="false"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

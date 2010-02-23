<%@ Page Language="C#" MasterPageFile="~/Shared/Site.master" AutoEventWireup="true"
    CodeFile="BookSlot.aspx.cs" Inherits="Appointment_BookSlot" Title="Book a Slot" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Default" ContentPlaceHolderID="SiteContent" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="scriptManager" />
    <div id="middle">
        <div id="content" class="clearfix">
            <div class="dashboard_header">
                <div class="dh_titlebar clearfix">
                    <h2>
                        New Appointment</h2>
                </div>
            </div>
            <div class="bar clearfix tab_bar">
                <div id="tabs">
                    <div class="inactivetab">
                        <a href="SelectPatient.aspx">Step 1: Select Patient</a></div>
                    <div class="inactivetab">
                        <a href="SelectProvider.aspx">Step 2: Select Provider</a></div>
                    <div class="activetab">
                        <a href="BookSlot.aspx">Step 3: Book Slot</a></div>
                    <div class="disabled">
                        Step 4: Summary</div>
                </div>
            </div>
            <asp:Panel ID="pnlSelectSlot" runat="server">
                <div class="editor_panel clearfix">
                    <table cellspacing="2" border="0" class="editor" >
                        <tbody>
                            <tr class="row">
                                <td class="label">
                                    Start Date:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="StartDate" Width="70px" />
                                    <asp:Image runat="Server" ID="imgStartDate" AlternateText="Click to select start date" /><br />
                                    <ajaxToolkit:CalendarExtender ID="startDateCalendarExtender" runat="server" TargetControlID="StartDate"
                                        PopupButtonID="imgStartDate" />
                                </td>
                                <td class="label">
                                    Time:
                                </td>
                                <td>
                                   <asp:DropDownList ID="StartTime" runat="server" Width="80" />
                                </td>
                                 <td>
                                    &nbsp;
                                </td>
                                <td class="label">
                                    End Date:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="EndDate" Width="70px" />
                                    <asp:Image runat="Server" ID="imgEndDate" AlternateText="Click to select an end date" /><br />
                                    <ajaxToolkit:CalendarExtender ID="endDateCalendarExtender" runat="server" TargetControlID="EndDate"
                                        PopupButtonID="imgEndDate" />
                                </td>
                                <td class="label">
                                    Time:
                                </td>
                                <td>
                                   <asp:DropDownList ID="EndTime" runat="server" Width="80"/>
                                </td>                                
                            </tr>
                            <tr >
                                <td>&nbsp;</td>
                            </tr>
                            <tr class="row">
                                <td class="label">
                                    Week Days:
                                </td>
                                <td colspan="2">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <th>M</th><th>Tu</th>
                                            <th>W</th><th>Th</th>
                                            <th>F</th><th>Sa</th>
                                            <th>Su</th>
                                        </tr>
                                        <tr>
                                            <td><asp:CheckBox ID="chkMonday" runat="server" checked="true"/></td>
                                            <td><asp:CheckBox ID="chkTuesday" runat="server" checked="true"/></td>
                                            <td><asp:CheckBox ID="chkWednesday" runat="server" checked="true"/></td>
                                            <td><asp:CheckBox ID="chkThursday" runat="server" checked="true"/></td>
                                            <td><asp:CheckBox ID="chkFriday" runat="server" checked="true"/></td>
                                            <td><asp:CheckBox ID="chkSaturday" runat="server" checked="true"/></td>
                                            <td><asp:CheckBox ID="chkSunday" runat="server" checked="true"/></td>
                                        </tr>
                                    </tbody>
                                </table>                                   
                                </td>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="Search" runat="server" SkinID="SearchButton" CausesValidation="true" ValidationGroup="DateValidations" OnClick="Search_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:UpdatePanel ID="pnlList" runat="server">
                           <ContentTemplate>                     
                                <ucl:SlotList ID="slotList" runat="Server" Visible="false" />
                            </ContentTemplate>
                         </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlSlotDetails" runat="Server" Visible="false">
                <ucl:SlotDetails ID="slotDetails" runat="server" />
            </asp:Panel>
            <div id="nextprevpanel">
                <div style="float: right; padding: 10px 0px 0px 10px">
                    <asp:ImageButton ID="Next" runat="server" SkinID="NextButton" OnClick="Next_Click" />
                </div>
                <div style="float: right; padding: 10px 0px 0px 10px">
                    <asp:ImageButton ID="Back" runat="server" SkinID="BackButton" OnClick="Back_Click" />
                </div>
                 <div style="float: right; padding: 10px 0px 0px 10px">
                    <asp:ImageButton ID="Edit" runat="server" SkinID="EditButton" OnClick="Edit_Click"
                        Visible="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

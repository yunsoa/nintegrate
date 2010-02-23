<%@ Page Language="C#" MasterPageFile="~/Shared/Site.master" AutoEventWireup="true"
    CodeFile="Summary.aspx.cs" Inherits="Appointment_Summary" Title="Summary" %>

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
                     <div class="dh_new_media_shell" id="CreateAppointment" runat="server">
                       <asp:LinkButton ID="lnkCreateAppointment" runat="server" OnClick="lnkCreateAppointment_Click" CssClass="dh_new_media">
                            <div class="tr">
                                <div class="bl">
                                    <div class="br">
                                        <span>Create an Appointment</span>
                                    </div>
                                </div>
                            </div>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="bar clearfix tab_bar">
                <div id="tabs">
                    <div class="inactivetab">
                        <a href="SelectPatient.aspx">Step 1: Select Patient</a></div>
                    <div class="inactivetab">
                        <a href="SelectProvider.aspx">Step 2: Select Provider</a></div>
                    <div class="inactivetab">
                        <a href="BookSlot.aspx">Step 3: Book Slot</a></div>
                    <div class="activetab">
                        <a href="Summary.aspx">Step 4: Summary</a></div>
                </div>
            </div>
            <div id="appointmentsummary">               
                <ucl:AppointmentDetails ID="apptDetails" runat="server" EditMode="true"/>                
            </div>
            <div id="nextprevpanel">
                <div style="float: right; padding: 10px 0px 0px 10px">
                    <asp:ImageButton ID="OK" runat="server" SkinID="OKButton" OnClick="OK_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

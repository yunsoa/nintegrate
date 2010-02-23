<%@ Page Language="C#" MasterPageFile="~/Shared/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Appointment_Default" Title="Welcome - E-Appointments Dashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Default" ContentPlaceHolderID="SiteContent" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="scriptManager" />
    <div id="left">
        <div class="sidebar">
            <div class="sidebarheader">
                Dates</div>
            <div class="sidebarcontent">                
                <asp:Panel ID="pnlDateRangeSelect" runat="server">
                    <div class="editor_panel clearfix">
                        <table border="0" class="editor" width="100%">
                            <tbody>
                                <tr class="row">
                                    <td class="label">
                                        <asp:Label runat="server" ID="lblStartDate" Text="Start Date :" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtStartDate" Width="70px"/>
                                        <asp:Image runat="Server" ID="imgStartDate" AlternateText="Click to select start date" /><br />
                                        <ajaxToolkit:CalendarExtender ID="startDateCalendarExtender" runat="server" TargetControlID="txtStartDate"
                                            PopupButtonID="imgStartDate" />
                                    </td>
                                </tr>
                                <tr class="row">
                                    <td class="label">
                                        <asp:Label runat="server" ID="lblEndDate" Text="End Date :" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtEndDate" Width="70px" />
                                        <asp:Image runat="Server" ID="imgEndDate" AlternateText="Click to select an end date" /><br />
                                        <ajaxToolkit:CalendarExtender ID="endDateCalendarExtender" runat="server" TargetControlID="txtEndDate"
                                            PopupButtonID="imgEndDate" />
                                    </td>
                                </tr>
                                <tr class="row">
                                    <td colspan="2">
                                        <asp:ValidationSummary runat="server" ID="validationDates" ValidationGroup="DateValidations"
                                         DisplayMode="BulletList" ShowSummary="false" ShowMessageBox="true" HeaderText="Error(s):"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                         <div style="float:right;padding: 0px 20px 0px 0px">
                                            <asp:ImageButton ID="DateFilter" runat="server" SkinID="FilterButton" CausesValidation="true" ValidationGroup="DateValidations" OnClick="DateFilter_Click"  />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="sidebar">
            <div class="sidebarheader">
                Current View</div>
            <div class="sidebarcontent">
                <div class="editor_panel clearfix">
                    <table border="0" class="editor" width="100%" style="text-align:left;">
                        <tbody>
                            <tr>
                                <td width="50%">
                                    <asp:RadioButton id="radAll" Checked="True" Text="All" GroupName="radcurrentview" runat="server"  CssClass="radiobtnlist"/>
                                </td>
                                <td>
                                    &nbsp;
                                </td>                           
                            </tr>
                            <tr>
                                <td>   
                                    <asp:RadioButton id="radPending" Text="Pending" GroupName="radcurrentview" runat="server" />                                    
                               </td>
                               <td> 
                                    <asp:Image ID="imgPending" AlternateText="" ImageUrl="~/App_Themes/Default/Images/Pending.bmp" runat="server" EnableTheming="false"/>
                               </td>
                            </tr>    
                            <tr>
                               <td>   
                                    <asp:RadioButton id="radBooked" Text="Booked" GroupName="radcurrentview" runat="server"  CssClass="radiobtnlist" />
                               </td>
                               <td>    
                                    <asp:Image ID="imgBooked" AlternateText="" ImageUrl="~/App_Themes/Default/Images/Booked.bmp" runat="server" EnableTheming="false"/>
                               </td>
                            </tr>                          
                            <tr>
                                <td>   
                                    <asp:RadioButton id="radApproved" Text="Approved" GroupName="radcurrentview" runat="server" />
                               </td>
                               <td> 
                                    <asp:Image ID="imgApproved" AlternateText="" ImageUrl="~/App_Themes/Default/Images/Approved.bmp" runat="server" EnableTheming="false"/>
                               </td>
                            </tr>
                            <tr>
                                <td>   
                                    <asp:RadioButton id="radRejected" Text="Rejected" GroupName="radcurrentview" runat="server" />
                               </td>
                               <td> 
                                    <asp:Image ID="imgRejected" AlternateText="" ImageUrl="~/App_Themes/Default/Images/Rejected.bmp" runat="server" EnableTheming="false"/>
                               </td>
                            </tr>
                            <tr>
                                <td>   
                                    <asp:RadioButton id="radCancelled" Text="Cancelled" GroupName="radcurrentview" runat="server" />
                               </td>
                               <td> 
                                    <asp:Image ID="imgCancelled" AlternateText="" ImageUrl="~/App_Themes/Default/Images/Cancelled.bmp" runat="server" EnableTheming="false"/>
                               </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="float:right;padding: 0px 20px 0px 0px">
                                        <asp:ImageButton ID="ViewFilter" runat="server" SkinID="FilterButton" OnClick="ViewFilter_Click" />
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="middle" style="margin-left:180px">
        <div id="content" class="clearfix">
            <div class="dashboard_header">
                <div class="dh_titlebar clearfix">
                    <h2>
                        Appointments</h2>
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
            <ajaxToolkit:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" >
                <ajaxToolkit:TabPanel ID="TabList" runat="server" HeaderText="List View" >
                    <ContentTemplate>
                        <asp:UpdatePanel ID="pnlList" runat="server">
                            <ContentTemplate>
                                <ucl:AppointmentList ID="appointmentList" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="updateGridProgress" AssociatedUpdatePanelID="pnlList" runat="server">
                            <ProgressTemplate>
                                <div class="ProgressBarIcon"></div>
                                <div class="ProgressBarText">Loading...</div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabMonth" runat="server" HeaderText="Month View">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="pnlMonth" runat="server">
                            <ContentTemplate>
                                <ucl:MonthView ID="monthView" runat="server"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         <asp:UpdateProgress ID="updateMonthProgress" AssociatedUpdatePanelID="pnlMonth" runat="server">
                            <ProgressTemplate>
                                <div class="ProgressBarIcon"></div>
                                <div class="ProgressBarText">Loading...</div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabWeek" runat="server" HeaderText="Week View">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="pnlWeek" runat="server">
                            <ContentTemplate>
                                <ucl:WeekView ID="weekView" runat="server"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         <asp:UpdateProgress ID="updateWeekProgress" AssociatedUpdatePanelID="pnlWeek" runat="server">
                            <ProgressTemplate>
                                <div class="ProgressBarIcon"></div>
                                <div class="ProgressBarText">Loading...</div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </div>
    </div>
    <div id="right">
        <asp:UpdatePanel ID="pnlDetails" runat="server" UpdateMode="conditional">
        <ContentTemplate>
            <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None"
                RequireOpenedPane="false" SuppressHeaderPostbacks="true" CssClass="accordion">
                <Panes>                                
                    <ajaxToolkit:AccordionPane ID="PatientAccordionPane" runat="server">
                        <Header>
                            <a href="" class="accordionLink">Patient Details</a>
                        </Header>
                        <Content>
                            <ucl:PatientDetails ID="patientDetails" runat="server" />
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="AppointmentAccordionPane" runat="server">
                        <Header>
                            <a href="" class="accordionLink">Appointment Details</a></Header>
                        <Content>
                            <ucl:AppointmentDetails ID="appointmentDetails" runat="server" />
                        </Content>
                    </ajaxToolkit:AccordionPane>
                     <ajaxToolkit:AccordionPane ID="ActionAccordionPane" runat="server" Visible="false">
                        <Header>
                            <a href="" class="accordionLink">Actions</a>
                        </Header>
                        <Content>
                         <div style="padding:5px;">
                           <asp:ImageButton ID="Edit" runat="server" SkinID="EditButton" OnClick="Edit_Click"/>
                           <asp:ImageButton ID="Book" runat="server" SkinID="BookButton" OnClick="Book_Click"/>
                           <asp:ImageButton ID="Cancel" runat="server" SkinID="CancelButton" OnClick="Cancel_Click"/>
                           <asp:ImageButton ID="ReBook" runat="server" SkinID="ReBookButton" OnClick="ReBook_Click"/>
                           <asp:ImageButton ID="Delete" runat="server" SkinID="DeleteButton" OnClick="Delete_Click"/>   
                           <asp:ImageButton ID="Approve" runat="server" SkinID="ApproveButton" OnClick="Approve_Click"/>   
                           <asp:ImageButton ID="Reject" runat="server" SkinID="RejectButton" OnClick="Reject_Click"/>                           
                         <div>                           
                        </Content>                    
                    </ajaxToolkit:AccordionPane>       
                </Panes>
            </ajaxToolkit:Accordion>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

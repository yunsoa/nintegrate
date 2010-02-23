<%@ Page Language="C#" MasterPageFile="~/Shared/Site.master" AutoEventWireup="true"
    CodeFile="SelectProvider.aspx.cs" Inherits="Appointment_SelectProvider" Title="Select Provider" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Default" ContentPlaceHolderID="SiteContent" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="scriptManager" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=5" />
        </Scripts>
    </ajaxToolkit:ToolkitScriptManager>
    
    <script type="text/javascript">   

    function onExpandCollapse(mEvent)
    {
        var o = mEvent.target || mEvent.srcElement;
        var rightDiv = o;
        
        while(rightDiv && rightDiv.id != "right")
            rightDiv = rightDiv.parentElement;
        
        if (o.innerText == "Maximize")
        {
            rightDiv.style.width = "690px";  
            o.innerText = "Minimize"; 
            pageLoad();
        }
        else
        {
            rightDiv.style.width = "287px";
            o.innerText = "Maximize";        
            pageLoad();
        }
    }   
    
    </script>
    
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
                    <div class="activetab">
                        <a href="SelectProvider.aspx">Step 2: Select Provider</a></div>
                    <div class="disabled">
                        Step 3: Book Slot</div>
                    <div class="disabled">
                        Step 4: Summary</div>
                </div>
            </div>
            <asp:Panel ID="pnlSelectProvider" runat="server">
                <div class="editor_panel clearfix">
                    <table cellspacing="2" border="0" class="editor" width="100%">
                        <tbody>
                            <tr class="row">
                                <td class="label">
                                    Specialty:
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="SpecialtyList" runat="server" Width="170" />
                                    <asp:RequiredFieldValidator runat="server" ID="requireSpecialty" Display="none" ValidationGroup="SpecialtyGroup"
                                            ControlToValidate="SpecialtyList" EnableClientScript="true" ErrorMessage="Please select a Specialty."                                             
                                            />
                                </td>
                                <td class="label">
                                    Sub-Specialty (Clinic Type):
                                </td>
                                <ajaxToolkit:CascadingDropDown ID="SpecialtyCDD" runat="server" TargetControlID="SpecialtyList"
                                    Category="Specialty" PromptText="Select a Specialty" LoadingText="[Loading Specialties..]"
                                    ServiceMethod="GetDropDownContentsPageMethod" />
                                <td colspan="8">
                                    <asp:DropDownList ID="ClinicTypeList" runat="server" Width="170" />
                                    <asp:RequiredFieldValidator runat="server" ID="requireClinicType" Display="none" ValidationGroup="SpecialtyGroup"
                                            ControlToValidate="ClinicTypeList" EnableClientScript="true" ErrorMessage="Please select a Sub-Specialty."                                             
                                            />
                                </td>
                                <ajaxToolkit:CascadingDropDown ID="ClinicTypeCDD" runat="server" TargetControlID="ClinicTypeList"
                                    Category="ClinicType" PromptText="Select a Sub-Specialty" LoadingText="[Loading Sub-Specialties..]"
                                    ServiceMethod="GetDropDownContentsPageMethod" ParentControlID="SpecialtyList" />
                                <asp:ValidationSummary runat="server" ID="validateSpecialties" ValidationGroup="SpecialtyGroup"
                                         DisplayMode="BulletList" ShowSummary="false" ShowMessageBox="true" HeaderText="Error(s):"/>
                            </tr>
                            <tr class="row">
                                <td class="label">
                                    Location Within
                                </td>
                                <td colspan="1">
                                    <asp:TextBox ID="Miles" runat="server" Width="50px" Text="5" />
                                </td>
                                <td class="label">
                                    Kms of ZipCode
                                </td>
                                <td colspan="10">
                                    <asp:TextBox ID="ZipCode" runat="server" Width="50px" Text="411001" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    Keywords:
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="Keywords" runat="server" Width="300px" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="Search" runat="server" SkinID="SearchButton" CausesValidation="true" ValidationGroup="SpecialtyGroup" OnClick="Search_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                     <asp:UpdatePanel ID="pnlList" runat="server">
                        <ContentTemplate>
                            <ucl:ProviderList ID="providerList" runat="Server" Visible="false" />
                        </ContentTemplate>
                     </asp:UpdatePanel>   
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlProviderDetails" runat="Server" Visible="false">
                <ucl:ProviderDetails ID="providerDetails" runat="server" />
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
                <div style="float: right; padding: 10px 0px 0px 10px">
                    <asp:ImageButton ID="Save" runat="server" SkinID="SaveButton" OnClick="Save_Click"
                        Visible="false" />
                </div>
            </div>
        </div>
    </div>
    <div id="right">
        <asp:Panel ID="pnlMap" runat="server" Visible="false">
            <div class="sidebar">
                <div class="sidebarheader">
                    View Providers on Map
                    <a href="#" id="expandlink" onclick="onExpandCollapse(event);" style="margin: -18px 10px; float: right;color: white;">Maximize</a>
                 </div>    
                <div class="sidebarcontent">
                    <ucl:ProviderMap ID="ProviderMap" runat="server" />
                </div>
            </div>    
        </asp:Panel>
    </div>
</asp:Content>

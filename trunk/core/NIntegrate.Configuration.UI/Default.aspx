<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="NIntegrate.Configuration.UI.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="gridview">
        <tr>
            <th>
                Configuration Sections
            </th>
        </tr>
        <tr>
            <td>
                <a href="ManageEnvironments.aspx">Environments</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="ManageFarms.aspx">Farms</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="ManageConnectingStrings.aspx">ConnectionStrings</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="ManageApplications.aspx">Applications</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="ManageCustomBehaviorTypes.aspx">Custom Behavior Types</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Behaviors</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="ManageServiceHostTypes.aspx">ServiceHost Types</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Services</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Bindings</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Endpoints</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">ServiceContract Compatibilities</a>
            </td>
        </tr>
    </table>
</asp:Content>

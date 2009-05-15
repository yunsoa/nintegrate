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
                <a href="ManageEnvironments.aspx">Manage Environments</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="ManageFarms.aspx">Manage Farms</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="ManageConnectingStrings.aspx">Manage ConnectionStrings</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="ManageApplications.aspx">Manage Applications</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Manage Custom Behavior Types</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Manage Behaviors</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Manage ServiceHost Types</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Manage Services</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Manage Bindings</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Manage Endpoints</a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#">Manage ServiceContract Compatibilities</a>
            </td>
        </tr>
    </table>
</asp:Content>

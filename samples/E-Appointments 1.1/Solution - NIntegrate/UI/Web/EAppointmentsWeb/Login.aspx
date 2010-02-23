<%@ Page Language="C#" MasterPageFile="~/Shared/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" Title="User Login" %>

<asp:Content ID="Default" ContentPlaceHolderID="SiteContent" runat="Server">
    <div class="clearfix" id="content">
        <div class="login_title_header shorten add_border">
            <h2 class="no_icon">
                Login</h2>
        </div>
        <div id="loginbox">
            <asp:Login ID="loginControl" runat="server" RememberMeSet="false" UserName="sanjiv"/>
        </div>
    </div>
</asp:Content>
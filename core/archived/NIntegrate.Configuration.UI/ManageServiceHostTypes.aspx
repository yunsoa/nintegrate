﻿<%@ Page Title="Manage ServiceHost Types" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ManageServiceHostTypes.aspx.cs" Inherits="NIntegrate.Configuration.UI.ManageServiceHostTypes" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Manage ServiceHost Types</h2>
    <div class="bottomhyperlink"><img alt="Add new servicehost type" src="Images/plus.gif" /><asp:LinkButton 
            ID="btnShowAddNewPanel" runat="server" Text="Add new servicehost type" 
            onclick="btnShowAddNewPanel_Click"></asp:LinkButton></div>
    <br />
    <asp:GridView CssClass="gridview"
        ID="gvServiceHostTypes" runat="server" 
        DataSourceID="dsServiceHostTypes"
        DataKeyNames="ServiceHostType_id"
        AllowSorting="true"
        AutoGenerateDeleteButton="true"
        AutoGenerateColumns="false"
        AutoGenerateEditButton="true" onrowcommand="gvServiceHostTypes_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="FriendlyName" SortExpression="ServiceHostTypeFriendlyName">
                <ItemTemplate>
                    <%#Eval("ServiceHostTypeFriendlyName")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbServiceHostTypeFriendlyName" runat="server" MaxLength="100" Text='<%#Bind("ServiceHostTypeFriendlyName") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="tbServiceHostTypeFriendlyNameRequired" runat="server"
                        ControlToValidate="tbServiceHostTypeFriendlyName" EnableClientScript="false"
                        ErrorMessage="Required"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ClassName" SortExpression="ServiceHostTypeClassName">
                <ItemTemplate>
                    <%#Eval("ServiceHostTypeClassName")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbServiceHostTypeClassName" runat="server" Text='<%#Bind("ServiceHostTypeClassName") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="tbServiceHostTypeClassNameRequired" runat="server"
                        ControlToValidate="tbServiceHostTypeClassName" EnableClientScript="false"
                        ErrorMessage="Required"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
        <SelectedRowStyle BackColor="LightYellow" />
    </asp:GridView>
    <asp:Panel ID="panelBottom" runat="server" CssClass="panelBottom">
        <asp:DetailsView ID="dvAddServiceHostType" runat="server" Visible="false"
            DataSourceID="dsServiceHostTypes" DefaultMode="Insert"
            AutoGenerateRows="false"
            AutoGenerateInsertButton="true"
            CssClass="detailstable" onitemcommand="dvAddServiceHostType_ItemCommand" 
            oniteminserted="dvAddServiceHostType_ItemInserted">
            <Fields>
                <asp:TemplateField HeaderText="FriendlyName">
                    <InsertItemTemplate>
                        <asp:TextBox ID="tbServiceHostTypeFriendlyName" runat="server" MaxLength="100" Text='<%#Bind("ServiceHostTypeFriendlyName") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="tbServiceHostTypeFriendlyNameRequired" runat="server"
                            ControlToValidate="tbServiceHostTypeFriendlyName" EnableClientScript="false"
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ClassName">
                    <InsertItemTemplate>
                        <asp:TextBox ID="tbServiceHostTypeClassName" runat="server" Text='<%#Bind("ServiceHostTypeClassName") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="tbServiceHostTypeClassNameRequired" runat="server"
                            ControlToValidate="tbServiceHostTypeClassName" EnableClientScript="false"
                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>      
    </asp:Panel>
    <ni:QueryDataSource ID="dsServiceHostTypes" runat="server" UseLocalQueryService="true">
    </ni:QueryDataSource>
</asp:Content>

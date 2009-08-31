﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NIntegrate.WebTest._Default" %>

<%@ Register assembly="NIntegrate.Web" namespace="NIntegrate.Web" tagprefix="ni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateDeleteButton="True" 
            AutoGenerateEditButton="True" DataKeyNames="ProductCategoryID" 
            DataSourceID="QueryDataSource1" PageSize="5">
        </asp:GridView>
        <br />
        <asp:DetailsView ID="DetailsView1" runat="server" 
            AutoGenerateInsertButton="True" AutoGenerateRows="False" 
            DataKeyNames="ProductCategoryID" DataSourceID="QueryDataSource1" 
            DefaultMode="Insert" Height="50px" oniteminserted="DetailsView1_ItemInserted" 
            Width="125px">
            <Fields>
                <asp:BoundField DataField="Name" HeaderText="Name" />
            </Fields>
        </asp:DetailsView>
    
    </div>
    <ni:QueryDataSource ID="QueryDataSource1" 
        QueryTableType="NIntegrate.WebTest.Code.ProductCategoryTable" runat="server" 
        ConflictDetection="CompareAllValues">
    </ni:QueryDataSource>
       
    </form>
</body>
</html>

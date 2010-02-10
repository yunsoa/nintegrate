<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryCodeGenerator.aspx.cs"
    Inherits="NIntegrate.CodeGenerator.QueryCodeGenerator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QueryTable/QuerySproc Code Generator (SQL Server 2005/2008)</title>
    <script type="text/javascript" language="javascript">
        function selectUnselectAll(cblid, checked) {
            var list = document.getElementsByTagName('INPUT');
            for (var i = 0; i < list.length; ++i) {
                if (list[i].type == 'checkbox' && list[i].id.substr(0, cblid.length) == cblid)
                    list[i].checked = checked;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        ConnectionString:
        <asp:TextBox ID="tbConnStr" runat="server" Width="500px" Text="Server=.;Database=ConfigurationDB;Trusted_Connection=True;"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnConnect" runat="server" Text="Connect" 
            onclick="btnConnect_Click" />
    </div>
    <br />
    <div>
        Output Directory:
        <asp:TextBox ID="tbOutputDir" runat="server" Width="300px"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnGen" runat="server" Text="Generate Code" />
        &nbsp;
        <asp:CheckBox ID="cbDeleteBeforeGen" runat="server" Text="Clear output directory before code generation" />
    </div>
    <br />
    <table align="left">
        <tr>
            <th style="width: 200px">
                Tables
            </th>
            <th style="width: 200px">
                Views
            </th>
            <th style="width: 200px">
                Stored Procedures
            </th>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbSelectAllTables" runat="server" Text="Select/Unselect All" onclick="selectUnselectAll('cblTables', this.checked)" />
            </td>
            <td>
                <asp:CheckBox ID="cbSelectAllViews" runat="server" Text="Select/Unselect All" onclick="selectUnselectAll('cblViews', this.checked)" />
            </td>
            <td>
                <asp:CheckBox ID="cbSelectAllSprocs" runat="server" Text="Select/Unselect All" onclick="selectUnselectAll('cblSprocs', this.checked)" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:CheckBoxList ID="cblTables" runat="server" DataTextField="name" DataValueField="name">
                </asp:CheckBoxList>
            </td>
            <td valign="top">
                <asp:CheckBoxList ID="cblViews" runat="server" DataTextField="name" DataValueField="name">
                </asp:CheckBoxList>
            </td>
            <td valign="top">
                <asp:CheckBoxList ID="cblSprocs" runat="server" DataTextField="name" DataValueField="name">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>

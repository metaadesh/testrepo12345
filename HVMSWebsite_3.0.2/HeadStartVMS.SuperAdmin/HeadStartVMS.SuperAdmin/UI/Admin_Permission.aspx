<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Permission.aspx.cs"
    Inherits="METAOPTION.UI.AdminPermission" Title="Admin Panel :: Permission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HeadStart VMS</title>
    <link href="../Styles/ControlStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <center style="padding-top: 80px;">
        <table border="0" id="tblForget" cellpadding="0" cellspacing="0" style="padding: 20px;
            width: 750px; font-size: 17px; font-weight: bold; font-family: 'Arial Black' Arial Verdana;
            border: solid 1px gray;">
            <tr>
                <td class="gvHeading" style="text-align: center; font-size: 24px">
                    Permission Problem
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSorry" runat="server" Text="Sorry, You don't have sufficient permission to see the requested page!" />
                </td>
            </tr>
            <tr>
                <td id="tdPermission" runat="server">
                    To see the requested page you need to have at least following permission:
                    <br />
                    <b>
                        <asp:Label ID="lblPermission" runat="server" CssClass="err" Font-Size="17px" Font-Bold="true" /></b>
                </td>
            </tr>
            <tr>
                <td>
                    <p style="width: 745px; text-align: center; font-weight: bold;" id="paradefault"
                        runat="server">
                        Click <a id="hlnkDefault" runat="server" href="../Admin_Home.aspx" title="click to go back to Home page">
                            here </a>to go back to Home page
                    </p>
                    <p style="width: 745px; text-align: center; font-weight: bold;" id="paralogin" runat="server">
                        Click <a href="../Admin_Login.aspx" title="click to go back to Login page">here
                        </a>
                    to go back to Login page
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>

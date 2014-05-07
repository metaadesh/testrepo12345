<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Login.aspx.cs" Inherits="METAOPTION.Admin_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>HeadStart VMS :: Login</title>
    <link href="Styles/ControlStyle.css" rel="stylesheet" type="text/css" />
    <link href="Styles/MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        window.history.forward(1);
    </script>
</head>
<body style="background: #E3ECF5 url(images/page_bg.jpg) center repeat-y;">
    <form id="form1" runat="server">
    <center>
        <div id="LoginContainer">
            <table border="0" width="430" cellpadding="0" style="border-collapse: collapse" id="LoginTable">
                <tr>
                    <td id="MainHeading">
                        Login to Headstart VMS SUPER ADMIN
                    </td>
                </tr>
                <tr>
                    <td id="LoginContent">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" class="formTxtBold" width="90">
                                    Admin Login Id
                                </td>
                                <td align="left" class="formTxtBold" width="200">
                                    <asp:TextBox ID="txtLogin" runat="server" CssClass="FormItems" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="formTxtBold">
                                    Password
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPass" EnableViewState="true" runat="server" CssClass="FormItems"
                                        TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                       <%--     <tr>
                                <td align="left" class="formTxtBold">
                                    Org. Code
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOrgCode" EnableViewState="true" runat="server" CssClass="FormItems"
                                        Style='text-transform: uppercase'></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="2" align="center" class="formTxtBold">
                                    <table border="0" cellspacing="3" cellpadding="3">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblMessage" runat="server" CssClass="err" EnableViewState="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" id="chkRememberMe" runat="server" />
                                            </td>
                                            <td>
                                                Remember me on this computer
                                            </td>
                                        </tr>
                                        <tr>
                                           <%-- <td colspan="2" align="center">
                                                Forgot Password <u><a href="AdminForgetPassword.aspx">Click Here</a></u>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="~/Images/Login_Button.gif" ToolTip="Login"
                                        OnClick="btnLogin_Click" />
                                    <%--<img src="../Images/Login_Button.gif" width="62" height="19" />--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="METAOPTION.Login.aspx.cs" 
    Inherits="METAOPTION.UI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>HeadStart VMS :: Login</title>
    <%-- <script src="CSS/min.js?v=1" type="text/javascript"></script>--%>
    <link href="CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <link href="CSS/MainStyle.css" rel="stylesheet" type="text/css" />
    <script src="CSS/jquery-1.2.6.min.js" type="text/javascript"></script>
</head>
<body style="background: #E3ECF5 url(images/page_bg.jpg) center repeat-y;">
    <form id="form1" runat="server">
    <center>
        <div id="LoginContainer">
            <table border="0" width="430" cellpadding="0" style="border-collapse: collapse" id="LoginTable">
                <tr>
                    <td id="MainHeading">
                        Login to Headstart VMS
                    </td>
                </tr>
                <tr>
                    <td id="LoginContent">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" class="formTxtBold" width="90">
                                    Login Id
                                </td>
                                <td align="left" class="formTxtBold" width="200">
                                    <asp:TextBox ID="txtLogin" runat="server" CssClass="FormItems" onblur="read(this)" />
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
                            <tr>
                                <td align="left" class="formTxtBold">
                                    Org. Code
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOrgCode" runat="server" CssClass="FormItems" style='text-transform:uppercase'></asp:TextBox>
                                </td>
                            </tr>
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
                                            <td colspan="2" align="center">
                                                Forgot Password <u><a href="ForgotPassword.aspx">Click Here</a></u>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="~/Images/Login_Button.gif"
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
<script type="text/javascript" language="javascript">
    function read(obj) {
        debugger;

        var name = 'OrgCode_' + obj.value;
        var cookies=document.cookie;

        var c, C, i; 
            //if (cookies!=null) { return cookies[name]; }

            c = document.cookie.split('; ');
            cookies = {};

            for (i = c.length - 1; i >= 0; i--) {
                C = c[i].split('=');
                cookies[C[0]] = C[1];
            }

            var res = cookies[name];
            $('#<%=txtOrgCode.ClientID %>').val('');
            $('#<%=txtOrgCode.ClientID %>').val(res);
            
        

        //window.readCookie = readCookie; // or expose it however you want
    }
</script>
</html>

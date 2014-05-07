<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminChangePassword.aspx.cs" Inherits="METAOPTION.UI.AdminChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
   <title>HeadStart VMS :: ADMIN CHANGE PASSWORD</title>
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: #E3ECF5 url(images/page_bg.jpg) center repeat-y;">
   <form id="form1" runat="server">
   <center>
      <div id="LoginContainer">
         <table border="0" width="430" cellpadding="0" style="border-collapse: collapse" id="LoginTable">
            <tr>
               <td id="MainHeading">
                   Change User Password</td>
            </tr>
            <tr>
               <td id="LoginContent">
                  <table border="0" cellpadding="0" cellspacing="0">
                     <tr>
                        <td align="left" class="formTxtBold" width="90">
                           Login Id
                        </td>
                        <td align="left" class="formTxtBold" width="200">
                               <asp:TextBox ID="txtLogin" runat="server" CssClass="FormItems" 
                                   ReadOnly="True" />                           
                         <asp:RequiredFieldValidator ID="rfvLogin" ControlToValidate="txtLogin" runat="server" 
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                       
                        </td>
                     </tr>
                     <tr>
                        <td align="left" class="formTxtBold">
                            New
                           Password
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtNewPassword" EnableViewState="true" runat="server" CssClass="FormItems" TextMode="Password"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfvPassword" runat="server"  ControlToValidate="txtNewPassword"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                       <tr>
                        <td align="left" class="formTxtBold">
                           Confirm New
                           Password
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtConfirmNewPassword" EnableViewState="true" runat="server" CssClass="FormItems" TextMode="Password"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfvNewPwd" ControlToValidate="txtConfirmNewPassword" runat="server" 
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td colspan="2" align="center" class="formTxtBold">
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="err" Text=""></asp:Label>
                         </td>
                     </tr>
                     <tr>
                        <td align="left">
                           &nbsp;
                        </td>
                        <td align="center">
                           <asp:Button ID="btnBack" CausesValidation="false"  Width="70px" runat="server" CssClass="Btn_Form" 
                                Text="Back" onclick="btnBack_Click" />
                           &nbsp;
                           <asp:Button ID="btnChangeUserPassword"  Width="70px" runat="server" 
                                CssClass="Btn_Form" Text="Save" onclick="btnChangeUserPassword_Click" />
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

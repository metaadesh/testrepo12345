<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs"
   Inherits="METAOPTION.ForgetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>HeadStart VMS :: Password Recovery</title>
   <%--<link href="CSS/min.css?v=1" rel="stylesheet" type="text/css" />
    <script src="CSS/min.js?v=1" type="text/javascript"></script>--%>

   <link href="CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
   <link href="CSS/MainStyle.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: #E3ECF5 url(images/page_bg.jpg) center repeat-y;">
   <form id="form1" runat="server">
   <center>
      <div>
         <asp:Label ID="lblError" runat="server" CssClass="err" Visible="false" />
      </div>
      <table border="1" id="tblForget" cellpadding="0" cellspacing="0" style="text-align: left;
         width: 550px" runat="server">
         <tr>
            <td id="MainHeading" colspan="2" align="center">
			<!--Forgot Your Password-->
               Forget Password
            </td>
         </tr>
         <tr>
            <td class="lblb">
               User Name (Login Id)
            </td>
            <td class="lbl">
               <asp:TextBox ID="txtUserName" runat="server" CssClass="txt2" />
            </td>
         </tr>
         <tr>
            <td class="lblb" nowrap style="width: 200px">
               Email Id to Send the password
            </td>
            <td class="lbl">
               <asp:TextBox ID="txtEmail" runat="server" CssClass="txt3" />
            </td>
         </tr>
         <tr>
            <td>
            </td>
            <td class="lbl" style="text-align: center; padding: 7px;">
               <asp:Button ID="btnSubmit" runat="server" Text=" Submit " CssClass="btn" 
                  onclick="btnSubmit_Click" />
            </td>
         </tr>         
      </table>
      
      <div id="divMessage" runat="server">
      <table border="1" cellpadding="0" cellspacing="0" style="text-align: left;
         width: 550px" >
         <tr>
            <td id="MainHeading" colspan="2" align="center">
               Password Recovery
            </td>
         </tr>
         <tr>
            <td style="text-align:center; font-size:14px" >
               <p style="width:530px; text-align:left">
                  We have sent a mail to you at your provided email address, It may take time so please
                  check your mail for your password!
                  <br />
                  <br />
                  Thanks
                  <br />
                  Hollenshead VMS
               </p>
               <p style="width:530px; text-align:center;font-weight:bold;">
                  Click <a href="Login.aspx" title="cliclk to back to login page" >here  </a>to go back to login page
               </p>
            </td>
         </tr>
      </table>
      </div>
   </center>
   </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemError.aspx.cs" Inherits="METAOPTION.UI.SystemError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>HeadStart VMS</title>
   <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
   <form id="form1" runat="server">
   <center style="padding-top: 80px;">
      <table border="0" id="tblForget" cellpadding="0" cellspacing="0"
         style="padding: 20px; width: 750px; font-size:17px; font-weight:bold; font-family:'Arial Black' Arial Verdana; border: solid 1px gray;">
         <tr>
            <td class="gvHeading" style="text-align:center; font-size:24px">
               OOPS! Some Error has been Occurred at Server end.
            </td>
         </tr>
         <tr>
            <td >
              Please try again or Call System Admin.
                <br />
                <br />
                Go Back to home page</td>
         </tr>
         <%--<tr>
            <td >
               To see the requested page you need to have at least following permission:
               <br /> <b>
               <asp:Label ID="lblPermission" runat="server" CssClass="err" Font-Size="17px" Font-Bold="true"  /></b>
            </td>
         </tr>
         --%>
         <tr>
            <td >
               <p style="width: 745px; text-align: center; font-weight: bold;">
                  Click <a href="Default.aspx" title="click here go back to Home page">here </a>to go
                  back to Home page
               </p>
            </td>
         </tr>
      </table>
   </center>
   </form>
</body>
</html>

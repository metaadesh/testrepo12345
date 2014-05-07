<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNote.aspx.cs" Inherits="METAOPTION.UI.AddNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
<link href="../CSS/AjaxRelated.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />

    <script src="../CSS/ext-core-debug.js" type="text/javascript"></script>

    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
    
</head>

<body style="background-color:White;" >
    <form id="form1" runat="server">
  <div class="RightPanel">
     <fieldset class="ForFieldSet" >
     <legend class="ForLegend">Add Notes</legend><br>
     <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
     <tr>
      <td class="GridContent" align="center" style="font-weight:bold;" >Note</td>
      <td style="width:70%;" class="GridContent">
          <asp:TextBox ID="txtComment" runat="server" CssClass="txtMulti" Rows="5" 
              TextMode="MultiLine"></asp:TextBox>
         </td>
     </tr>
     <tr>
     
      <td colspan="2" align="center">
          <br />
          <asp:Button ID="btnAddComment" runat="server" CssClass="Btn_Form" 
              Text="Submit" onclick="btnAddComment_Click" />
                     &nbsp;<asp:Button ID="btnUpdate" runat="server" CssClass="Btn_Form" 
              onclick="btnUpdate_Click" Text="Save" Width="59px" />
                     </td>
     </tr>
     </table>
     </fieldset>  
   </div>
    </form>
</body>
</html>

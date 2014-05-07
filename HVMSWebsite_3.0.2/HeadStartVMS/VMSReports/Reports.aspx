<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Reports.aspx.cs" Inherits="METAOPTION.Reports.Reports" %>



<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reports</title>
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
       #form1{height: 600px;width: 100%;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <br />
      <div style="text-align: center">
         <br />
         <asp:Button ID="btnBack" runat="server" Text="  Back  " CssClass="btn" 
            onclick="btnBack_Click" />
      </div>
  
        <rsweb:ReportViewer 
            ID="rptView"
            runat="server"
            Width="100%"
            Height="550px"
            ProcessingMode="Remote" 
            BackColor="White" 
            Font-Size="8pt" 
            Font-Names="Verdana"
            ZoomMode="PageWidth" 
            ShowCredentialPrompts="false">
            
        </rsweb:ReportViewer>
        <br />       
     <div style="text-align: center">
         <br />
         <asp:Button ID="Button1" runat="server" Text="  Back  " CssClass="btn" 
            onclick="btnBack_Click" />
      </div>
    </form>
</body>
</html>

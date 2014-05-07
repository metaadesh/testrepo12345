<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scanner.aspx.cs" Inherits="METAOPTION.UI.Scanner" %>

<%@ Register Src="../UserControls/Scanner.ascx" TagName="Scanner" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfEntityID" runat="server" />
    <asp:HiddenField ID="hfEntityTypeID" runat="server" />
    <div id="div1" style="overflow: auto; background-color: #4B6C9E">
        <script src="../CSS/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
        <script src="../CSS/jquery-1.4.1.js" type="text/javascript"></script>
        <script src="../CSS/jquery-1.4.1.min.js" type="text/javascript"></script>
        <uc1:Scanner ID="scanner1" runat="server" OnScannedFilesUploaded="webScanner_ScannedFilesUploaded" />
        <%--<uc1:Scanner ID="Scanner1" runat="server" />--%>
    </div>
    </form>
</body>
</html>

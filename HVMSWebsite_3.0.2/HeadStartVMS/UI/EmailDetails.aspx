<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailDetails.aspx.cs" Inherits="METAOPTION.UI.EmailDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <div style="background-color: #fff; width: 100%; height: 100%">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="lblb" style="width: 15%; vertical-align: top">
                    To
                </td>
                <td class="lbl">
                    <asp:Label ID="lblMailTo" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    Subject
                </td>
                <td class="lbl">
                    <asp:Label ID="lblMailSubject" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    From
                </td>
                <td class="lbl">
                    <asp:Label ID="lblMailFrom" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    CC
                </td>
                <td class="lbl">
                    <asp:Label ID="lblMailCCed" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    BCC
                </td>
                <td class="lbl">
                    <asp:Label ID="lblMailBCCed" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    Attempt
                </td>
                <td class="lbl">
                    <asp:Label ID="lblAttempt" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    Logged-On
                </td>
                <td class="lbl">
                    <asp:Label ID="lblLoggedOn" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    Sent-On
                </td>
                <td class="lbl">
                    <asp:Label ID="lblSentOn" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top" colspan="2">
                    Message
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="lbl" style="width: 100%">
                    <asp:Label ID="lblMailBody" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMSDetail.aspx.cs" Inherits="METAOPTION.UI.SMSDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div style="background-color:#fff; width:100%; height:100%">
        <asp:DataList ID="dlsmsdetails" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <ItemTemplate>
        <div style="margin-bottom:10px;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="lblb" style="width:15%; vertical-align: top">
                    To
                </td>
                <td class="lbl">
                    <asp:Label ID="lblMailTo" runat="server" Text='<%# Eval("MailTo") %>' />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    Attempt
                </td>
                <td class="lbl">
                    <asp:Label ID="lblAttempt" runat="server" Text='<%# Eval("AttemptCount") %>' />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    Logged-On
                </td>
                <td class="lbl">
                    <asp:Label ID="lblLoggedOn" runat="server" Text='<%# Eval("LogTime") %>' />
                </td>
            </tr>
            <tr>
                <td class="lblb" style="vertical-align: top">
                    Sent-On
                </td>
                <td class="lbl">
                    <asp:Label ID="lblSentOn" runat="server" Text='<%# Eval("SentTime") %>' />
                </td>
            </tr>
             <tr>
                <td class="lblb" style="vertical-align: top">
                    Status
                </td>
                <td class="lbl">
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Remark") %>' />
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
                <td class="lbl" style="width:100%">
                    <asp:Label ID="lblMailBody" runat="server" Text='<%# Eval("Body") %>' />
                </td>
            </tr>
        </table>
        </div>
        </ItemTemplate>
        </asp:DataList>
    </div>
</body>
</html>

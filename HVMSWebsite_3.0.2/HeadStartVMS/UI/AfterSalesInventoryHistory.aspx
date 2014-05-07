<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AfterSalesInventoryHistory.aspx.cs"
    Inherits="METAOPTION.UI.AfterSalesInventoryHistory" Title="METAOPTION::AFTER SALES INVENTORY-HISTORY" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ModalPopUp.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <center style="margin:3%">
       
        
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td class="AddHeading" style="width:100%" >
                   <asp:Label ID="lblInventoryHeader" runat="server" Text=""></asp:Label>
                </td>
                <td align="right">
                    <input type="button" id="btnClose" value="Close" onclick="javascript:window.close();" /><br />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:GridView runat="server" Width="100%" ID="gvAfterSalesInventoryHistory" AllowPaging="True"
                        AutoGenerateColumns="true" PageSize="20"  EmptyDataText="No Rows found" 
                        OnPageIndexChanging="gvAfterSalesInventoryHistory_PageIndexChanging" >
                        <AlternatingRowStyle CssClass="gvAlternateRow" />
                        <HeaderStyle CssClass="gvHeading" />
                        <RowStyle CssClass="gvRow" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                    </asp:GridView>
                    
                    <br /> <br />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPreview_ExpenseAgainstPayment.aspx.cs"
    Inherits="METAOPTION.UI.PrintPreview_ExpenseAgainstPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/AjaxRelated.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        @media print
        {
            input#printButton
            {
                display: none;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!-- Header Start -->
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td class="Logo">
                    <img border="0" src="../images/Logo.gif" width="207" height="35" alt="" />
                    <asp:ScriptManager ID="ScriptManager2" runat="server">
                    </asp:ScriptManager>
                </td>
                <td>
                    <asp:Label ID="lblServerDate" CssClass="lbl" runat="server"></asp:Label>
                    <img id="printButton" onclick="window.print();" src="../Images/print.gif" alt="Print this Page" />
                </td>
            </tr>
        </table>
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td>
                    <asp:HiddenField ID="hdnEntityId" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnEntityTypeId" runat="server" Value="0" />
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                        <tr>
                            <td class="GridContent" width="25%">
                                <b>Recipient Type</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblRecipientType" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="GridContent" width="25%">
                                <b>Recipient Name</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblRecipientName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent" width="25%">
                                <b>Check Number</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblCheckNumber" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="GridContent" width="25%">
                                <b>Check Date</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblCheckDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent" width="25%">
                                <b>Amount</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hdnAmount" runat="server" />
                            </td>
                            <td class="GridContent" width="25%">
                                <b>Invoice Number</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblInvoicenumber" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent" width="25%">
                                <b>Added by</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblAddedBy" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="GridContent" width="25%">
                                <b>Date Added</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblDateAdded" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent" width="25%">
                                <b>Bank Name</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblBankName" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="GridContent" width="25%">
                                <b>Comments</b>
                            </td>
                            <td class="GridContent" width="75%">
                                <asp:Label ID="lblComments" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent" width="25%">
                                <b>Peachtree Ref. Number</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblPeachtreeRefNo" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="GridContent" width="25%">
                                <b>Peachtree Ref. Date</b>
                            </td>
                            <td class="GridContent" width="25%">
                                <asp:Label ID="lblPeachtreeRefDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent">
                                <b>Accounting Code</b>
                            </td>
                            <td class="GridContent" colspan="3">
                                <asp:Label ID="lblAccountingCode" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hdnGUID" runat="server" />
                                <asp:Label ID="lblPeachtreeMsg" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td class="TableHeadingBg TableHeading">
                                Expenses
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                <asp:GridView ID="grvSelectedExpense" runat="server" DataKeyNames="ExpenseId" AutoGenerateColumns="false"
                                    ShowHeader="true" AllowPaging="false" PageSize="100" Width="100%" EmptyDataText="No record found."
                                    AlternatingRowStyle-CssClass="gridViewAlt" EmptyDataRowStyle-CssClass="GridEmptyRow"
                                    CssClass="Grid" ShowFooter="true" OnRowDataBound="grvSelectedExpense_RowDataBound">
                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:BoundField DataFormatString="{0:MM/dd/yyyy}" DataField="ExpenseDate" HeaderText="Expense Date"
                                            HeaderStyle-Width="100px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" HeaderStyle-CssClass="GridHeader"
                                            HeaderStyle-Width="105px" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                        <asp:BoundField DataField="ExpenseAmount" DataFormatString="{0:C}" HeaderText="Amount"
                                            HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="70px" ItemStyle-CssClass="GridContentRight">
                                        </asp:BoundField>
                                        <asp:TemplateField SortExpression="VIN" HeaderStyle-CssClass="GridHeader" HeaderText="VIN"
                                            ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnExpenseAmount" runat="server" Value='<%# Eval("ExpenseAmount")%>' />
                                                <asp:Label ID="lnkVIN" CommandArgument='<%# Eval("InventoryId")%>' runat="server"
                                                    Text='<%# Eval("VIN")%>' CausesValidation="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                        <asp:BoundField DataField="Make" HeaderText="Make" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                        <%-- <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent"></asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnComment" runat="server" Value='<%# Eval("Comments") %>' />
                                                <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="grvVoidedExpense" runat="server" DataKeyNames="ExpenseId" AutoGenerateColumns="false"
                                    ShowHeader="true" AllowPaging="true" PageSize="15" Width="100%" EmptyDataText="No record found."
                                    AlternatingRowStyle-CssClass="gridViewAlt" EmptyDataRowStyle-CssClass="GridEmptyRow"
                                    CssClass="Grid">
                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:BoundField DataFormatString="{0:MM/dd/yyyy}" DataField="ExpenseDate" HeaderText="Expense Date"
                                            HeaderStyle-Width="100px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" HeaderStyle-CssClass="GridHeader"
                                            HeaderStyle-Width="105px" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                        <asp:BoundField DataField="ExpenseAmount" DataFormatString="{0:C}" HeaderText="Amount"
                                            HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="70px" ItemStyle-CssClass="GridContent">
                                        </asp:BoundField>
                                        <asp:TemplateField SortExpression="VIN" HeaderStyle-CssClass="GridHeader" HeaderText="VIN"
                                            ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkVIN" CommandArgument='<%# Eval("InventoryID")%>' runat="server"
                                                    Text='<%# Eval("VIN")%>' CausesValidation="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                        <asp:BoundField DataField="Make" HeaderText="Make" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                        <asp:BoundField DataField="VoidDescription" HeaderText="Void Reason" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                        <asp:BoundField DataField="voidcomment" HeaderText="Void Comments" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tblOutstandingAmt" runat="server" border="0" width="100%" cellpadding="0"
                        cellspacing="0" style="border-collapse: collapse">
                        <tr>
                            <td class="TableHeadingBg TableHeading" colspan="2">
                                Amount Details
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Payment made Amount
                            </td>
                            <td class="TableBorder">
                                <asp:Label ID="lblPaymentMade" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Expense Amount
                            </td>
                            <td class="TableBorder">
                                <asp:Label ID="lblExpAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Oustanding Amount
                            </td>
                            <td class="TableBorder">
                                <asp:Label ID="lblOutstandingAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Total Amount
                            </td>
                            <td class="TableBorder">
                                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

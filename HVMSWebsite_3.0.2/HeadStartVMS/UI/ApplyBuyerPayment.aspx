<%@ Page Language="C#"  AutoEventWireup="true"  EnableEventValidation="false"
    CodeBehind="ApplyBuyerPayment.aspx.cs" Inherits="METAOPTION.UI.ApplyBuyerPayment"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function showProgressMsg()
        { 
            document.getElementById('ctl00_ContentPlaceHolder1_imgProgress').style.display = "";
        }
        
        function showWaitMsg()
        { 
            var ctrl = document.getElementById('ctl00_ContentPlaceHolder1_btnRefreshVendorsFromPeachtree');
            ctrl.src = "../Images/Wait.gif"; 
            ctrl.style.height = "16px";
            ctrl.style.width = "16px";
        } 
    </script>

    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td>
                <asp:HiddenField ID="hdnEntityId" runat="server" Value="0" />
                <asp:HiddenField ID="hdnEntityTypeId" runat="server" Value="0" />
                <asp:HiddenField ID="hdnBankAccountID" runat="server" Value="0" />
                <asp:HiddenField ID="hdnAmount" runat="server" Value="0" />
                <asp:Label ID="lblError" runat="server" Text="" Style="color: Red; font-weight: bold;"></asp:Label>
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
                            <b>Accounting Code</b>
                        </td>
                        <td class="GridContent" width="25%">
                            <asp:Label ID="lblAccountingCode" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hdnGUID" runat="server" />
                            <asp:LinkButton ID="lnkSelectAccountingCode" runat="server">Change</asp:LinkButton>
                            <!-- Popup for Accounting Code  -->
                            <ajax:ModalPopupExtender ID="mpeAccountingCode" runat="server" TargetControlID="lnkSelectAccountingCode"
                                PopupControlID="pnlAccountingCode" BackgroundCssClass="modalBackground" DropShadow="False"
                                CancelControlID="btnCancelAccountingCode" PopupDragHandleControlID="pnlHeader">
                            </ajax:ModalPopupExtender>
                            <asp:Panel runat="server" ID="pnlAccountingCode" Style="display: none; background: #f0f0f0;">
                                <table border="0" width="600" cellpadding="0" class="PopUpBox">
                                    <tr>
                                        <td class="PopUpBoxHeading" align="left">
                                            Select Accounting Code
                                        </td>
                                        <td class="PopUpBoxHeading" align="right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/Images/close.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="padding: 10px">
                                            <div class="TableHeadingBg" style="text-align: right;">
                                                <asp:ImageButton ID="btnRefreshVendorsFromPeachtree" runat="server" CausesValidation="false"
                                                    ImageUrl="~/Images/arrow_refresh.png" Style="margin: 8px;" AlternateText="Refresh"
                                                    ToolTip="Refresh Vendors From Peachtree." OnClick="btnRefreshVendorsFromPeachtree_Click"
                                                    OnClientClick="javascript:showWaitMsg();" />
                                            </div>
                                            <div class="TableBorder">
                                                <!--  Filter Section In Accounting Code Popup -->
                                                <table class="arial-12" cellpadding="0" border="0" width="100%" style="border-collapse: collapse;
                                                    margin-bottom: 20px;">
                                                    <tr>
                                                        <td class="GridContent_padding5">
                                                            <div style="padding-left: 10px;">
                                                                <b>Search: </b>
                                                                <asp:DropDownList ID="ddlSearchField" runat="server" class="FormItem">
                                                                    <asp:ListItem Value="Name" Text="Name" Selected="True"> </asp:ListItem>
                                                                    <asp:ListItem Value="ID" Text="ID"> </asp:ListItem>
                                                                    <asp:ListItem Value="Line1" Text="Line1"> </asp:ListItem>
                                                                    <asp:ListItem Value="City" Text="City"> </asp:ListItem>
                                                                    <asp:ListItem Value="State" Text="State"> </asp:ListItem>
                                                                    <asp:ListItem Value="Country" Text="Country"> </asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlSearchOperator" runat="server" class="FormItem">
                                                                    <asp:ListItem Value="begins with">Begins With</asp:ListItem>
                                                                    <asp:ListItem Value="ends with">Ends With</asp:ListItem>
                                                                    <asp:ListItem Value="contains">Contains</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtSearchString" runat="server" class="FormItem"></asp:TextBox>
                                                                <asp:Button ID="btnFilter" runat="server" Text="Filter" class="Btn_Form filterbtn"
                                                                    CausesValidation="false" OnClick="btnFilter_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!--  Filter Section End -  In Accounting Code Popup -->
                                                <asp:GridView ID="grvPeachtreeVendors" runat="server" AutoGenerateColumns="false"
                                                    DataKeyNames="ID" ShowHeader="true" AllowPaging="true" AllowSorting="true" PageSize="10"
                                                    Width="100%" EmptyDataText="No records found." EmptyDataRowStyle-CssClass="GridEmptyRow"
                                                    CssClass="TableBorder" OnPageIndexChanging="grvPeachtreeVendors_PageIndexChanging"
                                                    OnSorting="grvPeachtreeVendors_OnSorting" PagerSettings-Mode="NumericFirstLast">
                                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="25px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                            <ItemTemplate>
                                                                <mo:GroupRadioButton ID="selectedRadioButton" runat="server" GroupName="peachTreeCode" />
                                                                <asp:HiddenField ID="hdnGUID" runat="server" Value='<%# Eval("GUID")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                        <asp:BoundField DataField="ID" HeaderText="Code" SortExpression="ID" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                        <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country"
                                                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 10px" align="center">
                                            <asp:Button ID="btnCancelAccountingCode" runat="server" Text="Cancel" class="Btn_Form"
                                                CausesValidation="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSelectAccCode" runat="server" Text="Update" CausesValidation="false"
                                                class="Btn_Form" OnClick="btnSelectAccCode_Click" ToolTip="Click here to update the selected accounting code for this payment." />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <!-- Popup End - for Accounting Code  -->
                        </td>
                        <td class="GridContent" width="25%">
                            <b>Check Number</b>
                        </td>
                        <td class="GridContent" width="25%">
                            <asp:Label ID="lblCheckNumber" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                       <%-- <td class="GridContent" width="25%">
                            <b>Amount</b>
                        </td>
                        <td class="GridContent" width="25%">
                            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hdnAmount" runat="server" />
                        </td>--%>
                        <td class="GridContent" width="25%">
                            <b>Check Date</b>
                        </td>
                        <td class="GridContent"  width="25%">
                            <asp:Label ID="lblCheckDate" runat="server" Text=""></asp:Label>
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
                            <b>Bank Name</b>
                        </td>
                        <td class="GridContent" width="25%">
                            <asp:Label ID="lblBankName" runat="server" Text=""></asp:Label>
                        </td>
                           <td class="GridContent" width="25%">
                            <b>Account Number</b>
                        </td>
                        <td class="GridContent" width="25%">
                            <asp:Label ID="lblAccountNumber" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                     
                        <td class="GridContent" width="25%">
                            <b>Comments</b>
                        </td>
                        <td class="GridContent" colspan="3">
                            <asp:Label ID="lblComments" runat="server" Text=""></asp:Label>
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
                <asp:UpdatePanel ID="updPnlPaymentAdjustment" runat="server">
                    <ContentTemplate>
                        <fieldset class="ForFieldSet">
                            <legend class="ForLegend">Payment Adjustment</legend>
                        </fieldset>
                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                            <tr>
                                <td class="GridContent" width="25%">
                                    <b>Payment Amount</b>
                                </td>
                                <td class="GridContent" width="25%">
                                    <asp:TextBox ID="txtPaymentAmount" CssClass="txt2" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtPaymentAmount_TextChanged"></asp:TextBox>
                                           <ajax:FilteredTextBoxExtender ID="txtPaymentAmount_FilteredTextBoxExtender" runat="server"
                                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtPaymentAmount">
                                                        </ajax:FilteredTextBoxExtender>
                                </td>
                                <td class="GridContent" width="25%">
                                    <b>Selected Expenses Amount</b>
                                </td>
                                <td class="GridContent"  width="25%">
                                    <asp:Label ID="lblTotalSelExpAmount"   runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent" width="25%">
                                    <b>Current Outstanding</b>
                                </td>
                                <td class="GridContent" width="25%">
                                    <asp:Label ID="lblCurrentOutstanding" runat="server"></asp:Label>
                                </td>
                                <td class="GridContent" width="25%">
                                    <b>Previous Outstanding Amount(if any)</b>
                                </td>
                                <td class="GridContent" width="25%">
                                    <asp:Label ID="lblPrevOutstandingAmount" runat="server"></asp:Label>
                                </td>
                            </tr>
                             <tr class="TableHeadingBg">
                                <td class="GridContent" width="25%">
                                    <b>Total Outstanding Amount (if any)</b>
                                </td>
                                <td class="GridContent" colspan="3">
                                    <asp:Label ID="lblTotalOutstanding" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="GridContent">
                <asp:Button ID="btnApplySelectedExpenses" runat="server" Text="Apply Payment" class="Btn_Form"
                    OnClick="btnApplySelectedExpenses_Click" Visible="false" />
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
                    class="arial-12" id="selectedExpensesTable" runat="server">
                    <tr>
                        <td class="TableHeadingBg TableHeading">
                            Selected Expenses
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder">
                            <asp:GridView ID="grvSelectedExpense" runat="server" DataKeyNames="ObjectId" AutoGenerateColumns="false"
                                ShowHeader="true" AllowPaging="False" PageSize="10" Width="100%" OnRowCommand="grvOpenExpenses_RowCommand"
                                OnPageIndexChanging="grvSelectedExpense_PageIndexChanging" OnRowDataBound="grvSelectedExpense_RowDataBound"
                                EmptyDataText="No record found." EmptyDataRowStyle-CssClass="GridEmptyRow">
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
                                    <asp:BoundField DataField="VIN" HeaderText="VIN Number" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent"></asp:BoundField>
                                    <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnExpenseAmount" runat="server" Value='<%# Eval("ExpenseAmount")%>' />
                                            <asp:ImageButton ID="btnDelete" CommandArgument='<%# Eval("ObjectId")%>' OnClick="btnDelete_OnClick"
                                                OnClientClick="return confirm('Are you sure you want to remove this record?');"
                                                runat="server" CausesValidation="false" ToolTip="Click here to remove the record from the list."
                                                ImageUrl="~/Images/DeleteButton.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <table class="arial-12" cellpadding="0" border="0" width="100%" style="border-collapse: collapse;">
                                <tr>
                                    <td class="TableBorder GridHeader" style="text-align: right; width: 214px;">
                                        Total
                                    </td>
                                    <td class="TableBorder GridHeader">
                                        <b>
                                            <asp:Label ID="lblExpenseTotal" runat="server" Text=""></asp:Label></b>
                                        <asp:HiddenField ID="hdnExpenseTotal" runat="server" Value="0.0" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <%--<tr>
            <td class="height30" align="right">
                <asp:Button ID="btnApplySelectedExpenses" runat="server" Text="Apply Selected Expenses"
                    class="Btn_Form" OnClick="btnApplySelectedExpenses_Click" Visible="false" />
            </td>
        </tr>--%>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                    class="arial-12" id="openExpensesTable" runat="server">
                    <tr>
                        <td class="TableHeadingBg TableHeading" colspan="2">
                            <div style="float: left; width: 300px;">
                                Open Expenses</div>
                            <div style="float: right; width: 300px; text-align: right; padding-right: 5px; text-transform: capitalize;">
                                <asp:LinkButton ID="btnSelectAll" runat="server" CssClass="BlackTxt_Link" OnClick="btnSelectAll_Click"
                                    ToolTip="Select/Unselect from open expenses list" Text="Select All"> </asp:LinkButton>
                                &nbsp;&nbsp;|&nbsp;&nbsp;
                                <asp:LinkButton ID="btnAddToSelectedList" runat="server" CssClass="BlackTxt_Link"
                                    OnClick="btnAddToSelectedList_Click" ToolTip="Click here add the selected expense(s) from open expense(s) list to selected expense(s) list.">Add 
                                To Selected Expenses</asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" colspan="2">
                            <asp:GridView ID="grvOpenExpenses" runat="server" DataKeyNames="ObjectId" AutoGenerateColumns="false"
                                ShowHeader="true" AllowPaging="false" PageSize="10" Width="100%" OnRowCommand="grvOpenExpenses_RowCommand"
                                OnPageIndexChanging="grvOpenExpenses_PageIndexChanging" EmptyDataText="No record found."
                                EmptyDataRowStyle-CssClass="GridEmptyRow">
                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="55px" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnExpenseId" runat="server" Value='<%# Eval("ObjectId").ToString()%>' />
                                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false" OnCheckedChanged="chkSelect_OnCheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataFormatString="{0:MM/dd/yyyy}" DataField="ExpenseDate" HeaderText="Expense Date"
                                        HeaderStyle-Width="100px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" HeaderStyle-CssClass="GridHeader"
                                        HeaderStyle-Width="105px" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                    <asp:BoundField DataField="ExpenseAmount" DataFormatString="{0:C}" HeaderText="Amount"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="70px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VIN" HeaderText="VIN Number" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent"></asp:BoundField>
                                    <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn" runat="server" ImageUrl="~/Images/Preview.gif" ToolTip="Preview" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

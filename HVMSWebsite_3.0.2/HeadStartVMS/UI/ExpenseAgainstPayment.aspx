<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpenseAgainstPayment.aspx.cs"
    Inherits="METAOPTION.UI.ExpenseAgainstPayment" Title="HeadStart VMS :: Expense Against Payment"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        function showProgressMsg() {
            document.getElementById('ctl00_ContentPlaceHolder1_imgProgress').style.display = "";
        }

        function showWaitMsg() {
            var ctrl = document.getElementById('ctl00_ContentPlaceHolder1_btnRefreshVendorsFromPeachtree');
            ctrl.src = "../Images/Wait.gif";
            ctrl.style.height = "16px";
            ctrl.style.width = "16px";
        }

        function validateUpdatePrint() {
            var ctrl = document.getElementById('ctl00_ContentPlaceHolder1_chkIsPrinted');

            if (!ctrl.checked)
                alert('Checkbox is not selected');
        }

        function OpenWindow() {
            var paymentId = document.getElementById('<%=hfPaymentId.ClientID %>').value;
            //alert(paymentId);

            window.open("PrintPreview_ExpenseAgainstPayment.aspx?paymentid=" + paymentId, 'ExpenseAgainstPayment_Print', 'height=600,width=900,scrollbars=1');

        }


        
    </script>
    <asp:UpdatePanel ID="upExpenseAgainstPayment" runat="server">
        <ContentTemplate>
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                <tr>
                    <td>
                        <asp:HiddenField ID="hfPaymentId" runat="server" />
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
                                    <asp:LinkButton ID="lnkRecipientName" runat="server" OnClick="lnkRecipientName_Click"
                                        CausesValidation="false">
                                        <asp:Label ID="lblRecipientName" runat="server" Text=""></asp:Label>
                                    </asp:LinkButton>
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
                                    <b>Account Number</b>
                                </td>
                                <td class="GridContent" width="25%">
                                    <asp:Label ID="lblAccountNumber" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="GridContent" width="25%">
                                    <b>Bank Name</b>
                                </td>
                                <td class="GridContent" width="25%">
                                    <asp:Label ID="lblBankName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent" width="25%">
                                    <b>Added By</b>
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
                                    <b>Comments</b>
                                </td>
                                <td class="GridContent" width="75%" colspan="3">
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
                                <td class="GridContent" width="25%">
                                    <b>Accounting Code</b>
                                </td>
                                <td class="GridContent" width="75%" colspan="3">
                                    <asp:Label ID="lblAccountingCode" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hdnGUID" runat="server" />
                                    <asp:LinkButton ID="lnkSelectAccountingCode" runat="server" ToolTip="Change the accounting code">Change</asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkUpdatePeachtree" runat="server" ToolTip="Update this payment in Peachtree"
                                        OnClick="lnkUpdatePeachtree_Click" CausesValidation="false" OnClientClick="javascript:showProgressMsg()">Update Peachtree</asp:LinkButton>
                                    <asp:Image ID="imgProgress" runat="server" ImageUrl="../Images/Wait.gif" Style="width: 13px;
                                        height: 13px; display: none;" />
                                    <asp:Label ID="lblPeachtreeMsg" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent" width="25%">
                                    <b>Check Print Status</b>
                                </td>
                                <td class="GridContent" width="75%" colspan="3">
                                    <asp:CheckBox ID="chkIsPrinted" runat="server" />
                                    &nbsp;&nbsp;<asp:LinkButton ID="lnkUpdatePrintStatus" runat="server" OnClick="lnkUpdatePrintStatus_Click"
                                        Visible="false" ToolTip="Click here to update this print status." OnClientClick="validateUpdatePrint();"
                                        CausesValidation="false">Update</asp:LinkButton>
                                    &nbsp;&nbsp;<asp:LinkButton ID="lnkPrintCheck" runat="server" ToolTip="Click here to print this check."
                                        OnClick="lnkPrintCheck_Click" Visible="false" CausesValidation="false">Print Check</asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkPrintCheckVersion1" runat="server" ToolTip="Click here to print this check."
                                        OnClick="lnkPrintCheckVersion1_Click" CausesValidation="false" Visible="false">Print Check v1.0</asp:LinkButton>
                                    <%-- <b>( <asp:Label ID="lblPrintStatus" runat="server" Text=""></asp:Label> )</b>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent" width="25%">
                                    <b>Void Status</b>
                                </td>
                                <td class="GridContent" width="25%" colspan="3">
                                    <b>
                                        <asp:Label ID="lblVoidStatus" runat="server" Text=""></asp:Label></b> &nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkVoidPayment" runat="server" ToolTip="Click here to void this payment."
                                        OnClick="lnkVoidPayment_Click" OnClientClick="javascript:return confirm('Are you sure you want to void this payment?');"
                                        CausesValidation="false">Void Payment</asp:LinkButton>
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
                    <td class="height30" align="right">
                        <asp:Button ID="btnVoidPayment" runat="server" Text="Button" Style="display: none"
                            CausesValidation="false" />
                        <ajax:ModalPopupExtender ID="mpeVoidPayment" runat="server" TargetControlID="btnVoidPayment"
                            PopupControlID="pnlVoidPayment" BackgroundCssClass="modalBackground" DropShadow="False"
                            CancelControlID="btnCancel" PopupDragHandleControlID="pnlHeader">
                        </ajax:ModalPopupExtender>
                        <asp:Panel runat="server" ID="pnlVoidPayment" Style="display: none; background: #f0f0f0;">
                            <table border="0" width="400" cellpadding="0" class="PopUpBox">
                                <tr>
                                    <td class="PopUpBoxHeading" align="left">
                                        Void Comment
                                    </td>
                                    <td class="PopUpBoxHeading" align="right">
                                        <asp:ImageButton ID="btnClosePopup" runat="server" CausesValidation="false" ImageUrl="~/Images/close.gif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="padding: 10px">
                                        <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse"
                                            class="Nornmal-Arial-12">
                                            <tr>
                                                <td class="GridContent_padding5" valign="top">
                                                    <b>Void Reason</b>
                                                </td>
                                                <td class="GridContent_padding5">
                                                    <asp:DropDownList ID="ddlVoidReason" runat="server" class="FormItem" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0" Text="Select One "></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvVoidReason" runat="server" ControlToValidate="ddlVoidReason"
                                                        ErrorMessage="Select a valid void reason." Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="GridContent_padding5" valign="top">
                                                    <b>Comments</b>
                                                </td>
                                                <td class="GridContent_padding5">
                                                    <asp:TextBox ID="txtVoidComments" runat="server" class="FormItem" TextMode="MultiLine"
                                                        Rows="6" Columns="30" MaxLength="250" Style="width: 250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding: 10px" align="center">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="Btn_Form" CausesValidation="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCommitVoidPayment" runat="server" Text="Submit" class="Btn_Form"
                                            OnClick="btnCommitVoidPayment_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
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
                                            <asp:Label ID="lblError" runat="server" Text="" Style="color: Red; font-weight: bold;"></asp:Label>
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
                                                        <%-- <div ID="pleaseWaitDiv" runat="server"  style="position:absolute; left:50%; top:50%; padding:4px 10px 4px 10px; border:2px solid #bbdef1; background:#ffffff; vertical-align:middle;">
                                                            <asp:Image ID="imgWait" runat="server" ImageUrl="~/Images/Wait.gif" style="vertical-align:middle;"  /> <span><b>Please Wait ...</b></span>
                                                        </div>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!--  Filter Section End -  In Accounting Code Popup -->
                                            <asp:GridView ID="grvPeachtreeVendors" runat="server" AutoGenerateColumns="false"
                                                GridLines="None" DataKeyNames="ID" ShowHeader="true" AllowPaging="true" AllowSorting="true"
                                                PageSize="10" Width="100%" EmptyDataText="No records found." EmptyDataRowStyle-CssClass="GridEmptyRow"
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
                                    Expenses &nbsp;&nbsp;
                                    <asp:ImageButton ID="imgPrintPreview" runat="server" CausesValidation="false" AlternateText="Print Preview"
                                        ImageUrl="~/Images/preview.gif" OnClientClick="OpenWindow();" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    <asp:GridView ID="grvSelectedExpense" runat="server" DataKeyNames="ExpenseId" AutoGenerateColumns="false"
                                        ShowHeader="true" AllowPaging="true" PageSize="250" Width="100%" EmptyDataText="No record found."
                                        EmptyDataRowStyle-CssClass="GridEmptyRow" CssClass="Grid" ShowFooter="true" OnRowDataBound="grvSelectedExpense_RowDataBound"
                                        OnPageIndexChanging="grvSelectedExpense_PageIndexChanging">
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
                                                    <asp:HiddenField ID="hdnExpenseAmount" runat="server" Value='<%# Eval("ExpenseAmount")%>' />
                                                    <asp:LinkButton ID="lnkVIN" CommandArgument='<%# Eval("InventoryId")%>' OnClick="lnkVIN_Click"
                                                        runat="server" Text='<%# Eval("VIN")%>' CausesValidation="false"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-CssClass="GridHeader"
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
                                        ShowHeader="true" AllowPaging="true" PageSize="250" Width="100%" EmptyDataText="No record found."
                                        EmptyDataRowStyle-CssClass="GridEmptyRow" CssClass="Grid" OnPageIndexChanging="grvVoidedExpense_PageIndexChanging">
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
                                                    <asp:LinkButton ID="lnkVIN" CommandArgument='<%# Eval("InventoryID")%>' OnClick="lnkVIN_Click"
                                                        runat="server" Text='<%# Eval("VIN")%>' CausesValidation="false"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkPrintCheckVersion1" />
            <asp:PostBackTrigger ControlID="imgPrintPreview" />
        </Triggers>
    </asp:UpdatePanel>
</asp:content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryExpense.aspx.cs"
    EnableEventValidation="false" Inherits="METAOPTION.UI.InventoryExpense" Title="Inventory Expense" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validateAmount(sender, args) {
            var txt = (args.Value);
            var startindex = txt.indexOf(".")
            var lastindex = txt.lastIndexOf(".")

            var startindexMinus = txt.indexOf("-")
            var lastindexMinus = txt.lastIndexOf("-")

            if (startindexMinus != lastindexMinus) {

                args.IsValid = false;
                return;
            }


            if (startindex != lastindex) {

                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }
    </script>
    <%--  <asp:UpdatePanel ID="upnlExpense" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddExpense" />
        </Triggers>
        <ContentTemplate>--%>
    <%--<div class="AddHeading">
        <asp:Label ID="lblInventoryHeader" runat="server" Text=""></asp:Label>
    </div>--%>
    <asp:HiddenField ID="hfSelectedCount" runat="server" Value="0" />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="AddHeading" style="width: 85%">
                <asp:LinkButton ID="lblInventoryHeader" runat="server" OnClick="lblInventoryHeader_Click"
                    CausesValidation="false"></asp:LinkButton>
                <%-- <asp:Label ID="lblInventoryHeader" runat="server" Text=""></asp:Label>--%>
            </td>
            <td>
                <asp:ImageButton ID="ibtncars1" runat="server" ImageUrl="~/Images/car_icon.gif" CausesValidation="false"
                    OnClick="ibtncars1_Click" />
            </td>
        </tr>
    </table>
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td align="left">
                <asp:GridView ID="gvExpenses" runat="server" Width="100%" DataKeyNames="RowId" AllowPaging="true"
                    PageSize="20" AutoGenerateColumns="False" EmptyDataText="No Rows found" OnRowDataBound="gvExpenses_RowDataBound"
                    OnPageIndexChanging="gvExpenses_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="22px" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkall" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false" /><%-- OnCheckedChanged="chkSelect_OnCheckedChanged"--%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent">
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle CssClass="GridContent"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" HeaderText="Name" ItemStyle-Wrap="false"
                            ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpylnkOpenEntityPage" Text='<%#Eval("EntityName")%>' ToolTip='<%#Eval("EntityId")%>'
                                    runat="server"></asp:HyperLink>
                                <asp:ImageButton ID="ibtnPreExpDetail" runat="server" ImageUrl="~/Images/mobile.png"
                                    CausesValidation="false" OnClick="ibtnPreExpDetail_Click" />
                                <asp:HiddenField ID="hdEntityTypeId" runat="server" Value='<%#Eval("EntityTypeId")%>' />
                                <asp:HiddenField ID="hdExpenseId" runat="server" Value='<%#Eval("RowID")%>' />
                                <asp:HiddenField ID="hdEntityId" runat="server" Value='<%#Eval("EntityId")%>' />
                                <asp:HiddenField ID="hfPreExpID" runat="server" Value='<%#Eval("PreExpenseID") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle Wrap="False" CssClass="GridContent"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpenseAmount" HeaderText="Exp. Amount" DataFormatString="{0:C}"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Exp. Type" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnExpenseTypeId" runat="server" Value='<%# Eval("ExpenseTypeId") %>' />
                                <asp:Label ID="lblExpenseTyp" Text='<%#Eval("ExpenseType")%>' runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkShowBuyerCalculation" Text='<%#Eval("ExpenseType")%>' ToolTip="Display Commission Calculation Information"
                                    runat="server"></asp:LinkButton>
                                <asp:Panel ID="pnlShowCommissionDetails" Style="display: none;" runat="server" CssClass="modalPopup"
                                    Width="700">
                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                        <tr>
                                            <td class="PopUpBoxHeading">
                                                &nbsp;<asp:Label ID="lblHeaderInventoryInfo" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="PopUpBoxHeading" align="right">
                                                <asp:ImageButton ID="imgCloseBuyerCalcPopUp" runat="server" CausesValidation="false"
                                                    ImageUrl="../Images/close.gif" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:FormView ID="frmBCommissionCalculationDetails" runat="server" DataKeyNames="BuyerCommissionId"
                                                    Width="100%" OnDataBound="frmBCommissionCalculationDetails_DataBound" DataSourceID="objBuyerCommissionCalculation">
                                                    <ItemTemplate>
                                                        <table border="0" cellpadding="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                            <tr>
                                                                <td class="TableBorderB" width="25%">
                                                                    Buyer Name
                                                                </td>
                                                                <td class="TableBorder" width="75%">
                                                                    <%# Eval("BuyerName")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB" width="25%">
                                                                    Commission Type
                                                                </td>
                                                                <td class="TableBorder" width="75%">
                                                                    <asp:Label ID="lblCommissionTypeId" Width="1%" Text='<%# Eval("CommissionTypeId")%>'
                                                                        runat="server"></asp:Label>
                                                                    <%# Eval("CommissionType")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Formulae Description
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:Label ID="lblCommissionRuleDesc" Text='<%# Eval("Description")%>' runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr5050" runat="server">
                                                                <td colspan="2">
                                                                    <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                                                                        <tr>
                                                                            <td class="TableBorderB" width="25%">
                                                                                Deposit Amount
                                                                            </td>
                                                                            <td class="TableBorder" width="25%">
                                                                                <%# String.Format("{0:C}",Eval("DepositAmount")?? "&nbsp;&nbsp;")%>
                                                                            </td>
                                                                            <td class="TableBorderB" width="25%">
                                                                                Car Cost (expense amt)
                                                                            </td>
                                                                            <td class="TableBorder" width="25%">
                                                                                <%# String.Format("{0:C}", Eval("ExpenseAmount") ?? "&nbsp;&nbsp;")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB" nowrap="nowrap">
                                                                                <asp:Label ID="lblExpensesText" runat="server" Text="Expenses"></asp:Label>
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <%# String.Format("{0:C}", Eval("TotalInventoryExpense") ?? "&nbsp;&nbsp;")%>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Title Fee
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <%# String.Format("{0:C}", Eval("Title_Fee") ?? "&nbsp;&nbsp;")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="trReconFee" runat="server">
                                                                            <td class="TableBorderB">
                                                                                Recon Fee
                                                                            </td>
                                                                            <td class="TableBorder" colspan="3">
                                                                                <%# Eval("Recon_fee") ?? "&nbsp;&nbsp;"%>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                                                    <td class="TableBorderB">
                                                                                                        Recon Fee
                                                                                                    </td>
                                                                                                    <td class="TableBorder" colspan="3">
                                                                                                        <%# Eval("Recon_fee")?? "&nbsp;&nbsp;"%>
                                                                                                    </td>
                                                                                                </tr>--%>
                                                                        <tr id="tr5050IIndLevelComm" runat="server">
                                                                            <td class="TableBorderB">
                                                                                First Level Commission Buyer (if any)
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <%# Eval("FirstCommission_BuyerId_5050Split") ?? "&nbsp;&nbsp;"%>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Other Buyer Commission (if any)
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <%# String.Format("{0:C}", Eval("SecondBuyerCommission_5050Split") ?? "&nbsp;&nbsp;")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                IInd Level Buyer (if any)
                                                                            </td>
                                                                            <td class="TableBorder" colspan="3">
                                                                                <%# Eval("SecondCommission_BuyerId") ?? "&nbsp;&nbsp;"%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="trGrossProfit" runat="server">
                                                                <td colspan="2">
                                                                    <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                                                                        <tr>
                                                                            <td class="TableBorderB" width="25%">
                                                                                Sold Price
                                                                            </td>
                                                                            <td class="TableBorder" width="25%">
                                                                                <%# String.Format("{0:C}", Eval("SoldPrice") ?? "&nbsp;&nbsp;")%>
                                                                            </td>
                                                                            <td class="TableBorderB" width="25%">
                                                                                Car Cost (expense amount)
                                                                            </td>
                                                                            <td class="TableBorder" width="25%">
                                                                                <%# String.Format("{0:C}", Eval("ExpenseAmount") ?? "&nbsp;&nbsp;")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Min
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <%# Eval("Min_Gross")?? "&nbsp;&nbsp;"%>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Min Value
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <%# Eval("MinValue_Gross")?? "&nbsp;&nbsp;"%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Max
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <%# Eval("Max_Gross")?? "&nbsp;&nbsp;"%>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Max Value
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <%# Eval("MaxValue_Gross")?? "&nbsp;&nbsp;"%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="trFixedCommission" runat="server">
                                                                <td class="TableBorderB">
                                                                    Fixed Commission
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <%# String.Format("{0:C}", Eval("FixedCommission") ?? "&nbsp;&nbsp;")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Buyer Commission Amount
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    <%# String.Format("{0:C}", Eval("CommissionAmount") ?? "&nbsp;&nbsp;")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:FormView>
                                                <asp:ObjectDataSource ID="objBuyerCommissionCalculation" runat="server" SelectMethod="GetBuyerComm_CalculationInfo"
                                                    TypeName="METAOPTION.BAL.BuyerBAL">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hdEntityId" Name="BuyerId" PropertyName="Value"
                                                            Type="Int64" />
                                                        <asp:ControlParameter ControlID="hdExpenseId" Name="expenseId" PropertyName="Value"
                                                            Type="Int64" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajax:ModalPopupExtender ID="MPEBuyerCommCalculation" runat="server" TargetControlID="lnkShowBuyerCalculation"
                                    PopupControlID="pnlShowCommissionDetails" CancelControlID="imgCloseBuyerCalcPopUp"
                                    PopupDragHandleControlID="pnlShowCommissionDetails" DropShadow="false" BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>
                                <headerstyle cssclass="GridHeader"></headerstyle>
                                <itemstyle cssclass="GridContent"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpenseTypeId" HeaderText="Expense Type" />
                        <asp:TemplateField HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader" HeaderText="Check No."
                            ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:HyperLink ID="hylnk" runat="server" Text='<%#Eval("CheckNo")%>' NavigateUrl='<%#"~/UI/ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId")%>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="GridHeader" Width="80px"></HeaderStyle>
                            <ItemStyle CssClass="GridContent"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CheckPaid" HeaderText="Paid" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent">
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="Comment" HeaderText="Comments" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent">
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle CssClass="GridContent"></ItemStyle>
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnExpenseId" runat="server" Value='<%#Eval("RowID")%>' />
                                <asp:HiddenField ID="hdnComment" runat="server" Value='<%#Eval("Comment")%>' />
                                <asp:HiddenField ID="hdnInvoiceNumber" runat="server" Value='<%#Eval("InvoiceNo")%>' />
                                <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EntityTypeId" HeaderText="EntityType Id"  />
                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="false" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                             <asp:HiddenField ID="hdExpenseAmount" runat="server" Value='<%#Eval("ExpenseAmount")%>'>
                                </asp:HiddenField>
                                <asp:ImageButton ID="imbEditExp" CausesValidation="false" ImageUrl="~/Images/edit-icon.jpg"
                                    runat="server" OnClick="imbEditExp_Click" ToolTip='<%#Eval("AddedModifiedBy")%>' />
                                <asp:ImageButton ID="imgDelete" CausesValidation="false" ImageUrl="~/Images/DeleteButton.jpg"
                                    runat="server" OnClick="imgDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this expense?\n\nClick Ok to Confirm\nClick Cancel to ignore');" />
                                <asp:ImageButton ID="ibtncars" runat="server" ImageUrl="~/Images/car_icon.gif" CausesValidation="false"
                                    OnClick="ibtncars_Click" />
                                <%-- <asp:Label ID="lblAddedByModifiedBy" runat="server" Text  ='<%#Eval("AddedModifiedBy")%>' />--%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle Wrap="False" CssClass="GridContent"></ItemStyle>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdExpenseAmount" runat="server" Value='<%#Eval("ExpenseAmount")%>'>
                                </asp:HiddenField>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                    <HeaderStyle CssClass="gvHeading" />
                    <RowStyle CssClass="gvRow" />
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellpadding="0" style="width: 98%; padding-left: 10px;">
                    <tr>
                        <td align="right">
                            <asp:LinkButton ID="lnkAddNewExpense" runat="server" CausesValidation="false" CssClass="AddNewExpenseTxt"
                                OnClick="lnkAddNewExpense_Click">
                             <img src="../Images/AddNew.gif"  alt="Add New" style="border:none; padding-top:10px" /> 
                             Add New Expense
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnAddInvoice" runat="server" Width="120px" CssClass="AddNewExpenseTxt"
                                CausesValidation="false" OnClientClick="return ShowAddInvoiceNoPopup();">
                                 <img src="../Images/AddNew.gif"  alt="Add New" style="border:none; padding-top:10px" /> 
                                 Add Invoice No
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dvLinkedCars" runat="server" visible="false" class="AddHeading">
                    Linked Cars
                </div>
                <asp:GridView runat="server" Width="100%" ID="gvLinkedFilter" AllowPaging="True"
                    PageSize="5" AutoGenerateColumns="False" OnPageIndexChanging="gvLinkedFilter_PageIndexChanging"
                    OnRowDataBound="gvLinkedFilter_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Date Added" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:HyperLink ID="hylnk" ToolTip="Show Expenses" runat="server" Text='<%# String.Format("{0:MM/dd/yyyy}", Eval("DateAdded") ?? "&nbsp;")%>'
                                    NavigateUrl='<%#"~/UI/InventoryExpense.aspx?Code="+Eval("InventoryId")%>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle CssClass="GridContent"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Linked Cars" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:HyperLink ID="hylnkVIN" ToolTip="Show Inventory Details" runat="server" Text='<%#Eval("VIN")%>'
                                    NavigateUrl='<%#"~/UI/InventoryDetail.aspx?Code="+Eval("InventoryId")%>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle CssClass="GridContent"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DealerName" HeaderText="Dealer" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent">
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle CssClass="GridContent"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent">
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle CssClass="GridContent"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField SortExpression="CheckNumber" HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader"
                            HeaderText="Check No." ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:HyperLink ID="hylnkCHKNO" runat="server" Text='<%#Eval("CheckNumber")%>' NavigateUrl='<%#"~/UI/ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId")%>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="GridHeader" Width="80px"></HeaderStyle>
                            <ItemStyle CssClass="GridContent"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Amount" HeaderText="Car Cost" DataFormatString="{0:C}&nbsp;"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SoldDate" HeaderText="Sold On" DataFormatString="{0:MM/dd/yyyy}"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <ItemStyle CssClass="GridContent"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                    <HeaderStyle CssClass="gvHeading" />
                    <RowStyle CssClass="TableBorder" />
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 98%; padding: 10px 0px;">
                    <tr>
                        <td>
                            <asp:HyperLink ID="hlnkBack" runat="server"  CssClass="btn" Style="padding: 3px 4px;
                                text-decoration: none" Width="60px" Text="<< Back" />
                            <%--<asp:LinkButton ID="lnkbtnBack" runat="server" OnClick="lnkbtnBack_Click" CausesValidation="false">
                                <img src="../Images/back.jpg" alt="back" style="border:none; padding-top:10px" />
                            </asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblAddExpense" width="80%" runat="server" style="display: none;" class="modalPopup"
        border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="PopUpBoxHeading">
                &nbsp;&nbsp;<asp:Label ID="lblHeading" runat="server"></asp:Label>
            </td>
            <td class="PopUpBoxHeading" align="right">
                <img border="0" src="../Images/close.gif" onclick="$find('mdpopExpense').hide();return false;"
                    alt="" />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" style="padding: 10px">
                <table border="0" cellspacing="0" cellpadding="0" style="border-collapse: collapse;
                    width: 100%;" class="Nornmal-Arial-12">
                    <tr id="trType" runat="server">
                        <td nowrap="nowrap" class="GridContent_padding5">
                            <b>Entity Type</b>
                        </td>
                        <td class="GridContent_padding5">
                            <asp:DropDownList ID="ddlEntityType" AutoPostBack="true" runat="server" CssClass="txt2"
                                OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="Vendor" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Buyer" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Dealer" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdExpenseUpId" runat="server" />
                        </td>
                    </tr>
                    <tr id="trEntityName" runat="server">
                        <td class="GridContent_padding5">
                            <b>Name</b>
                        </td>
                        <td class="GridContent_padding5">
                            <asp:Label ID="lblEntityName" CssClass="txtMan2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trVendor" runat="server">
                        <td class="GridContent_padding5" nowrap="nowrap">
                            <b>Entity Name</b>
                        </td>
                        <td class="GridContent_padding5">
                            <asp:UpdatePanel ID="updPnlEntity" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlEntityType" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlEntity" AppendDataBoundItems="true" runat="server" CssClass="txt2">
                                        <asp:ListItem Text="" Value="-1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvEntity" runat="server" InitialValue="-1" ErrorMessage="*"
                                        ControlToValidate="ddlEntity">
                                    </asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" class="GridContent_padding5">
                            <b>Cost</b>
                        </td>
                        <td class="GridContent_padding5">
                            <asp:TextBox ID="txtExpenseAmount" size="20" MaxLength="14" runat="server" CssClass="txt2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="*" ControlToValidate="txtExpenseAmount">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvExpenseAmount" runat="server" ControlToValidate="txtExpenseAmount"
                                ErrorMessage="Invalid Amount" ClientValidationFunction="validateAmount"></asp:CustomValidator>
                            <ajax:FilteredTextBoxExtender ID="txtExpenseAmount_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers,Custom" ValidChars=".,-" TargetControlID="txtExpenseAmount">
                            </ajax:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" class="GridContent_padding5">
                            <b>Expense Type</b>
                        </td>
                        <td class="GridContent_padding5">
                            <asp:UpdatePanel ID="updPnlExpenseType" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlEntityType" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlExpenseType" AppendDataBoundItems="true" runat="server"
                                        DataTextField="ExpenseType" DataValueField="ExpenseTypeId" CssClass="txt2">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="objExpenseTypes" runat="server" SelectMethod="GetExpenses"
                                        TypeName="METAOPTION.BAL.InventoryBAL"></asp:ObjectDataSource>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="GridContent_padding5" nowrap="nowrap">
                            <b>Expense Date</b>
                        </td>
                        <td class="GridContent_padding5" nowrap="nowrap">
                            <asp:TextBox ID="txtExpenseDate" runat="server" CssClass="txt2"> 
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                ErrorMessage="*" ControlToValidate="txtExpenseDate">
                            </asp:RequiredFieldValidator>
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgArrivalDate"
                                TargetControlID="txtExpenseDate">
                            </ajax:CalendarExtender>
                            <asp:Image ID="imgArrivalDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                Style="cursor: pointer;" />
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" class="GridContent_padding5">
                            <b>Invoice Number</b>
                        </td>
                        <td class="GridContent_padding5">
                            <asp:TextBox ID="txtInvoice" runat="server" CssClass="txtMan2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" class="GridContent_padding5">
                            <b>Comments</b>
                        </td>
                        <td class="GridContent_padding5">
                            <asp:TextBox ID="txtComments" runat="server" CssClass="txtMan2" TextMode="MultiLine"
                                Rows="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnExpenseCancel" runat="server" Text="Cancel" CssClass="Btn_Form"
                                OnClientClick="$find('mdpopExpense').hide();return false;" Width="75px" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAddExpense" runat="server" Text="Save" CssClass="Btn_Form" Width="75px"
                                OnClick="btnAddExpense_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="Btn_Form" Width="75px"
                                OnClick="btnContinue_Click" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="divMsg" runat="server" class="err">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="display: none; width: 1px;">
        <asp:Button ID="btnpopupOpener" runat="server" />
        <asp:HiddenField ID="hdUpdateDriverId" runat="server" />
    </div>
    <ajax:ModalPopupExtender ID="MPEAddExpense" BehaviorID="mdpopExpense" runat="server"
        TargetControlID="btnpopupOpener" PopupControlID="tblAddExpense" BackgroundCssClass="modalBackground"
        DropShadow="true" CancelControlID="btnExpenseCancel" />
    <%----------Pre-Expense Detail Popup----------%>
    <div id="dvPreExpDetail" runat="server" class="modalPopup" style="display: none;
        width: 650px; min-height: 150px">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td class="PopUpBoxHeading">
                    &nbsp;&nbsp;Pre-Expense Details
                </td>
                <td class="PopUpBoxHeading" align="right">
                    <img border="0" src="../Images/close.gif" id="imgClosePreExpDetail" alt="close" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPreExpDetail" runat="server" Width="100%" AutoGenerateColumns="false"
            GridLines="None" ShowHeader="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>VIN</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("VIN") %>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Expense Date</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("ExpenseDate")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Count</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("Count")%>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Sync Date</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("SyncDate")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Default Price($)</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("DefaultPrice")%>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Approved By</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("AddedBy")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Total Price($)</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("TotalPrice")%>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Added By</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("EntityName")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Description</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("Description")%>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Device</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <%#Eval("DeviceName")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Approval Note</b>
                                </td>
                                <td class="GridContent_padding5" colspan="3">
                                    <%#Eval("ApprovalNote")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:HiddenField ID="hfPreExp" runat="server" />
    <ajax:ModalPopupExtender ID="mpePreExpDetail" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="hfPreExp" PopupControlID="dvPreExpDetail" CancelControlID="imgClosePreExpDetail">
    </ajax:ModalPopupExtender>
    <%----------Pre-Expense Detail Popup----------%>
    <%----------Images----------%>
    <asp:HiddenField ID="hfPopup" runat="server" />
    <ajax:ModalPopupExtender ID="mpeImages" runat="server" TargetControlID="hfPopup"
        PopupControlID="panOpen" BackgroundCssClass="modalBackground" CancelControlID="imgCancel"
        PopupDragHandleControlID="panOpen" />
    <asp:Panel ID="panOpen" runat="server" Height="600px" Width="700px" CssClass="ModalWindow">
        <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
            width: 662px;">
            <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" />
        </div>
        <iframe id="frmImage" runat="server" scrolling="no" style="height: 600px; width: 700px;"
            frameborder="0"></iframe>
    </asp:Panel>
    <%----------Images----------%>
    <%--        </ContentTemplate>
    
    </asp:UpdatePanel>--%>
    <%--  Add Invoice Number--%>
    <div id="divAddInvoiceNo" class="web_dialog_overlay">
    </div>
    <div id="divAddInvoiceNumber" class="web_dialog" style="display: none; width: 300px;
        min-height: 120px">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td class="PopUpBoxHeading" colspan="2">
                    &nbsp;&nbsp;Add Invoice Number
                </td>
                <td class="PopUpBoxHeading" align="right" colspan="2" style="padding-right: 5px">
                    <img border="0" src="../Images/close.gif" id="img1" alt="close" onclick="HideInvoicePopup();" />
                </td>
            </tr>
            <tr>
                <td class="GridContent_padding5">
                    <b>Invoice Number</b>
                </td>
                <td class="GridContent_padding5">
                    <asp:TextBox ID="txtInvoiceNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="GridContent_padding5" colspan="2" style="text-align: center">
                    <asp:Button ID="btnInvoiceNumberSave" runat="server" Text="Save" CssClass="btn" OnClick="btnInvoiceNumberSave_Click"
                        OnClientClick="return ShowEmailAlert();" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </div>
    <%--END--%>
    <script type="text/javascript">
        function ConfirmMessage() {
            debugger;
            var result = Confirm("dfkjdkl");
            if (result)
                return true;
            else
                return false;
        }

        function SelectAllCheckboxes(chk) {
            var counter = 0;
            $('#<%=gvExpenses.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                    counter = counter + 1;
                }
            });

            document.getElementById("ctl00_ContentPlaceHolder1_hfSelectedCount").value = chk.checked == true ? counter : "0";

        }

        function ShowAddInvoiceNoPopup() {
            var checked = ($("#<%=gvExpenses.ClientID %> >tbody >tr >td:first-child > input[type=checkbox]:checked").length > 0)
            if (checked == false)
                alert('Please select at least one Expense.');
            else
                ShowAddInvoicePopup(true);
            return false;
        }

        function ShowAddInvoicePopup(modal) {

            $("#divAddInvoiceNo").show();
            $("#divAddInvoiceNumber").fadeIn(10);

            if (modal) {
                $("#divAddInvoiceNo").unbind("click");
            }
            else {
                $("#divAddInvoiceNo").click(function (e) {
                    HideInvoicePopup();
                });
            }
        }

        function HideInvoicePopup() {
            $("#divAddInvoiceNo").hide();
            $("#divAddInvoiceNumber").fadeOut(10);
            return false;
        }
        function ShowEmailAlert() {
            var r = confirm("Do you want to update invoice number?");
            if (r == true) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
</asp:content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAllCommission.aspx.cs"
    Inherits="METAOPTION.UI.ViewAllCommission" %>

<asp:Content ID="contViewLocation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="padding: 5px">
                <asp:HiddenField id="ParentID" runat="server"></asp:HiddenField>
                 <asp:HiddenField id="EntityTYpeID" runat="server"></asp:HiddenField>
                
                
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td class="TableHeadingBg TableHeading">
                            Commissions
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                <div style="padding: 5px">
                                  <asp:HiddenField ID="hdnTotalrows" runat="server" />
                                    <div style="width: 25%; float: left; padding: 5px; padding-left: 0px;">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td class="TableBorder">
                                                    VIN#
                                                </td>
                                                <td class="TableBorder">
                                                    <asp:TextBox ID="txtVIN" runat="server" CssClass="txt2" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    From Date
                                                </td>
                                                <td class="TableBorder">
                                                    <asp:TextBox ID="txtSyncDateFrom" runat="server" CssClass="txt2" />
                                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSyncDateFrom"
                                                        PopupButtonID="txtSyncDateFrom" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="width: 25%; float: left; padding: 5px 5px 5px 5px">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td class="TableBorder" style="width: 110px">
                                                    Expense Type
                                                </td>
                                                <td class="TableBorder" style="width: 200px">
                                                    <asp:DropDownList runat="server" ID="ddlExpenseType" CssClass="txt2" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    To Date
                                                </td>
                                                <td class="TableBorder">
                                                    <asp:TextBox ID="txtSyncDateTo" runat="server" CssClass="txt2" />
                                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSyncDateTo"
                                                        PopupButtonID="txtSyncDateTo" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="width: 23%; float: left; padding: 5px 5px 5px 5px">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td class="TableBorder">
                                                    Added By
                                                </td>
                                                <td class="TableBorder">
                                                    <asp:DropDownList runat="server" ID="ddlAddedBy" CssClass="txt2" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    Check#
                                                </td>
                                                <td class="TableBorder">
                                                    <asp:TextBox ID="txtCheck" runat="server" CssClass="txt2" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="width: 10%; float: left; padding: 5px 5px 5px 5px">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    &nbsp; &nbsp;
                                                    <br />
                                                    &nbsp; &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="width: 80px !important;
                                                        margin-top: 5px;" CssClass="btn" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                             <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblCount" runat="server" BorderColor="Transparent" BackColor="Transparent"
                                    ForeColor="#21618C" />
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                                Page&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPaging" runat="server" CssClass="txt1" Width="50px"  AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged" />
                                <%= gvExpense.PageCount%>
                            </td>
                            <td style="text-align: right; padding-right: 10px; color: #21618C">
                                Page size&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPageSize1" runat="server" CssClass="txt1" Width="50px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" Selected="True" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="250" Value="250" />
                                </asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap; text-align: right">
                                <asp:Button ID="btnFirst" runat="server" Text="First" CssClass="btn" OnClick="btnFirst_Click"
                                    CausesValidation="false" />
                                <asp:Button ID="btnPrev" runat="server" Text="Prev" CssClass="btn" OnClick="btnPrev_Click"
                                    CausesValidation="false" />
                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn" OnClick="btnNext_Click"
                                    CausesValidation="false" />
                                <asp:Button ID="btnLast" runat="server" Text="Last" CausesValidation="false" CssClass="btn"
                                    OnClick="btnLast_Click" />
                            </td>
                        </tr>
                    </table>
                                <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="gridView" CellPadding="4" GridLines="None" AllowPaging="True" PageSize="25"
                                    OnRowDataBound="gvExpense_RowDataBound"  PagerSettings-Visible="false"
                                    OnPageIndexChanging="gvExpense_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
                                        <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ExpenseAmount" DataFormatString="{0:c}&nbsp;" HeaderText="Commission"
                                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight" SortExpression="">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="VIN" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkVIN" runat="server" Text='<%#Eval("VIN") %>' NavigateUrl='<%# "InventoryDetail.aspx?Code="+Eval("InventoryId")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Check#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnkCheckNo" runat="server" Text='<%#Eval("CheckNumber") %>'
                                                    NavigateUrl='<%# "ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="ExpenseID" HeaderText="Expense ID" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" />--%>
                                        <asp:BoundField DataField="AddedByText" HeaderText="Added By" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkShowBuyerCalculation" runat="server" ImageUrl="~/Images/hist-icon-lane.jpg"
                                                    ToolTip="Display Commission Calculation Information" CausesValidation="false" />
                                                <asp:HiddenField ID="hdExpenseId" runat="server" Value='<%#Eval("ExpenseID") %>' />
                                                <asp:HiddenField ID="hdInventoryId" runat="server" Value='<%#Eval("InventoryId") %>' />
                                                <asp:Panel ID="pnlShowCommissionDetails" Style="display: none;" runat="server" CssClass="modalPopup"
                                                    Width="700">
                                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                        <tr>
                                                            <td class="PopUpBoxHeading">
                                                                &nbsp;
                                                                <asp:Label ID="lblHeaderInventoryInfo" runat="server" Text=""></asp:Label>
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
                                                                                        <%--<tr>
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
                                                                                                    </tr>--%>
                                                                                        <%-- <tr>
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
                                                                                                    </tr>--%>
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
                                                                <asp:ObjectDataSource ID="objBuyerCommissionCalculation" runat="server" SelectMethod="GetBuyerComm_CalculationInformation"
                                                                    TypeName="METAOPTION.BAL.BuyerBAL">
                                                                    <SelectParameters>
                                                                        <asp:SessionParameter Name="buyerId" SessionField="UserEntityID" Type="Int64" />                                                                         
                                                                         <asp:ControlParameter ControlID="ParentID" Name="ParentBuyerID" PropertyName="Value"
                                                                            Type="Int32" />
                                                                            <asp:ControlParameter ControlID="EntityTYpeID" Name="EntityTypeID" PropertyName="Value"
                                                                            Type="Int32" />
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
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Commission found"></asp:Label>
                                    </EmptyDataTemplate>
                                    <AlternatingRowStyle BackColor="#E4EDF4" />
                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                </asp:GridView>
                                  <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblCount1" runat="server" BorderColor="Transparent" BackColor="Transparent"
                                    ForeColor="#21618C" />
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                                Page&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged" />
                                &nbsp;<%= gvExpense.PageCount%></td>
                            <td style="text-align: right; padding-right: 10px; color: #21618C">
                                Page size&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPageSize2" runat="server" CssClass="txt1" Width="50px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" Selected="True" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="250" Value="250" />
                                </asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap; text-align: right">
                                <asp:Button ID="btnFirst1" runat="server" Text="First" CssClass="btn" OnClick="btnFirst_Click"
                                    Width="46px" CausesValidation="false" />
                                <asp:Button ID="btnPrev1" runat="server" Text="Prev" CssClass="btn" OnClick="btnPrev_Click"
                                    CausesValidation="false" />
                                <asp:Button ID="btnNext1" runat="server" Text="Next" CssClass="btn" OnClick="btnNext_Click"
                                    CausesValidation="false" />
                                <asp:Button ID="btnLast1" runat="server" Text="Last" CssClass="btn" OnClick="btnLast_Click"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="FooterContentDetails">
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="updMain">
            <ProgressTemplate>
                <div id="dvProg" class="overlay">
                    <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                    wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>

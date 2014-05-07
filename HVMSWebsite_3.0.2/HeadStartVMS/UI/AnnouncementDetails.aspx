<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeBehind="AnnouncementDetails.aspx.cs" Inherits="HeadStartVMS.UI.AnnouncementDetails" %>

<asp:content contentplaceholderid="ContentPlaceHolder1" id="cphViewLaneAssg" runat="server">
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                    class="arial-12">
                    <tr>
                        <td class="TableHeadingBg TableHeading" colspan="2">
                            Announcement Details
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="width: 128px">
                            <b>Title</b>
                        </td>
                        <td class="TableBorder">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="width: 128px">
                            <b>Description</b>
                        </td>
                        <td class="TableBorder">
                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="width: 128px">
                            <b>Type</b>
                        </td>
                        <td class="TableBorder">
                            <asp:Label ID="lblType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="width: 128px">
                            <b>Date Added</b>
                        </td>
                        <td class="TableBorder">
                            <asp:Label ID="lblDateAdded" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="width: 128px">
                            <b>Added By</b>
                        </td>
                        <td class="TableBorder">
                            <asp:Label ID="lblAddedby" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 25px" align="right">
                &nbsp;
            </td>
            <tr>
                <td>
                    <table id="tblLane" runat="server" border="0" width="100%" visible="false" cellpadding="0"
                        style="border-collapse: collapse">
                        <tr>
                            <td class="TableHeadingBg TableHeading">
                                &nbsp; Lane Assignments
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                <table cellpadding="0" style="width: 100%; border-collapse: collapse">
                                    <tr>
                                        <td align="right">
                                            <asp:GridView ID="gvLaneAssignments" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                Width="100%" OnRowDataBound="gvLaneAssignments_RowDataBound">
                                                <Columns>
                                                    <%--<asp:BoundField HeaderText="VIN Number" DataField="VIN">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" />
                                                    </asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="VIN">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hplViewCarPage" runat="server" NavigateUrl='<%# "InventoryDetail.aspx?Code=" +Eval("InventoryId") %>'
                                                                ToolTip="View car details"><%# Eval("VIN") %></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Regular Lane No." DataField="RegularLane">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Year" DataField="Year">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Make" DataField="Make">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Model" DataField="Model">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Cost" DataField="CarCost" DataFormatString="{0:N}">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContentRight" Height="18px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Market Price" DataField="MarketPrice" DataFormatString="{0:N}">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContentRight" Height="18px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="tblGeneral" runat="server" visible="false" border="0" width="100%" cellpadding="0"
                        style="border-collapse: collapse">
                        <tr>
                            <td class="TableHeadingBg TableHeading">
                                General Assignments
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                <table cellpadding="0" style="width: 100%; border-collapse: collapse">
                                    <tr>
                                        <td align="right">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="VIN Number" DataField="VIN">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Regular Lane No." DataField="RegularLane">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Year" DataField="Year">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Make" DataField="Make">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Model" DataField="Model">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Cost" DataField="CarCost" DataFormatString="{0:N}">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContentRight" Height="18px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Market Price" DataField="MarketPrice" DataFormatString="{0:N}">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContentRight" Height="18px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="tblChrome" runat="server" visible="false" border="0" width="100%" cellpadding="0"
                        style="border-collapse: collapse">
                        <tr>
                            <td class="TableHeadingBg TableHeading">
                                Chrome UPDATE
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                <table cellpadding="0" style="width: 100%; border-collapse: collapse">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="gvChromeUpdate" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                Width="50%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="DateAdded" DataField="DateAdded" DataFormatString="{0:d}">
                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                        <ItemStyle CssClass="GridContent" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Description" DataField="Description">
                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                        <ItemStyle CssClass="GridContent" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="tblCommissions" runat="server" visible="false" border="0" width="100%"
                        cellpadding="0" style="border-collapse: collapse">
                        <tr>
                            <td class="TableHeadingBg TableHeading">
                                Commissions Calculated
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                <table cellpadding="0" style="width: 100%; border-collapse: collapse">
                                    <tr>
                                        <td align="right">
                                            <asp:GridView ID="gvCommission" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                Width="100%" OnRowDataBound="gvCommission_RowDataBound" PageSize="25" AllowPaging="True"
                                                OnPageIndexChanging="gvCommission_PageIndexChanging">
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Buyer">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "ViewBuyerDetails.aspx?Mode=View&BuyerId="+Eval("BuyerId")+"&type=2" %>'
                                                                runat="server" Text='<%# Eval("Buyer") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" />
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField HeaderText="VIN" DataField="VIN">
                                                        <HeaderStyle CssClass="GridHeader" Wrap="False" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" Wrap="False" />
                                                    </asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="VIN">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hplViewCarPage" runat="server" NavigateUrl='<%# "InventoryDetail.aspx?Code=" +Eval("InventoryId") %>'
                                                                ToolTip="View car details"><%# Eval("VIN") %></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Car Cost" DataField="CarCost" DataFormatString="{0:C}">
                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                        <ItemStyle CssClass="GridContentRight" Height="18px" HorizontalAlign="Right" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Commission Type" DataField="CommissionType">
                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Commission" DataField="CommissionAmount" DataFormatString="{0:C}">
                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                        <ItemStyle CssClass="GridContentRight" Height="18px" HorizontalAlign="Right" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Make" DataField="Make">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Model" DataField="Model">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Body" HeaderText="Body">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Check#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hylnkCheckNo" runat="server" Text='<%#Eval("CheckNumber") %>'
                                                                NavigateUrl='<%# "ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkShowBuyerCalculation" runat="server" ImageUrl="~/Images/hist-icon-lane.jpg"
                                                                ToolTip="Display Commission Calculation Information" CausesValidation="false" />
                                                            <asp:HiddenField ID="hdExpenseId" runat="server" Value='<%#Eval("ExpenseID") %>' />
                                                            <asp:HiddenField ID="hdBuyerId" runat="server" Value='<%#Eval("BuyerId") %>' />
                                                            <asp:HiddenField ID="hdInventoryId" runat="server" Value='<%#Eval("InventoryId") %>' />
                                                            <asp:Panel ID="pnlShowCommissionDetails" Style="display: none;" runat="server" CssClass="modalPopup"
                                                                Width="700">
                                                                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                                    <tr>
                                                                        <td class="PopUpBoxHeading">
                                                                            &nbsp;<asp:Label ID="lblHeaderInventoryInfo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="PopUpBoxHeading" align="right">
                                                                            <asp:ImageButton ID="imgCloseBuyerCalcPopUp"  runat="server" CausesValidation="false"
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
                                                                                    <asp:ControlParameter ControlID="hdBuyerId" Name="BuyerId" PropertyName="Value" Type="Int64" />
                                                                                    <asp:ControlParameter ControlID="hdExpenseId" Name="expenseId" PropertyName="Value"
                                                                                        Type="Int64" />
                                                                                </SelectParameters>
                                                                            </asp:ObjectDataSource>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <ajax:ModalPopupExtender ID="MPEBuyerCommCalculation" runat="server" TargetControlID="lnkShowBuyerCalculation"
                                                                PopupControlID="pnlShowCommissionDetails" BackgroundCssClass="modalBackground" CancelControlID="imgCloseBuyerCalcPopUp" 
                                                                PopupDragHandleControlID="pnlShowCommissionDetails" DropShadow="false">
                                                            </ajax:ModalPopupExtender>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
    </table>
</asp:content>

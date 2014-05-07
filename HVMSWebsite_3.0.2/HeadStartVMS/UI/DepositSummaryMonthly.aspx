<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="DepositSummaryMonthly.aspx.cs"
    MasterPageFile="~/UI/MasterPageNoLeftPanel.Master" Inherits="METAOPTION.UI.DepositSummaryMonthly" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upResultSearch" runat="server" UpdateMode="Conditional">
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>--%>
            <ContentTemplate>
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        Monthly Deposits Summary</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;">
                    </div>
                </div>
                <div id="dvSearch" runat="server" class="dvSearch">
                    <div style="width: 48%; float: left; padding: 5px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    Year Range
                                </td>
                                <td class="TableBorder">
                                    <table>
                                        <tr>
                                            <td style="white-space: nowrap;">
                                                From :
                                                <asp:DropDownList ID="ddlMonthFrom" runat="server" CssClass="txt1" Width="100px" />
                                                <asp:DropDownList ID="ddlYearFrom" runat="server" CssClass="txt1" Width="50px" />
                                            </td>
                                            <td style="width: 30px">
                                            </td>
                                            <td>
                                                To :
                                                <asp:DropDownList ID="ddlMonthTo" runat="server" CssClass="txt1" Width="100px" />
                                                <asp:DropDownList ID="ddlYearTo" runat="server" CssClass="txt1" Width="50px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 38%; float: right; padding: 5px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upResultSearch">
                                        <ProgressTemplate>
                                            <div id="dvProg" class="overlay">
                                                <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                                wait...
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td style="padding: 9px 0px" align="right">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" CssClass="btn"
                                        OnClick="btnSearch_Click" OnClientClick="return Validate();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="clear: both;">
                    <table border="0" style="width: 100%;" class="TableHeadingBg TableHeading">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblCount" runat="server" BorderColor="Transparent" BackColor="Transparent"
                                    ForeColor="#21618C" />
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                                Page&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPaging" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                                    AutoPostBack="true" />
                                of
                                <%= grvDepositSummary.PageCount%>
                            </td>
                            <td style="text-align: right; padding-right: 10px; color: #21618C">
                                Page size&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPageSize1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" Selected="True" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="250" Value="250" />
                                </asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap; text-align: right">
                                <asp:Button ID="btnFirst" runat="server" Text="First" CssClass="btn" OnClick="btnFirst_Click" />
                                <asp:Button ID="btnPrev" runat="server" Text="Prev" CssClass="btn" OnClick="btnPrev_Click" />
                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn" OnClick="btnNext_Click" />
                                <asp:Button ID="btnLast" runat="server" Text="Last" CssClass="btn" OnClick="btnLast_Click" />
                            </td>
                        </tr>
                    </table>
                    <div>
                        <asp:GridView ID="grvDepositSummary" runat="server" DataKeyNames="Year,Month" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                            RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                            PageSize="50" AllowSorting="true" OnSorting="grvDepositSummary_Sorting" CssClass="Grid"
                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvDepositSummary_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Month" HeaderStyle-CssClass="GridHeader" SortExpression="DepositDate"
                                    HeaderStyle-Font-Underline="true" ItemStyle-CssClass="GridContent" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnExpand" runat="server" ImageUrl="~/Images/expand.png" OnClick="ibtnExpand_Click" />
                                        &nbsp;
                                        <asp:HyperLink ID="hylnkDeposits" ToolTip="Deposit Detail" NavigateUrl='#' runat="server"
                                            Text='<%#Eval("MonthYear")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DepositAmount" HeaderText="<div style='text-align:right;'>Deposit Amount ($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="DepositAmount"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="GridHeader_New"
                                    HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="CarCost" HeaderText="<div style='text-align:right;'>Car Cost ($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="CarCost"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="ExpenseCost" HeaderText="<div style='text-align:right;'>Expenses ($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="ExpenseCost"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="TotalCost" HeaderText="<div style='text-align:right;'>Total Cost ($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="TotalCost"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="SoldPrice" HeaderText="<div style='text-align:right;'>Sold Price ($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="SoldPrice"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="Margin" HeaderText="<div style='text-align:right;'>Margin ($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="Margin"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="MarginPercent" HeaderText="<div style='text-align:right;'>Margin (%)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="MarginPercent"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="Count" HeaderText="<div style='text-align:center;'>Count</div>"
                                    ItemStyle-CssClass="GridContent_New" HtmlEncode="false" SortExpression="Count"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader_New" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField ItemStyle-Width="0px" ShowHeader="false" ControlStyle-Width="100%">
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="9">
                                                <asp:Panel ID="pnlNestedGrid" runat="server" CssClass="NestedGridPanel">
                                                    <div style="float: left; width: 35px; height: 35px;">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/downright.png" Height="20px"
                                                            Width="35px" />
                                                    </div>
                                                    <div style="margin: 4px 20px 4px 0px; overflow: hidden;">
                                                        <asp:GridView ID="grdSummaryDetail" runat="server" Width="100%" RowStyle-CssClass="gvRow"
                                                            AutoGenerateColumns="false" GridLines="None">
                                                            <Columns>
                                                                <asp:BoundField DataField="BankName" HeaderText="Bank" HtmlEncode="false" SortExpression="Bank"
                                                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="left" />
                                                                <asp:BoundField DataField="DepositAmount" HeaderText="<div style='text-align:right;'>Deposit Amount ($)</div>"
                                                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="DepositAmount"
                                                                    HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false"
                                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                                                <asp:BoundField DataField="CarCost" HeaderText="<div style='text-align:right;'>Car Cost ($)</div>"
                                                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="CarCost"
                                                                    HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                                                <asp:BoundField DataField="ExpenseCost" HeaderText="<div style='text-align:right;'>Expenses ($)</div>"
                                                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="ExpenseCost"
                                                                    HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                                                <asp:BoundField DataField="TotalCost" HeaderText="<div style='text-align:right;'>Total Cost ($)</div>"
                                                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="TotalCost"
                                                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                                                <asp:BoundField DataField="SoldPrice" HeaderText="<div style='text-align:right;'>Sold Price ($)</div>"
                                                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="SoldPrice"
                                                                    HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                                                <asp:BoundField DataField="Margin" HeaderText="<div style='text-align:right;'>Margin ($)</div>"
                                                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="Margin"
                                                                    HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                                                <asp:BoundField DataField="MarginPercent" HeaderText="<div style='text-align:right;'>Margin (%)</div>"
                                                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="MarginPercent"
                                                                    HeaderStyle-CssClass="GridHeader_New" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                                                <asp:BoundField DataField="Count" HeaderText="<div style='text-align:center;'>Count</div>"
                                                                    ItemStyle-CssClass="GridContent_New" HtmlEncode="false" SortExpression="Count"
                                                                    HeaderStyle-CssClass="GridHeader_New" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                                    ItemStyle-HorizontalAlign="Center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>
                                                <ajax:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="pnlNestedGrid"
                                                    CollapsedSize="0" Collapsed="True" ExpandControlID="ibtnExpand" CollapseControlID="ibtnExpand"
                                                    AutoCollapse="False" AutoExpand="False" ScrollContents="false" ExpandDirection="Vertical" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                            <RowStyle CssClass="gvRow" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                            <EmptyDataRowStyle CssClass="gvEmpty" />
                        </asp:GridView>
                    </div>
                    <asp:HiddenField ID="hInventoryID" runat="server" />
                    <asp:HiddenField ID="hDepositHistory" runat="server" />
                    <ajax:ModalPopupExtender ID="MPEDepositHistory" runat="server" BackgroundCssClass="modalBackground"
                        TargetControlID="hDepositHistory" PopupControlID="divDepositHistory" CancelControlID="imgCloseDuplicate">
                    </ajax:ModalPopupExtender>
                    <%--prem code for model popup extender --%>
                    <div id="divDepositHistory" class="modalPopup" style="display: none; width: 850px;
                        min-height: 490px" runat="server">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="PopUpBoxHeading">
                                    &nbsp;&nbsp;Deposit History
                                </td>
                                <td class="PopUpBoxHeading" align="right">
                                    <img border="0" src="../Images/close.gif" id="imgCloseDuplicate" alt="close" style="margin-right: 5px;" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label1" runat="server" Text="VIN#" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblVin" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label3" runat="server" Text="Deposit Amount" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder" style="white-space: nowrap;">
                                                <span style="color: Red;">$</span>
                                                <asp:Label ID="lblDepositAmount" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label2" runat="server" Text="Year" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblYear" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label6" runat="server" Text="Sold Price" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder" style="white-space: nowrap;">
                                                <span>$</span>
                                                <asp:Label ID="lblSoldPrice" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label7" runat="server" Text="Make" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblMake" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label8" runat="server" Text="Sold Date" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblSoldDate" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label9" runat="server" Text="Model" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblModel" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label10" runat="server" Text="Sold Status" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblSoldStatus" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label11" runat="server" Text="Body" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblBody" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label12" runat="server" Text="Sold Comment" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblSoldComment" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label5" runat="server" Text="Added By" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblAddedBy" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="Label4" runat="server" Text="Buyer" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Label ID="lblBuyer" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--end code for model popup extender--%>
                    <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblCount1" runat="server" BorderColor="Transparent" BackColor="Transparent"
                                    ForeColor="#21618C" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                                Page
                                <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                                    AutoPostBack="true" />
                                of
                                <%= grvDepositSummary.PageCount%>
                            </td>
                            <td style="text-align: right; padding-right: 10px; color: #21618C">
                                Page size&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPageSize2" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" Selected="True" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="250" Value="250" />
                                </asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap; text-align: right">
                                <asp:Button ID="btnFirst1" runat="server" Text="First" CssClass="btn" OnClick="btnFirst_Click" />
                                <asp:Button ID="btnPrev1" runat="server" Text="Prev" CssClass="btn" OnClick="btnPrev_Click" />
                                <asp:Button ID="btnNext1" runat="server" Text="Next" CssClass="btn" OnClick="btnNext_Click" />
                                <asp:Button ID="btnLast1" runat="server" Text="Last" CssClass="btn" OnClick="btnLast_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script language="javascript" type="text/javascript">
        function Validate() {
            var AmountFrom = document.getElementById("= txtAmountFrom.ClientID %>");
            var AmountTo = document.getElementById("= txtAmountTo.ClientID %>");
            if (isNaN(AmountFrom.value)) {
                alert("Please enter a valid amount !");
                AmountFrom.focus();
                AmountFrom.select();
                return false;
            }
            if (isNaN(AmountTo.value)) {
                alert("Please enter a valid amount !");
                AmountTo.focus();
                AmountTo.select();
                return false;
            }
            var dateFrom = document.getElementById("= txtDateFrom.ClientID %>");
            var dateTo = document.getElementById("= txtDateTo.ClientID %>");
            var strDateFrom = trim(dateFrom.value);
            var strDateTo = trim(dateTo.value);
            if (strDateFrom != "") {
                if (!isValidDate(strDateFrom)) {
                    alert("Please enter a valid date !");
                    dateFrom.focus();
                    dateFrom.select();
                    return false;
                }
            }
            if (strDateTo != "") {
                if (!isValidDate(strDateTo)) {
                    alert("Please enter a valid date !");
                    dateTo.focus();
                    dateTo.select();
                    return false;
                }
            }
        }

        function isValidDate(theDate) {
            if (theDate.match(/(\d{1,2})\/(\d{1,2})\/(\d{2,4})/)) {
                var theMonth = parseInt(RegExp.$1, 10) - 1;
                var theDay = parseInt(RegExp.$2, 10);
                var theYear = parseInt(RegExp.$3, 10);
                if (String(theYear).length <= 2)
                    theYear += 2000;
                var checkDate = new Date(theYear, theMonth, theDay)
                if ((checkDate.getMonth() == theMonth) &&
		     (checkDate.getDate() == theDay) &&
		     (checkDate.getFullYear() == theYear)
		   ) {
                    return true;
                }
            }
            return false;
        }

        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }
        function SetProgressPosition(e) {
            var posx = 0;
            var posy = 0;
            if (!e) var e = window.event;
            if (e.pageX || e.pageY) {
                posx = e.pageX;
                posy = e.pageY;
            }
            else if (e.clientX || e.clientY) {
                posx = e.clientX + document.documentElement.scrollLeft;
                posy = e.clientY + document.documentElement.scrollTop;
            }
            document.getElementById('dvProg').style.left = posx + 10 + "px";
            document.getElementById('dvProg').style.top = posy + "px";
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        var objHidden;
        var combo;
        var cancelDropDownClosing = false;

        function StopPropagation(e) {
            //cancel bubbling
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function onDropDownClosing() {
            cancelDropDownClosing = false;
        }

        function onCheckBoxClick(chk, ctrlType) {
            if (ctrlType == "AddedBy") {
                objHidden = document.getElementById("= this.hAddedBy.ClientID %>");
                combo = $find("= ddlAddedBy.ClientID %>");

            }
            else if (ctrlType == "Buyer") {
                objHidden = document.getElementById("= this.hBuyer.ClientID %>");
                combo = $find("= ddlBuyer.ClientID %>");

            }

            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items
            var items = combo.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var chk1 = $get(combo.get_id() + "_i" + i + "_chk1");
                if (chk1.checked) {
                    text += item.get_text() + ",";
                    values += item.get_value() + ",";
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);

            if (text.length > 0) {
                //set the text of the combobox                   
                combo.set_text(text);
                objHidden.value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combo.set_text("");
                objHidden.value = '';
            }

        }

        //this method removes the ending comma from a string
        function removeLastComma(str) {
            return str.replace(/,$/, "");
        }
 
    </script>
    <style type="text/css">
        .cb label
        {
            margin-left: 5px;
        }
        .GridHeader_New
        {
            border: #e2e2e2 1px solid;
            background-color: #D9D9D9;
            font-weight: bold;
            padding: 4px;
        }
        .GridContent_New
        {
            border: 1px solid #BBDEF1;
            font-size: 11px;
            color: #2C2C2C;
            font-family: Arial, Helvetica, sans-serif;
            text-decoration: none;
            padding: 4px;
        }
    </style>
</asp:Content>

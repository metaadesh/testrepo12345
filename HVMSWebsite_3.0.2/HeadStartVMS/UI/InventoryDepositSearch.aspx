<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="InventoryDepositSearch.aspx.cs"
    MasterPageFile="~/UI/MasterPageNoLeftPanel.Master" Inherits="METAOPTION.UI.InventoryDepositSearch" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hAddedBy" runat="server" />
    <asp:HiddenField ID="hBuyer" runat="server" />
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upResultSearch" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        View All Deposits</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;">
                    </div>
                </div>
                <div id="dvSearch" runat="server" class="dvSearch">
                    <div style="width: 32%; float: left; padding: 5px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    VIN#
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtVINNumber" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Year
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="txt2" AutoPostBack="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Make
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlMake" runat="server" CssClass="txt2" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Model
                                </td>
                                <td class="TableBorder" style="width: 150px">
                                    <asp:DropDownList ID="ddlModel" runat="server" CssClass="txt2" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" style="width: 100px">
                                    Body
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                    <asp:DropDownList ID="ddlBody" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 26%; float: left; padding: 5px 5px 5px 10px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder" style="white-space: nowrap;">
                                    Amount Range
                                </td>
                                <td class="TableBorder" style="white-space: nowrap;">
                                    <asp:TextBox ID="txtAmountFrom" runat="server" onkeypress="return isNumber(event)"
                                        MaxLength="8" CssClass="txt1" Width="78px" />
                                    <asp:TextBox ID="txtAmountTo" runat="server" onkeypress="return isNumber(event)"
                                        MaxLength="8" CssClass="txt1" Width="78px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Deposit Date
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom"
                                        PopupButtonID="txtDateFrom" />
                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo"
                                        PopupButtonID="txtDateTo" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Buyer
                                </td>
                                <td class="TableBorder">
                                    <telerik:RadComboBox ID="ddlBuyer" runat="server" Width="150px" AllowCustomText="true"
                                        EmptyMessage="">
                                        <ItemTemplate>
                                            <div onclick="StopPropagation(event)" class="combo-item-template">
                                                <asp:CheckBox runat="server" ID="chk1" CssClass="cb" Text='<%#Eval("BuyerName")%>'
                                                    onclick="onCheckBoxClick(this,'Buyer')" />
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Added By
                                </td>
                                <td class="TableBorder">
                                    <telerik:RadComboBox ID="ddlAddedBy" runat="server" Width="150px" AllowCustomText="true"
                                        EmptyMessage="">
                                        <ItemTemplate>
                                            <div onclick="StopPropagation(event)" class="combo-item-template">
                                                <asp:CheckBox runat="server" ID="chk1" CssClass="cb" Text='<%#Eval("UserName")%>'
                                                    onclick="onCheckBoxClick(this,'AddedBy')" />
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Comment
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtComment" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 38%; float: left; padding: 5px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder" style="vertical-align: top; width: 85px">
                                    Sort 1
                                </td>
                                <td class="TableBorder" style="width: 250px">
                                    <div style="float: left; margin-right: 10px">
                                        <asp:DropDownList ID="ddlSort1" runat="server" CssClass="txt2" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSort1_SelectedIndexChanged" />
                                    </div>
                                    <asp:RadioButtonList ID="rbtnSort1Direction" runat="server" RepeatDirection="Horizontal"
                                        CellPadding="2">
                                        <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                                        <asp:ListItem Value="DESC" Text=" Z - A " Selected="True" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" style="vertical-align: top">
                                    Sort 2
                                </td>
                                <td class="TableBorder">
                                    <div style="float: left; margin-right: 10px">
                                        <asp:DropDownList ID="ddlSort2" runat="server" CssClass="txt2" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSort2_SelectedIndexChanged" />
                                    </div>
                                    <asp:RadioButtonList ID="rbtnSort2Direction" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" />
                                        <asp:ListItem Value="DESC" Text=" Z - A " />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" style="vertical-align: top">
                                    Sort 3
                                </td>
                                <td class="TableBorder">
                                    <div style="float: left; margin-right: 10px">
                                        <asp:DropDownList ID="ddlSort3" runat="server" CssClass="txt2" AutoPostBack="true" />
                                    </div>
                                    <asp:RadioButtonList ID="rbtnSort3Direction" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" />
                                        <asp:ListItem Value="DESC" Text=" Z - A " />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
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
                                <%= gvDeposits.PageCount%>
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
                        <asp:GridView ID="gvDeposits" runat="server" DataKeyNames="InventoryID" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                            RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                            PageSize="50" AllowSorting="true" OnSorting="gvDeposits_Sorting" CssClass="Grid">
                            <Columns>
                                <asp:TemplateField HeaderText="VIN#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    SortExpression="Inventory.VIN" HeaderStyle-Font-Underline="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylnkView" ToolTip="Detail" NavigateUrl='<%# "InventoryDetail.aspx?Mode=View&Code="+Eval("InventoryId") %>'
                                            runat="server" Text='<%# Bind("Vin")%>' />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridContent" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        <asp:LinkButton CommandName="Sort" CommandArgument="Inventory.Year" ID="lnkYear"
                                            runat="server" Font-Underline="true">Year</asp:LinkButton>
                                        <asp:LinkButton CommandName="Sort" CommandArgument="Mobile_Chrome_Make.VINDivisionName"
                                            ID="lnkMake" runat="server" Font-Underline="true">Make</asp:LinkButton>
                                        <asp:LinkButton CommandName="Sort" CommandArgument="Mobile_Chrome_Model.VINModelName"
                                            ID="lnkModel" runat="server" Font-Underline="true">Model</asp:LinkButton>
                                        <asp:LinkButton CommandName="Sort" CommandArgument="Mobile_Chrome_Body.VINStyleName"
                                            ID="lnkBody" runat="server" Font-Underline="true">Body</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Year")%>
                                        <br />
                                        <%#Eval("VinDivisionName")%>
                                        <br />
                                        <%#Eval("VinModelName")%>
                                        <br />
                                        <%#Eval("VinStyleName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BuyerName" HeaderText="Buyer" ItemStyle-CssClass="GridContent"
                                    SortExpression="Buyer.FirstName" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Width="120px" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="DepositDate" HeaderText="D. Date" ItemStyle-CssClass="GridContent"
                                    SortExpression="Inventory.DepositDate" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Width="50px" DataFormatString="{0:M/d/yyyy}" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="DepositAmount" HeaderText="<br/>D. Amount<br /><div style='text-align:center;'>($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="Inventory.DepositAmount"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false"
                                    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="CarCost" HeaderText="<br/>Car Cost<br /><div style='text-align:center;'>($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="Inventory.CarCost"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false"
                                    ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="ExpenseCost" HeaderText="<br/>Expense<br /><div style='text-align:center;'>($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="dbo.Deposit_ExpAmount(Inventory.InventoryID)"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false"
                                    ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="SoldPrice" HeaderText="<br/>Sold Price<br /><div style='text-align:center;'>($)</div>"
                                    ItemStyle-CssClass="GridContentNumbers" HtmlEncode="false" SortExpression="Inventory.SoldPrice"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false"
                                    ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,###0.00}" />
                                <asp:BoundField DataField="MarginPercent" HeaderText="Margin" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false" HtmlEncode="false" />
                                <asp:BoundField DataField="BankName" HeaderText="Bank" ItemStyle-CssClass="GridContent"
                                    SortExpression="BankName" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-Wrap="false" />
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-Wrap="false">
                                    <HeaderTemplate>
                                        <br />
                                        <asp:LinkButton CommandName="Sort" CommandArgument="SecurityUser.DisplayName" ID="lnkAddedBy"
                                            runat="server" Font-Underline="true">Added By</asp:LinkButton>
                                        <asp:LinkButton CommandName="Sort" CommandArgument="Inventory.DateAdded" ID="lnkDateAdded"
                                            runat="server" Font-Underline="true">Date Added</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("DisplayName")%>
                                        <br />
                                        <%#Eval("DateAdded", "{0:M/d/yyyy HH:mm:ss}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="D. Comment" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    SortExpression="Inventory.DepositComment" HeaderStyle-Font-Underline="true">
                                    <ItemTemplate>
                                        <%#Eval("DepositComment")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnDepositHistory" ToolTip="View History" runat="server"
                                            ImageUrl="~/Images/DHistory.png" OnClick="imgbtnDepositHistory_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent_New" HorizontalAlign="Center" />
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
                        overflow: auto; max-height: 600px; min-height: 490px" runat="server">
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
                        <asp:UpdatePanel runat="server" ID="upModal" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="divDepositHistoryGrid" runat="server">
                                    <asp:GridView ID="grdDepositHistory" runat="server" Width="100%" BorderWidth="0"
                                        AllowPaging="True" CssClass="Grid" PageSize="10" AutoGenerateColumns="false"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Rows found" EmptyDataRowStyle-CssClass="gvEmpty"
                                        OnPageIndexChanging="grdDepositHistory_PageIndexChanging" OnRowDataBound="grdDepositHistory_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="130px">
                                                <HeaderTemplate>
                                                    Modified Field
                                                    <br />
                                                    <asp:DropDownList ID="ddlColumnName" CssClass="txt1" Width="120" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlColumnName_SelectedIndexChanged">
                                                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                        <asp:ListItem Text="Deposit Amount" Value="DepositAmount"></asp:ListItem>
                                                        <asp:ListItem Text="Deposit Comment" Value="DepositComment"></asp:ListItem>
                                                        <asp:ListItem Text="Deposit Date" Value="DepositDate"></asp:ListItem>
                                                        <asp:ListItem Text="Sold Price" Value="SoldPrice"></asp:ListItem>
                                                        <asp:ListItem Text="Sold Date" Value="SoldDate"></asp:ListItem>
                                                        <asp:ListItem Text="Sold Status" Value="SoldStatus"></asp:ListItem>
                                                        <asp:ListItem Text="Sold Comment" Value="SoldComment"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblColumnName" runat="server" Text='<%#Eval("ColumnName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OldValue" HeaderText="Old Value" ItemStyle-CssClass="GridContent"
                                                HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="left"
                                                DataFormatString="{0:#,###0.00}" />
                                            <asp:BoundField DataField="NewValue" HeaderText="New Value" ItemStyle-CssClass="GridContent"
                                                HeaderStyle-HorizontalAlign="left" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false"
                                                DataFormatString="{0:#,###0.00}" />
                                            <asp:BoundField DataField="ModifiedDate" HeaderText="Modified Date" ItemStyle-CssClass="GridContent_New"
                                                HeaderStyle-CssClass="GridHeader_New" DataFormatString="{0:M/d/yyyy HH:mm:ss}"
                                                HeaderStyle-Wrap="false" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" ItemStyle-CssClass="GridContent"
                                                ItemStyle-Width="150px" HeaderStyle-CssClass="GridHeader" />
                                        </Columns>
                                        <AlternatingRowStyle CssClass="gvAlternateRow" />
                                        <HeaderStyle CssClass="PreTableBorderB" />
                                        <RowStyle CssClass="gvRow" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <PagerStyle CssClass="PreFooterContentDetails" HorizontalAlign="Right" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                                <%= gvDeposits.PageCount%>
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
            var AmountFrom = document.getElementById("<%= txtAmountFrom.ClientID %>");
            var AmountTo = document.getElementById("<%= txtAmountTo.ClientID %>");
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
            var dateFrom = document.getElementById("<%= txtDateFrom.ClientID %>");
            var dateTo = document.getElementById("<%= txtDateTo.ClientID %>");
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
                objHidden = document.getElementById("<%= this.hAddedBy.ClientID %>");
                combo = $find("<%= ddlAddedBy.ClientID %>");

            }
            else if (ctrlType == "Buyer") {
                objHidden = document.getElementById("<%= this.hBuyer.ClientID %>");
                combo = $find("<%= ddlBuyer.ClientID %>");

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

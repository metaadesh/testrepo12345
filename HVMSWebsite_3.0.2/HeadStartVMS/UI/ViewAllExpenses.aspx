<%@ Page Title="HeadstartVMS::ViewAllExpenses" Language="C#" AutoEventWireup="true"
    Trace="true" CodeBehind="ViewAllExpenses.aspx.cs"
    Inherits="METAOPTION.UI.ViewAllExpenses" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<asp:Content ID="cphViewAllExpense" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upResultGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hfSelectedCount" runat="server" Value="0" />
                <asp:HiddenField ID="hdnExpenseId" runat="server" />
                <asp:HiddenField ID="hdnTotalrows" runat="server" />
                <asp:HiddenField ID="hdnInventory" runat="server" />
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 40%; padding: 5px; float: left">
                        VIEW ALL EXPENSES
                    </div>
                    <div style="width: 40%; padding: 5px; float: right; text-align: right">
                        <asp:LinkButton ID="lnkAddNewExpense" runat="server" CausesValidation="false" CssClass="AddNewExpenseTxt"
                            OnClick="lnkAddNewExpense_Click">
                                        <img src="../Images/AddNew.gif"  alt="Add New" style="border:none;" /> 
                                          Add New Expense
                        </asp:LinkButton>
                    </div>
                </div>
                <div id="dvSearch" runat="server" class="dvSearch" style="display: block; width: 100%;
                    float: left">
                    <div style="width: 58%; float: left; padding: 5px;">
                        <div style="width: 48.5%; float: left">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="TableBorder">
                                        VIN #
                                    </td>
                                    <td class="TableBorder">
                                        <asp:TextBox ID="txtVINNumber" runat="server" MaxLength="17" CssClass="txt2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        Expense Type
                                    </td>
                                    <td class="TableBorder">
                                        <asp:DropDownList ID="dlExpenseType" runat="server" CssClass="txt2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        Entity Type
                                    </td>
                                    <td class="TableBorder">
                                        <asp:DropDownList ID="dlEntityType" runat="server" CssClass="txt2" AutoPostBack="true"
                                            OnSelectedIndexChanged="dlEntityType_SelectedIndexChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        Added By
                                    </td>
                                    <td class="TableBorder">
                                        <asp:DropDownList ID="ddlAddedBy" runat="server" CssClass="txt2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        Invoice Number
                                    </td>
                                    <td class="TableBorder">
                                        <asp:DropDownList ID="ddlInvoiceNumber" runat="server" CssClass="txt2">
                                            <asp:ListItem Value="-1">ALL</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 48.5%; float: left; margin-left: 15px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="TableBorder">
                                        Check#
                                    </td>
                                    <td class="TableBorder">
                                        <asp:TextBox ID="txtCheck" runat="server" CssClass="txt2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        Paid
                                    </td>
                                    <td class="TableBorder">
                                        <asp:DropDownList ID="ddlPaid" runat="server" CssClass="txt2">
                                            <asp:ListItem Value="-1">ALL</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        Entity
                                    </td>
                                    <td class="TableBorder">
                                        <asp:TextBox ID="txtDealerShip" onblur="GetCustomerId(this.id)" CssClass="txt2" Width="150px"
                                            ToolTip="Type at least two characters to find customer name started with i.e MA or %MA to find all customer names having characters entered"
                                            Wrap="false" runat="server" autocomplete="off" Visible="false" />
                                        <div style="float: left;">
                                            <Ajax:AutoCompleteExtender ID="txtTest_AutoCompleteExtender" runat="server" TargetControlID="txtDealerShip"
                                                ServicePath="../WS/AutoFillCustomers.asmx" ServiceMethod="AutoFillCustomers" UseContextKey="true"
                                                EnableCaching="true" MinimumPrefixLength="2" CompletionSetCount="25" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                DelimiterCharacters=";, :" OnClientPopulated="onListPopulated" BehaviorID="AutoCompleteset">
                                            </Ajax:AutoCompleteExtender>
                                        </div>
                                        <asp:HiddenField ID="hdDealerId" runat="server" />
                                        <asp:DropDownList ID="dlEntities" runat="server" CssClass="txt2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        Date Added
                                    </td>
                                    <td class="TableBorder" colspan="2">
                                        <asp:TextBox ID="txtSyncDateFrom" runat="server" CssClass="txt1" Width="70px" />
                                        <Ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSyncDateFrom"
                                            PopupButtonID="txtSyncDateFrom" />
                                        <asp:TextBox ID="txtSyncDateTo" runat="server" CssClass="txt1" Width="70px" />
                                        <Ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSyncDateTo"
                                            PopupButtonID="txtSyncDateTo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        Source
                                    </td>
                                    <td class="TableBorder">
                                        <asp:DropDownList ID="ddlSourceFilter" runat="server" CssClass="txt2">
                                            <asp:ListItem Value="-1">ALL</asp:ListItem>
                                            <asp:ListItem Value="1">Mobile</asp:ListItem>
                                            <asp:ListItem Value="0">Web</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="width: 40%; float: left; padding: 5px">
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
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upResultGrid">
                                        <ProgressTemplate>
                                            <div id="dvProg" class="overlay">
                                                <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                                wait...
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td style="padding: 7px 0px 0px 10px">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" CssClass="btn"
                                        OnClick="btnSearch_Click" CausesValidation="false" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnAddInvoice" runat="server" Text="Add Invoice Number" Width="130px"
                                        CssClass="btn" CausesValidation="false" OnClientClick="return ShowAddInvoiceNoPopup();" />
                                    <%-- OnClick="btnAddInvoice_Click" --%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="clear: both;">
                    <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblCount" runat="server" BorderColor="Transparent" BackColor="Transparent"
                                    ForeColor="#21618C" />
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                                Page&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPaging" runat="server" CssClass="txt1" Width="50px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged" />
                                <%= gvAllExpenseList.PageCount%>
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
                    <div>
                        <asp:GridView ID="gvAllExpenseList" runat="server" DataKeyNames="ExpenseID" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                            RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                            PageSize="50" AllowSorting="true" CssClass="Grid" OnRowDataBound="gvAllExpenseList_RowDataBound"
                            OnSorting="gvAllExpenseList_Sorting" ShowFooter="true" OnRowCommand="gvAllExpenseList_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="22px" ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkall" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false" /><%-- OnCheckedChanged="chkSelect_OnCheckedChanged"--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expense Date" ItemStyle-Width="60px" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Wrap="true" ItemStyle-CssClass="GridContent" SortExpression="E.ExpenseDate">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkExpenseDate" runat="server" Text='<%#Eval("ExpenseDate") %>'
                                            CommandArgument='<%#Eval("InventoryId") %>' CommandName="SelectExpenseDate" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Entity Name" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="150px"
                                    HeaderStyle-Wrap="true" ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDealer" runat="server" Text='<%#Eval("DealerName") %>' CommandArgument='<%#String.Format("{0},{1}", Eval("EntityTypeId"), Eval("EntityId"))%>'
                                            CommandName="Select" CausesValidation="false"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnDealerEntityName" runat="server" Value='<%#Eval("DealerEntityName") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="width: 95%; float: left; text-align: right; font-size: 10pt; padding: 5px;
                                            color: #21618C">
                                            <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expense Amount ($)" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-Wrap="true" SortExpression="E.ExpenseAmount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# String.Format("{0:#,###}",Eval("ExpenseAmount")) %>'
                                            Style="padding-right: 5px" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="width: 100px; float: left; text-align: right; font-size: 10pt; color: #21618C">
                                            <asp:Label ID="lblTotalCount" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </FooterTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Width="100px" SortExpression="ET.ExpenseType" />
                                <asp:TemplateField HeaderText="VIN#" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Wrap="false" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <div style="text-transform: uppercase">
                                            <asp:Label ID="lblvin" runat="server" Text='<%# Eval("VIN") %>' CssClass="Tooltip"
                                                Style="cursor: pointer; display: none"></asp:Label>
                                            <asp:HyperLink ID="hlnkVIN" NavigateUrl='<%# "InventoryDetail.aspx?Mode=View&Code="+Eval("InventoryId")%>'
                                                runat="server" Text='<%#Eval("VIN") %>' /><br />
                                            <asp:Label ID="lblCode" runat="server" Text="Code: " Visible="false" />
                                            <asp:HyperLink ID="hlnkCode" runat="server" Visible="false" />
                                            <asp:HiddenField ID="hdnVINDetails" runat="server" Value='<%# Eval("VINDetails") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="GridHeader" HeaderText="Check#"
                                    ItemStyle-CssClass="GridContentNumbers">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylnk" runat="server" ForeColor="#2C2C2C" Text='<%#Eval("CheckNumber")%>'
                                            NavigateUrl='<%#"~/UI/ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId")%>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-Width="25px" ItemStyle-Width="25px" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                                    SortExpression="E.Checkpaid">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpaid" runat="server" Text='<%#Eval("Checkpaid") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Added By" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    ItemStyle-Width="110px" SortExpression="SU.DisplayName">
                                    <ItemTemplate>
                                        <%#Eval("DisplayName")%><br />
                                        (<%#Eval("EntityType")%>)
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DateAdded" HeaderText="Date Added" ItemStyle-CssClass="GridContent"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px" ItemStyle-Wrap="true"
                                    SortExpression="E.DateAdded" />
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderText="Action" ItemStyle-Wrap="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnEntityId" runat="server" Value='<%# Eval("EntityId") %>' />
                                        <asp:HiddenField ID="hdnExpenseID" runat="server" Value='<%# Eval("ExpenseID") %>' />
                                        <asp:HiddenField ID="hdnComments" runat="server" Value='<%# Eval("Comments") %>' />
                                        <asp:HiddenField ID="hdnInvoiceNumber" runat="server" Value='<%# Eval("InvoiceNo") %>' />
                                        <asp:HiddenField ID="hdnEntityType" runat="server" Value='<%# Eval("EntTypeId") %>' />
                                        <asp:HiddenField ID="hdnCheckDetails" runat="server" Value='<%# Eval("AddedModifiedBy") %>' />
                                        <asp:HiddenField ID="hdExpenseAmount" runat="server" Value='<%# Eval("ExpenseAmount") %>' />
                                        <asp:HiddenField ID="hdnPreExpID" runat="server" Value='<%#Eval("MoExpenseId") %>' />
                                        <asp:HiddenField ID="hdnExpenseTypeId" runat="server" Value='<%# Eval("ExpenseTypeId") %>' />
                                        <asp:HiddenField ID="hdnInventoryNo" runat="server" Value='<%# Eval("InventoryId") %>' />
                                        <asp:HiddenField ID="hdnYear" runat="server" Value='<%# Eval("Year") %>' />
                                        <asp:HiddenField ID="hdnMake" runat="server" Value='<%# Eval("Make") %>' />
                                        <asp:HiddenField ID="hdnModel" runat="server" Value='<%# Eval("ModelName") %>' />
                                        <asp:HiddenField ID="hdnEntityTypeId" runat="server" Value='<%# Eval("EntityTypeId") %>' />
                                        <div style="width: 90%; float: left; text-align: right">
                                            <asp:ImageButton ID="ibtnPreExpDetail" runat="server" ImageUrl="~/Images/mobile.png"
                                                ToolTip="Show Pre-Expense Deatils." CausesValidation="false" OnClientClick='<%# String.Format("ShowPreExpenseDetailsPopup(\"{0}\");return false;",
                                         Eval("MoExpenseId")) %>' />
                                            <%--OnClick="ibtnPreExpDetail_Click"--%>
                                            <asp:ImageButton ID="imbEditExp" CausesValidation="false" ImageUrl="~/Images/newedit.gif"
                                                runat="server" ToolTip="Edit expense" OnClick="imbEditExp_Click" />
                                            <asp:ImageButton ID="imgDelete" CausesValidation="false" ImageUrl="~/Images/DeleteButton.jpg"
                                                runat="server" ToolTip="Delete expense" OnClientClick="return DeleteAlert();"
                                                OnClick="imgDelete_Click" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle Wrap="False" CssClass="GridContent" HorizontalAlign="Right"></ItemStyle>
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
                                &nbsp;<%= gvAllExpenseList.PageCount%></td>
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
                </div>
                <div style="width: 99%; float: left; height: 15px; margin: 4px; clear: both;">
                    <Ajax:ModalPopupExtender ID="mpePreExpDetail" runat="server" BackgroundCssClass="modalBackground"
                        TargetControlID="Button2" PopupControlID="divAddInvoiceNumber" CancelControlID="img1">
                    </Ajax:ModalPopupExtender>
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
                    <Ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                        TargetControlID="Button1" PopupControlID="divShowInventory" CancelControlID="imggrid">
                    </Ajax:ModalPopupExtender>
                    <%-- <asp:HiddenField ID="imggrid" runat="server" />--%>
                    <div id="divShowInventory" class="modalPopup" style="display: none; width: 650px;
                        min-height: 150px;">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="PopUpBoxHeading">
                                    &nbsp;&nbsp;Search VIN
                                </td>
                                <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                    <img id="imggrid" border="0" src="../Images/close.gif" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Search VIN</b> (Please enter last 6 digit VIN#)
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:TextBox ID="txtVIN" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnVINSearch" runat="server" Text="Search" CssClass="Btn_Form" Width="75px"
                                        OnClick="btnVINSearch_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="grdInventoryShow" runat="server" Width="100%" AutoGenerateColumns="False"
                            EmptyDataText="No record found for this search criteria." RowStyle-CssClass="gvRow"
                            GridLines="None" PagerSettings-Visible="false" AllowPaging="true" PageSize="50"
                            AllowSorting="true" CssClass="Grid">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="22px" ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnInventoryId" runat="server" Value='<%# Eval("InventoryId") %>' />
                                        <asp:HiddenField ID="hdnMakeId" runat="server" Value='<%# Eval("MakeId") %>' />
                                        <asp:HiddenField ID="hdnModelId" runat="server" Value='<%# Eval("ModelId") %>' />
                                        <asp:RadioButton ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="InventoryId" HeaderText="InventoryId" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="VIN" HeaderText="VIN#" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="true" />
                                <asp:TemplateField HeaderText="Year" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake" runat="server" Text='<%# Eval("VINDivisionName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Model" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" runat="server" Text='<%# Eval("VINModelName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Body" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBody" runat="server" Text='<%# Eval("Body") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mileage" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMilege" runat="server" Text='<%# Eval("Mileage") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Added" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateAdded" runat="server" Text='<%# Eval("DateAdded") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="gvRow" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                            <EmptyDataRowStyle CssClass="gvEmpty" />
                        </asp:GridView>
                        <div style="width: 99%; float: left; text-align: center; margin: 5px">
                            <asp:Button ID="btnSelect" runat="server" Text="Submit" CssClass="Btn_Form" Width="75px"
                                CausesValidation="false" OnClick="btnSelect_Click" />
                        </div>
                    </div>
                    <%-- <asp:HiddenField ID="hfPreExp" runat="server" />--%>
                    <%-- <Ajax:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdShowExpense" runat="server"
                    BackgroundCssClass="modalBackground" TargetControlID="hfPreExp" PopupControlID="dvPreExpDetail"
                    CancelControlID="imgClosePreExpDetail" DropShadow="true">
                </Ajax:ModalPopupExtender>--%>
                    <div id="PreExpenseOverlays" class="web_dialog_overlay">
                    </div>
                    <div id="dvPreExpDetail" class="web_dialogPreExpense" style="display: none; width: 650px;
                        min-height: 150px">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td colspan="3" class="PopUpBoxHeading">
                                    &nbsp;&nbsp;Pre-Expense Details
                                </td>
                                <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                    <img border="0" src="../Images/close.gif" id="imgClosePreExpDetail" alt="close" onclick="HidePreExpPopup();" />
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>VIN</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblVIN" runat="server"></asp:Label>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Expense Date</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblExpenseDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Count</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblExpenseCount" runat="server"></asp:Label>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Sync Date</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblSyncdate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Default Price($)</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblDefaultprice" runat="server"></asp:Label>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Approved By</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblAddedby" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Total Price($)</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Added By</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblEntityType" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Description</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                </td>
                                <td class="GridContent_padding5">
                                    <b>Device</b>
                                </td>
                                <td class="GridContent_padding5">
                                    <asp:Label ID="lblDeviceName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridContent_padding5">
                                    <b>Approval Note</b>
                                </td>
                                <td class="GridContent_padding5" colspan="3">
                                    <asp:Label ID="lblApproveNote" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <Ajax:ModalPopupExtender ID="MPEAddExpense" BehaviorID="mdpopExpense" runat="server"
                        TargetControlID="lnkGrid" PopupControlID="tblAddExpense" BackgroundCssClass="modalBackground"
                        DropShadow="true" CancelControlID="btnExpenseCancel">
                    </Ajax:ModalPopupExtender>
                    <table id="tblAddExpense" width="50%" runat="server" style="display: none;" class="modalPopup"
                        border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="PopUpBoxHeading" style="padding-left: 5px">
                                <asp:Label ID="lblHeading" runat="server"></asp:Label>
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" onclick="$find('mdpopExpense').hide();return false;"
                                    alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="padding: 10px">
                                <table border="0" cellspacing="0" cellpadding="0" style="border-collapse: collapse;
                                    width: 100%;" class="Nornmal-Arial-12">
                                    <tr id="trEntityName" runat="server">
                                        <td class="GridContent_padding5">
                                            <b>Select Inventory</b>
                                        </td>
                                        <td class="GridContent_padding5">
                                            <asp:TextBox ID="txtInventoryId" Enabled="false" runat="server" Wrap="true" Style="display: none"></asp:TextBox>
                                            <asp:Label ID="lblInventoryId" runat="server" Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lnkGridshow" runat="server" Font-Underline="true" ForeColor="Blue"
                                                OnClick="lnkGridshow_Click" CausesValidation="false">Select</asp:LinkButton>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                ControlToValidate="txtInventoryId"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="trType" runat="server">
                                        <td nowrap="nowrap" class="GridContent_padding5">
                                            <b>Entity Type</b>
                                        </td>
                                        <td class="GridContent_padding5">
                                            <asp:DropDownList ID="ddlEntityType" AutoPostBack="true" runat="server" CssClass="txt2"
                                                OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Vendor" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Buyer" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Dealer" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="-1"
                                                ErrorMessage="*" ControlToValidate="ddlEntityType"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdExpenseUpId" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="trEntityShowname" runat="server">
                                        <td class="GridContent_padding5">
                                            <b>Entity Name</b>
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
                                            <Ajax:FilteredTextBoxExtender ID="txtExpenseAmount_FilteredTextBoxExtender" runat="server"
                                                FilterType="Numbers,Custom" ValidChars=".,-" TargetControlID="txtExpenseAmount">
                                            </Ajax:FilteredTextBoxExtender>
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="--Select--"
                                                        ErrorMessage="*" ControlToValidate="ddlExpenseType">
                                                    </asp:RequiredFieldValidator>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                ControlToValidate="txtExpenseDate"></asp:RequiredFieldValidator>
                                            <Ajax:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="imgArrivalDate"
                                                TargetControlID="txtExpenseDate">
                                            </Ajax:CalendarExtender>
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
                                            <asp:Button ID="btnAddExpense" runat="server" Text="Save" CssClass="Btn_Form" Width="75px"
                                                OnClick="btnAddExpense_Click" />
                                        </td>
                                    </tr>
                            </td>
                        </tr>
                    </table>
                    <div style="display: none; width: 1px;">
                        <asp:Button ID="Button2" runat="server" />
                        <asp:Button ID="Button1" runat="server" />
                        <asp:Button ID="lnkGrid" runat="server" />
                        <asp:Button ID="btnExpenseCancel" runat="server" />
                        <asp:HiddenField ID="hdUpdateDriverId" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" language="javascript">

        function SelectAllCheckboxes(chk) {
            var counter = 0;
            $('#<%=gvAllExpenseList.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                    counter = counter + 1;
                }
            });

            document.getElementById("ctl00_ContentPlaceHolder1_hfSelectedCount").value = chk.checked == true ? counter : "0";

        }

        function validateAmount(sender, args) {
            var txt = (args.Value);
            var startindex = txt.indexOf(".")
            var lastindex = txt.lastIndexOf(".")

            if (startindex != lastindex) {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
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
        function DeleteAlert() {
            var r = confirm("Do you want to delete expense?");
            if (r == true) {
                return true;
            }
            else {
                return false;
            }
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
    </script>
    <script type="text/javascript">

        function ShowAddInvoiceNoPopup() {
            var checked = ($("#<%=gvAllExpenseList.ClientID %> >tbody >tr >td:first-child > input[type=checkbox]:checked").length > 0)
            if (checked == false)
                alert('Please check at least one Expense.');
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





        function ShowPreExpenseDetailsPopup(PreExpenseID) {
            BindPreExpenseDetails(PreExpenseID);
            ShowPreExpPopup(true);
            return false;
        }

        function ShowPreExpPopup(modal) {

            $("#PreExpenseOverlays").show();
            $("#dvPreExpDetail").fadeIn(10);

            if (modal) {
                $("#PreExpenseOverlays").unbind("click");
            }
            else {
                $("#PreExpenseOverlays").click(function (e) {
                    HidePreExpPopup();
                });
            }
        }

        function HidePreExpPopup() {
            $("#PreExpenseOverlays").hide();
            $("#dvPreExpDetail").fadeOut(10);
            return false;
        }

        function BindPreExpenseDetails(PreExpenseID) {

            $.ajax({
                type: "POST",
                url: "ViewAllExpenses.aspx/ViewPreExpenseDetails",
                data: "{PreExpenseID:" + PreExpenseID + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {

                    if (data.d.length > 0) {

                        $('#<%=lblVIN.ClientID %>').text(data.d[0].PreExpVIN);
                        $('#<%=lblExpenseDate.ClientID %>').text(data.d[0].PreExpenseDate);
                        $('#<%=lblExpenseCount.ClientID %>').text(data.d[0].PreCount);
                        $('#<%=lblSyncdate.ClientID %>').text(data.d[0].PreSyncDate);
                        $('#<%=lblDefaultprice.ClientID %>').text(data.d[0].PreDefaultPrice);
                        $('#<%=lblAddedby.ClientID %>').text(data.d[0].PreApprovedBy);
                        $('#<%=lblTotalPrice.ClientID %>').text(data.d[0].PreTotalPrice);
                        $('#<%=lblEntityType.ClientID %>').text(data.d[0].PreAddedBy);
                        $('#<%=lblDescription.ClientID %>').html(data.d[0].PreDescription);
                        $('#<%=lblDeviceName.ClientID %>').html(data.d[0].PreDeviceName);
                        $('#<%=lblApproveNote.ClientID %>').html(data.d[0].PreApprovalNote);
                    }
                    else {
                        $('#<%=lblVIN.ClientID %>').text('');
                        $('#<%=lblExpenseDate.ClientID %>').text('');
                        $('#<%=lblExpenseCount.ClientID %>').text('');
                        $('#<%=lblSyncdate.ClientID %>').text('');
                        $('#<%=lblDefaultprice.ClientID %>').text('');
                        $('#<%=lblAddedby.ClientID %>').text('');
                        $('#<%=lblTotalPrice.ClientID %>').text('');
                        $('#<%=lblEntityType.ClientID %>').text('');
                        $('#<%=lblDescription.ClientID %>').text('');
                        $('#<%=lblDeviceName.ClientID %>').text('');
                        $('#<%=lblApproveNote.ClientID %>').text('');
                    }
                }
            });
        }


    </script>
    <script type="text/javascript">
        function GetCustomerId(val) {
            var txtcustomer = document.getElementById(val);

            // var hfId = val.replace("txtCustomerName", "hdCustomerId")
            var hfcontrol = document.getElementById('<%=hdDealerId.ClientID%>');
            var str = (txtcustomer.value).split("ID:");
            if (str.length > 1) {
                txtcustomer.value = str[0];
                hfcontrol.value = str[1];
            }
        }
        function onListPopulated() {
            var completionList = $find("AutoCompleteset").get_completionList();
            completionList.style.width = 'auto';
        }
    </script>
</asp:Content>

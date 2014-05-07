<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPageFullScreen.Master"
    CodeBehind="InventorySearch.aspx.cs" Inherits="METAOPTION.UI.InventorySearch"
    Title="HeadstartVMS::Inventory Search" %>

<asp:Content ID="cphSearchInventory" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
        div.dealer1 span
        {
            display: none;
        }
    </style>
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upResultSearch" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnOK" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="hfInventoryID" runat="server" Value="0" />
                <asp:HiddenField ID="hfcrquerystring" runat="server" />
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        Inventory Search</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px; display: none" onclick="toggleMe()">
                        <img id="imgCollapse" src="../Images/arrow_up.jpg" alt="" title="Expand" />
                    </div>
                    <div id="dvExpand" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;" onclick="toggleMe()">
                        <img id="imgExpand" src="../Images/arrow_down.jpg" alt="" title="Collapse" />
                    </div>
                </div>
                <%--------------------------------------------------------------------------%>
                <div id="dvSearch" runat="server" class="dvSearch" style="display: block; width: 100%;">
                    <div style="width: 32%; float: left; padding: 5px;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="TableBorder">
                                    Year Range
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="txt1" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFromYear_SelectedIndexChanged" />
                                    &nbsp;&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlToYear" runat="server" CssClass="txt1" />
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
                            <tr>
                                <td class="TableBorder" nowrap="nowrap">
                                    VIN Number
                                </td>
                                <td class="TableBorder" nowrap="nowrap">
                                    <asp:DropDownList ID="ddlVINPattern" runat="server" CssClass="txt1">
                                        <asp:ListItem Value="-1">Exact</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="6">Last 6</asp:ListItem>
                                        <asp:ListItem Value="10">First 10</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtVINNo" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    UCR
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlUCR" runat="server" CssClass="txt2">
                                        <asp:ListItem Text="ALL" Value="-1" Selected="True" />
                                        <asp:ListItem Text="Not Ready" Value="0" />
                                        <asp:ListItem Text="Initiated" Value="1" />
                                        <asp:ListItem Text="Available" Value="2" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Sold Date
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSoldDateFrom" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSoldDateFrom"
                                        PopupButtonID="txtSoldDateFrom" />
                                    <asp:TextBox ID="txtSoldDateTo" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSoldDateTo"
                                        PopupButtonID="txtSoldDateTo" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 26%; float: left; padding: 5px 5px 5px 10px">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="TableBorder" nowrap="nowrap">
                                    Check Number
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCheckNo" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Designation
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Buyer
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlBuyer" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Comeback
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlComeBack" runat="server" CssClass="txt1">
                                        <asp:ListItem Value="-1" Selected="True">Both</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Sold
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlSold" runat="server" CssClass="txt1">
                                        <asp:ListItem Value="-1" Selected="True">Both</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Car Status
                                </td>
                                <td class="lbl">
                                    <asp:DropDownList ID="ddlCarStatus" runat="server" CssClass="txt2">
                                        <asp:ListItem Value="-1" Selected="True">ALL</asp:ListItem>
                                        <asp:ListItem Value="2">Active</asp:ListItem>
                                        <asp:ListItem Value="3">Archived</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 38%; float: left; padding: 5px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 300px" colspan="2">
                                    <asp:UpdatePanel ID="upDealerSelector" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HiddenField ID="hfDealerCustomerType" runat="server" />
                                            <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse;"
                                                id="Table4" runat="server">
                                                <tr>
                                                    <td class="TableBorder" style="width: 95px">
                                                        <asp:Label runat="server" ID="lblCustomer" Text="Sold To" AssociatedControlID="txtCustomer" />
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="txt2" onblur="GetCustomerId(this)" />&nbsp;
                                                        <a href="#" class="lnktxt" onclick="Dealer_Customer(1);"><b>Sold To</b></a>
                                                        <div>
                                                            <ajax:AutoCompleteExtender ID="txtCustomer_AutoCompleteExtender" runat="server" TargetControlID="txtCustomer"
                                                                ServicePath="../WS/AutoFillCustomers.asmx" ServiceMethod="AutoFillCustomers"
                                                                UseContextKey="true" MinimumPrefixLength="2" CompletionSetCount="25" EnableCaching="false"
                                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:">
                                                            </ajax:AutoCompleteExtender>
                                                        </div>
                                                        <asp:HiddenField ID="hfCustomerId" runat="server" Value="-1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorder">
                                                        <asp:Label runat="server" ID="Label1" Text="Purchased From" AssociatedControlID="txtDealer" />
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtDealer" runat="server" CssClass="txt2" onblur="GetDealerId(this)" />
                                                        &nbsp;
                                                        <asp:HyperLink NavigateUrl="#" ID="hlnkSelectDealerOpener" runat="server" CssClass="lnktxt"
                                                            onclick="Dealer_Customer(2);"><b>Purchased From</b></asp:HyperLink>
                                                        <div>
                                                            <ajax:AutoCompleteExtender ID="txtDealer_AutoCompleteExtender" runat="server" TargetControlID="txtDealer"
                                                                ServicePath="../WS/AutoFillCustomers.asmx" ServiceMethod="AutoFillCustomers"
                                                                UseContextKey="true" MinimumPrefixLength="2" CompletionSetCount="25" EnableCaching="false"
                                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:">
                                                            </ajax:AutoCompleteExtender>
                                                        </div>
                                                        <asp:HiddenField ID="hfDealerId" runat="server" Value="-1" />
                                                        <div id="divDealerSelect" runat="server" style="display: none; width: 810px;" class="modalPopup">
                                                            <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse">
                                                                <tr>
                                                                    <td align="left">
                                                                        <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
                                                                            <tr id="popupDrag" runat="server">
                                                                                <td class="PopUpBoxHeading" colspan="5" style="padding-left: 5px">
                                                                                    <asp:Label ID="lblDealerCustomerHeading" runat="server" Text="Select Dealer" />
                                                                                </td>
                                                                                <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                                                                    <%--<img id="ibtnClose" runat="server" border="0" src="../Images/close.gif" title="Close"
                                                                                        alt="" />
                                                                                        <asp:ImageButton ID="imgbtn_close" runat="server" ImageUrl="../Images/close.gif" ToolTip="Close" OnClick="imgbtn_close_click" OnClientClick="ClearPopUpContent()" />--%>
                                                                                    <asp:ImageButton ID="ibtnClose" runat="server" OnClientClick="ClearPopUpContent()"
                                                                                        OnClick="ibtnClose_click" ToolTip="Close" ImageUrl="../Images/close.gif" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TableBorderB" style="width: 100px">
                                                                                    <b>Name</b>
                                                                                </td>
                                                                                <td class="TableBorder" style="width: 150px">
                                                                                    <asp:TextBox ID="txtDealerName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td class="TableBorder" style="width: 100px">
                                                                                    Country
                                                                                </td>
                                                                                <td class="lbl">
                                                                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txt2" AutoPostBack="true"
                                                                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                                                                                </td>
                                                                                <td class="TableBorder" style="width: 100px">
                                                                                    State
                                                                                </td>
                                                                                <td class="TableBorder">
                                                                                    <asp:DropDownList ID="ddlDealerState" runat="server" CssClass="txt2" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TableBorderB">
                                                                                    <b>City</b>
                                                                                </td>
                                                                                <td class="TableBorder">
                                                                                    <asp:TextBox ID="txtCity" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td class="TableBorderB">
                                                                                    <b>Zip</b>
                                                                                </td>
                                                                                <td class="TableBorder">
                                                                                    <asp:TextBox ID="txtZip" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                                </td>
                                                                                <td colspan="2" class="TableBorder" style="text-align: right">
                                                                                    <asp:Button ID="btnSearchDealers" runat="server" CssClass="btn" Text="   Search   "
                                                                                        OnClick="btnSearchDealers_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="gvDealerDetails" runat="server" DataKeyNames="DealerId" AutoGenerateColumns="False"
                                                                            GridLines="Vertical" AllowPaging="True" PageSize="10" HeaderStyle-CssClass="gvHeading"
                                                                            Width="100%" DataSourceID="odsDealerSearch" OnPageIndexChanging="gvDealerDetails_PageIndexChanging">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-Width="25px">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgbtnSelect" runat="server" ImageUrl="~/Images/confirm.gif"
                                                                                            OnClientClick="ClearPopUpContent()" OnClick="imgbtnSelect_Click" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="25px"></HeaderStyle>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DealerId" HeaderText="DealerId" ItemStyle-CssClass="hideCol"
                                                                                    HeaderStyle-CssClass="hideCol">
                                                                                    <HeaderStyle CssClass="hideCol"></HeaderStyle>
                                                                                    <ItemStyle CssClass="hideCol"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DealerName" HeaderText="Name" />
                                                                                <asp:BoundField DataField="City" HeaderText="City" ItemStyle-Width="100px" />
                                                                                <asp:BoundField DataField="State" HeaderText="State" ItemStyle-Width="100px" />
                                                                                <asp:BoundField DataField="Zip" HeaderText="Zip" ItemStyle-Width="50px" />
                                                                                <asp:BoundField DataField="Country" HeaderText="Country" ItemStyle-Width="150px" />
                                                                                <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Width="75px" />
                                                                                <asp:BoundField DataField="DealerType" HeaderText="Type" ItemStyle-Width="75px" />
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="gvHeading" HorizontalAlign="Left" />
                                                                            <RowStyle CssClass="gvRow" />
                                                                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                                                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                        </asp:GridView>
                                                                        <asp:ObjectDataSource ID="odsDealerSearch" runat="server" EnablePaging="True" SelectCountMethod="SearchDealerPagedCount"
                                                                            SelectMethod="SearchDealerPaged" TypeName="METAOPTION.BAL.InventoryBAL">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="txtDealerName" DefaultValue="" Name="strDealerName"
                                                                                    PropertyName="Text" Type="String" />
                                                                                <asp:ControlParameter ControlID="txtCity" DefaultValue="" Name="strCity" PropertyName="Text"
                                                                                    Type="String" />
                                                                                <asp:ControlParameter ControlID="ddlDealerState" Name="dealerStateId" PropertyName="SelectedValue"
                                                                                    Type="Int32" />
                                                                                <asp:ControlParameter ControlID="txtZip" Name="zip" PropertyName="Text" Type="String" />
                                                                                <asp:ControlParameter ControlID="ddlCountry" Name="CountryId" PropertyName="SelectedValue"
                                                                                    Type="Int32" />
                                                                                <asp:Parameter Name="StartRowIndex" Type="Int32" />
                                                                                <asp:Parameter Name="MaximumRows" Type="Int32" />
                                                                                <asp:SessionParameter Name="OrgID" SessionField="OrgID" Type="Int16" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <ajax:ModalPopupExtender ID="mpeOpenDealerSelector" runat="server" TargetControlID="btnCustomerDealerOpennerButton"
                                                            PopupControlID="divDealerSelect" BehaviorID="mpopDealerSelector" BackgroundCssClass="modalBackground"
                                                            PopupDragHandleControlID="popupDrag" DropShadow="false" />
                                                        <%--CancelControlID="ibtnClose"--%>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="hideCol">
                                                <asp:Button ID="btnCustomerDealerOpennerButton" runat="server" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" style="vertical-align: top; width: 87px">
                                    Sort 1
                                </td>
                                <td class="TableBorder" style="vertical-align: top; width: 250px">
                                    <div style="float: left; margin-right: 10px">
                                        <asp:DropDownList ID="ddlSort1" runat="server" CssClass="txt2" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSort1_SelectedIndexChanged" />
                                    </div>
                                    <asp:RadioButtonList ID="rbtnSort1Direction" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                                        <asp:ListItem Value="DESC" Text=" Z - A " Selected="True" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" style="vertical-align: top">
                                    Sort 2
                                </td>
                                <td class="TableBorder" style="vertical-align: top">
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
                                <td class="TableBorder" style="vertical-align: top">
                                    <div style="float: left; margin-right: 10px">
                                        <asp:DropDownList ID="ddlSort3" runat="server" CssClass="txt2" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSort3_SelectedIndexChanged" />
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
                                <td style="padding: 7px 0px 0px 10px">
                                <asp:Button ID="btn_SearchReset" runat="server" Text="Reset" CssClass="btn" Width="100px" OnClick="btn_SearchReset_Click" />
                                    <asp:Button ID="btnSearchInventory" runat="server" Text="Search" CssClass="btn" Width="100px"
                                        OnClick="btnSearchInventory_Click" />
                                </td>
                            </tr>
                        </table>
                         <table><tr><td> <p style="color:Red; font-size:10px"><b>* Search filters will not be reset if the browser back button is clicked after viewing the details on the next page.</b></p></td></tr></table>
                    </div>
                   </div>
                <%--------------------------------------------------------------------------%>
                <div style="clear: both">
                    <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
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
                                <%= gvInventoryList.PageCount%>
                            </td>
                            <td style="text-align: right; padding-right: 10px; color: #21618C">
                                Page size&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPageSize1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="250" Value="250" Selected="True" />
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
                        <asp:GridView ID="gvInventoryList" runat="server" DataKeyNames="InventoryId" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                            RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                            PageSize="50" AllowSorting="true" OnSorting="gvInventoryList_Sorting" OnRowDataBound="gvInventoryList_RowDataBound">
                            <Columns>
                                <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" HeaderStyle-CssClass="GridHeader" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "InventoryDetail.aspx?Mode=View&Code="+Eval("InventoryId") %>'
                                            runat="server" ImageUrl="~/Images/Select.gif" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="VIN" HeaderText="VIN #" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-CssClass="GridContent"
                                    SortExpression="Inventory.[Year]" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="MakeName" HeaderText="Make" ItemStyle-CssClass="GridContent"
                                    SortExpression="Mobile_Chrome_Make.VINDivisionName" HeaderStyle-Font-Underline="true"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="ModelName" HeaderText="Model" ItemStyle-CssClass="GridContent"
                                    SortExpression="Mobile_Chrome_Model.VINModelName" HeaderStyle-Font-Underline="true"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="Body" HeaderText="Body" ItemStyle-CssClass="GridContent"
                                    SortExpression="Mobile_Chrome_Body.VINStyleName" HeaderStyle-Font-Underline="true"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="VehiclePresent" HeaderText="V" ItemStyle-CssClass="GridContent"
                                    SortExpression="Inventory.VehiclePresent" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="Arrival" HeaderText="Arrival Date" ItemStyle-CssClass="GridContent"
                                    SortExpression="[ARRIVALDATE]" DataFormatString="{0:M/d/yyyy}" HeaderStyle-Font-Underline="true"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TitlePresent" HeaderText="T" ItemStyle-CssClass="GridContent"
                                    SortExpression="Inventory.TitlePresent" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="CarCost" HeaderText="Car Cost ($)" ItemStyle-CssClass="GridContentNumbers"
                                    SortExpression="CarCost" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,###}"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="MileageIn" HeaderText="Mileage In" ItemStyle-CssClass="GridContentNumbers"
                                    SortExpression="MileageIn" DataFormatString="{0:#,###}" HeaderStyle-Font-Underline="true"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="ComeBackYes" HeaderText="CB" ItemStyle-CssClass="GridContent"
                                    SortExpression="ComeBackStatus" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="Ext / Int Color" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <%# Eval("ExtColor")%>&nbsp;-&nbsp;<%# Eval("IntColor")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Buyer" HeaderText="Buyer" ItemStyle-CssClass="GridContent"
                                    SortExpression="FirstName" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-Wrap="false" HeaderStyle-Width="50px" />
                                <asp:TemplateField HeaderText="Purchased From" SortExpression="DEALER.DealerName"
                                    HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <div class="dealer">
                                            <%# Eval("DealerName")%>
                                            <span>
                                                <%#String.Format("{0} {1} [ID: {2}]", Eval("DealerName"), Eval("DealerAddress"), Eval("DealerId"))%></span>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SoldToName" HeaderText="Sold To" ItemStyle-CssClass="GridContent"
                                    SortExpression="Customer.DealerName" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-Wrap="false" HeaderStyle-Width="70px" />
                                <asp:BoundField DataField="SoldToState" HeaderText="State" ItemStyle-CssClass="GridContent"
                                    SortExpression="CusState.[State]" HeaderStyle-Font-Underline="true" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="SoldDate" HeaderText="Sold Date" ItemStyle-CssClass="GridContent"
                                    SortExpression="Inventory.SoldDate" DataFormatString="{0:M/d/yyyy}" HeaderStyle-Font-Underline="true"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderText="Check#" SortExpression="Inventory.CheckNumber" HeaderStyle-Font-Underline="true"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylnkCheckNo" runat="server" Text='<%#Eval("CheckNumber") %>'
                                            NavigateUrl='<%# "ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers"
                                    ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnInventoryId" runat="server" Value='<%# Bind("InventoryId") %>' />
                                        <asp:HiddenField ID="hdNVIN" runat="server" Value='<%# Bind("VIN") %>' />
                                        <asp:ImageButton ID="ibtnCR" runat="server" OnClick="ibtnCR_Click" />
                                        <asp:HyperLink ID="ancrCR" runat="server" Target="_blank" Visible="false">
                                            <asp:Image ID="imggCR" runat="server" />
                                        </asp:HyperLink>
                                        <a id="ibtnCRAvailable" runat="server" target="_blank" href="#" visible="false" style="text-decoration: none">
                                            <img id="imgAvailabel" alt="No Image" src="../Images/ucr-btn.png" />
                                        </a>
                                        <asp:ImageButton ID="ibtncars" runat="server" ImageUrl="~/Images/car_icon.gif" OnClick="ibtncars_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ImageCount" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblImageCount" runat="server" Text='<%# Bind("ImageCount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CRStatus" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCRStatus" runat="server" Text='<%# Eval("Inv_CRStatus") %>'></asp:Label>
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
                    <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblCount1" runat="server" BorderColor="Transparent" BackColor="Transparent"
                                    ForeColor="#21618C" />&nbsp;
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn" OnClick="btnExport_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                                Page
                                <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                                    AutoPostBack="true" />
                                of
                                <%= gvInventoryList.PageCount%>
                            </td>
                            <td style="text-align: right; padding-right: 10px; color: #21618C">
                                Page size&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPageSize2" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="250" Value="250" Selected="True" />
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
                <%--------------------UCR BEGIN--------------------%>
                <div id="dvUCR" class="modalPopup" style="display: none; min-width: 400px; min-height: 200px"
                    runat="server">
                    <div>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="PopUpBoxHeading">
                                    &nbsp;&nbsp;Add/Link UCR
                                    <asp:Label ID="lblucrheader" runat="server"></asp:Label>
                                </td>
                                <td class="PopUpBoxHeading" align="right">
                                    <img border="0" src="../Images/close.gif" id="imgCloseCR" alt="close" />
                                </td>
                            </tr>
                        </table>
                        <div style="padding: 10px 0px; margin: 0px 10px" class="LeftPanelContentHeading">
                            What do you want to do?</div>
                        <div style="padding: 0px 5px; margin: 0px10px">
                            <asp:RadioButtonList ID="rbtnListCR" runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="rbtnListCR_SelectedIndexChanged"
                                CellSpacing="5">
                                <asp:ListItem Text="Create CR" Value="1" class="LeftPanelContentHeading" Selected="True" />
                                <asp:ListItem Text="Link CR" Value="2" class="LeftPanelContentHeading" />
                            </asp:RadioButtonList>
                        </div>
                        <div id="dvLinkCR" runat="server" style="padding: 0 0 5px 25px; margin: 0 0px 0px 10px;
                            display: none; width: 100%">
                            <div style="float: left;">
                                <asp:RadioButtonList ID="rbtnCRIdUrl" runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="rbtnCRIdUrl_SelectedIndexChanged"
                                    CellSpacing="5">
                                    <asp:ListItem Text="CR ID" Value="1" Selected="True" class="LeftPanelContentHeading" />
                                    <asp:ListItem Text="CR URL" Value="2" class="LeftPanelContentHeading" />
                                    <asp:ListItem Text="Find By VIN" Value="3" class="LeftPanelContentHeading" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div id="dvCRIdUrl" runat="server" style="padding: 5px; margin: 0 0px 0px 42px; display: none;
                            clear: both;">
                            <asp:TextBox ID="txtCRIdUrl" runat="server" CssClass="txt1" />
                        </div>
                        <div style="padding: 5px 0px 0px 15; margin: 5px 0px 10px 30px">
                            <asp:Button ID="btncridvalidate" runat="server" Text="Validate" Style="display: none;"
                                CssClass="btn" OnClick="btncridvalidate_Click" Width="80px" />
                            <asp:Button ID="btncrurlvalidate" runat="server" Text="Validate" CssClass="btn" Style="display: none;"
                                OnClick="btncrurlvalidate_Click" Width="80px" />
                            <asp:Button ID="btncrsearch" runat="server" Text="Search" CssClass="btn" Style="display: none;"
                                OnClick="btncrsearch_Click" Width="80px" />
                            <asp:Button ID="btnCRok" runat="server" Text="OK" OnClick="btnCRok_Click" OnClientClick="OpenCreateUCRLink();"
                                CssClass="btn" Width="80px" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCRcancel" runat="server" Text="Cancel" CssClass="btn" Width="80px"
                                OnClick="btnCRcancel_Click" />
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfCR" runat="server" />
                <asp:HiddenField ID="hfvin" runat="server" />
                <ajax:ModalPopupExtender ID="mpeCR" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="mpecrbeh" TargetControlID="hfCR" PopupControlID="dvUCR" CancelControlID="imgCloseCR">
                </ajax:ModalPopupExtender>
                <%--------------------UCR END--------------------%>
                <%----------------------Change UCR Begin----------------------%>
                <div id="dvChangeUCR" class="modalPopup" style="display: none; min-width: 400px;
                    min-height: 200px" runat="server">
                    <div>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="PopUpBoxHeading">
                                    &nbsp;&nbsp;Change UCR
                                    <asp:Label ID="lblucrheader2" runat="server"></asp:Label>
                                </td>
                                <td class="PopUpBoxHeading" align="right">
                                    <img border="0" src="../Images/close.gif" id="imgCloseCR2" alt="close" />
                                </td>
                            </tr>
                        </table>
                        <div style="padding: 10px 0px; margin: 0px 10px" class="LeftPanelContentHeading">
                            What do you want to do?</div>
                        <div style="padding: 0px 5px; margin: 0px10px">
                            <asp:RadioButtonList ID="rbtnlistchangecr" runat="server" RepeatDirection="Vertical"
                                CellSpacing="5">
                                <asp:ListItem Text="Remove UCR" Value="1" class="LeftPanelContentHeading" Selected="True" />
                                <asp:ListItem Text="Relink UCR" Value="2" class="LeftPanelContentHeading" />
                            </asp:RadioButtonList>
                        </div>
                        <div id="dvLinkCR2" runat="server" style="padding: 0 0 5px 25px; margin: 0 0px 0px 10px;
                            display: none; width: 100%">
                            <div style="float: left;">
                                <asp:RadioButtonList ID="rbtnChangeCRIdUrl" runat="server" RepeatDirection="Vertical"
                                    CellSpacing="5">
                                    <asp:ListItem Text="CR ID" Value="1" Selected="True" class="LeftPanelContentHeading" />
                                    <asp:ListItem Text="CR URL" Value="2" class="LeftPanelContentHeading" />
                                    <asp:ListItem Text="Find By VIN" Value="3" class="LeftPanelContentHeading" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div id="dvCRIdUrl2" runat="server" style="padding: 5px; margin: 0 0px 0px 42px;
                            display: none; clear: both;">
                            <asp:TextBox ID="txtCRIdUrl2" runat="server" CssClass="txt1" />
                        </div>
                        <div style="padding: 5px 0px 0px 15; margin: 5px 0px 10px 30px">
                            <asp:Button ID="btncridvalidate2" runat="server" Text="Validate" CssClass="btn" Width="80px"
                                Style="display: none;" OnClick="btncridvalidate2_Click" />
                            <asp:Button ID="btncrurlvalidate2" runat="server" Text="Validate" CssClass="btn"
                                Width="80px" Style="display: none;" OnClick="btncrurlvalidate2_Click" />
                            <asp:Button ID="btncrsearch2" runat="server" Text="Search" CssClass="btn" Style="display: none;"
                                Width="80px" OnClick="btncrsearch2_Click" />
                            <asp:Button ID="btnCRok2" runat="server" Text="OK" OnClick="btnCRok2_Click" CssClass="btn"
                                Width="80px" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCRcancel2" runat="server" Text="Cancel" CssClass="btn" Width="80px"
                                OnClick="btnCRcancel_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="hlnkViewReport" runat="server" Text="View Report" Target="_blank"
                                CssClass="btn" Width="100px" />
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfCR2" runat="server" />
                <ajax:ModalPopupExtender ID="mpeChangeCR" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="mpecrbeh2" TargetControlID="hfCR2" PopupControlID="dvChangeUCR" CancelControlID="imgCloseCR2">
                </ajax:ModalPopupExtender>
                <%------------------------Change UCR End------------------------%>
                <%--Show UCR Response Begin--%>
                <asp:Panel ID="pnlUCRResponse" CssClass="modalPopup" Style="display: none; height: auto;
                    width: 700px;" runat="server" HorizontalAlign="Left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;UCR Details
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="imgucrclose" alt="close" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 330px;">
                                <iframe id="iframeucr" scrolling="no" style="width: 700px; height: 330px;" frameborder="0"
                                    runat="server"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; display: none;">
                                <asp:Button ID="btnucrok" runat="server" Text="OK" />
                                <asp:Button ID="btnucrcancel" runat="server" Text="Cancel" OnClick="btnucrcancel_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:HiddenField ID="hfucr" runat="server" />
                <ajax:ModalPopupExtender ID="mpucr" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="hfucr" PopupControlID="pnlUCRResponse" CancelControlID="imgucrclose"
                    OkControlID="btnucrok">
                </ajax:ModalPopupExtender>
                <%--Show UCR Response End--%>
                <%----------Image slideshow----------%>
                <asp:HiddenField ID="hfPopup" runat="server" />
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfPopup"
                    PopupControlID="panOpen" BackgroundCssClass="modalBackground" CancelControlID="imgCancel"
                    PopupDragHandleControlID="panOpen" BehaviorID="ImageModelPopup" />
                <asp:Panel ID="panOpen" runat="server" Height="680px" Width="750px" CssClass="ModalWindow">
                    <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
                        width: 662px; display: none">
                        <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" />
                    </div>
                    <iframe id="ifrmSlideShow" runat="server" scrolling="no" style="height: 680px; width: 720px;"
                        frameborder="0"></iframe>
                </asp:Panel>
                <%----------Image slideshow----------%>
                <%----------Added by Rupendra 16 Nov 12 for show Exprt popup----------%>
                <asp:HiddenField ID="hdnExport" runat="server" />
                <ajax:ModalPopupExtender ID="ModalPopupExtenderExport" runat="server" TargetControlID="hdnExport"
                    PopupControlID="pnlExport" BackgroundCssClass="modalBackground" CancelControlID="btnCancel"
                    PopupDragHandleControlID="pnlExport" BehaviorID="mpeExport" />
                <asp:Panel ID="pnlExport" runat="server" CssClass="popup_Body" Style="display: none;
                    background: white; width: 600px; height: 200px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading" style="height: 20px">
                                &nbsp;&nbsp;<asp:Label ID="lblHeader" runat="server"></asp:Label>
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="imgClose" onclick="HidempeExport();"
                                    alt="close" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftPanelContentHeading">
                                You are trying to export more data. This will slow down the system.<br />
                                You can export only 5,000 records in excel.<br />
                                Do you want to export?
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; margin-top: 50px">
                                <asp:Button ID="btnOK" runat="server" CssClass="btn" Text="Yes" OnClick="btnOK_Click"
                                    Width="80px" OnClientClick="return HidempeExport();" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="No" Width="80px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%----------Image slideshow----------%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">

        var BrowserName = $.browser.name;

        function Dealer_Customer(dcType) {
            ClearPopUpContent();
            document.getElementById('<%=hfDealerCustomerType.ClientID %>').value = dcType;
            if (dcType == 1)   //Customer
            {
                document.getElementById('<%=lblDealerCustomerHeading.ClientID %>').innerHTML = "Select Customer";
            }
            else              // Dealer
            {
                document.getElementById('<%=lblDealerCustomerHeading.ClientID %>').innerHTML = "Select Dealer";
            }
            document.getElementById('<%=btnCustomerDealerOpennerButton.ClientID %>').click();
        }

        function toggleMe() {
            $(".TOtoggle").toggle();
            $('.dvSearch').toggle();
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

        function GetCustomerId(txt) {

            var hfcontrol = document.getElementById('<%=hfCustomerId.ClientID %>');
            if (txt.value.length == 0) {
                hfcontrol.value = "-1"; return;
            }
            var str = (txt.value).split("ID:");
            if (str.length > 1) {
                txt.value = str[0];
                hfcontrol.value = str[1];
            }
        }

        function GetDealerId(txtD) {


            var hfcontrolD = document.getElementById('<%=hfDealerId.ClientID %>');
            if (txtD.value.length == 0) {
                hfcontrolD.value = "-1"; return;
            }
            var strD = (txtD.value).split("ID:");
            if (strD.length > 1) {
                txtD.value = strD[0];
                hfcontrolD.value = strD[1];
            }
        }

        function ClearPopUpContent() {
            document.getElementById('<%=txtDealerName.ClientID %>').value = "";
            document.getElementById('<%=ddlDealerState.ClientID %>').selectedIndex.value = "-1";
            document.getElementById('<%=txtCity.ClientID %>').value = "";
            document.getElementById('<%=txtZip.ClientID %>').value = "";
            var ddlCountry = document.getElementById('<%=ddlCountry.ClientID %>');
            ddlCountry.value = "-1";
            var ddlState = document.getElementById('<%=ddlDealerState.ClientID %>');
            ddlState.value = "-1";
            document.oncontextmenu = function ()
            { event.returnValue = false; }

        }

        function ChangeCSS(rbl) {
            var url = '<%=UCRlinkUrl %>';
            var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
            if (selectedvalue == "1") {
                $('#<%=txtCRIdUrl.ClientID %>').attr('readonly', false);
                document.getElementById('<%=txtCRIdUrl.ClientID %>').style.width = "150px";
                $('#<%=btncridvalidate.ClientID %>').show();
                $('#<%=btncrurlvalidate.ClientID %>').hide();
                $('#<%=btncrsearch.ClientID %>').hide();
                $('#<%=txtCRIdUrl.ClientID %>').val('');
            }
            else if (selectedvalue == "2") {
                $('#<%=txtCRIdUrl.ClientID %>').attr('readonly', false);
                document.getElementById('<%=txtCRIdUrl.ClientID %>').style.width = "300px";
                $('#<%=btncridvalidate.ClientID %>').hide();
                $('#<%=btncrurlvalidate.ClientID %>').show();
                $('#<%=btncrsearch.ClientID %>').hide();
                //$('#<%=txtCRIdUrl.ClientID %>').val('http://web.metaoptionllc.com:82/Report/');
                $('#<%=txtCRIdUrl.ClientID %>').val(url);
            }
            else if (selectedvalue == "3") {
                var vin = $('#<%=hfvin.ClientID %>').val();
                document.getElementById('<%=txtCRIdUrl.ClientID %>').style.width = "150px";
                $('#<%=btncridvalidate.ClientID %>').hide();
                $('#<%=btncrurlvalidate.ClientID %>').hide();
                $('#<%=btncrsearch.ClientID %>').show();
                $('#<%=txtCRIdUrl.ClientID %>').val(vin);
                $('#<%=txtCRIdUrl.ClientID %>').attr('readonly', true);
            }
        }

        function ShowHideLinkCR(rbtn) {
            var selvalue = $("#" + rbtn.id + " input:radio:checked").val();

            if (selvalue == "1") {
                $('#<%=dvLinkCR.ClientID %>').hide();
                $('#<%=dvCRIdUrl.ClientID %>').hide();
                $('#<%=btnCRok.ClientID %>').show();
                $('#<%=btncridvalidate.ClientID %>').hide();
                $('#<%=btncrurlvalidate.ClientID %>').hide();
                $('#<%=btncrsearch.ClientID %>').hide();
            }
            else if (selvalue == "2") {
                $('#<%=dvLinkCR.ClientID %>').show();
                $('#<%=dvCRIdUrl.ClientID %>').show();
                $('#<%=btnCRok.ClientID %>').hide();
                $('#<%=rbtnCRIdUrl.ClientID %>').find("input[value='1']").attr("checked", "checked");
                $('#<%=txtCRIdUrl.ClientID %>').val('');
                $('#<%=btncridvalidate.ClientID %>').show();
                $('#<%=btncrurlvalidate.ClientID %>').hide();
                $('#<%=btncrsearch.ClientID %>').hide();
            }
        }

        function OpenCreateUCRLink() {
            debugger;
            var url = '<%=CreateUCRUrl %>';
            var qstring = $('#<%=hfcrquerystring.ClientID %>').val();
            //window.open("http://web.metaoptionllc.com:82/cardetail/newcr?"+querystring);
            window.open(url + qstring);
        }

        function ShowHideChangeCR(rbtn) {
            var selvalue = $("#" + rbtn.id + " input:radio:checked").val();

            if (selvalue == "1") {
                $('#<%=dvLinkCR2.ClientID %>').hide();
                $('#<%=dvCRIdUrl2.ClientID %>').hide();
                $('#<%=btnCRok2.ClientID %>').show();
                $('#<%=btncridvalidate2.ClientID %>').hide();
                $('#<%=btncrurlvalidate2.ClientID %>').hide();
                $('#<%=btncrsearch2.ClientID %>').hide();
            }
            else if (selvalue == "2") {
                $('#<%=dvLinkCR2.ClientID %>').show();
                $('#<%=dvCRIdUrl2.ClientID %>').show();
                $('#<%=btnCRok2.ClientID %>').hide();
                $('#<%=rbtnChangeCRIdUrl.ClientID %>').find("input[value='1']").attr("checked", "checked");
                $('#<%=txtCRIdUrl2.ClientID %>').val('');
                $('#<%=btncridvalidate2.ClientID %>').show();
                $('#<%=btncrurlvalidate2.ClientID %>').hide();
                $('#<%=btncrsearch2.ClientID %>').hide();

            }
        }

        function ChangeCSS2(rbl) {
            var url = '<%=UCRlinkUrl %>';
            var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
            if (selectedvalue == "1") {
                document.getElementById('<%=txtCRIdUrl2.ClientID %>').style.width = "150px";
                $('#<%=btncridvalidate2.ClientID %>').show();
                $('#<%=btncrurlvalidate2.ClientID %>').hide();
                $('#<%=btncrsearch2.ClientID %>').hide();
                $('#<%=txtCRIdUrl2.ClientID %>').val('');
                $('#<%=txtCRIdUrl2.ClientID %>').attr('readonly', false);
            }
            else if (selectedvalue == "2") {
                document.getElementById('<%=txtCRIdUrl2.ClientID %>').style.width = "300px";
                $('#<%=btncridvalidate2.ClientID %>').hide();
                $('#<%=btncrurlvalidate2.ClientID %>').show();
                $('#<%=btncrsearch2.ClientID %>').hide();
                //$('#<%=txtCRIdUrl.ClientID %>').val('http://web.metaoptionllc.com:82/Report/');
                $('#<%=txtCRIdUrl2.ClientID %>').val(url);
                $('#<%=txtCRIdUrl2.ClientID %>').attr('readonly', false);
            }
            else if (selectedvalue == "3") {
                var vin = $('#<%=hfvin.ClientID %>').val();
                document.getElementById('<%=txtCRIdUrl2.ClientID %>').style.width = "150px";
                $('#<%=btncridvalidate2.ClientID %>').hide();
                $('#<%=btncrurlvalidate2.ClientID %>').hide();
                $('#<%=btncrsearch2.ClientID %>').show();
                $('#<%=txtCRIdUrl2.ClientID %>').val(vin);
                $('#<%=txtCRIdUrl2.ClientID %>').attr('readonly', true);
            }
        }

        if (BrowserName == "firefox") {
            $('#<%=rbtnListCR.ClientID %>').live("change", function () { ShowHideLinkCR(this); });
            $('#<%=rbtnCRIdUrl.ClientID %>').live("change", function () { ChangeCSS(this); });

            $('#<%=rbtnlistchangecr.ClientID %>').live("change", function () { ShowHideChangeCR(this); });
            $('#<%=rbtnChangeCRIdUrl.ClientID %>').live("change", function () { ChangeCSS2(this); });
        }
        else if (BrowserName == "msie") {
            $('#<%=rbtnListCR.ClientID %>').live("change", function () { ShowHideLinkCR(this); });
            $('#<%=rbtnCRIdUrl.ClientID %>').live("change", function () { ChangeCSS(this); });

            $('#<%=rbtnlistchangecr.ClientID %>').live("change", function () { ShowHideChangeCR(this); });
            $('#<%=rbtnChangeCRIdUrl.ClientID %>').live("change", function () { ChangeCSS2(this); });
        }
        else if (BrowserName == "chrome") {
            $('#<%=rbtnListCR.ClientID %>').live("click", function () { ShowHideLinkCR(this); });
            $('#<%=rbtnCRIdUrl.ClientID %>').live("click", function () { ChangeCSS(this); });

            $('#<%=rbtnlistchangecr.ClientID %>').live("click", function () { ShowHideChangeCR(this); });
            $('#<%=rbtnChangeCRIdUrl.ClientID %>').live("click", function () { ChangeCSS2(this); });
        }
    </script>
    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function HideModelpopup() {
            $find('ImageModelPopup').hide();
            return false;
        }

        function HidempeExport() {
            var ss = $find('mpeExport');
            if (ss != null)
                ss.hide();
            return true;
        }

    </script>
</asp:Content>

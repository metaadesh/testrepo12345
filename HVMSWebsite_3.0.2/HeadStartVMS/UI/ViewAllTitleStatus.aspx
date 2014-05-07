<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ViewAllTitleStatus.aspx.cs"
    Inherits="METAOPTION.UI.ViewAllTitleStatus" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upViewAllTitleGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        View All Title Status</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;">
                    </div>
                </div>
                <div id="dvSearch" runat="server" class="dvSearch">
                    <div style="width: 30%; float: left; padding: 5px;">
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
                                <td class="TableBorder" style="width: 100px">
                                    Year
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="txt1" />
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
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlModel" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Late Fee
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlLateFee" runat="server" CssClass="txt2">
                                        <asp:ListItem Text="ALL" Value="-1" Selected="True" />
                                        <asp:ListItem Text="Yes" Value="1" />
                                        <asp:ListItem Text="No" Value="0" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 28%; float: left; padding: 5px 5px 5px 10px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 300px" colspan="2">
                                    <asp:UpdatePanel ID="upDealerSelector" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HiddenField ID="hfDealerCustomerType" runat="server" />
                                            <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse;"
                                                id="Table4" runat="server">
                                                <tr>
                                                    <td class="TableBorder" style="width: 108px">
                                                        <asp:Label runat="server" ID="Label1" Text="Purchased From" AssociatedControlID="txtDealer" />
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtDealer" runat="server" CssClass="txt2" onblur="GetDealerId(this)"
                                                            autocomplete="off" Wrap="false" />
                                                        &nbsp;<br />
                                                        <asp:HyperLink NavigateUrl="#" ID="hlnkSelectDealerOpener" runat="server" CssClass="lnktxt"
                                                            onclick="Dealer_Customer(2);"><b>Purchased From</b></asp:HyperLink>
                                                        <div>
                                                        
                                                            <ajax:AutoCompleteExtender ID="txtDealer_AutoCompleteExtender" runat="server" TargetControlID="txtDealer"
                                                                ServicePath="../WS/AutoFillCustomers.asmx" ServiceMethod="AutoFillCustomers" UseContextKey="true"
                                                                MinimumPrefixLength="2" CompletionSetCount="25" EnableCaching="false" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" BehaviorID="AutoCompleteset"
                                                                OnClientPopulated="onListPopulated" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                DelimiterCharacters=";,:">
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
                                                                                    <%--<img id="ibtnClose" runat="server" border="0" onclick='ClearPopUpContent()' src="../Images/close.gif" title="Close"
                                                                                        alt="" />--%>
                                                                                    <asp:ImageButton ID="ibtnClose" runat="server" OnClientClick="ClearPopUpContent()"  OnClick="ibtnClose_click" ToolTip="Close" ImageUrl="../Images/close.gif" />
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
                                                                       <%-- <asp:UpdatePanel ID="UpdatePanelx" runat="server"UpdateMode="Conditional">
                                                                                     
                                                                           <ContentTemplate>--%>
                                                                        <asp:GridView ID="gvDealerDetails" runat="server" DataKeyNames="DealerId" AutoGenerateColumns="False"
                                                                            GridLines="Vertical" AllowPaging="True" PageSize="10" HeaderStyle-CssClass="gvHeading"
                                                                            Width="100%" DataSourceID="odsDealerSearch" OnPageIndexChanging="gvDealerDetails_PageIndexChanging">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-Width="25px">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgbtnSelect" runat="server" ImageUrl="~/Images/confirm.gif"
                                                                                            OnClick="imgbtnSelect_Click" OnClientClick="ClearPopUpContent()"/>
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
                                                                                <asp:SessionParameter Name="OrgID" Type="Int16" SessionField="OrgID" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                      <%--  </ContentTemplate>
                                                                      </asp:UpdatePanelx>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <ajax:ModalPopupExtender ID="mpeOpenDealerSelector" runat="server" TargetControlID="btnCustomerDealerOpennerButton"
                                                            PopupControlID="divDealerSelect" BehaviorID="mpopDealerSelector" BackgroundCssClass="modalBackground"
                                                            PopupDragHandleControlID="popupDrag" DropShadow="false"  /><%--CancelControlID="ibtnClose"--%>
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
                                <td class="TableBorder">
                                    Purchased date
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
                                <td class="TableBorder" style="width: 100px">
                                    Title Present
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                    <asp:DropDownList ID="ddlTitlePresent" runat="server" CssClass="txt2">
                                        <asp:ListItem Text="ALL" Value="-1" />
                                        <asp:ListItem Text="Yes" Value="1" />
                                        <asp:ListItem Text="No" Value="0" Selected="True" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Car Status
                                </td>
                                <td class="lbl">
                                    <asp:DropDownList ID="ddlCarStatus" runat="server" CssClass="txt2">
                                        <asp:ListItem Value="-1">ALL</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="3">Archived</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 38%; float: left; padding: 5px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
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
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upViewAllTitleGrid">
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
                                <%= gvViewAllTitleStatus.PageCount%>
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
                        <asp:GridView ID="gvViewAllTitleStatus" runat="server" DataKeyNames="InventoryId"
                            Width="100%" AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                            RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                            PageSize="50" AllowSorting="true" OnSorting="gvViewAllTitleStatus_Sorting" CssClass="Grid">
                            <Columns>
                                <asp:TemplateField HeaderText="VIN#" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylnk" runat="server" ForeColor="#2C2C2C" Text='<%#Eval("VIN")%>'
                                            NavigateUrl='<%#"~/UI/InventoryDetail.aspx?Mode=View&Code="+Eval("InventoryId")%>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--   <asp:BoundField DataField="VIN" HeaderText="Vin#" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />--%>
                                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="I.[Year]" />
                                <asp:BoundField DataField="MakeName" HeaderText="Make" SortExpression="Mobile_Chrome_Make.VINDivisionName "
                                    ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="ModelName" HeaderText="Model" SortExpression="Mobile_Chrome_Model.VINModelName "
                                    ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="Body" HeaderText="Body" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="Mobile_Chrome_Body.VINStyleName" />
                                <asp:BoundField DataField="MileageIn" HeaderText="Mileage" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridHeader" DataFormatString="{0:#,###}" SortExpression="I.MileageIn" />
                                <asp:BoundField DataField="CarCost" HeaderText="Cost" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridHeader" DataFormatString="{0:#,###}" SortExpression="I.CarCost" />
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="GridHeader" HeaderText="Check#"
                                    ItemStyle-CssClass="GridContentNumbers">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylnk" runat="server" ForeColor="#2C2C2C" Text='<%#Eval("CheckNumber")%>'
                                            NavigateUrl='<%#"~/UI/ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId")%>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--   <asp:BoundField DataField="CheckNumber" HeaderText="Check#" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="I.CheckNumber" HeaderStyle-Width="50px" />--%>
                                <asp:BoundField DataField="TitlePresent" HeaderText="Title " ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="I.TitlePresent" />
                                <asp:TemplateField HeaderText="Sold to Dealer" SortExpression="DS.DealerName" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <div class="dealer">
                                            <%#Eval("SoldDealer")%>
                                            <span>
                                                <%#String.Format("{0} {1} [ID: {2}]", Eval("SoldDealer"), Eval("SoldDealerAddress"), Eval("SoldDealerId"))%></span>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SoldDate" HeaderText="Date Sold" ItemStyle-CssClass="GridContent" DataFormatString="{0:MM/dd/yyyy}" 
                                    HeaderStyle-CssClass="GridHeader" SortExpression="I.SoldDate" />
                                <asp:TemplateField HeaderText="Purchased From" SortExpression="D.DealerName" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <div class="dealer">
                                            <%#Eval("DealerName")%>
                                            <span>
                                                <%#String.Format("{0} {1} [ID: {2}]", Eval("DealerName"), Eval("DealerAddress"), Eval("DealerId"))%></span>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Purchased Date" SortExpression="I.PurchaseDate" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                    <%# String.Format("{0:MM/dd/yyyy}", Convert.ToString(Eval("PurchaseDate")))%>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <asp:BoundField DataField="PurchaseDate" SortExpression="I.PurchaseDate" HeaderText="Purchased Date"   DataFormatString="{0:MM/dd/yyyy}"
                                    ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="DaysDelayed" HeaderText="Days Delayed" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridHeader" DataFormatString="{0:#,###}" HeaderStyle-Width="30px"
                                    SortExpression="BT.DaysDelayed" />
                                <asp:BoundField DataField="LateFee" HeaderText="Late Fee ($)" SortExpression="LateFee"
                                    ItemStyle-CssClass="GridContentNumbers" DataFormatString="{0:#,###0}" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-Width="50px" />
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
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                                Page
                                <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                                    AutoPostBack="true" />
                                of
                                <%= gvViewAllTitleStatus.PageCount%>
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

        function onListPopulated() {
            var completionList = $find("AutoCompleteset").get_completionList();
            completionList.style.width = 'auto';
        }

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

    </script>
</asp:content>

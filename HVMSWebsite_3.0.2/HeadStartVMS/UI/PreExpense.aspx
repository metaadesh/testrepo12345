<%@ Page Title="HeadstartVMS::Mobile Expenses" Language="C#" AutoEventWireup="true"
    CodeBehind="PreExpense.aspx.cs" EnableEventValidation="false" Inherits="METAOPTION.UI.PreExpense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<asp:content id="cphPreExpense" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upResultGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdnVIN" runat="server" />
                <asp:HiddenField ID="hdnExpTypeID" runat="server" />
                <asp:HiddenField ID="hdnExpAmount" runat="server" />
                <asp:HiddenField ID="hdnEntityID" runat="server" />
                <asp:HiddenField ID="hdnEntityTypeID" runat="server" />
                <asp:HiddenField ID="hfSelectedCount" runat="server" Value="0" />
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div id="mainlbl" runat="server" style="width: 90%; padding: 0px 0px; float: left">
                        VIEW ALL PRE-EXPENSES</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;">
                        <asp:Button ID="btnapprove" Text="Approve" class="btn" runat="server" OnClick="btnapprove_Click"
                            OnClientClick="return ValidateChecked();" />
                    </div>
                </div>
                <div id="dvSearch" runat="server" class="dvSearch" style="display: block; width: 100%;">
                    <div style="width: 32%; float: left; padding: 5px;">
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
                                <td class="TableBorder" style="width: 100px">
                                    Added By
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                    <asp:DropDownList ID="ddlAddedBy" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Vendor
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlVendor" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 26%; float: left; padding: 5px 5px 5px 10px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    Status
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txt2">
                                        <asp:ListItem Text="ALL" Value="-1" />
                                        <asp:ListItem Text="Pending" Value="0" Selected="True" />
                                        <asp:ListItem Text="Rejected" Value="1" />
                                        <asp:ListItem Text="Approved" Value="2" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Sync Date
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSyncDateFrom" runat="server" CssClass="txt1" Width="78px" />
                                    <Ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSyncDateFrom"
                                        PopupButtonID="txtSyncDateFrom" />
                                    <asp:TextBox ID="txtSyncDateTo" runat="server" CssClass="txt1" Width="78px" />
                                    <Ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSyncDateTo"
                                        PopupButtonID="txtSyncDateTo" />
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
                                        OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
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
                                <%= gvExpenseList.PageCount%>
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
                        <asp:GridView ID="gvExpenseList" runat="server" DataKeyNames="PreExpenseID" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                            RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                            PageSize="50" AllowSorting="true" OnSorting="gvExpenseList_Sorting" CssClass="Grid"
                            OnRowDataBound="gvExpenseList_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true" HeaderStyle-Width="10px"
                                    ItemStyle-Width="10px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkallappr" runat="server" onclick="javascript:SelectAllCheckboxes1(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkappr" runat="server" onclick="ChangeCount(this);" />
                                        <asp:ImageButton ID="ibtnappr" runat="server" Visible="false" ImageUrl="~/Images/H_active.png"
                                            ToolTip="Approved" CssClass="Tooltip" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VIN#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <div style="text-transform: uppercase">
                                            <asp:Label ID="lblvin" runat="server" Text='<%# Eval("VIN") %>' CssClass="Tooltip"></asp:Label>
                                            <asp:HyperLink ID="hlnkVIN" NavigateUrl='<%# "InventoryExpense.aspx?Code="+Eval("InventoryId")%>'
                                                runat="server" Text='<%#Eval("VIN") %>' Visible="false" /><br />
                                            <asp:Label ID="lblCode" runat="server" Text="Code: " Visible="false" />
                                            <asp:HyperLink ID="hlnkCode" runat="server" Visible="false" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Width="30px" />
                                <asp:TemplateField HeaderText="Make<br />Model" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="GridContent" ItemStyle-Width="70px"
                                    HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <%#Eval("Make") %><br />
                                        <%#Eval("Model") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Added By" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <%#Eval("AddedBy")%><br />
                                        (<%#Eval("EntityType")%>)
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    SortExpression="V.VendorName" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td style="width: 70%;">
                                                        <asp:HyperLink ID="hlnkVendor" runat="server" Text='<%#Eval("Vendor") %>' NavigateUrl='<%# "ViewVendor.aspx?Mode=View&EntityId="+Eval("VendorID")+"&type=3" %>' />
                                                    </td>
                                                    <td style="border-width: 0px; width: 15%">
                                                        <asp:ImageButton ID="imgDriverEmailStatus" runat="server" ImageUrl="~/Images/driveremailsent.png"
                                                            ToolTip="Driver Email Status" OnClientClick='<%# String.Format("DriverEmailStatusPopup(\"{0}\");return false;",
                                                             Eval("PreExpenseID")) %>' /><%-- OnClick="imgDriverEmailStatus_Click"--%>
                                                    </td>
                                                    <td style="border-width: 0px; position: relative; top: 1px; width: 15%">
                                                        <asp:ImageButton ID="imgDriverSMSStatus" runat="server" ImageUrl="~/Images/sms-sent.png"
                                                            OnClientClick='<%# String.Format("DriverSMSDetailPopup(\"{0}\");return false;", Eval("PreExpenseID")) %>'
                                                            ToolTip="Driver SMS Status" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="text-align: left;">
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# FormatDescription(Eval("Description")) %>'
                                                ToolTip='<%# Eval("Description") %>' CssClass="Tooltip" />
                                        </div>
                                        <asp:HiddenField ID="hfIsDriverEmailSent" runat="server" Value='<%# Eval("IsDriverEmailSent") %>' />
                                        <asp:HiddenField ID="hfIsDriverSMSSent" runat="server" Value='<%# Eval("IsDriverSMSSent") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="ExpenseType" HeaderText="Expense" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="ET.ExpenseType" ItemStyle-Width="80px" />--%>

                                 <asp:TemplateField HeaderText="Expense" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="ET.ExpenseType" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpenseType" runat="server" Text='<%# FormatExpense(Eval("ExpenseType"),Eval("Zone"),Eval("Distance")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" ItemStyle-CssClass="GridContent"
                                    ItemStyle-Width="60px" HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="true"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" SortExpression="E.ExpenseDate" />
                                <asp:BoundField DataField="Count" HeaderText="Count" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###}" ItemStyle-CssClass="GridContentNumbers"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20px" SortExpression="E.Count" />
                                <asp:BoundField DataField="DefaultPrice" HeaderText="Default Price($)" ItemStyle-CssClass="GridContentNumbers"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="GridHeader" DataFormatString="{0:#,###}"
                                    HeaderStyle-Wrap="true" HeaderStyle-Width="30px" ItemStyle-Width="30px" SortExpression="E.DefaultPrice" />
                                <%--<asp:BoundField DataField="TotalPrice" HeaderText="Total Price($)" ItemStyle-CssClass="GridContentNumbers"
                                    DataFormatString="{0:#,###}" HeaderStyle-Width="30px" ItemStyle-Width="30px" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right" SortExpression="E.TotalPrice" />--%>
                                <asp:TemplateField HeaderText="Total Price($)" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContentNumbers" HeaderStyle-Width="30px" ItemStyle-Width="30px"
                                    HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right" SortExpression="E.TotalPrice">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalprice" runat="server" Text='<%# String.Format("{0:#,###}",Eval("TotalPrice")) %>' />
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/Images/newedit.gif" ToolTip="Edit Expense"
                                            Visible="false" OnClientClick='<%# String.Format("ShowEditPreExpensePopup(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\");return false;",
                                    Eval("VIN"),Eval("Year"),Eval("ExpenseType"),Eval("AddedBy"),Eval("PreExpenseID"),Eval("DefaultPrice"),Eval("TotalPrice"),Eval("Count")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Device Info" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="30px"
                                    ItemStyle-Width="30px" ItemStyle-CssClass="GridContent" SortExpression="E.DeviceName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeviceName" runat="server" Text='<%# FormatDeviceName(Eval("DeviceName")) %>' />
                                        <br />
                                        <asp:Label ID="lbldeviceid" runat="server" Text='<%# FormatDeviceID(Eval("DeviceID")) %>'
                                            ToolTip='<%# Eval("DeviceID") %>' CssClass="Tooltip"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address<br/>Lat#/Long#" HeaderStyle-CssClass="GridHeader" HeaderStyle-HorizontalAlign="Left" 
                                   ItemStyle-CssClass="GridContent"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" HeaderStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# FormatLatLongAddress(Eval("LatitudeLongitudeAddress"),Eval("Latitude"),Eval("Longitude")) %>' ToolTip='<%# FormatLatLongToolTip(Eval("Latitude"),Eval("Longitude")) %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SyncDate" HeaderText="Sync Date" ItemStyle-CssClass="GridContent"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px" ItemStyle-Wrap="true"
                                    SortExpression="E.SyncDate" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="GridHeader GridHeaderAction"
                                    ItemStyle-CssClass="GridContentNumbers" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEmailStatus" runat="server" ImageUrl="~/Images/Email_Sent.png"
                                            ToolTip="Buyer Email Status" OnClientClick='<%# String.Format("ShowEmailDetailsPopup(\"{0}\");return false;",
                                         Eval("PreExpenseID")) %>' />
                                        <%--OnClick="imgEmailStatus_Click"--%>
                                        <asp:HiddenField ID="hfIsEmailSent" runat="server" Value='<%# Eval("IsEmailSent") %>' />
                                        <asp:ImageButton ID="ibtnInfo" runat="server" ImageUrl="~/Images/info.png" ToolTip="No Inventory Found for this Expense."
                                            Visible="false" />
                                        <asp:ImageButton ID="ibtnInv" runat="server" ImageUrl="~/Images/valid.png" OnClick="ibtnInv_Click" />
                                        <asp:ImageButton ID="ibtnExpenseDetail" runat="server" ImageUrl="~/Images/Select1.png"
                                            ToolTip="View Details" Visible="false" OnClientClick='<%# String.Format("ShowPreExpenseDetailsPopup(\"{0}\");return false;",
                                         Eval("PreExpenseID")) %>' />
                                        <%--OnClick="ibtnExpenseDetail_Click"--%>
                                        <asp:ImageButton ID="ibtnDuplicate" CssClass="Tooltip" runat="server" ImageUrl="~/Images/duplicate-icon.png"
                                            OnClick="ibtnDuplicate_Click" ToolTip="View Duplicates" />
                                        <asp:ImageButton ID="ibtncars" CssClass="Tooltip" runat="server" ImageUrl="~/Images/car_icon.gif"
                                            OnClick="ibtncars_Click" />
                                        <asp:ImageButton ID="ibtnDelete" ToolTip="Discard/Reject Pre-Expense" runat="server"
                                            CssClass="Tooltip" ImageUrl="~/Images/DeleteButton1.png" OnClientClick='<%# String.Format("ShowDeleteDetailPopup(\"{0}\");return false;",
                                         Eval("PreExpenseID")) %>' />
                                        <%--OnClick="ibtnDelete_Click"--%>
                                        <asp:ImageButton ID="ibtnPending" Visible="false" ToolTip="Make Pending" CssClass="Tooltip"
                                            runat="server" ImageUrl="~/Images/Pending.png" OnClick="ibtnPending_Click" />
                                        <asp:ImageButton ID="ibtnWO" runat="server" Visible="false" ToolTip="View WO"
                                            ImageUrl="~/Images/WorkOrder.png" OnClick="ibtnWO_Click" />
                                        <asp:HiddenField ID="hfExpID" Value='<%# Eval("ExpenseId") %>' runat="server" />
                                        <asp:HiddenField ID="hfvin" Value='<%# Eval("VIN") %>' runat="server" />
                                        <asp:HiddenField ID="hfInvCount" Value='<%# Eval("InvCount") %>' runat="server" />
                                        <asp:HiddenField ID="hfInvID" Value='<%# Eval("InventoryId") %>' runat="server" />
                                        <asp:HiddenField ID="hfExpenseTypeID" Value='<%# Eval("ExpenseTypeID") %>' runat="server" />
                                        <asp:HiddenField ID="hfTotalPrice" Value='<%# Eval("TotalPrice") %>' runat="server" />
                                        <asp:HiddenField ID="hfEntityId" Value='<%# Eval("EntityId") %>' runat="server" />
                                        <asp:HiddenField ID="hfEntityTypeId" Value='<%# Eval("EntityTypeId") %>' runat="server" />
                                        <asp:HiddenField ID="hfDuplicateExp" runat="server" Value='<%# Eval("DuplicateExpenses") %>' />
                                        <asp:HiddenField ID="hfpending" runat="server" Value='<%# Eval("IsPending") %>' />
                                        <asp:HiddenField ID="hfRejected" runat="server" Value='<%# Eval("IsRejected") %>' />
                                        <asp:HiddenField ID="hfWOId" runat="server" Value='<%# Eval("WorkOrderID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ImageCount" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblImageCount" runat="server" Text='<%# Bind("ImageCount") %>'></asp:Label>
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
                    <%--Image popup box--%>
                    <asp:HiddenField ID="hfPopup" runat="server" />
                    <Ajax:ModalPopupExtender ID="mpeImages" runat="server" TargetControlID="hfPopup"
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
                    <%--Image popup box--%>
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
                                <%= gvExpenseList.PageCount%>
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
                <%--Discard/Reject ConfirmBox Start--%>
                <div id="overlaysDelete" class="web_dialog_overlay">
                </div>
                <div id="pnlDelete" class="web_dialog" style="display: none;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Discard / Reject Pre-Expense
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="imgClose" onclick="HideDeletePopup();"
                                    alt="close" /><%--ClearReasonTextBox();$find('MBILinkedCars').hide();return false;--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="LeftPanelContentHeading">
                                What do you want to do?
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rdlist" runat="server" RepeatDirection="Vertical">
                                    <asp:ListItem Text="Discard / Delete this Pre-Expense<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<i>Once deleted, this record will not be appear in the list and you cannot perform any action</i>)"
                                        Value="D" Selected="True" class="LeftPanelContentHeading"></asp:ListItem>
                                    <asp:ListItem Text="Reject this Pre-Expense<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<i>Once rejected, it will appear in the list, but you cannot perform any action</i>)"
                                        Value="R" class="LeftPanelContentHeading"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trReason" runat="server" visible="true">
                            <td style="padding-top: 10px;">
                                <span class="LeftPanelContentHeading" style="vertical-align: top;">Reason</span>
                                <asp:TextBox ID="txtreason" TextMode="MultiLine" runat="server" Width="300" Height="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="DeleteOk" runat="server" CausesValidation="false" Text="OK" OnClick="DeleteOk_Click"
                                    class="btn" />
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" class="btn" OnClientClick="HideDeletePopup();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- <Ajax:ModalPopupExtender ID="mpDelete" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="MBILinkedCars" TargetControlID="hfdel" PopupControlID="pnlDelete"
                    CancelControlID="CancelButton">
                </Ajax:ModalPopupExtender>--%>
                <%--Discard/Reject ConfirmBox End--%>
                <%--Approval Note Start--%>
                <asp:Panel ID="pnlapprove" CssClass="modalPopup" Style="display: none;" runat="server"
                    HorizontalAlign="Left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Approval Note
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="img1" onclick="$find('MBILinkedExpense').hide();return false;"
                                    alt="close" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px;">
                                <span class="LeftPanelContentHeading" style="vertical-align: top;">Approval Note</span>
                                <asp:TextBox ID="txtapprovalnate" TextMode="MultiLine" runat="server" Width="300"
                                    Height="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnApproveOk" runat="server" Text="OK" OnClick="btnApproveOk_Click"
                                    class="btn" />
                                <asp:Button ID="btnApproveCancel" runat="server" Text="Cancel" class="btn" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <Ajax:ModalPopupExtender ID="mpApprove" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="MBILinkedExpense" TargetControlID="hfappr" PopupControlID="pnlapprove"
                    CancelControlID="btnApproveCancel">
                </Ajax:ModalPopupExtender>
                <%--Approval Note Start--%>
                <%--Inventory Selection Start--%>
                <asp:Panel ID="pnlinv" CssClass="modalPopup" Style="display: none; width: 600px;
                    min-height: 150px; clear: both" runat="server" HorizontalAlign="Left">
                    <div>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="PopUpBoxHeading">
                                    &nbsp;&nbsp;Inventory Linking
                                    <asp:Label ID="lblVINInvLinking" runat="server"></asp:Label>
                                </td>
                                <td class="PopUpBoxHeading" align="right">
                                    <img border="0" src="../Images/close.gif" id="img2" onclick="$find('MBILinkedInventory').hide();return false;"
                                        alt="close" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <%--<asp:PlaceHolder ID="phcExpInv" runat="server"></asp:PlaceHolder>--%>
                        <asp:GridView ID="grvinventory" runat="server" AutoGenerateColumns="False" Width="100%"
                            DataKeyNames="InventoryId" RowStyle-CssClass="gvRow" AlternatingRowStyle-CssClass="gvAlternateRow"
                            CellPadding="4" GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="grvinventory_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-Width="5px" ItemStyle-Width="5px">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rdselect" runat="server" onclick="toggleSelection(this);" AutoPostBack="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderText="Inventory ID" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <a href='<%# "InventoryDetail.aspx?Mode=View&Code="+Eval("InventoryId")%>' title="View Details"
                                            id="ancdetail" runat="server" target="_blank" style="text-decoration: underline;
                                            cursor: pointer; color: Red;">
                                            <%# Eval("InventoryId")%>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Make" HeaderText="Make" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Body" HeaderText="Body" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Expense found"></asp:Label>
                            </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="#E4EDF4" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                    <div style="padding-top: 10px;">
                        <span class="LeftPanelContentHeading" style="vertical-align: top;">Approval Note</span>
                        <asp:TextBox ID="txtapprnote2" TextMode="MultiLine" runat="server" Columns="60" Rows="2"></asp:TextBox>
                    </div>
                    <div style="text-align: center; padding: 10px 0px">
                        <asp:Button ID="btnSubmitInv" runat="server" Text="Approve" CssClass="btn" Width="80px"
                            OnClick="btnSubmitInv_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn" Width="80px" />
                    </div>
                </asp:Panel>
                <Ajax:ModalPopupExtender ID="mpexpinv" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="MBILinkedInventory" TargetControlID="hfinv" PopupControlID="pnlinv"
                    CancelControlID="btncancel">
                </Ajax:ModalPopupExtender>
                <%--Inventory Selection End--%>
                <asp:HiddenField ID="hfDeletePreExpID" runat="server" />
                <asp:HiddenField ID="hfdel" runat="server" />
                <asp:HiddenField ID="hfappr" runat="server" />
                <asp:HiddenField ID="hfinv" runat="server" />
                <asp:HiddenField ID="hfVINForSave" runat="server" />
                <asp:HiddenField ID="hfExpIDForSave" runat="server" />
                <%----------------------Duplicate VIN popup------------------------%>
                <div id="dvDuplicateExp" class="modalPopup" style="display: none; width: 650px; min-height: 100px"
                    runat="server">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Duplicate Expense
                            </td>
                            <td class="PopUpBoxHeading">
                                <asp:RadioButtonList ID="rbtnlstDuplicateVIN" runat="server" RepeatDirection="Horizontal"
                                    CellSpacing="3" CssClass="PopupHeaderRadio" AutoPostBack="true" OnSelectedIndexChanged="rbtnlstDuplicateVIN_SelectedIndexChanged">
                                    <asp:ListItem Text="Pre-Expense" Value="P" Selected="True" class="PopupHeaderRadio" />
                                    <asp:ListItem Text="Expense" Value="E" class="PopupHeaderRadio" />
                                </asp:RadioButtonList>
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="imgCloseDuplicate" alt="close" />
                            </td>
                        </tr>
                    </table>
                    <div id="divDuplicateExpenses" runat="server">
                        <asp:GridView ID="gvDuplicateExpenses" runat="server" Width="100%" BorderWidth="0"
                            AllowPaging="True" PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Rows found"
                            OnPageIndexChanging="gvDuplicateExpenses_PageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderText="Expense Type" DataField="ExpenseType" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Amount($)" DataField="ExpenseAmount" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContentNumbers" />
                                <asp:BoundField HeaderText="Expense Date" DataField="ExpenseDate" DataFormatString="{0:MM/dd/yyyy hh:mm tt}"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Vendor" DataField="Vendor" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Added On" DataField="DateAdded" DataFormatString="{0:MM/dd/yyyy hh:mm tt}"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Added By" DataField="DisplayName" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                            </Columns>
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="PreTableBorderB" />
                            <RowStyle CssClass="gvRow" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <PagerStyle CssClass="PreFooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                    <div id="divDuplicatePreExpenses" runat="server">
                        <asp:GridView ID="gvDuplicatePreExpenses" runat="server" Width="100%" BorderWidth="0"
                            AllowPaging="True" PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Rows found"
                            OnPageIndexChanging="gvDuplicatePreExpenses_PageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderText="Expense Type" DataField="ExpenseType" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Amount($)" DataField="TotalPrice" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContentNumbers" />
                                <asp:BoundField HeaderText="Expense Date" DataField="ExpenseDate" DataFormatString="{0:MM/dd/yyyy hh:mm tt}"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Vendor" DataField="Vendor" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Added On" DataField="SyncDate" DataFormatString="{0:MM/dd/yyyy hh:mm tt}"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Added By" DataField="DisplayName" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                            </Columns>
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="PreTableBorderB" />
                            <RowStyle CssClass="gvRow" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <PagerStyle CssClass="PreFooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                </div>
                <asp:HiddenField ID="hfDuplicateExp" runat="server" />
                <Ajax:ModalPopupExtender ID="mpeDuplicateExp" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="hfDuplicateExp" PopupControlID="dvDuplicateExp" CancelControlID="imgCloseDuplicate">
                </Ajax:ModalPopupExtender>
                <%----------------------Duplicate VIN popup------------------------%>
                <%--Email popup box--%>
                <%--<asp:HiddenField ID="hfemail" runat="server" />
                <Ajax:ModalPopupExtender ID="mpeEmail" runat="server" TargetControlID="hfemail" PopupControlID="pnlemail"
                    BackgroundCssClass="modalBackground" CancelControlID="imgCloseEmailDetail" PopupDragHandleControlID="pnlemail" />--%>
                <div id="EmailOverlays" class="web_dialog_overlay">
                </div>
                <div id="pnlemail" class="web_dialogEmail" style="display: none; height: 600px; width: 800px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="position: relative;">
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Email Details
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="img3" alt="close" onclick="HideEditDialogEmail();" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%--<iframe id="ifrmemail" runat="server" style="width: 800px; height: 600px; overflow: auto;
                                    border: solid 1px #115481; background-color: #fff" frameborder="0"></iframe>--%>
                                <div style="background-color: #fff; width: 100%; height: 575px; margin-top: 2px;
                                    overflow: auto;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="lblb" style="width: 15%; vertical-align: top">
                                                To
                                            </td>
                                            <td class="lbl">
                                                <asp:Label ID="lblMailTo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblb" style="vertical-align: top">
                                                Subject
                                            </td>
                                            <td class="lbl">
                                                <asp:Label ID="lblMailSubject" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblb" style="vertical-align: top">
                                                From
                                            </td>
                                            <td class="lbl">
                                                <asp:Label ID="lblMailFrom" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblb" style="vertical-align: top">
                                                CC
                                            </td>
                                            <td class="lbl">
                                                <asp:Label ID="lblMailCCed" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblb" style="vertical-align: top">
                                                BCC
                                            </td>
                                            <td class="lbl">
                                                <asp:Label ID="lblMailBCCed" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblb" style="vertical-align: top">
                                                Attempt
                                            </td>
                                            <td class="lbl">
                                                <asp:Label ID="lblAttempt" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblb" style="vertical-align: top">
                                                Logged-On
                                            </td>
                                            <td class="lbl">
                                                <asp:Label ID="lblLoggedOn" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblb" style="vertical-align: top">
                                                Sent-On
                                            </td>
                                            <td class="lbl">
                                                <asp:Label ID="lblSentOn" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblb" style="vertical-align: top" colspan="2">
                                                Message
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="lbl" style="width: 100%">
                                                <asp:Label ID="lblMailBody" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--Email popup box--%>                

                <%--SMSDetailPopUp--%>
                <div id="SMSOverlays" class="web_dialog_overlay"></div>
                <div id="pnlsms" class="web_dialogEmail" style="display: none; height: 600px; width: 800px; overflow:auto">
                    <table id="tblsms" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="position: relative;">
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;SMS Details
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="img4" alt="close" onclick="HideEditDialogSMS();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%--SMSDetailPopUp--%>

                <%----------Pre-Expense Detail Popup----------%>
                <div id="PreExpenseOverlays" class="web_dialog_overlay">
                </div>
                <div id="dvPreExpDetail" class="web_dialogPreExpense" style="display: none; width: 650px;
                    min-height: 150px">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Pre-Expense Details
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="imgClosePreExpDetail" alt="close" onclick="HidePreExpPopup();" />
                            </td>
                        </tr>
                    </table>
                    <%-- <asp:GridView ID="gvPreExpDetail" runat="server" Width="100%" AutoGenerateColumns="false"
                        GridLines="None" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>--%>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="GridContent_padding5">
                                <b>VIN</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblVIN" runat="server"></asp:Label>
                                <%-- <%#Eval("VIN") %>--%>
                            </td>
                            <td class="GridContent_padding5">
                                <b>Expense Date</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblExpenseDate" runat="server"></asp:Label>
                                <%--   <%#Eval("ExpenseDate")%>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent_padding5">
                                <b>Count</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblPreExpenseCount" runat="server"></asp:Label>
                                <%--  <%#Eval("Count")%>--%>
                            </td>
                            <td class="GridContent_padding5">
                                <b>Sync Date</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblSyncDate" runat="server"></asp:Label>
                                <%--  <%#Eval("SyncDate")%>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent_padding5">
                                <b>Default Price($)</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblDefaultPrice" runat="server"></asp:Label>
                                <%--    <%#Eval("DefaultPrice")%>--%>
                            </td>
                            <td class="GridContent_padding5">
                                <b>Approved By</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblAddedBy" runat="server"></asp:Label>
                                <%--  <%#Eval("AddedBy")%>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent_padding5">
                                <b>Total Price($)</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                                <%--  <%#Eval("TotalPrice")%>--%>
                            </td>
                            <td class="GridContent_padding5">
                                <b>Added By</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblEntityName" runat="server"></asp:Label>
                                <%--  <%#Eval("EntityName")%>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent_padding5">
                                <b>Description</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                <%--  <%#Eval("Description")%>--%>
                            </td>
                            <td class="GridContent_padding5">
                                <b>Device</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblDeviceName" runat="server"></asp:Label>
                                <%-- <%#Eval("DeviceName")%>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent_padding5">
                                <b>Approval Note</b>
                            </td>
                            <td class="GridContent_padding5" colspan="3">
                                <asp:Label ID="lblApprovalNote" runat="server"></asp:Label>
                                <%-- <%#Eval("ApprovalNote")%>--%>
                            </td>
                        </tr>
                    </table>
                    <%-- </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>
                </div>
                <%-- <asp:HiddenField ID="hfPreExp" runat="server" />
                <Ajax:ModalPopupExtender ID="mpePreExpDetail" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="hfPreExp" PopupControlID="dvPreExpDetail" CancelControlID="imgClosePreExpDetail">
                </Ajax:ModalPopupExtender>--%>
                <%----------Pre-Expense Detail Popup----------%>
                <%--Edit PreExpense Popup--%>
                <div id="overlay" class="web_dialog_overlay">
                </div>
                <div id="divEditPreExp" class="web_dialog" style="display: none; min-width: 400px;
                    min-height: 150px">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="Nornmal-Arial-12">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Edit
                                <asp:Label ID="lbleditheader" runat="server"></asp:Label>
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="imgCloseCR" alt="close" onclick="HideEditDialog();" />
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="GridContent_padding5" nowrap="nowrap">
                                <b>Count</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:DropDownList ID="ddlcount" runat="server" AutoPostBack="false" onchange="ddlcount_SelectedIndexChanged($(this).attr('id'));">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hfddlCount" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent_padding5" nowrap="nowrap">
                                <b>Default Price($)</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:TextBox ID="txtdefaultprice" runat="server" MaxLength="18"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="ftbdprice" runat="server" TargetControlID="txtdefaultprice"
                                    FilterType="Custom,Numbers" ValidChars="+-." />
                                <%--   <asp:RequiredFieldValidator ID="rfvdprice" runat="server" ControlToValidate="txtdefaultprice"
                                            ErrorMessage="Enter a valid price." Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revdprice" runat="server" ControlToValidate="txtdefaultprice"
                                            ErrorMessage="Enter a valid price with max. upto two decimal point." 
                                            ValidationExpression="^[-+]?[0-9]\d{0,9}(\.\d{1,2})?%?$" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent_padding5" nowrap="nowrap">
                                <b>Total Price($)</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:TextBox ID="txttotalprice" runat="server"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="ftbtprice" runat="server" TargetControlID="txttotalprice"
                                    FilterType="Custom,Numbers" ValidChars="+-." />
                                <%--<asp:RequiredFieldValidator ID="rfvtprice" runat="server" ControlToValidate="txttotalprice"
                                            ErrorMessage="Enter a valid price." Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                <%--  <asp:RegularExpressionValidator ID="revtprice" runat="server" ControlToValidate="txttotalprice"
                                            ErrorMessage="Enter a valid price with max. upto two decimal point." 
                                            ValidationExpression="^[-+]?[0-9]\d{0,9}(\.\d{1,2})?%?$" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:HiddenField ID="hfEditPreExpID" runat="server" />
                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="Btn_Form" OnClick="btnsave_Click"
                                    CausesValidation="false" OnClientClick="javascript:return confirm('Are you sure you want to Edit this Expense? \n\nClick Ok to Confirm\nClick Cancel to ignore');" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%--Edit PreExpense Popup--%>
                <%--WO Details--%>
                <div id="dvWODetails" runat="server" class="modalPopup" 
                    style="display: none; width: 800px; min-height: 300px">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Work Order History
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="imgCloseWODetails" alt="close" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvExpenseDetail" runat="server" Width="100%" RowStyle-CssClass="gvRow"
                        AutoGenerateColumns="false" GridLines="None" DataKeyNames="WOExpenseID" OnRowDataBound="gvExpenseDetail_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="User" DataField="GroupName" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" />
                            <asp:BoundField HeaderText="ExpenseType" DataField="ExpenseType" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" />
                            <asp:BoundField HeaderText="Count" DataField="Count" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContentNumbers" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Default Price($)" DataField="DefaultPrice" DataFormatString="{0:#,###}"
                                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Total Price($)" DataField="TotalPrice" DataFormatString="{0:#,###}"
                                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" />
                            <asp:TemplateField HeaderText="Device Info" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="30px"
                                ItemStyle-Width="30px" ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%# FormatDeviceName(Eval("DeviceName")) %>' />
                                    <br />
                                    <asp:Label ID="lbldeviceid" runat="server" Text='<%# FormatDeviceID(Eval("DeviceID")) %>'
                                        ToolTip='<%# Eval("DeviceID") %>' CssClass="Tooltip"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Latitude" DataField="Latitude" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Longitude" DataField="Longitude" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Sync Date" DataField="SyncDate" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" ItemStyle-Width="110px" DataFormatString="{0:MM/dd/yyyy hh:mm tt}" />
                            <asp:BoundField HeaderText="Added On" DataField="DateAdded" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" ItemStyle-Width="110px" DataFormatString="{0:MM/dd/yyyy hh:mm tt}" />
                            <asp:BoundField HeaderText="Modified On" DataField="DateModified" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" ItemStyle-Width="110px" DataFormatString="{0:MM/dd/yyyy hh:mm tt}" />                            
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfRowType" runat="server" Value='<%# Eval("RowType") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:HiddenField ID="hfWOPopUp" runat="server" />
                <ajax:ModalPopupExtender ID="mpeWODetails" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="hfWOPopUp" PopupControlID="dvWODetails" CancelControlID="imgCloseWODetails">
                </ajax:ModalPopupExtender>
                <%--WO Details--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">

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
                url: "PreExpense.aspx/PreExpenseDetails",
                data: "{PreExpenseID:" + PreExpenseID + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {

                    if (data.d.length > 0) {

                        $('#<%=lblVIN.ClientID %>').text(data.d[0].VIN);
                        $('#<%=lblExpenseDate.ClientID %>').text(data.d[0].ExpenseDate);
                        $('#<%=lblPreExpenseCount.ClientID %>').text(data.d[0].Count);
                        $('#<%=lblSyncDate.ClientID %>').text(data.d[0].SyncDate);
                        $('#<%=lblDefaultPrice.ClientID %>').text(data.d[0].DefaultPrice);
                        $('#<%=lblAddedBy.ClientID %>').text(data.d[0].EntityName);
                        $('#<%=lblTotalPrice.ClientID %>').text(data.d[0].TotalPrice);
                        $('#<%=lblEntityName.ClientID %>').text(data.d[0].AddedBy);
                        $('#<%=lblDescription.ClientID %>').html(data.d[0].Description);
                        $('#<%=lblDeviceName.ClientID %>').html(data.d[0].DeviceName);
                        $('#<%=lblApprovalNote.ClientID %>').html(data.d[0].ApprovalNote);
                    }
                    else {
                        $('#<%=lblVIN.ClientID %>').text('');
                        $('#<%=lblExpenseDate.ClientID %>').text('');
                        $('#<%=lblPreExpenseCount.ClientID %>').text('');
                        $('#<%=lblSyncDate.ClientID %>').text('');
                        $('#<%=lblDefaultPrice.ClientID %>').text('');
                        $('#<%=lblAddedBy.ClientID %>').text('');
                        $('#<%=lblTotalPrice.ClientID %>').text('');
                        $('#<%=lblEntityName.ClientID %>').text('');
                        $('#<%=lblDescription.ClientID %>').text('');
                        $('#<%=lblDeviceName.ClientID %>').text('');
                        $('#<%=lblApprovalNote.ClientID %>').text('');
                    }
                }
            });
        }


        function ShowDeleteDetailPopup(PreExpenseID) {
            $('#<%=hfDeletePreExpID.ClientID %>').val(PreExpenseID);
            ShowDeletePopup(true);
            return false;
        }

        function ShowDeletePopup(modal) {

            $("#overlaysDelete").show();
            $("#pnlDelete").fadeIn(10);

            if (modal) {
                $("#overlaysDelete").unbind("click");
            }
            else {
                $("#overlaysDelete").click(function (e) {
                    HideDeletePopup();
                });
            }
        }

        function HideDeletePopup() {
            $("#overlaysDelete").hide();
            $("#pnlDelete").fadeOut(10);
            return false;
        }


        function DriverEmailStatusPopup(PreExpenseID) {

            BindDriverEmailStatus(PreExpenseID);
            ShowEditDialogEmail(true);
        }
        function ShowEmailDetailsPopup(PreExpenseID) {

            BindEmailDetails(PreExpenseID);
            ShowEditDialogEmail(true);
        }

        function ShowEditDialogEmail(modal) {
            $("#EmailOverlays").show();
            $("#pnlemail").fadeIn(10);

            if (modal) {
                $("#EmailOverlays").unbind("click");
            }
            else {
                $("#EmailOverlays").click(function (e) {
                    HideCRDialog();
                });
            }
        }

        function HideEditDialogEmail() {
            $("#EmailOverlays").hide();
            $("#pnlemail").fadeOut(10);
        }

        function BindEmailDetails(PreExpenseID) {

            $.ajax({
                type: "POST",
                url: "PreExpense.aspx/EmailDetails",
                data: "{PreExpenseID:" + PreExpenseID + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {

                    if (data.d.length > 0) {

                        $('#<%=lblMailTo.ClientID %>').text(data.d[0].Mailto);
                        $('#<%=lblMailBCCed.ClientID %>').text(data.d[0].MailBCED);
                        $('#<%=lblMailSubject.ClientID %>').text(data.d[0].MailSubject);
                        $('#<%=lblMailFrom.ClientID %>').text(data.d[0].MailFrom);
                        $('#<%=lblMailCCed.ClientID %>').text(data.d[0].MailCCED);
                        $('#<%=lblAttempt.ClientID %>').text(data.d[0].Attempt);
                        $('#<%=lblLoggedOn.ClientID %>').text(data.d[0].LoggedOn);
                        $('#<%=lblSentOn.ClientID %>').text(data.d[0].SentOn);
                        $('#<%=lblMailBody.ClientID %>').html(data.d[0].MailBody);
                    }
                    else {
                        $('#<%=lblMailTo.ClientID %>').text('');
                        $('#<%=lblMailBCCed.ClientID %>').text('');
                        $('#<%=lblMailSubject.ClientID %>').text('');
                        $('#<%=lblMailFrom.ClientID %>').text('');
                        $('#<%=lblMailCCed.ClientID %>').text('');
                        $('#<%=lblAttempt.ClientID %>').text('');
                        $('#<%=lblLoggedOn.ClientID %>').text('');
                        $('#<%=lblSentOn.ClientID %>').text('');
                        $('#<%=lblMailBody.ClientID %>').text('');
                    }
                }
            });
        }

        function BindDriverEmailStatus(PreExpenseID) {

            $.ajax({
                type: "POST",
                url: "PreExpense.aspx/DriverEmailStatus",
                data: "{PreExpenseID:" + PreExpenseID + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {

                    if (data.d.length > 0) {

                        $('#<%=lblMailTo.ClientID %>').text(data.d[0].Mailto);
                        $('#<%=lblMailBCCed.ClientID %>').text(data.d[0].MailBCED);
                        $('#<%=lblMailSubject.ClientID %>').text(data.d[0].MailSubject);
                        $('#<%=lblMailFrom.ClientID %>').text(data.d[0].MailFrom);
                        $('#<%=lblMailCCed.ClientID %>').text(data.d[0].MailCCED);
                        $('#<%=lblAttempt.ClientID %>').text(data.d[0].Attempt);
                        $('#<%=lblLoggedOn.ClientID %>').text(data.d[0].LoggedOn);
                        $('#<%=lblSentOn.ClientID %>').text(data.d[0].SentOn);
                        $('#<%=lblMailBody.ClientID %>').html(data.d[0].MailBody);
                    }
                    else {
                        $('#<%=lblMailTo.ClientID %>').text('');
                        $('#<%=lblMailBCCed.ClientID %>').text('');
                        $('#<%=lblMailSubject.ClientID %>').text('');
                        $('#<%=lblMailFrom.ClientID %>').text('');
                        $('#<%=lblMailCCed.ClientID %>').text('');
                        $('#<%=lblAttempt.ClientID %>').text('');
                        $('#<%=lblLoggedOn.ClientID %>').text('');
                        $('#<%=lblSentOn.ClientID %>').text('');
                        $('#<%=lblMailBody.ClientID %>').text('');
                    }
                }
            });
        }

        function DriverSMSDetailPopup(PreExpenseID) {
            BindDriverSMSDetail(PreExpenseID);
            ShowEditDialogSMS(true);
        }

        function BindDriverSMSDetail(PreExpenseID) {

            $.ajax({
                type: "POST",
                url: "PreExpense.aspx/DriverSMSDetail",
                data: "{PreExpenseID:" + PreExpenseID + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {
                    $('#<%=tblsms.ClientID %> tr:gt(0)').remove()
                    if (data.d.length > 0) {
                        for (var i = 0; i < data.d.length; i++) {

                            $('#<%=tblsms.ClientID %>').append("<tr><td class='lblb' style='width:15%'>To</td><td class='lbl'>" + data.d[i].SMSTo + "</td></tr>");
                            $('#<%=tblsms.ClientID %>').append("<tr><td class='lblb'>From</td><td class='lbl'>" + data.d[i].SMSFrom + "</td></tr>");
                            $('#<%=tblsms.ClientID %>').append("<tr><td class='lblb'>Attempt</td><td class='lbl'>" + data.d[i].SMSAttempt + "</td></tr>");
                            $('#<%=tblsms.ClientID %>').append("<tr><td class='lblb'>Logged On</td><td class='lbl'>" + data.d[i].SMSLoggedOn + "</td></tr>");
                            $('#<%=tblsms.ClientID %>').append("<tr><td class='lblb'>Sent On</td><td class='lbl'>" + data.d[i].SMSSentOn + "</td></tr>");
                            $('#<%=tblsms.ClientID %>').append("<tr><td class='lblb'>Message</td><td class='lbl'>" + data.d[i].SMSBody + "</td></tr>");
                            $('#<%=tblsms.ClientID %>').append("<tr><td colspan='2'>&nbsp;</td></tr>");
                        }
                    }
                }
            });
        }

        function ShowEditDialogSMS(modal) {
            $("#SMSOverlays").show();
            $("#pnlsms").fadeIn(10);

            if (modal) {
                $("#SMSOverlays").unbind("click");
            }
            else {
                $("#SMSOverlays").click(function (e) {
                    HideCRDialog();
                });
            }
        }

        function HideEditDialogSMS() {
            $("#SMSOverlays").hide();
            $("#pnlsms").fadeOut(10);
        }
    </script>
    <script type="text/javascript">


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

        function ClearReasonTextBox()
        { $('#<%=txtreason.ClientID %>').val(''); }

        function SelectAllCheckboxes1(chk) {
            var counter = 0;
            $('#<%=gvExpenseList.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                    counter = counter + 1;
                }
            });

            document.getElementById("ctl00_ContentPlaceHolder1_hfSelectedCount").value = chk.checked == true ? counter : "0";

        }

        function toggleSelection(Source) {
            var crvalues = '';
            var isChecked = Source.checked;
            $("#ctl00_ContentPlaceHolder1_grvinventory input[id*='rdselect']").each(function (index) {
                $(this).attr('checked', false);
            });
            Source.checked = isChecked;
        }

        function ValidateChecked() {
            var counter = document.getElementById("ctl00_ContentPlaceHolder1_hfSelectedCount").value;
            if (counter == "0") {
                alert("Please select a record to approve");
                return false;
            }
            else
                return confirm('Do you want to approve ' + counter + ' expense(s)?');
        }

        function ChangeCount(chk) {
            var counter = document.getElementById("ctl00_ContentPlaceHolder1_hfSelectedCount");
            var count = counter.value;

            if (chk.checked == true)
                counter.value = parseInt(count) + 1;
            else
                counter.value = parseInt(count) - 1;

        }

        function ShowEditPreExpensePopup(VIN, Year, ExpenseType, AddedBy, PreExpenseID, DefaultPrice, TotalPrice, Count) {
            //console.log(VIN+'|'+Year+'|'+ExpenseType+'|'+AddedBy+'|'+PreExpenseID);
            $('#<%=lbleditheader.ClientID %>').text(' [' + VIN + '-' + Year + '-' + ExpenseType + '-' + AddedBy + ']');
            $('#<%=txtdefaultprice.ClientID %>').val(DefaultPrice);
            $('#<%=txttotalprice.ClientID %>').val(TotalPrice);
            $('#<%=hfEditPreExpID.ClientID %>').val(PreExpenseID);
            BindExpenseCountDropDownlist(PreExpenseID, Count);
            ShowEditDialog(true);
        }

        function ShowEditDialog(modal) {
            $("#overlay").show();
            $("#divEditPreExp").fadeIn(10);

            if (modal) {
                $("#overlay").unbind("click");
            }
            else {
                $("#overlay").click(function (e) {
                    HideCRDialog();
                });
            }
        }

        function HideEditDialog() {
            $("#overlay").hide();
            $("#divEditPreExp").fadeOut(10);
        }

        function BindExpenseCountDropDownlist(PreExpenseID, Count) {
            $.ajax({
                type: "POST",
                url: "PreExpense.aspx/GetPreExpenseMinMaxCount",
                data: "{PreExpenseID:" + PreExpenseID + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {
                    var myarr = [];

                    if (data.d.length > 0) {
                        for (var i = data.d[0].MinCount; i <= data.d[0].MaxCount; i++) {
                            myarr.push({ 'val': i, 'text': i });
                        }

                    }
                    else {
                        for (var i = 1; i <= Count; i++) {
                            myarr.push({ 'val': i, 'text': i });
                        }
                    }

                    $('#<%=ddlcount.ClientID %>' + '>option').remove();
                    for (var j = 0; j < myarr.length; j++) {
                        $('#<%=ddlcount.ClientID %>').append($('<option></option>').val(myarr[j].val).html(myarr[j].text));
                    }
                    $('#<%=ddlcount.ClientID %>').val(Count);
                    $('#<%=hfddlCount.ClientID %>').val(Count);
                }
            });
        }

        function ddlcount_SelectedIndexChanged(CtrlID) {
            $('#<%=hfddlCount.ClientID %>').val($('#' + CtrlID).val());
        }

        $('#<%=txtdefaultprice.ClientID %>').live("blur", function () {
            if ($('#<%=txtdefaultprice.ClientID %>').val() == '') {
                alert("Enter Default Price!!!");
                $('#<%=txtdefaultprice.ClientID %>').focus();
                $('#<%=btnsave.ClientID %>').attr('disabled', 'disabled');
            }
            else
                $('#<%=btnsave.ClientID %>').removeAttr('disabled');
        });

        $('#<%=txttotalprice.ClientID %>').live("blur", function () {
            if ($('#<%=txttotalprice.ClientID %>').val() == '') {
                alert("Enter Total Price!!!");
                $('#<%=txttotalprice.ClientID %>').focus();
                $('#<%=btnsave.ClientID %>').attr('disabled', 'disabled');
            }
            else
                $('#<%=btnsave.ClientID %>').removeAttr('disabled');
        });

    </script>
</asp:content>

<%@ Page Language="C#"  AutoEventWireup="true"
    CodeBehind="WorkOrderExpense.aspx.cs" Inherits="METAOPTION.UI.WorkOrderExpense"
    Title="Work Order Expenses" %>

<asp:Content ID="woExpenses" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upResultGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="TableHeadingBg TableHeading" colspan="6">
                                VIEW ALL WORK ORDER EXPENSES
                            </td>
                        </tr>
                    </table>
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
                                    WO Number
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtWONumber" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" style="width: 100px">
                                    Status
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txt2">
                                        <asp:ListItem Text="All" Value="-1" Selected="True" />
                                        <asp:ListItem Text="Pending" Value="0" />
                                        <asp:ListItem Text="Modified" Value="1" />
                                        <asp:ListItem Text="Accepted" Value="2" />
                                        <asp:ListItem Text="Completed" Value="3" />
                                        <asp:ListItem Text="Approved" Value="4" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Sync Date
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSyncDateFrom" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSyncDateFrom"
                                        PopupButtonID="txtSyncDateFrom" />
                                    <asp:TextBox ID="txtSyncDateTo" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSyncDateTo"
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
                                <%= gvWOExpenses.PageCount%>
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
                        <asp:GridView ID="gvWOExpenses" runat="server" Width="100%" DataKeyNames="WorkOrderID"
                            AutoGenerateColumns="false" EmptyDataText="No record found for this search criteria."
                            GridLines="None" RowStyle-CssClass="gvRow" PagerSettings-Visible="false" AllowPaging="true"
                            AllowSorting="true" OnSorting="gvWOExpenses_Sorting" PageSize="50" CssClass="Grid"
                            EmptyDataRowStyle-CssClass="gvEmpty" OnRowDataBound="gvWOExpenses_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnUpdateStatus" runat="server" ImageUrl="~/Images/H_active.png" OnClick="ibtnUpdateStatus_Click"  />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="WO#" DataField="WorkOrderNumber" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="WO.WorkOrderNumber" />
                                <asp:TemplateField HeaderText="<div title='Line Items Count'>&nbsp;L.I.C</div>" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnExpand" runat="server" ImageUrl="~/Images/expand.png" OnClick="ibtnExpand_Click" />
                                        <%#Eval("TotalExpenses") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VIN#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvinNo" runat="server" Text='<%# Eval("VIN") %>' CssClass="Tooltip" />
                                        <asp:HyperLink ID="hlnkVIN"  runat="server" 
                                            NavigateUrl='<%# "InventoryExpense.aspx?Code="+Eval("InventoryId")%>'
                                            Text='<%#Eval("VIN") %>' Visible="false" /><br />
                                        <asp:Label ID="lblCode" runat="server" Text="Code: " Visible="false" />
                                        <asp:HyperLink ID="hlnkCode" runat="server"
                                            NavigateUrl='<%# "InventoryDetail.aspx?Mode=View&Code="+Eval("InventoryId")%>'
                                            Text='<%#Eval("InventoryId") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Year" DataField="Year" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                <asp:TemplateField HeaderText="Make<br />Model" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <%#Eval("Make") %><br />
                                        <%#Eval("Model") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Added By" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    ItemStyle-Width="120px" SortExpression="SU.DisplayName">
                                    <ItemTemplate>
                                        <%#Eval("AddedBy")%><br />
                                        (<%#Eval("EntityType")%>)
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    SortExpression="V.VendorName" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 70%;">
                                                    <asp:HyperLink ID="hlnkVendor" runat="server" Text='<%#Eval("VendorName") %>' NavigateUrl='<%# "ViewVendor.aspx?Mode=View&EntityId="+Eval("VendorID")+"&type=3" %>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Total Price($)" DataField="TotalPrice" DataFormatString="{0:#,###}"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers" SortExpression="WD.TotalPrice" />
                                <asp:BoundField HeaderText="Description" DataField="WODescription" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                
                                <asp:BoundField HeaderText="Status" DataField="StatusDesc" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="WO.Status" />
                                <asp:BoundField DataField="SyncDate" HeaderText="Sync Date" ItemStyle-CssClass="GridContent"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px" ItemStyle-Wrap="true"
                                    SortExpression="WD.SyncDate" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="GridHeader GridHeaderAction"
                                    ItemStyle-CssClass="GridContentNumbers" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnWOImages" runat="server" Visible="false" ImageUrl="~/Images/car_icon.gif" OnClick="ibtnWOImages_Click" />
                                        <asp:ImageButton ID="ibtnApprove" runat="server" ToolTip="View Pre-Expense" 
                                            ImageUrl="~/Images/Select1.png"
                                            OnClientClick='<%# String.Format("ShowPreExpenseDetailsPopup(\"{0}\");return false;", Eval("PreExpenseID")) %>' />
                                        <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/Images/DeleteButton1.png"
                                            ToolTip="Delete" OnClick="ibtnDelete_Click" />
                                        <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />
                                        <asp:HiddenField ID="hfImageCount" runat="server" Value='<%#Eval("ImageCount") %>' />
                                        <asp:HiddenField ID="hfInventoryID" runat="server" Value='<%#Eval("InventoryID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="0px" ShowHeader="false" ControlStyle-Width="100%">
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="15">
                                                <asp:Panel ID="pnlNestedGrid" runat="server" CssClass="NestedGridPanel">
                                                    <div style="margin: 4px 20px;">
                                                        <asp:GridView ID="gvExpenseDetail" runat="server" Width="100%" RowStyle-CssClass="gvRow"
                                                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="WOExpenseID">
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
                                                                <asp:TemplateField HeaderText="Notifications" HeaderStyle-CssClass="GridHeader GridHeaderAction"
                                                                    ItemStyle-CssClass="GridContentNumbers" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgVendorSMSStatus" runat="server" ImageUrl="~/Images/sms-sent.png"
                                                                            OnClientClick='<%# String.Format("VendorSMSDetailPopup(\"{0}\");return false;", Eval("WorkOrderDetailID")) %>' />
                                                                        <asp:ImageButton ID="imgVendorEmailStatus" runat="server" ImageUrl="~/Images/Email_Sent.png"
                                                                            OnClientClick='<%# String.Format("VendorEmailDetailPopup(\"{0}\");return false;", Eval("WorkOrderDetailID")) %>' />
                                                                        <asp:ImageButton ID="imgDriverSMSStatus" runat="server" ImageUrl="~/Images/Driver_SMSSent.png" Visible="false"
                                                                            OnClientClick='<%# String.Format("DriverSMSDetailPopup(\"{0}\");return false;", Eval("WorkOrderDetailID")) %>' />
                                                                        <asp:ImageButton ID="imgDriverEmailStatus" runat="server" ImageUrl="~/Images/Driver_EmailSent.png" Visible="false"
                                                                            OnClientClick='<%# String.Format("DriverEmailDetailPopup(\"{0}\");return false;", Eval("WorkOrderDetailID")) %>' />

                                                                        <asp:HiddenField ID="hfStatusCode" runat="server" Value='<%# Eval("StatusCode") %>' />
                                                                        <asp:HiddenField ID="hfIsVendorEmailSent" runat="server" Value='<%# Eval("IsVendorEmailSent") %>' />
                                                                        <asp:HiddenField ID="hfIsVendorSMSSent" runat="server" Value='<%# Eval("IsVendorSMSSent") %>' />
                                                                        <asp:HiddenField ID="hfIsDriverEmailSent" runat="server" Value='<%# Eval("IsDriverEmailSent") %>' />
                                                                        <asp:HiddenField ID="hfIsDriverSMSSent" runat="server" Value='<%# Eval("IsDriverSMSSent") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hfRowType" runat="server" Value='<%# Eval("RowType") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
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
                                <%= gvWOExpenses.PageCount%>
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
                <%--Delete/Discard panel begin--%>
                <asp:HiddenField ID="hfDeleteWOID" runat="server" />
                <asp:HiddenField ID="hfDel" runat="server" />
                <asp:Panel ID="pnlDelete" CssClass="modalPopup" Style="display: none;" runat="server"
                    HorizontalAlign="Left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Discard / Reject Work Order
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="imgClose" onclick="return false;"
                                    alt="close" />
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
                                <asp:Button ID="btnDelOK" runat="server" Text="OK" OnClick="btnDelOK_Click" CssClass="btn"  />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajax:ModalPopupExtender ID="mpeDeleteWO" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="hfDel" PopupControlID="pnlDelete" CancelControlID="imgClose">
                </ajax:ModalPopupExtender>
                <%--Delete/Discard panel end--%>

                <%--EmailDetailPopUP--%>
                <div id="EmailOverlays" class="web_dialog_overlay"></div>
                <div id="pnlemail" class="web_dialogEmail" style="display: none; height: 500px; width: 800px; overflow:auto">
                    <table id="tblEmail" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="position: relative;">
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Email Details
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="img3" alt="close" onclick="HideEditDialogEmail();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%--EmailDetailPopUp--%>

                <%--SMSDetailPopUp--%>
                <div id="SMSOverlays" class="web_dialog_overlay"></div>
                <div id="pnlsms" class="web_dialogEmail" style="display: none; height: 600px; width: 800px; overflow:auto">
                    <table id="tblsms" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="position: relative;">
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;SMS Details
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="img1" alt="close" onclick="HideEditDialogSMS();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%--SMSDetailPopUp--%>

                <%--WorkOrder Images--%>
                <asp:HiddenField ID="hfWOImgPopUp" runat="server" />
                <ajax:ModalPopupExtender ID="mpeWOImages" runat="server" TargetControlID="hfWOImgPopUp"
                    PopupControlID="panWOImages" BackgroundCssClass="modalBackground" CancelControlID="imgCancel"
                    PopupDragHandleControlID="panWOImages" />
                <asp:Panel ID="panWOImages" runat="server" Height="600px" Width="700px" CssClass="ModalWindow">
                    <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
                        width: 662px;">
                        <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" />
                    </div>
                    <iframe id="frmWOImages" runat="server" scrolling="no" style="height: 600px; width: 700px;"
                        frameborder="0"></iframe>
                </asp:Panel>
                <%--WorkOrder Images--%>
                <%----------Pre-Expense Detail Popup----------%>
                <div id="PreExpenseOverlays" class="web_dialog_overlay"></div>
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
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                                <asp:Label ID="lblPreExpenseCount" runat="server"></asp:Label>
                            </td>
                            <td class="GridContent_padding5">
                                <b>Sync Date</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblSyncDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent_padding5">
                                <b>Default Price($)</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblDefaultPrice" runat="server"></asp:Label>
                            </td>
                            <td class="GridContent_padding5">
                                <b>Total Price($)</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="GridContent_padding5">
                                <b>Added By</b>
                            </td>
                            <td class="GridContent_padding5">
                                <asp:Label ID="lblEntityName" runat="server"></asp:Label>
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
                                <b>Description</b>
                            </td>
                            <td class="GridContent_padding5" colspan="3">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="GridContent_padding5">
                                <b>Approval Note</b>
                            </td>
                            <td class="GridContent_padding5" colspan="3">
                                <asp:Label ID="lblApprovalNote" runat="server"></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
                </div>
                <%----------Pre-Expense Detail Popup----------%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

<script type="text/javascript">
    function VendorEmailDetailPopup(WorkOrderDetailID) {
        BindVendorEmailDetail(WorkOrderDetailID);
        ShowEditDialogEmail(true);
    }

    function BindVendorEmailDetail(WorkOrderDetailID) {

        $.ajax({
            type: "POST",
            url: "WorkOrderExpense.aspx/VendorEmailDetail",
            data: "{WorkOrderDetailID:" + WorkOrderDetailID + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                $('#<%=tblEmail.ClientID %> tr:gt(0)').remove()
                if (data.d.length > 0) {
                    for (var i = 0; i < data.d.length; i++) {

                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb' style='width:15%'>To</td><td class='lbl'>" + data.d[i].Mailto + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>CC</td><td class='lbl'>" + data.d[i].MailCCED + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>BCC</td><td class='lbl'>" + data.d[i].MailBCED + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>From</td><td class='lbl'>" + data.d[i].MailFrom + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>Attempt</td><td class='lbl'>" + data.d[i].Attempt + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>Logged On</td><td class='lbl'>" + data.d[i].LoggedOn + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>Sent On</td><td class='lbl'>" + data.d[i].SentOn + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>Subject</td><td class='lbl'>" + data.d[i].MailSubject + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td colspan='2' class='lblb'>Message</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td colspan='2' class='lbl'>" + data.d[i].MailBody + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td colspan='2'>&nbsp;</td></tr>");
                    }
                }
            }
        });
    }

    function ShowEditDialogEmail(modal) {
        $("#EmailOverlays").show();
        $("#pnlemail").fadeIn(10);

        if (modal) {
            $("#EmailOverlays").unbind("click");
        }
        else {
            $("#EmailOverlays").click(function (e) {
            });
        }
    }

    function HideEditDialogEmail() {
        $("#EmailOverlays").hide();
        $("#pnlemail").fadeOut(10);
    }

    function DriverEmailDetailPopup(WorkOrderDetailID) {
        BindDriverEmailDetail(WorkOrderDetailID);
        ShowEditDialogEmail(true);
    }

    function BindDriverEmailDetail(WorkOrderDetailID) {

        $.ajax({
            type: "POST",
            url: "WorkOrderExpense.aspx/DriverEmailDetail",
            data: "{WorkOrderDetailID:" + WorkOrderDetailID + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                $('#<%=tblEmail.ClientID %> tr:gt(0)').remove()
                if (data.d.length > 0) {
                    for (var i = 0; i < data.d.length; i++) {

                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb' style='width:15%'>To</td><td class='lbl'>" + data.d[i].Mailto + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>CC</td><td class='lbl'>" + data.d[i].MailCCED + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>BCC</td><td class='lbl'>" + data.d[i].MailBCED + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>From</td><td class='lbl'>" + data.d[i].MailFrom + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>Attempt</td><td class='lbl'>" + data.d[i].Attempt + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>Logged On</td><td class='lbl'>" + data.d[i].LoggedOn + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>Sent On</td><td class='lbl'>" + data.d[i].SentOn + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td class='lblb'>Subject</td><td class='lbl'>" + data.d[i].MailSubject + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td colspan='2' class='lblb'>Message</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td colspan='2' class='lbl'>" + data.d[i].MailBody + "</td></tr>");
                        $('#<%=tblEmail.ClientID %>').append("<tr><td colspan='2'>&nbsp;</td></tr>");
                    }
                }
            }
        });
    }

    function VendorSMSDetailPopup(WorkOrderDetailID) {
        BindVendorSMSDetail(WorkOrderDetailID);
        ShowEditDialogSMS(true);
    }

    function BindVendorSMSDetail(WorkOrderDetailID) {

        $.ajax({
            type: "POST",
            url: "WorkOrderExpense.aspx/VendorSMSDetail",
            data: "{WorkOrderDetailID:" + WorkOrderDetailID + "}",
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

    function DriverSMSDetailPopup(WorkOrderDetailID) {
        BindDriverSMSDetail(WorkOrderDetailID);
        ShowEditDialogSMS(true);
    }

    function BindDriverSMSDetail(WorkOrderDetailID) {

        $.ajax({
            type: "POST",
            url: "WorkOrderExpense.aspx/DriverSMSDetail",
            data: "{WorkOrderDetailID:" + WorkOrderDetailID + "}",
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
            });
        }
    }

    function HideEditDialogSMS() {
        $("#SMSOverlays").hide();
        $("#pnlsms").fadeOut(10);
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
            url: "WorkOrderExpense.aspx/PreExpenseDetails",
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
                    
                    $('#<%=lblTotalPrice.ClientID %>').text(data.d[0].TotalPrice);
                    $('#<%=lblEntityName.ClientID %>').text(data.d[0].AddedBy);
                    $('#<%=lblDescription.ClientID %>').html(data.d[0].Description);
                    $('#<%=lblDeviceName.ClientID %>').html(data.d[0].DeviceName);
                   
                }
                else {
                    $('#<%=lblVIN.ClientID %>').text('');
                    $('#<%=lblExpenseDate.ClientID %>').text('');
                    $('#<%=lblPreExpenseCount.ClientID %>').text('');
                    $('#<%=lblSyncDate.ClientID %>').text('');
                    $('#<%=lblDefaultPrice.ClientID %>').text('');
                   
                    $('#<%=lblTotalPrice.ClientID %>').text('');
                    $('#<%=lblEntityName.ClientID %>').text('');
                    $('#<%=lblDescription.ClientID %>').text('');
                    $('#<%=lblDeviceName.ClientID %>').text('');
                   
                }
            }
        });
    }

    
</script>
</asp:Content>

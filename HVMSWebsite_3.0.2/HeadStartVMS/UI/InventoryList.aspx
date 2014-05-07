<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="InventoryList.aspx.cs" Inherits="METAOPTION.UI.InventoryList" Title="HeadstartVMS::InventoryList" %>

<asp:Content ID="cphInventoryList" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upnlSearch" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnOK" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="hfInventoryID" runat="server" Value="0" />
                <asp:HiddenField ID="hfcryear" runat="server" />
                <asp:HiddenField ID="hfcrmake" runat="server" />
                <asp:HiddenField ID="hfcrmodel" runat="server" />
                <asp:HiddenField ID="hfcrbody" runat="server" />
                <asp:HiddenField ID="hfcrprice" runat="server" />
                <asp:HiddenField ID="hfcrmileage" runat="server" />
                <asp:HiddenField ID="hfcrextcol" runat="server" />
                <asp:HiddenField ID="hfcrintcol" runat="server" />
                <div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="TableHeadingBg TableHeading" colspan="6">
                                Inventory List
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvSearch" runat="server" class="dvSearch" style="display: block; width: 100%;">
                    <div style="width: 32%; float: left; padding: 5px;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="TableBorder">
                                    VIN #
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtVINNumber" runat="server" MaxLength="50" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Year
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="txt1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Make
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtMake" runat="server" MaxLength="50" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Model
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtModel" runat="server" MaxLength="50" CssClass="txt2" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 26%; float: left; padding: 5px 5px 5px 10px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="TableBorder">
                                    Dealer
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtDealer" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Car Status
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlCarStatus" runat="server" CssClass="txt1">
                                        <asp:ListItem Value="-1">ALL</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">Active</asp:ListItem>
                                        <asp:ListItem Value="3">Archived</asp:ListItem>
                                    </asp:DropDownList>
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
                        </table>
                    </div>
                    <div style="width: 38%; float: left; padding: 5px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder" style="vertical-align: top; width: 87px">
                                    Sort 1
                                </td>
                                <td class="TableBorder" style="vertical-align: top; width: 290px">
                                    <div style="float: left; margin-right: 10px">
                                        <asp:DropDownList ID="ddlSort1" runat="server" CssClass="txt2" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSort1_SelectedIndexChanged" />
                                    </div>
                                    <asp:RadioButtonList ID="rbtnSort1Direction" runat="server" RepeatDirection="Horizontal"
                                        CellPadding="2">
                                        <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
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
                                        <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                            Selected="True" />
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
                                        <asp:DropDownList ID="ddlSort3" runat="server" CssClass="txt2" AutoPostBack="true" />
                                    </div>
                                    <asp:RadioButtonList ID="rbtnSort3Direction" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                            Selected="True" />
                                        <asp:ListItem Value="DESC" Text=" Z - A " />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="padding: 7px 0px 0px 10px">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click"
                                        Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upnlSearch">
                        <ProgressTemplate>
                            <div id="dvProg" class="overlay">
                                <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                wait...
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="lblCount" runat="server" BorderColor="Transparent" BackColor="Transparent"
                                ForeColor="#21618C" />
                        </td>
                        <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                            Page#&nbsp;&nbsp;
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
                    <asp:GridView ID="gvInventoryList" runat="server" Width="100%" EmptyDataText="No record found for the selected criteria"
                        AutoGenerateColumns="False" AllowPaging="true" PageSize="50" PagerSettings-Visible="false"
                        AllowSorting="true" OnSorting="gvInventoryList_Sorting" DataKeyNames="InventoryID"
                        OnRowDataBound="gvInventoryList_RowDataBound">
                        <Columns>
                            <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
                            <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "InventoryDetail.aspx?Mode=View&Code="+Eval("InventoryId") %>'
                                        runat="server" ImageUrl="~/Images/Select.gif" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="VIN" HeaderText="VIN #" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" SortExpression="I.[YEAR]" HeaderStyle-Font-Underline="true" />
                            <asp:BoundField DataField="MakeName" HeaderText="Make" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" SortExpression="Mobile_Chrome_Make.VINDivisionName"
                                HeaderStyle-Font-Underline="true" />
                            <asp:BoundField DataField="ModelName" HeaderText="Model" HeaderStyle-Wrap="false"
                                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" SortExpression="Mobile_Chrome_Model.VINModelName"
                                HeaderStyle-Font-Underline="true" />
                            <asp:BoundField DataField="Body" HeaderText="Body" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" SortExpression="Mobile_Chrome_Body.VINStyleName" HeaderStyle-Font-Underline="true" />
                            <asp:BoundField DataField="VehiclePresent" HeaderText="V" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" SortExpression="[VehiclePresent]"
                                HeaderStyle-Font-Underline="true">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Arrival" HeaderText="Arrival Date" HeaderStyle-Wrap="false"
                                DataFormatString="{0:d}" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                SortExpression="[ARRIVALDATE]" HeaderStyle-Font-Underline="true">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TitlePresent" HeaderText="T" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" SortExpression="[TITLEPRESENT]"
                                HeaderStyle-Font-Underline="true">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CarCost" HeaderText="Car Cost ($)" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:#,###}" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers"
                                SortExpression="[CARCOST]" HeaderStyle-Font-Underline="true">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MileageIn" HeaderText="Mileage" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:#,###}" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers"
                                SortExpression="[MileageIn]" HeaderStyle-Font-Underline="true">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ComeBack" HeaderText="CB" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" SortExpression="[ComeBackStatus]"
                                HeaderStyle-Font-Underline="true">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Ext / Int Color" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <%# Eval("ExtColor")%>&nbsp;-&nbsp;<%# Eval("IntColor")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Buyer" HeaderText="Buyer" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" ItemStyle-Wrap="false" SortExpression="FirstName"
                                HeaderStyle-Font-Underline="true" />
                            <asp:TemplateField HeaderText="Purchased From" SortExpression="[DEALERNAME]" HeaderStyle-Font-Underline="true"
                                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <div class="dealer">
                                        <%#Eval("DealerName")%>
                                        <span>
                                            <%#String.Format("{0} {1} [ID: {2}]", Eval("DealerName"), Eval("DealerAddress"), Eval("DealerId"))%></span>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers"
                                ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnCR" runat="server" OnClick="ibtnCR_Click" />
                                    <a id="ibtnCRAvailable" runat="server" target="_blank" href="#" visible="false" style="text-decoration: none">
                                        <img id="imgAvailabel" alt="No Image" src="../Images/ucr-btn.png" />
                                    </a>
                                    <asp:ImageButton ID="ibtncars" runat="server" ImageUrl="~/Images/car_icon.gif" OnClick="ibtncars_Click" />
                                    <asp:HiddenField ID="hdnInventoryId" runat="server" Value='<%# Eval("InventoryID") %>' />
                                    <asp:HiddenField ID="hdnCarcost" runat="server" Value='<%# Eval("CarCost") %>' />
                                    <asp:HiddenField ID="hfCRStatus" runat="server" Value='<%# Eval("Inv_CRStatus") %>' />
                                    <asp:HiddenField ID="hfVIN2" runat="server" Value='<%# Eval("VIN") %>' />
                                    <asp:HiddenField ID="hfyear" runat="server" Value='<%# Eval("Year") %>' />
                                    <asp:HiddenField ID="hfmake" runat="server" Value='<%# Eval("MakeName") %>' />
                                    <asp:HiddenField ID="hfmodel" runat="server" Value='<%# Eval("ModelName") %>' />
                                    <asp:HiddenField ID="hfbody" runat="server" Value='<%# Eval("Body") %>' />
                                    <asp:HiddenField ID="hfprice" runat="server" Value='<%# Eval("CarCost") %>' />
                                    <asp:HiddenField ID="hfmileage" runat="server" Value='<%# Eval("MileageIn") %>' />
                                    <asp:HiddenField ID="hfextcol" runat="server" Value='<%# Eval("ExtColor") %>' />
                                    <asp:HiddenField ID="hfintcol" runat="server" Value='<%# Eval("IntColor") %>' />
                                    <asp:HiddenField ID="hfCRId" runat="server" Value='<%# Eval("Inv_CRId") %>' />
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
                        <HeaderStyle CssClass="gvHeading"></HeaderStyle>
                        <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                        <EmptyDataRowStyle CssClass="gvEmpty" />
                    </asp:GridView>
                </div>
                <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                    <tr>
                        <td style="width: 40%">
                            <asp:Label ID="lblCount1" runat="server" BorderColor="Transparent" BackColor="Transparent"
                                ForeColor="#21618C" />
                            &nbsp;
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn" OnClick="btnExport_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                            Page#
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
                <div class="FooterContentDetails" style="text-align: left">
                    <a href="ValidateVin.aspx" class="AddNewExpenseTxt" id="hlnkAddNew" runat="server">
                        <img src="../Images/AddNew.gif" alt="New" style="border: none" />
                        Add New Inventory</a>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upnlSearch">
                        <ProgressTemplate>
                            <div id="dvProg1" class="overlay">
                                <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                wait...
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
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
                <asp:Panel ID="pnlExport" runat="server"  CssClass="popup_Body" Style="display: none;
                    margin: 10px; background: white; width: 600px; height: 200px">
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
                                <asp:Button ID="btnOK" runat="server" CssClass="btn" Text="Yes" Width="80px" OnClick="btnOK_Click"
                                    OnClientClick="return HidempeExport();" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="No" Width="80px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%----------Image slideshow----------%>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        var BrowserName = $.browser.name;

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
            document.getElementById('dvProg1').style.left = posx + 10 + "px";
            document.getElementById('dvProg1').style.top = posy + "px";
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
            var querystring = "SystemID=2&VIN=" + $('#<%=hfvin.ClientID %>').val();
            querystring += "&SourceInventoryID=" + $('#<%=hfInventoryID.ClientID %>').val();
            querystring += "&Year=" + $('#<%=hfcryear.ClientID %>').val();
            querystring += "&Make=" + $('#<%=hfcrmake.ClientID %>').val();
            querystring += "&Model=" + $('#<%=hfcrmodel.ClientID %>').val();
            querystring += "&Body=" + $('#<%=hfcrbody.ClientID %>').val();
            querystring += "&Mileage=" + $('#<%=hfcrmileage.ClientID %>').val();
            querystring += "&MPrice=" + $('#<%=hfcrprice.ClientID %>').val();
            querystring += "&ExtColor=" + $('#<%=hfcrextcol.ClientID %>').val();
            querystring += "&IntColor=" + $('#<%=hfcrintcol.ClientID %>').val();

            //window.open("http://web.metaoptionllc.com:82/cardetail/newcr?"+querystring);
            window.open(url + querystring);
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

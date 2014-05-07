<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreInventory.aspx.cs" Inherits="METAOPTION.UI.PreInventory"
    Title="HeadStart VMS :: PreInventory" %>

<%--<%@ Register Src="~/UserControls/CarSlideShow.ascx" TagName="CarSlideShow" TagPrefix="mo1" %>--%>
<asp:content id="cphInventoryList" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upnlSearch" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hfPreInvID" runat="server" Value="0" />
                <asp:HiddenField ID="hdnVIN" runat="server" Value="0" />
                <div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td id="tdtitle" runat="server" class="TableHeadingBg TableHeading" colspan="6">
                                VIEW ALL PRE-INVENTORIES
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
                                    <asp:TextBox ID="txtVINNumber" runat="server" MaxLength="50" CssClass="txt2" />
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
                                    <asp:DropDownList ID="ddlModel" runat="server" CssClass="txt2" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Body
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlBody" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 26%; float: left; padding: 5px 5px 5px 10px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    Added By
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlUsers" runat="server" CssClass="txt2" />
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
                                    Dealer
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtDealer" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Status
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txt2">
                                        <asp:ListItem Text="ALL" Value="-1" />
                                        <asp:ListItem Text="Pending" Value="1" Selected="True" />
                                        <asp:ListItem Text="Rejected" Value="2" />
                                        <asp:ListItem Text="Added To Inventory" Value="3" />
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
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upnlSearch">
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
                            <%= grvPreInv.PageCount%>
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
                    <mo:ExtGridView ID="grvPreInv" runat="server" DataKeyNames="PreInventoryId" AutoGenerateColumns="false"
                        ShowHeader="true" AllowPaging="true" PageSize="10" AllowSorting="true" Width="100%"
                        OnPageIndexChanging="grvPreInv_PageIndexChanging" OnSorting="grvPreInv_OnSorting"
                        OrderBy="PreInventoryId desc" EmptyDataText="No record found." EmptyDataRowStyle-CssClass="GridEmptyRow"
                        CssClass="Grid" OnRowDataBound="grvPreInv_RowDataBound">
                        <PagerSettings Mode="NumericFirstLast" Visible="true" />
                        <PagerSettings Position="TopAndBottom" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnaddinv" runat="server" ImageUrl="~/Images/H_add.png" ToolTip="Add to Inventory"
                                        OnClick="ibtnaddinv_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Year" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="15px"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="true" ItemStyle-CssClass="GridContentNumbers"
                                ItemStyle-Width="15px" SortExpression="p.Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblYear" runat="server" Text=' <%# Eval("Year")%>'></asp:Label><br />
                                    <asp:ImageButton ID="ibtnExpand" runat="server" OnClientClick="return ShowDetails(this);"
                                        ImageUrl="~/Images/expand.png" ToolTip="Expand" Style="padding: 5px;" /><%--OnClick="ibtnExpand_Click"--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridHeader"
                                HeaderStyle-Width="15px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="true"
                                ItemStyle-CssClass="GridContentNumbers" ItemStyle-Width="15px" SortExpression="p.Year">
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Make<br/>Model<br/>Body" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" SortExpression="mk.VINDivisionName">
                                <ItemTemplate>
                                    <%# Eval("Make")%><br />
                                    <%# Eval("Model")%><br />
                                    <%# Eval("Body")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Mileage" HeaderText="Mileage" DataFormatString="{0:#,###}"
                                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers" ItemStyle-HorizontalAlign="Right"
                                SortExpression="p.MileageIn" ItemStyle-Width="30px"></asp:BoundField>
                            <asp:BoundField HeaderText="Price($)" DataField="Price" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContentNumbers" DataFormatString="{0:#,###}" SortExpression="Price"
                                ItemStyle-Width="30px" />
                            <asp:TemplateField HeaderText="ExtColor<br/>IntColor" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" ItemStyle-Wrap="true">
                                <ItemTemplate>
                                    <%# Eval("ExtColor")%><br />
                                    <%# Eval("IntColor")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Buyer" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                ItemStyle-Wrap="true" SortExpression="Bu.FirstName">
                                <ItemTemplate>
                                    <%# FormatBuyer(Eval("Buyer")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hylnkView" NavigateUrl='<%# "ViewDealer.aspx?Mode=View&EntityId="+Eval("DealerId")+"&type=1" %>'
                                        runat="server" Text='<%#Eval("Dealer") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sun<br/>Lea<br/>Nav" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                <ItemTemplate>
                                    <%# Eval("SunRoof")%><br />
                                    <%# Eval("Leather")%><br />
                                    <%# Eval("Navigation")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VIN#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                ItemStyle-Wrap="false" ItemStyle-VerticalAlign="Top" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <div style="text-transform: uppercase">
                                        <asp:Label ID="lblvin" runat="server" Text='<%# Eval("VIN") %>'></asp:Label>
                                        <asp:HyperLink ID="hlnkVIN" NavigateUrl='<%# "InventoryDetail.aspx?Mode=View&Code="+Eval("InvID") %>'
                                            runat="server" Text='<%#Eval("VIN") %>' Visible="false" />
                                        <asp:ImageButton ID="ibtnDuplicate" runat="server" ImageUrl="~/Images/duplicate-icon.png"
                                            OnClick="ibtnDuplicate_Click" ToolTip="View Duplicate" /><br />
                                    </div>
                                    <i>
                                        <%# Duplicate(Eval("PreInvDuplicateVIN"),Eval("InvDuplicateVIN"))%></i>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sync Date<br />Added By" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" ItemStyle-Width="120px" ItemStyle-Wrap="true">   
                                <ItemTemplate>
                                    <%#Eval("SyncDate") %><br />
                                    <%#Eval("DisplayName")%><br />
                                    (<%#Eval("EntityType")%>)
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
                            <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="GridHeader GridHeaderAction"
                                ItemStyle-CssClass="GridContentNumbers" HeaderStyle-Width="140px" ItemStyle-Width="140px"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnEmailStatus" runat="server" ImageUrl="~/Images/Email_Sent.png"
                                        OnClick="ibtnEmailStatus_Click" />
                                    <asp:HiddenField ID="hfIsEmailSent" runat="server" Value='<%# Eval("IsEmailSent") %>' />
                                    <asp:ImageButton ID="ibtnCR" runat="server" />
                                    <asp:HyperLink id="ancrCR" runat="server" target="_blank" visible="false" >
                                    <asp:Image id="imggCR" runat="server" />
                                    </asp:HyperLink>
                                    <%--OnClick="ibtnCR_Click"--%>
                                    <asp:ImageButton ID="ibtnView" ToolTip="View Pre-Inventory Details" runat="server"
                                        ImageUrl="~/Images/Select1.png" OnClick="ibtnView_Click" />
                                    <asp:ImageButton ID="ibtncars" runat="server" ImageUrl="~/Images/car_icon.gif" OnClick="ibtncars_Click" />
                                    <asp:ImageButton ID="ibtnDelete" ToolTip="Discard/Reject Pre-Inventory" runat="server"
                                        ImageUrl="~/Images/DeleteButton1.png" OnClick="ibtnDelete_Click" />
                                    <asp:ImageButton ID="ibtnPending" ToolTip="Make Pending" runat="server" ImageUrl="~/Images/Pending.png"
                                        OnClick="ibtnPending_Click" />
                                    <asp:HiddenField ID="hfpending" runat="server" Value='<%# Eval("IsPending") %>' />
                                    <asp:HiddenField ID="hfIsActive" runat="server" Value='<%# Eval("IsActive") %>' />
                                    <asp:HiddenField ID="hfRejected" runat="server" Value='<%# Eval("IsRejected") %>' />
                                    <asp:HiddenField ID="hfyear" runat="server" Value='<%# Eval("Year") %>' />
                                    <asp:HiddenField ID="hfmake" runat="server" Value='<%# Eval("Make") %>' />
                                    <asp:HiddenField ID="hfmodel" runat="server" Value='<%# Eval("Model") %>' />
                                    <asp:HiddenField ID="hfCRStatus" runat="server" Value='<%# Eval("PreInv_CRStatus") %>' />
                                    <asp:HiddenField ID="hfPreInv_CRId" runat="server" Value='<%# Eval("PreInv_CRId") %>' />
                                    <asp:HiddenField ID="hfbody" runat="server" Value='<%# Eval("Body") %>' />
                                    <asp:HiddenField ID="hfprice" runat="server" Value='<%# Eval("Price") %>' />
                                    <asp:HiddenField ID="hfmileage" runat="server" Value='<%# Eval("Mileage") %>' />
                                    <asp:HiddenField ID="hfextcol" runat="server" Value='<%# Eval("ExtColor") %>' />
                                    <asp:HiddenField ID="hfintcol" runat="server" Value='<%# Eval("IntColor") %>' />
                                    <asp:HiddenField ID="hfDuplicateInv" runat="server" Value='<%# Eval("InvDuplicateVIN") %>' />
                                    <asp:HiddenField ID="hfDuplicatePreInv" runat="server" Value='<%# Eval("PreInvDuplicateVIN") %>' />
                                    <asp:HiddenField ID="hfBuyerNote" runat="server" Value='<%# Eval("BuyerNote") %>' />
                                    <%--<asp:Label ID="lblInvID" runat="server" Visible="false" Text='<%# Bind("InvID") %>'></asp:Label>
                                    <asp:Label ID="lblImageCount" runat="server" Visible="false" Text='<%# Bind("ImageCount") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                ItemStyle-Width="0px" HeaderStyle-Width="0px"  ControlStyle-Width="0px">
                                <ItemTemplate>
                                    <tr id="trnew" runat="server" style="display: none">
                                        <td colspan="14" class="GridContent" style="padding-left: 10px;">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 5px; max-width: 15px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Grade:
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <%# FormatEmptyString(Eval("Grade")) %>
                                                    </td>
                                                    <td style="width: 15px; max-width: 30px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Market Price($):
                                                    </td>
                                                    <td style="width: 20px; max-width: 30px;">
                                                        <%# FormatEmptyString(String.Format("{0:#,###}", Eval("MarketPrice")))%>
                                                    </td>
                                                    <td style="width: 20px; max-width: 28px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Bad CARFAX:
                                                    </td>
                                                    <td style="width: 5px;">
                                                        <%# FormatEmptyString(Eval("BadCarFax")) %>
                                                    </td>
                                                    <td style="width: 20px; max-width: 36px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Bad AUTOCHECK:
                                                    </td>
                                                    <td style="width: 5px;">
                                                        <%# FormatEmptyString(Eval("BadAutoCheck")) %>
                                                    </td>
                                                    <td style="width: 25px; max-width: 25px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Buyer Note:
                                                    </td>
                                                    <td style="width: 50px; min-height: 50px;">
                                                        <%# FormatEmptyString(Eval("BuyerNote")) %>
                                                    </td>
                                                    <td style="width: 10px; max-width: 24px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Comments:
                                                    </td>
                                                    <td style="width: 100px;">
                                                        <%# FormatEmptyString(Eval("Comments")) %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trnew1" runat="server" style="display: none">
                                        <td colspan="14" class="GridContent" style="padding-left: 10px;">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:5px; max-width:5px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Trans:
                                                    </td>
                                                    <td style="width: 10px; max-width:10px">
                                                        <%# FormatEmptyString(Eval("TransType"))%>
                                                    </td>
                                                    <td style="width:5px; max-width:5px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Engine:
                                                    </td>
                                                    <td style="width: 10px; max-width: 10px;">
                                                        <%# FormatEmptyString(Eval("EngineType"))%>
                                                    </td>
                                                    <td style="width:10px; max-width:10px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Wheel Drive:
                                                    </td>
                                                    <td style="width: 10px; max-width: 10px;">
                                                        <%# FormatEmptyString(Eval("WheelDrive"))%>
                                                    </td>
                                                    <td style="width: 5px; max-width:5px; padding-right: 1px;" class="GridNewRowHeader">
                                                        AC:
                                                    </td>
                                                    <td style="width:10px;max-width:10px">
                                                        <%# FormatEmptyString(Eval("AC")) %>
                                                    </td>
                                                    <td style="width:10px; max-width:10px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Power Windows:
                                                    </td>
                                                    <td style="width:10px;max-width:10px">
                                                        <%# FormatEmptyString(Eval("PowerWindows"))%>
                                                    </td>
                                                    <td style="width: 10px; max-width: 10px; padding-right: 1px;" class="GridNewRowHeader">
                                                        Power Locks:
                                                    </td>
                                                    <td style="width:10px;max-width:10px">
                                                        <%# FormatEmptyString(Eval("PowerLocks"))%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="InvID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvID" runat="server" Text='<%# Bind("InvID") %>'></asp:Label>
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
                    </mo:ExtGridView>
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
                            Page#
                            <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                                AutoPostBack="true" />
                            of
                            <%= grvPreInv.PageCount%>
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
                <div id="divImagepopup" runat="server" style="text-align: left; display: none; background-color: #FFF;
                    width: auto; height: auto; position: absolute; top: 3px; text-align: left">
                    <div class="popuptitlebar" runat="server" id="divAddNewTopicTitle">
                        <div style="float: left; padding: 4px">
                            Car Images
                        </div>
                        <div style="text-align: right">
                            <img src="../Images/close_icon.gif" alt="close" style="cursor: pointer" onclick="$find('mdpopCarShow').hide();" />
                        </div>
                    </div>
                    <div style="padding: 0px 10px">
                        <%--<mo1:CarSlideShow id="CarSlideShow" runat="server" />--%>
                    </div>
                </div>
                <asp:HiddenField ID="hfPopup" runat="server" />
                <asp:HiddenField ID="hfDeletePrvID" runat="server" />
                <asp:HiddenField ID="hfdel" runat="server" />
                <asp:HiddenField ID="hfucr" runat="server" />
                <asp:HiddenField ID="hfcryear" runat="server" />
                <asp:HiddenField ID="hfcrmake" runat="server" />
                <asp:HiddenField ID="hfcrmodel" runat="server" />
                <asp:HiddenField ID="hfcrbody" runat="server" />
                <asp:HiddenField ID="hfcrprice" runat="server" />
                <asp:HiddenField ID="hfcrmileage" runat="server" />
                <asp:HiddenField ID="hfcrextcol" runat="server" />
                <asp:HiddenField ID="hfcrintcol" runat="server" />
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfPopup"
                    PopupControlID="panOpen" BackgroundCssClass="modalBackground" CancelControlID="imgCancel"
                    PopupDragHandleControlID="panOpen" />
                <asp:Panel ID="panOpen" runat="server" Height="600px" Width="700px" CssClass="ModalWindow">
                    <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
                        width: 662px;">
                        <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" />
                    </div>
                    <iframe id="frame1" runat="server" scrolling="no" style="height: 600px; width: 700px;"
                        frameborder="0"></iframe>
                    <%--<asp:Button ID="btnCancel" runat="server" Text="Close" />--%>
                </asp:Panel>
                <%--Delete ConfirmBox--%>
                <asp:Panel ID="pnlDelete" CssClass="modalPopup" Style="display: none;" runat="server"
                    HorizontalAlign="Left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Discard / Reject Pre-Inventory
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="imgClose" onclick="ClearReasonTextBox();$find('MBILinkedCars').hide();return false;"
                                    alt="close" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="LeftPanelContentHeading">
                                What do you want to do?
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rdlist" runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="rdlist_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="Discard / Delete this Pre-Inventory<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<i>Once deleted, this record will not be appear in the list and you cannot perform any action</i>)"
                                        Value="D" Selected="True" class="LeftPanelContentHeading"></asp:ListItem>
                                    <asp:ListItem Text="Reject this Pre-Inventory<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<i>Once rejected, it will appear in the list, but you cannot perform any action</i>)"
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
                                <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" class="btn" />
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" class="btn" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajax:ModalPopupExtender ID="ModelPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="MBILinkedCars" TargetControlID="hfdel" PopupControlID="pnlDelete"
                    CancelControlID="CancelButton" OkControlID="OkButton">
                </ajax:ModalPopupExtender>
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
                <ajax:ModalPopupExtender ID="mpucr" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="hfucr" PopupControlID="pnlUCRResponse" CancelControlID="imgucrclose"
                    OkControlID="btnucrok">
                </ajax:ModalPopupExtender>
                <%--Show UCR Response End--%>
                <%--UCR BEGIN--%>
                <div id="overlay" class="web_dialog_overlay">
                </div>
                <div id="divCR" class="web_dialog" style="display: none; min-width: 400px; min-height: 200px">
                    <div id="dvUCR" class="modalPopup" style="display: none; min-width: 400px; min-height: 200px"
                        runat="server">
                    </div>
                    <div>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="PopUpBoxHeading">
                                    &nbsp;&nbsp;Add/Link UCR
                                    <asp:Label ID="lblucrheader" runat="server"></asp:Label>
                                </td>
                                <td class="PopUpBoxHeading" align="right">
                                    <img border="0" src="../Images/close.gif" id="imgCloseCR" alt="close" onclick="HideCRDialog();" />
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
                                OnClientClick="HideCRDialog();return false;" />
                            <%--OnClick="btnCRcancel_Click"--%>
                        </div>
                        <div style="padding: 5px 0px 0px 15; margin: 5px 0px 10px 30px">
                            <asp:Label ID="lblError1" runat="server" Visible="false" CssClass="error" />
                        </div>
                    </div>
                    <%--</div>--%>
                </div>
                <asp:HiddenField ID="hfCR" runat="server" />
                <asp:HiddenField ID="hfvin" runat="server" />
                <ajax:ModalPopupExtender ID="mpeCR" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="mpecrbeh" TargetControlID="hfCR" PopupControlID="dvUCR" CancelControlID="imgCloseCR">
                </ajax:ModalPopupExtender>
                <%--UCR END--%>
                <%----------------------Duplicate VIN popup------------------------%>
                <div id="dvDuplicates" class="modalPopup" style="display: none; width: 650px; min-height: 280px"
                    runat="server">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Duplicate VIN In
                            </td>
                            <td class="PopUpBoxHeading">
                                <asp:RadioButtonList ID="rbtnlstDuplicateVIN" runat="server" RepeatDirection="Horizontal"
                                    CellSpacing="3" CssClass="PopupHeaderRadio" AutoPostBack="true" OnSelectedIndexChanged="rbtnlstDuplicateVIN_SelectedIndexChanged">
                                    <asp:ListItem Text="Pre-Inventory" Value="P" Selected="True" class="PopupHeaderRadio" />
                                    <asp:ListItem Text="Inventory" Value="I" class="PopupHeaderRadio" />
                                </asp:RadioButtonList>
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="imgCloseDuplicate" alt="close" />
                            </td>
                        </tr>
                    </table>
                    <div id="dvDuplicatePreInv" runat="server">
                        <asp:GridView ID="gvLinkedFilter" runat="server" Width="100%" BorderWidth="0" AllowPaging="True"
                            CssClass="Grid" DataKeyNames="PreInventoryId" PageSize="10" AutoGenerateColumns="False"
                            EmptyDataText="No Rows found" EmptyDataRowStyle-CssClass="gvEmpty" OnPageIndexChanging="gvLinkedFilter_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="SyncDate" SortExpression="SyncDate" HeaderText="SyncDate"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Width="110px" HeaderStyle-Width="110px" ItemStyle-CssClass="GridContent">
                                </asp:BoundField>
                                <asp:BoundField DataField="ScanDate" SortExpression="ScanDate" HeaderText="ScanDate"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Width="110px" ItemStyle-CssClass="GridContent" HeaderStyle-Width="110px">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Linked Cars" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <%#Eval("VIN")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Device<br/>Info" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" HeaderStyle-Width="60px">
                                    <ItemTemplate>
                                        <%# Eval("DeviceName")%>
                                        <br />
                                        <asp:Label ID="lbldeviceid" runat="server" Text='<%# FormatDeviceID(Eval("DeviceID")) %>'
                                            ToolTip='<%# Eval("DeviceID") %>'></asp:Label>
                                        <asp:HiddenField ID="hfInvID" runat="server" Value='<%# Eval("InventoryID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ScanBy" SortExpression="ScanBy" HeaderText="Scan By" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" ItemStyle-Wrap="true" HeaderStyle-Width="90px">
                                </asp:BoundField>
                            </Columns>
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="PreTableBorderB" />
                            <RowStyle CssClass="gvRow" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <PagerStyle CssClass="PreFooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                    <div id="dvDuplicateInv" runat="server">
                        <asp:GridView ID="grdlinkedcarInv" runat="server" Width="100%" BorderWidth="0" AllowPaging="True"
                            CssClass="Grid" DataKeyNames="InventoryId" PageSize="10" AutoGenerateColumns="False"
                            EmptyDataText="No Rows found" EmptyDataRowStyle-CssClass="gvEmpty" OnPageIndexChanging="grdlinkedcarInv_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="AddedDate" SortExpression="AddedDate" HeaderText="AddedDate"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Width="110px" HeaderStyle-Width="110px" ItemStyle-CssClass="GridContent">
                                </asp:BoundField>
                                <asp:BoundField DataField="SyncDate" SortExpression="SyncDate" HeaderText="SyncDate"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Width="110px" HeaderStyle-Width="110px" ItemStyle-CssClass="GridContent">
                                </asp:BoundField>
                                <asp:BoundField DataField="ScanDate" SortExpression="ScanDate" HeaderText="ScanDate"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Width="110px" ItemStyle-CssClass="GridContent" HeaderStyle-Width="110px">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Linked Cars" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    ItemStyle-Width="120px" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <%#Eval("VIN")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DeviceName<br/>DeviceID" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" HeaderStyle-Width="60px">
                                    <ItemTemplate>
                                        <%# Eval("DeviceName")%>
                                        <br />
                                        <asp:Label ID="lbldeviceid" runat="server" Text='<%# FormatDeviceID(Eval("DeviceID")) %>'
                                            ToolTip='<%# Eval("DeviceID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AddedBy" SortExpression="AddedBy" HeaderText="AddedBy"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" ItemStyle-Wrap="true"
                                    HeaderStyle-Width="90px"></asp:BoundField>
                            </Columns>
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="PreTableBorderB" />
                            <RowStyle CssClass="gvRow" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <PagerStyle CssClass="PreFooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                </div>
                <asp:HiddenField ID="hfDuplicateVIN" runat="server" />
                <ajax:ModalPopupExtender ID="mpeDuplicateVIN" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="hfDuplicateVIN" PopupControlID="dvDuplicates" CancelControlID="imgCloseDuplicate">
                </ajax:ModalPopupExtender>
                <%----------------------Duplicate VIN popup------------------------%>
                <%----------------------Change UCR Begin----------------------%>
                <div id="dvChangeUCR" class="modalPopup" style="display: none; min-width: 400px;
                    min-height: 200px" runat="server">
                </div>
                <div id="divChangeCR" class="web_dialog" style="display: none; min-width: 400px;
                    min-height: 200px">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Change UCR
                                <asp:Label ID="lblucrheader2" runat="server"></asp:Label>
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="imgCloseCR2" alt="close" onclick="HideChangeCRDialog();return false;" />
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
                            OnClientClick="HideChangeCRDialog();return false;" />&nbsp;&nbsp;&nbsp;
                        <%--OnClick="btnCRcancel_Click"--%>
                        <asp:HyperLink ID="hlnkViewReport" runat="server" Text="View Report" Target="_blank"
                            CssClass="btn" Width="100px" />
                    </div>
                    <div style="padding: 5px 0px 0px 15; margin: 5px 0px 10px 30px">
                    </div>
                    <div style="padding: 5px 0px 0px 15; margin: 5px 0px 10px 30px">
                        <asp:Label ID="lblError2" runat="server" Visible="false" CssClass="error" />
                    </div>
                </div>
                <%--</div>--%>
                <asp:HiddenField ID="hfCR2" runat="server" />
                <ajax:ModalPopupExtender ID="mpeChangeCR" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="mpecrbeh2" TargetControlID="hfCR2" PopupControlID="dvChangeUCR" CancelControlID="imgCloseCR2">
                </ajax:ModalPopupExtender>
                <%------------------------Change UCR End------------------------%>
                <%------------------------Email Detail popup begin------------------------%>
                <asp:HiddenField ID="hfEmailDetailPopup" runat="server" />
                <ajax:ModalPopupExtender ID="mpeEmailDetail" runat="server" TargetControlID="hfEmailDetailPopup"
                    PopupControlID="pnlEmailDetail" BackgroundCssClass="modalBackground" CancelControlID="imgCloseEmailDetail"
                    PopupDragHandleControlID="pnlEmailDetail" />
                <div id="pnlEmailDetail" runat="server" style="height: 600px; width: 800px;" class="ModalWindow">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;Email Details
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="imgCloseEmailDetail" alt="close" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <iframe id="IfrmEmailDetail" runat="server" style="width: 800px; height: 500px; overflow: auto;
                                    border: solid 1px #115481; background-color: #fff" frameborder="0"></iframe>
                            </td>
                        </tr>
                    </table>
                </div>
                <%------------------------Email Detail popup End------------------------%>
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

        function fnClickUpdate(sender, e) {
            __doPostBack(sender, e);
        }

        $(function () {
            $(".Tooltip").tipTip({ maxWidth: "auto", edgeOffset: 10 });
        });

        function ClearReasonTextBox()
        { $('#<%=txtreason.ClientID %>').val(''); }

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

        function HideLinkCRDIVOnPopUpOpen() {
            $('#<%=dvLinkCR.ClientID %>').hide();
            $('#<%=dvCRIdUrl.ClientID %>').hide();
            $('#<%=btnCRok.ClientID %>').show();
            $('#<%=btncridvalidate.ClientID %>').hide();
            $('#<%=btncrurlvalidate.ClientID %>').hide();
            $('#<%=btncrsearch.ClientID %>').hide();
        }

        function OpenCreateUCRLink() {
            var url = '<%=CreateUCRUrl %>';
            var querystring = "SystemID=2&VIN=" + $('#<%=hfvin.ClientID %>').val();
            querystring += "&SourceInventoryID=" + $('#<%=hfPreInvID.ClientID %>').val();
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

        function HideChangeCROnPopupOpen() {
            $('#<%=dvLinkCR2.ClientID %>').hide();
            $('#<%=dvCRIdUrl2.ClientID %>').hide();
            $('#<%=btnCRok2.ClientID %>').show();
            $('#<%=btncridvalidate2.ClientID %>').hide();
            $('#<%=btncrurlvalidate2.ClientID %>').hide();
            $('#<%=btncrsearch2.ClientID %>').hide();
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

        function ShowUCRPopup(vin, year, make, model, body, price, mileage, extcol, intcol, crstatus, preinv_crid, preinvid) {

            //console.log(crstatus);
            var url = '<%=UCRlinkUrl %>';
            //console.log(vin + '|' + year + '|' + make + '|' + model + '|' + body + '|' + price + '|' + mileage + '|' + extcol + '|' + intcol + '|' + crstatus + '|' + preinv_crid);
            $('#<%=hfPreInvID.ClientID %>').val(preinvid);

            $('#<%=hfvin.ClientID %>').val(vin);
            $('#<%=hfcryear.ClientID %>').val(year);
            $('#<%=hfcrmake.ClientID %>').val(make);
            $('#<%=hfcrmodel.ClientID %>').val(model);
            $('#<%=hfcrbody.ClientID %>').val(body);
            $('#<%=hfcrprice.ClientID %>').val(price);
            $('#<%=hfcrmileage.ClientID %>').val(mileage);
            $('#<%=hfcrextcol.ClientID %>').val(extcol);
            $('#<%=hfcrintcol.ClientID %>').val(intcol);

            $('#<%=lblucrheader.ClientID %>').text(' [' + vin + ' ' + year + ' ' + make + ' ' + model + ']');
            $('#<%=lblucrheader2.ClientID %>').text(' [' + vin + ' ' + year + ' ' + make + ' ' + model + ']');
            $('#<%=txtCRIdUrl2.ClientID %>').val('');
            $('#<%=rbtnListCR.ClientID %>').find("input[value='1']").attr("checked", "checked");
            $('#<%=rbtnlistchangecr.ClientID %>').find("input[value='1']").attr("checked", "checked");

            if (crstatus == "0" || crstatus == '') {
                HideLinkCRDIVOnPopUpOpen();
                ShowCRDialog(true);

            }
            else if (crstatus == "10" || crstatus == "20" || crstatus == "30") {
                HideChangeCROnPopupOpen();
                ShowChangeCRDialog(true);
                $('#<%=hlnkViewReport.ClientID %>').attr('href', url + preinv_crid);
            }

        }

        function ShowCRDialog(modal) {
            $("#overlay").show();
            $("#divCR").fadeIn(10);

            if (modal) {
                $("#overlay").unbind("click");
            }
            else {
                $("#overlay").click(function (e) {
                    HideCRDialog();
                });
            }
        }

        function HideCRDialog() {
            $("#overlay").hide();
            $("#divCR").fadeOut(10);
        }

        function ShowChangeCRDialog(modal) {
            $("#overlay").show();
            $("#divChangeCR").fadeIn(10);

            if (modal) {
                $("#overlay").unbind("click");
            }
            else {
                $("#overlay").click(function (e) {
                    HideChangeCRDialog();
                });
            }
        }

        function HideChangeCRDialog() {
            $("#overlay").hide();
            $("#divChangeCR").fadeOut(10);
        }

        //Remove Last td of grvPreInv
        $(window).load(function () {
            RemoveLasttd();

        });

        $(document).ready(function () {
            RemoveLasttd();

        });

        function RemoveLasttd() {
            $('#<%=grvPreInv.ClientID %>').find('tr').each(function () {
                $(this).find('td:eq(13)').remove();
                $(this).find('th:eq(13)').remove();
            });
        }

        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            RemoveLasttd();
        }); 
    </script>
    <script type="text/javascript" language="javascript">
        function ShowDetails(ctrl) {
            var btnId = ctrl.id;
            var btnSplit = btnId.split('_');
            var row = 'ctl00_ContentPlaceHolder1_grvPreInv_' + btnSplit[3] + '_trnew';
            var row1 = 'ctl00_ContentPlaceHolder1_grvPreInv_' + btnSplit[3] + '_trnew1';
            var rowId = document.getElementById(row);
            var rowId1 = document.getElementById(row1);
            var btnName = document.getElementById(btnId);
            if (endsWith(btnName.src, 'expand.png')) {
                rowId.style.display = '';
                rowId1.style.display = '';
                btnName.title = "Collapse";
                btnName.src = "../Images/collapse.png";
            }
            else {
                rowId.style.display = 'none';
                rowId1.style.display = 'none';
                btnName.title = "Expand";
                btnName.src = "../Images/expand.png";
            }
            return false;
        }
        function endsWith(str, suffix) {
            return str.indexOf(suffix, str.length - suffix.length) !== -1;
        }
    </script>
</asp:content>

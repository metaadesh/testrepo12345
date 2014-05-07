<%@ Page Title="" Language="C#" 
    AutoEventWireup="true" CodeBehind="UCRLogListing.aspx.cs" Inherits="METAOPTION.UI.UCRLogListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div onmousemove="SetProgressPosition(event)">
        <asp:HiddenField ID="hfPreInvID" runat="server" Value="0" />
        <asp:UpdatePanel ID="upResultGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        UCR Listing</div>
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
                                    Lane#
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtLaneNo" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Run#
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtRunNo" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
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
                                    <asp:DropDownList ID="ddlMake" runat="server" CssClass="txt2" />
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
                                    CR#
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCRID" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    InventoryId
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtInventoryId" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Lagacy InventoryId
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtLegInventoryId" runat="server" MaxLength="17" CssClass="txt2"
                                        Height="16px" />
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
                                    Added Date
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
                                    UCR Linked
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlUCRLinked" runat="server" CssClass="txt2">
                                        <asp:ListItem Value="-1">ALL</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
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
                                <%= gvUCRListing.PageCount%>
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
                        <asp:GridView ID="gvUCRListing" runat="server" DataKeyNames="UCRID" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                            RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                            PageSize="50" AllowSorting="true" OnSorting="gvUCRListing_Sorting" CssClass="Grid"
                            OnRowDataBound="gvUCRListing_RowDataBound">
                            <Columns>
                                <%-- <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnUpdateHistory" runat="server" ImageUrl="~/Images/H_add.png"
                                            ToolTip="Show Update History" OnClick="btnUpdateHistory_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-CssClass="GridContent" HeaderText="CR#">
                                    <ItemTemplate>
                                        <div style="padding-top: 10px;">
                                            <%#Eval("CR_ID")%>
                                        </div>
                                        <div style="float: right; text-align: right; height: 10px">
                                            <asp:ImageButton ID="btnUpdateHis" runat="server" ImageUrl="~/Images/expand.png"
                                                ToolTip="Show Update History" OnClick="btnUpdateHis_Click" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:BoundField DataField="CR_ID" HeaderText="CR#" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Width="20px" />--%>
                                <asp:TemplateField HeaderText="VIN#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <div style="text-transform: uppercase; padding-top: 12px;">
                                            <asp:Label ID="lblvin" runat="server" Text='<%# Eval("VIN") %>' CssClass="Tooltip"></asp:Label>
                                            <asp:HyperLink ID="hlnkVIN" NavigateUrl='<%# "InventoryExpense.aspx?Code="+Eval("InventoryId")%>'
                                                runat="server" Text='<%#Eval("VIN") %>' Visible="false" /><br />
                                            &nbsp;<asp:Label ID="lblCode" runat="server" Text="INVID : " Visible="false" />
                                            <asp:HyperLink ID="hlnkCode" runat="server" Visible="false" />
                                            <asp:HiddenField ID="hfInvID" Value='<%# Eval("InventoryId") %>' runat="server" />
                                            <asp:HiddenField ID="hdnCRId" Value='<%# Eval("CR_ID") %>' runat="server" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:TemplateField HeaderText="Make<br />Model<br />Body" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="GridContent" ItemStyle-Width="120px"
                                    HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <%#Eval("Make") %><br />
                                        <%#Eval("Model") %><br />
                                        <%#Eval("Body") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MarketPrice" HeaderText="Market Price($)" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-Width="20px" DataFormatString="{0:#,###}" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Mileage" HeaderText="Mileage" ItemStyle-CssClass="GridContentNumbers"
                                    DataFormatString="{0:#,###}" HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="true"
                                    SortExpression="UCR.FromDate" />
                                <asp:TemplateField HeaderText="CarFax<br />AutoCheck" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <%#Eval("CarFax")%><br />
                                        <%#Eval("AutoCheck")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lane#<br />Run#" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContentNumbers">
                                    <ItemTemplate>
                                        <%#Eval("LaneNumber")%><br />
                                        <%#Eval("RunNumber")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--   <asp:BoundField DataField="LegacyInventoryID" HeaderText="Legacy InventoryID" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-Width="10px" ItemStyle-Width="10px" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-Wrap="true" />--%>
                                <asp:TemplateField HeaderText="DateAdded<br />AddedBy" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="GridContent" ItemStyle-Width="150px"
                                    HeaderStyle-Width="150px">
                                    <ItemTemplate>
                                        <%#Eval("CRDateAdded")%><br />
                                        <%#Eval("CRAddedBy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Modified<br />ModifiedBy" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="GridContent" ItemStyle-Width="100px"
                                    HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%#Eval("CRModifiedDate")%><br />
                                        <%#Eval("CRModifiedBy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="InventoryId" HeaderText="InventoryId" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Width="30px" />--%>
                                <asp:BoundField DataField="CR_StatusDesc" HeaderText="CRStatus" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <%--<asp:BoundField DataField="Auctioncode" HeaderText="Auction Code" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />--%>
                                <asp:BoundField DataField="AuctionStatusText" HeaderText="Auction Status" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-Width="30px" ItemStyle-Width="30px" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                                <asp:BoundField DataField="SaleDate" HeaderText="Sale Date" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                                <asp:TemplateField HeaderText="Ext / Int Color" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="GridContent" ItemStyle-Width="30px"
                                    HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <%#Eval("ExteriorColor")%><br />
                                        <%#Eval("InteriorColor")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-Width="90px" ItemStyle-CssClass="GridContentCentre"
                                    HeaderText="Action" HeaderStyle-Width="90px" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnURL" runat="server" Value=' <%# Eval("CR_URL")%>' />
                                        <asp:HiddenField ID="hdnImageCount" runat="server" Value=' <%# Eval("RowCount")%>' />
                                        <asp:HiddenField ID="hdnUCRDetails" runat="server" Value=' <%# Eval("CR_Status")%>' />
                                        <%-- <asp:ImageButton ID="btnUCRDetail" runat="server" OnClick="btnUCRDetail_Click" ImageUrl="~/Images/ucr-btn2.png" />--%>
                                        <a id="ibtnCRAvailable" runat="server" target="_blank" href="#" visible="false" style="text-decoration: none;">
                                            <img id="imgAvailabel" runat="server" border="0" alt="No Image" src="../Images/ucr-btn.png"
                                                style="padding-bottom: 4px" />
                                        </a>
                                        <asp:ImageButton ID="btnAudioVideo" runat="server" OnClick="ibtncars_Click" ImageUrl="~/Images/Camera.jpg"
                                            ToolTip="AudioVideoImage Display" />
                                        <asp:ImageButton ID="btnUCRResponseDetails" runat="server" CommandArgument='<%#Eval("UCRID") %>'
                                            OnClick="btnUCRResponseDetails_Click" Style="padding-bottom: 7px" ToolTip="UCR Response Details"
                                            ImageUrl="../Images/XML.jpg" />
                                        <a id="btnUpdateHistory" runat="server" title="Show All Update History" target="_blank"
                                            href="#" visible="false" style="text-decoration: none;">
                                            <img id="imgUpdateHistory" runat="server" border="0" src="~/Images/Select1.png" alt="No Image"
                                                style="padding-bottom: 5px" /><%-- OnClick="btnUpdateHistory_Click"--%>
                                        </a>
                                        <asp:HiddenField ID="hdnUCRUpdateCount" Value='<%# Eval("UCRUpdateCount") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr id="trnew" runat="server" visible="false">
                                            <td colspan="16" class="GridContent" style="padding: 5px 5px 5px 30px;">
                                                <asp:GridView ID="gvUpdateHistory" runat="server" Width="75%" EmptyDataText="No record found."
                                                    EmptyDataRowStyle-CssClass="GridEmptyRow" RowStyle-CssClass="gvRow" ItemStyle-CssClass="GridContent"
                                                    HeaderStyle-CssClass="GridHeader" GridLines="none">
                                                    <EmptyDataRowStyle CssClass="gvEmpty" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
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
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                                Page
                                <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                                    AutoPostBack="true" />
                                of
                                <%= gvUCRListing.PageCount%>
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
                <asp:HiddenField ID="hfPopup" runat="server" />
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfPopup"
                    PopupControlID="panOpen" BackgroundCssClass="modalBackground" CancelControlID="imgCancel"
                    PopupDragHandleControlID="panOpen" BehaviorID="ImageModelPopup" />
                <asp:Panel ID="panOpen" runat="server" Height="620px" Width="840px" CssClass="imagevideobox">
                    <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
                        width: 662px; display: none">
                        <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" />
                    </div>
                    <iframe id="ifrmSlideShow" runat="server" scrolling="no" style="height: 620px; width: 840px;"
                        frameborder="0"></iframe>
                </asp:Panel>
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

       
    </script>
    <style type="text/css">
        .imagevideobox
        {
            background-color: #CECECE;
            background-image: -moz-linear-gradient(center top , #CECECE, #EEEEEE);
            border-radius: 15px 15px 15px 15px;
            font-size: 12px;
            min-height: 23px;
            padding: 10px;
        }
        .gridCSS
        {
            padding: 4px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function HideModelpopup() {
            $find('ImageModelPopup').hide();
            return false;
        }
        function ShowResponseXML() {
            window.open("../UI/UCRResponseData.xml", "mywindow", "location=1,status=1,scrollbars=1, width=745,height=475,left=212,top=100");
        }
    </script>
</asp:Content>

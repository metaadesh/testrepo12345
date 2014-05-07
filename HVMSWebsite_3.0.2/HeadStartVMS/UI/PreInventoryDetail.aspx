<%@ Page Title="HeadStartVMS" Language="C#" AutoEventWireup="true" CodeBehind="PreInventoryDetail.aspx.cs"
    Inherits="METAOPTION.UI.PreInventoryDetail" %>

<%@ MasterType VirtualPath="~/UI/PreInventoryMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upnlSearch" runat="server">
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
                <asp:HiddenField ID="hfCRId" runat="server" />
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td nowrap="nowrap">
                            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn" Style="padding: 3px 4px;
                                text-decoration: none" Text="<< Back" NavigateUrl="~/UI/PreInventory.aspx" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCreateUcr" class="Btn_Form" Text="Create/Link UCR" runat="server"
                                OnClick="btnCreateUcr_Click" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnChangeUcr" class="Btn_Form" Text="Change UCR" runat="server" OnClick="btnChangeUcr_Click" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnViewUCR" class="Btn_Form" Text="View UCR" runat="server" OnClick="btnViewUCR_Click" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnAdd" class="Btn_Form" Text="Add to Inventory" runat="server" OnClick="btnAdd_Click" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnDeleteInventory" class="Btn_Form" Text="Discard Pre-Inventory"
                                runat="server" OnClick="btnDeleteInventory_Click" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnPending" class="Btn_Form" Text="Make Pending" runat="server" OnClick="btnPending_Click" />
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
                    </tr>
                </table>
                <table id="" border="0" width="100%" cellpadding="0" style="border-collapse: collapse;
                    margin-top: 10px;">
                    <tr>
                        <td style="text-align: left; padding: 10px 10px 10px 0px; font-size: 12px; font-weight: bold">
                            <asp:HiddenField ID="hfvin" runat="server" />
                            <asp:HiddenField ID="hfpending" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="dvCarDetails" runat="server" class="PreTableHeadingBg">
                                <%--TableHeadingBg--%>
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="pre-TableHeading">
                                            <asp:Label ID="lblCarStatus" runat="server" Text=""></asp:Label>
                                            <a href="" class="Tooltip" runat="server" id="btnDiscardedInfo" title="">
                                                <img src="../Images/DeleteButton1.png" alt="" />
                                            </a>
                                        </td>
                                        <td class="pre-TableHeading" style="text-align: right; padding-right: 10px;">
                                            <asp:Label ID="lblCRStatus" runat="server" Text="" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Repeater ID="rptdetail" runat="server" OnItemDataBound="rptdetail_ItemDataBound">
                                <ItemTemplate>
                                    <table width="100%" style="border-collapse: collapse">
                                        <tr>
                                            <td class="PreTableBorderB">
                                                VIN
                                            </td>
                                            <td id="Td1" class="PreTableBorder" runat="server">
                                                <asp:Label ID="lblvin" runat="server" Text='<%# Eval("VIN") %>' CssClass="Tooltip"></asp:Label>
                                                <asp:HyperLink ID="hlnkVIN" NavigateUrl='<%# "InventoryDetail.aspx?Mode=View&Code="+Eval("InvID") %>'
                                                    runat="server" Text='<%#Eval("VIN") %>' Visible="false" />
                                            </td>
                                            <td class="PreTableBorderB">
                                                Mileage In
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# String.Format("{0:#,###}",Eval("Mileage")) %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Year
                                            </td>
                                            <td class="PreTableBorder">
                                                <font color="red"><b>
                                                    <%# Eval("Year") %></b></font>
                                            </td>
                                            <td class="PreTableBorderB">
                                                Price($)
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# String.Format("{0:#,###}", Eval("Price"))%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Make
                                            </td>
                                            <td class="PreTableBorder">
                                                <font color="red"><b>
                                                    <%# Eval("Make") %></b></font>
                                            </td>
                                            <td class="PreTableBorderB">
                                                Interior Color
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# Eval("IntColor") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Model
                                            </td>
                                            <td class="PreTableBorder">
                                                <font color="red"><b>
                                                    <%# Eval("Model") %></b></font>
                                            </td>
                                            <td class="PreTableBorderB">
                                                Exterior Color
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# Eval("ExtColor") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Body
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# Eval("Body") %>
                                            </td>
                                            <td class="PreTableBorderB">
                                                Car Location
                                            </td>
                                            <td class="PreTableBorder">
                                                <asp:Label ID="lbllocdesc" CssClass="Tooltip" runat="server" Text='<%# Eval("LocationCode")%>'
                                                    ToolTip='<%# Eval("LocationDesc") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Comments
                                            </td>
                                            <td class="PreTableBorder" colspan="3">
                                                <%# Eval("Comments") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Category Flag Color
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# Eval("CategoryColor")%>
                                                <asp:ImageButton ID="imgCatflag" runat="server" ImageUrl='<%# String.Format("~/Images/{0}",Eval("ColorIcon")) %>'
                                                    AlternateText="[X]" ToolTip='<%# Eval("CategoryColor") %>' Style="vertical-align: top;" />
                                                <asp:HiddenField ID="hfcategoryid" runat="server" Value='<%# Eval("CategoryColorID") %>' />
                                            </td>
                                            <td class="PreTableBorderB">
                                                Current System
                                            </td>
                                            <td class="PreTableBorder" colspan="3">
                                                HVMS
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Market Price($)
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# String.Format("{0:#,###}", Eval("MarketPrice"))%>
                                            </td>
                                            <td class="PreTableBorderB">
                                                Grade
                                            </td>
                                            <td class="PreTableBorder" colspan="3">
                                                <%# Eval("Grade") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Bad CarFax
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# Eval("BadCarFax")%>
                                            </td>
                                            <td class="PreTableBorderB">
                                                Bad AutoCheck
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# Eval("BadAutoCheck")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Engine
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# Eval("EngineType")%>
                                            </td>
                                            <td class="PreTableBorderB">
                                                Trans
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# Eval("TransType")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PreTableBorderB">
                                                Wheel Drive
                                            </td>
                                            <td class="PreTableBorder">
                                                <%# Eval("WheelDrive")%>
                                            </td>
                                            <td class="PreTableBorderB" colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hfVIN2" runat="server" Value='<%# Eval("VIN") %>' />
                                    <asp:HiddenField ID="hfyear" runat="server" Value='<%# Eval("Year") %>' />
                                    <asp:HiddenField ID="hfmake" runat="server" Value='<%# Eval("Make") %>' />
                                    <asp:HiddenField ID="hfmodel" runat="server" Value='<%# Eval("Model") %>' />
                                    <asp:HiddenField ID="hfbody" runat="server" Value='<%# Eval("Body") %>' />
                                    <asp:HiddenField ID="hfprice" runat="server" Value='<%# Eval("Price") %>' />
                                    <asp:HiddenField ID="hfmileage" runat="server" Value='<%# Eval("Mileage") %>' />
                                    <asp:HiddenField ID="hfextcol" runat="server" Value='<%# Eval("ExtColor") %>' />
                                    <asp:HiddenField ID="hfintcol" runat="server" Value='<%# Eval("IntColor") %>' />
                                    <asp:HiddenField ID="hfInventID" runat="server" Value='<%# Eval("InvID") %>' />
                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;
                                        margin-top: 10px">
                                        <tr>
                                            <td colspan="2">
                                                <div id="dvCarProp" runat="server" class="PreTableHeadingBg">
                                                    <%--TableHeadingBg--%>
                                                    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                                        <tr>
                                                            <td class="pre-TableHeading">
                                                                Car Properties
                                                            </td>
                                                            <td align="right" class="Tooltip">
                                                                <%--<asp:ImageButton ID="imgbtnEditCarProp" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <table width="100%" style="border-collapse: collapse">
                                                    <tr>
                                                        <td class="PreTableBorderB">
                                                            AC
                                                        </td>
                                                        <td class="PreTableBorder">
                                                            <%#Eval("AC")%>
                                                        </td>
                                                        <td class="PreTableBorderB">
                                                            Sun Roof
                                                        </td>
                                                        <td class="PreTableBorder">
                                                            <%#Eval("SunRoof")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="PreTableBorderB">
                                                            Power Locks
                                                        </td>
                                                        <td class="PreTableBorder">
                                                            <%#Eval("PowerLocks")%>
                                                        </td>
                                                        <td class="PreTableBorderB">
                                                            Leather
                                                        </td>
                                                        <td class="PreTableBorder">
                                                            <%#Eval("Leather")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="PreTableBorderB">
                                                            Power Windows
                                                        </td>
                                                        <td class="PreTableBorder">
                                                            <%#Eval("PowerWindows")%>
                                                        </td>
                                                        <td class="PreTableBorderB">
                                                            Navigation
                                                        </td>
                                                        <td class="PreTableBorder">
                                                            <%#Eval("Navigation")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="dvDealerDet" runat="server" class="PreTableHeadingBg" style="margin-top: 10px">
                                        <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                            <tr>
                                                <td class="pre-TableHeading">
                                                    Buyer & Dealer Details
                                                </td>
                                                <td align="right" class="Tooltip">
                                                    <%--<asp:ImageButton ID="btnEditDealerDetails" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />--%>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                            <tr>
                                                <td class="PreTableBorderB">
                                                    Buyer Name
                                                </td>
                                                <td class="PreTableBorder" colspan="3">
                                                    <%# FormatBuyer(Eval("Buyer")) %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="PreTableBorderB">
                                                    Buyer Note&nbsp;&nbsp;
                                                    <asp:ImageButton ID="ibtnEmailStatus" runat="server" ImageUrl="~/Images/Email_Sent.png"
                                                        OnClick="ibtnEmailStatus_Click" />
                                                    <asp:HiddenField ID="hfIsEmailSent" runat="server" Value='<%# Eval("IsEmailSent") %>' />
                                                </td>
                                                <td class="PreTableBorder" colspan="3">
                                                    <%# Eval("BuyerNote") %>
                                                    <asp:HiddenField ID="hfBuyerNote" runat="server" Value='<%# Eval("BuyerNote") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="PreTableBorderB">
                                                    Street
                                                </td>
                                                <td class="PreTableBorder">
                                                    <%# Eval("Street")%>
                                                </td>
                                                <td class="PreTableBorderB" style="display: none;">
                                                    Email 1
                                                </td>
                                                <td class="PreTableBorder">
                                                    <%--<%# Eval("Email1")%>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="PreTableBorderB">
                                                    City
                                                </td>
                                                <td class="PreTableBorder">
                                                    <%# Eval("City") %>
                                                </td>
                                                <td class="PreTableBorderB" style="display: none;">
                                                    Email 2
                                                </td>
                                                <td class="PreTableBorder">
                                                    <%--<%# Eval("Email2")%>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="PreTableBorderB">
                                                    Zip
                                                </td>
                                                <td class="PreTableBorder">
                                                    <%# Eval("Zip") %>
                                                </td>
                                                <td class="PreTableBorderB" style="display: none;">
                                                    Phone 1
                                                </td>
                                                <td class="PreTableBorder">
                                                    <%--<%# Eval("Phone1")%>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="PreTableBorderB">
                                                    State
                                                </td>
                                                <td class="PreTableBorder">
                                                    <%# Eval("StateCode")%>
                                                </td>
                                                <td class="PreTableBorderB" style="display: none;">
                                                    Phone 2
                                                </td>
                                                <td class="PreTableBorder">
                                                    <%--<%# Eval("Phone2")%>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="PreTableBorderB">
                                                    Dealer Name & Address
                                                </td>
                                                <td class="PreTableBorder" colspan="3">
                                                    <%--<%# Eval("Dealer") %>--%>
                                                    <%--<a href="" class="Tooltip" runat="server" id="btnDealerInfo" title="">
                                            <%# Eval("Dealer") %>
                                        </a>--%>
                                                    <asp:HyperLink ID="hlinkDealerInfo" CssClass="Tooltip" runat="server" Style="text-decoration: underline;
                                                        cursor: pointer; color: Red;" NavigateUrl="" Target="_blank"></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="Div1" runat="server" class="PreTableHeadingBg" style="margin-top: 10px">
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="pre-TableHeading">
                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                            Linked Cars (Pre-Inventory)
                                        </td>
                                        <td align="right" class="Tooltip">
                                            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 1px solid #C0DCEE;">
                                <asp:GridView runat="server" Width="100%" ID="gvLinkedFilter" BorderWidth="0" AllowPaging="True"
                                    DataKeyNames="PreInventoryId" PageSize="5" AutoGenerateColumns="False" EmptyDataText="No Rows found"
                                    OnPageIndexChanging="gvLinkedFilter_PageIndexChanging" OnRowDataBound="gvLinkedFilter_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="SyncDate" SortExpression="SyncDate" HeaderText="SyncDate"
                                            DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-Width="110px" HeaderStyle-Width="110px" ItemStyle-CssClass="pre-GridContent">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ScanDate" SortExpression="ScanDate" HeaderText="ScanDate"
                                            DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-Width="110px" ItemStyle-CssClass="pre-GridContent" HeaderStyle-Width="110px">
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Linked Cars" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-CssClass="pre-GridContent" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnk" runat="server" Text='<%#Eval("VIN")%>' NavigateUrl='<%#"~/UI/PreInventoryDetail.aspx?Code="+Eval("PreInventoryId")%>'></asp:HyperLink>
                                                <div style="vertical-align: bottom; float: right;">
                                                    <asp:ImageButton ID="imgCatflag" runat="server" ImageUrl='<%# String.Format("~/Images/{0}",Eval("ColorIcon")) %>'
                                                        AlternateText="[X]" ToolTip='<%# Eval("CategoryColor") %>' /></div>
                                                <asp:HiddenField ID="hfcategoryid" runat="server" Value='<%# Eval("CategoryColorID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Device<br/>Info" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-CssClass="pre-GridContent" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <%# Eval("DeviceName")%>
                                                <br />
                                                <asp:Label ID="lbldeviceid" runat="server" Text='<%# FormatDeviceID(Eval("DeviceID")) %>'
                                                    ToolTip='<%# Eval("DeviceID") %>'></asp:Label>
                                                <asp:HiddenField ID="hfInvID" runat="server" Value='<%# Eval("InventoryID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ScanBy" SortExpression="ScanBy" HeaderText="Scan By" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-CssClass="pre-GridContent" ItemStyle-Wrap="true" HeaderStyle-Width="90px">
                                        </asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                                    <HeaderStyle CssClass="PreTableBorderB" />
                                    <RowStyle CssClass="gvRow" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <PagerStyle CssClass="PreFooterContentDetails" HorizontalAlign="Right" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="Div2" runat="server" class="PreTableHeadingBg" style="margin-top: 10px">
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="pre-TableHeading">
                                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                            Linked Cars (Inventory)
                                        </td>
                                        <td align="right" class="Tooltip">
                                            <%--<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 1px solid #C0DCEE;">
                                <asp:GridView runat="server" Width="100%" ID="grdlinkedcarInv" BorderWidth="0" AllowPaging="True"
                                    DataKeyNames="InventoryId" PageSize="5" AutoGenerateColumns="False" EmptyDataText="No Rows found"
                                    OnPageIndexChanging="grdlinkedcarInv_PageIndexChanging" OnRowDataBound="grdlinkedcarInv_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="AddedDate" SortExpression="AddedDate" HeaderText="AddedDate"
                                            DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-Width="90px" HeaderStyle-Width="90px" ItemStyle-CssClass="pre-GridContent">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SyncDate" SortExpression="SyncDate" HeaderText="SyncDate"
                                            DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-Width="90px" HeaderStyle-Width="90px" ItemStyle-CssClass="pre-GridContent">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ScanDate" SortExpression="ScanDate" HeaderText="ScanDate"
                                            DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-Width="90px" ItemStyle-CssClass="pre-GridContent" HeaderStyle-Width="90px">
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Linked Cars" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-CssClass="pre-GridContent" ItemStyle-Width="120px" HeaderStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnk" runat="server" Text='<%#Eval("VIN")%>' NavigateUrl='<%#"~/UI/InventoryDetail.aspx?Code="+Eval("InventoryId")%>'></asp:HyperLink>
                                                <div style="vertical-align: bottom; float: right;">
                                                    <asp:ImageButton ID="imgCatflag" runat="server" ImageUrl='<%# String.Format("~/Images/{0}",Eval("ColorIcon")) %>'
                                                        AlternateText="[X]" ToolTip='<%# Eval("CategoryColor") %>' /></div>
                                                <asp:HiddenField ID="hfcategoryid" runat="server" Value='<%# Eval("CategoryColorID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DeviceName<br/>DeviceID" HeaderStyle-CssClass="pre-GridHeader"
                                            ItemStyle-CssClass="pre-GridContent" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <%# Eval("DeviceName")%>
                                                <br />
                                                <asp:Label ID="lbldeviceid" runat="server" Text='<%# FormatDeviceID(Eval("DeviceID")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AddedBy" SortExpression="AddedBy" HeaderText="AddedBy"
                                            HeaderStyle-CssClass="pre-GridHeader" ItemStyle-CssClass="pre-GridContent" ItemStyle-Wrap="true"
                                            HeaderStyle-Width="90px"></asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                                    <HeaderStyle CssClass="PreTableBorderB" />
                                    <RowStyle CssClass="gvRow" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <PagerStyle CssClass="PreFooterContentDetails" HorizontalAlign="Right" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
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
                                    <asp:ListItem Text="Reject this Pre-Inventory,Please specify the reason also<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<i>Once rejected, it will appear in the list, but you cannot perform any action</i>)"
                                        Value="R" class="LeftPanelContentHeading"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trReason" runat="server" visible="true">
                            <td style="padding-top: 10px;">
                                <span class="LeftPanelContentHeading" style="vertical-align: top;">Reason</span>
                                <asp:TextBox ID="txtreason" TextMode="MultiLine" runat="server" Width="300" Height="100"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" />
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajax:ModalPopupExtender ID="ModelPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="MBILinkedCars" TargetControlID="btnDeleteInventory" PopupControlID="pnlDelete"
                    CancelControlID="CancelButton" OkControlID="OkButton">
                </ajax:ModalPopupExtender>
                <asp:Panel ID="panOpen" runat="server" Height="600px" Width="700px" CssClass="ModalWindow">
                    <iframe id="frame1" runat="server" scrolling="no" style="height: 600px; width: 700px;"
                        frameborder="0"></iframe>
                </asp:Panel>
                <asp:HiddenField ID="hfucr" runat="server" />
                <%--UCR BEGIN--%>
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
                            <asp:Button ID="btnCRok" runat="server" Text="OK" OnClientClick="OpenCreateUCRLink();"
                                CssClass="btn" Width="80px" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCRcancel" runat="server" Text="Cancel" CssClass="btn" Width="80px"
                                OnClick="btnCRcancel_Click" />
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfCR" runat="server" />
                <ajax:ModalPopupExtender ID="mpeCR" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="mpecrbeh" TargetControlID="hfCR" PopupControlID="dvUCR" CancelControlID="imgCloseCR"
                    OkControlID="btnCRok">
                </ajax:ModalPopupExtender>
                <%--UCR END--%>
                <%--Change UCR Begin--%>
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
                <%--Change UCR End--%>
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
                <%------------------------Email Detail popup begin------------------------%>
                <asp:HiddenField ID="hfPopup" runat="server" />
                <ajax:ModalPopupExtender ID="mpeEmailDetail" runat="server" TargetControlID="hfPopup"
                    PopupControlID="pnlEmailDetail" BackgroundCssClass="modalBackground" CancelControlID="imgCloseEmailDetail"
                    PopupDragHandleControlID="pnlEmailDetail" />
                <div id="pnlEmailDetail" runat="server" style="height: 600px; width: 800px" class="ModalWindow">
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
                                <iframe id="IfrmEmailDetail" runat="server" style="width: 800px; height: 600px; overflow: auto;
                                    border: solid 1px #115481; background-color: #fff" frameborder="0"></iframe>
                            </td>
                        </tr>
                    </table>
                </div>
                <%------------------------Email Detail popup End------------------------%>
            </ContentTemplate>
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

        }

        function fnClickUpdate(sender, e) {
            __doPostBack(sender, e);
        }

        $(function () {
            $(".Tooltip").tipTip({ maxWidth: "auto", edgeOffset: 10 });
        });

        $(document).ready(function () {
            $('#<%=CancelButton.ClientID %>').click(function () {
                $('#<%=txtreason.ClientID %>').val('');
            });
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
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageInventoryStats.ascx.cs"
    Inherits="METAOPTION.UserControls.ManageInventoryStats" %>
<%@ Register Src="Scanner.ascx" TagName="Scanner" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:HiddenField ID="hfvin" runat="server" />
<asp:HiddenField ID="hfInventoryID" runat="server" />
<asp:HiddenField ID="hfcryear" runat="server" />
<asp:HiddenField ID="hfcrmake" runat="server" />
<asp:HiddenField ID="hfcrmodel" runat="server" />
<asp:HiddenField ID="hfcrbody" runat="server" />
<asp:HiddenField ID="hfcrprice" runat="server" />
<asp:HiddenField ID="hfcrmileage" runat="server" />
<asp:HiddenField ID="hfcrextcol" runat="server" />
<asp:HiddenField ID="hfcrintcol" runat="server" />
<div style="width: 100%; height: auto">
    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
        <%----------UCR BOX BEGIN----------%>
        <tr>
            <td class="LeftPanelTable" colspan="2">
                <img border="0" src="../images/UCR.gif" width="100%" height="28" alt="" />
            </td>
        </tr>
        <tr>
            <td class="LeftPanelTable" style="padding: 10px" colspan="2" align="left">
                <table border="0" width="210" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="left" colspan="2" class="OrangeText_LeftPanel">
                            <div class="OrangeText_LeftPanel">
                                UCR ID :
                                <asp:Label ID="lblCRId" runat="server" />
                            </div>
                            <div class="OrangeText_LeftPanel">
                                Status :
                                <asp:ImageButton ID="ibtnCR" runat="server" OnClick="ibtnCR_Click" />
                                <asp:HyperLink ID="ancrCR" runat="server" Target="_blank" Visible="false">
                                    <asp:Image ID="imggCR" runat="server" />
                                </asp:HyperLink>
                            </div>
                            <div class="OrangeText_LeftPanel">
                                URL :
                                <asp:Label ID="lblUCRUrl" runat="server" Text="Not Available" />
                                <a id="ancUCRUrl" runat="server" target="_blank">
                                    <img src="../Images/ucr-btn.png" alt="UCR" />
                                </a>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%----------UCR BOX END----------%>
    <%----------EXPENSE BOX BEGIN----------%>
    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse;
        margin-top: 5px">
        <tr>
            <td class="LeftPanelTable" colspan="2">
                <img border="0" src="../images/Expenses.gif" width="100%" height="28" alt="" />
            </td>
        </tr>
        <tr>
            <td class="LeftPanelTable" style="padding: 10px" colspan="2" align="left">
                <table border="0" width="210" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="left" colspan="2" class="OrangeText_LeftPanel">
                            <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false" GridLines="None"
                                ShowHeader="false">
                                <Columns>
                                    <asp:BoundField DataField="ExpenseType" ItemStyle-Width="100px" ItemStyle-CssClass="OrangeText_LeftPanel" />
                                    <asp:BoundField DataField="ExpenseAmount" ItemStyle-Width="100px" DataFormatString="${0:#,###}"
                                        ItemStyle-CssClass="OrangeText_LeftPanel" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="LeftPanelFooter" align="right" colspan="2">
                <asp:HyperLink ID="hplViewExpenseDetails" runat="server" Target="_blank" CssClass="BlackTxt_Link">View Details</asp:HyperLink>
            </td>
        </tr>
    </table>
    <%----------EXPENSE BOX END----------%>
    <%----------LOCATION BOX BEGIN----------%>
    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse;
        margin-top: 5px">
        <tr>
            <td class="LeftPanelTable" colspan="2">
                <img border="0" src="../images/LocationMap.gif" width="100%" height="28" alt="" />
            </td>
        </tr>
        <tr>
            <td class="LeftPanelTable" style="padding: 10px" colspan="2" align="left">
                <table border="0" width="210" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="left" colspan="2" class="OrangeText_LeftPanel">
                            <asp:GridView ID="gvLocation" runat="server" AutoGenerateColumns="false" GridLines="None"
                                EmptyDataText="No Record Found" Width="100%" HeaderStyle-HorizontalAlign="Left">
                                <Columns>
                                    <asp:BoundField HeaderText="Location" DataField="Location" ItemStyle-Width="40%"
                                        ItemStyle-CssClass="OrangeText_InvDtlMenu" ItemStyle-VerticalAlign="Top" />
                                    <asp:BoundField HeaderText="Added On" DataField="DateAdded" ItemStyle-Width="60%"
                                        ItemStyle-CssClass="OrangeText_InvDtlMenu" ItemStyle-VerticalAlign="Top" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trLocationDetailsFooter" runat="server">
            <td class="LeftPanelFooter" align="right" colspan="2">
                <asp:LinkButton ID="btnLocationDetails" runat="server" Text="View Details" OnClick="btnLocationDetails_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <%----------LOCATION BOX END----------%>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <%----------Inventory Images Box Begin----------%>
            <%----------Inventory Images Box Begin----------%>
            <div id="divInventory" runat="server" style="width: 100%; float: left; margin-top: 5px">
                <table id="InventoryHeader" runat="server" onclick="HideShow(this)" border="0" cellpadding="0"
                    width="100%" style="border-collapse: collapse; width: 100%; height: 26px; padding-top: 0px;
                    cursor: pointer">
                    <tr>
                        <td class="LeftPanelTable" colspan="2">
                            <img id="imgInventory" runat="server" border="0" src="../images/inventory.gif" height="28"
                                alt="" style="width: 100%" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="tblInvImages" runat="server">
                    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                        <tr>
                            <td class="LeftPanelTable" style="padding: 10px" colspan="2" align="left">
                                <table border="0" width="210" cellpadding="0" style="border-collapse: collapse">
                                    <tr>
                                        <td align="left" colspan="2" class="OrangeText_LeftPanel">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblCount" runat="server" Style="font-weight: bold;" />
                                                    <div id="dvImagePaging" runat="server" style="text-align: center; font-weight: bold;
                                                        padding-top: 10px; clear: both">
                                                        <asp:LinkButton CssClass="Paging" OnClick="lnkFirst_Click" ID="lnkFirst" runat="server"
                                                            Text="<<" />
                                                        |
                                                        <asp:LinkButton ID="lnkPrev" OnClick="lnkPrev_Click" CssClass="Paging" runat="server"
                                                            Text="<" />
                                                        |
                                                        <asp:Label CssClass="Paging" ID="lblPageInfo" runat="server" />
                                                        <asp:LinkButton ID="lnkNext" OnClick="lnkNext_Click" CssClass="Paging" runat="server"
                                                            Text=">" />
                                                        |
                                                        <asp:LinkButton ID="lnkLast" OnClick="lnkLast_Click" runat="server" Text=">>" CssClass="Paging" />
                                                    </div>
                                                    <asp:DataList ID="rptImages" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
                                                        ItemStyle-VerticalAlign="Bottom">
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="padding: 3px">
                                                                        <asp:ImageButton ID="ibtnThumbImages" runat="server" ImageUrl='<%# GetImagePath(Eval("ThumbNailPath"))%>'
                                                                            AlternateText="Image" OnClick="ibtnThumbImages_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <%--Large image popup--%>
                                                    <asp:HiddenField ID="hfLargeImage" runat="server" Value='<%# Eval("ServerPath") %>' />
                                                    <ajax:ModalPopupExtender ID="mpeLargeImage" runat="server" TargetControlID="hfLargeImage"
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
                                                    <%--Large image popup--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <%----------Inventory Images Box End----------%>
            <%----------Expense Images Box End----------%>
            <div id="divExpense" runat="server" style="width: 100%; float: left; margin-top: 5px">
                <table id="PreHeader" runat="server" onclick="HideShow(this)" border="0" cellpadding="0"
                    width="100%" style="border-collapse: collapse; cursor: pointer">
                    <tr>
                        <td class="LeftPanelTable" colspan="2">
                            <img id="imgPreExpense" runat="server" border="0" src="../images/expense.gif" height="28"
                                alt="" style="width: 100%" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="tblExpImages" runat="server">
                    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                        <tr>
                            <td class="LeftPanelTable" style="padding: 10px" colspan="2" align="left">
                                <table border="0" width="210" cellpadding="0" style="border-collapse: collapse">
                                    <tr>
                                        <td align="left" colspan="2" class="OrangeText_LeftPanel">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblExpImgCount" runat="server" Style="font-weight: bold;" />
                                                    <div id="dvExpImagePaging" runat="server" style="text-align: center; font-weight: bold;
                                                        padding-top: 10px; clear: both">
                                                        <asp:LinkButton CssClass="Paging" OnClick="lnkExpFirst_Click" ID="lnkExpFirst" runat="server"
                                                            Text="<<" />
                                                        |
                                                        <asp:LinkButton ID="lnkExpPrev" OnClick="lnkExpPrev_Click" CssClass="Paging" runat="server"
                                                            Text="<" />
                                                        |
                                                        <asp:Label CssClass="Paging" ID="lblExpPageInfo" runat="server" />
                                                        <asp:LinkButton ID="lnkExpNext" OnClick="lnkExpNext_Click" CssClass="Paging" runat="server"
                                                            Text=">" />
                                                        |
                                                        <asp:LinkButton ID="lnkExpLast" OnClick="lnkExpLast_Click" runat="server" Text=">>"
                                                            CssClass="Paging" />
                                                    </div>
                                                    <asp:DataList ID="dlExpenseImages" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
                                                        ItemStyle-VerticalAlign="Bottom">
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="padding: 3px">
                                                                        <asp:ImageButton ID="ibtnExpThumbImages" runat="server" ImageUrl='<%# GetExpImagePath(Eval("ThumbNailPath"))%>'
                                                                            AlternateText="Image" OnClick="ibtnExpThumbImages_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <%--Large image popup--%>
                                                    <asp:HiddenField ID="hfLargeExpImage" runat="server" Value='<%# Eval("ServerPath") %>' />
                                                    <ajax:ModalPopupExtender ID="mpeLargeExpImage" runat="server" TargetControlID="hfLargeExpImage"
                                                        PopupControlID="panOpenExp" BackgroundCssClass="modalBackground" CancelControlID="imgCancelExp"
                                                        PopupDragHandleControlID="panOpen" />
                                                    <asp:Panel ID="panOpenExp" runat="server" Height="600px" Width="700px" CssClass="ModalWindow">
                                                        <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
                                                            width: 662px;">
                                                            <img src="../Images/close_icon.gif" alt="close" id="imgCancelExp" runat="server" />
                                                        </div>
                                                        <iframe id="IframeExp" runat="server" scrolling="no" style="height: 600px; width: 700px;"
                                                            frameborder="0"></iframe>
                                                        <%--<asp:Button ID="btnCancel" runat="server" Text="Close" />--%>
                                                    </asp:Panel>
                                                    <%--Large image popup--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <%----------Expense Images Box End----------%>
            <%----------Generic Images Box Start----------%>
            <div id="divGeneric" runat="server" style="width: 100%; float: left; margin-top: 5px;
                height: auto">
                <table id="GenericHeader" onclick="HideShow(this)" runat="server" border="0" cellpadding="0"
                    width="100%" style="border-collapse: collapse; cursor: pointer">
                    <tr>
                        <td class="LeftPanelTable" colspan="2">
                            <img id="imgGeneric" runat="server" border="0" src="../images/Generic.gif" height="28"
                                alt="" style="width: 100%" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="tblGenericImage" runat="server">
                    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse;
                        height: auto">
                        <tr>
                            <td class="LeftPanelTable" style="padding: 10px" colspan="2" align="left">
                                <table border="0" width="210" cellpadding="0" style="border-collapse: collapse">
                                    <tr>
                                        <td align="left" colspan="2" class="OrangeText_LeftPanel">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblGenImgCount" runat="server" Style="font-weight: bold;" /><br />
                                                    <asp:Label ID="lblDescription" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                                                    <div id="Div1" runat="server" style="text-align: center; font-weight: bold; padding-top: 10px;
                                                        clear: both">
                                                        <asp:LinkButton CssClass="Paging" OnClick="lnkGenericFirst_Click" ID="lnkGenFirst"
                                                            runat="server" Text="<<" />
                                                        |
                                                        <asp:LinkButton ID="lnkGenPrev" OnClick="lnkGenericPrev_Click" CssClass="Paging"
                                                            runat="server" Text="<" />
                                                        |
                                                        <asp:Label CssClass="Paging" ID="lblGenPageInfo" runat="server" />
                                                        <asp:LinkButton ID="lnkGenNext" OnClick="lnkGenericNext_Click" CssClass="Paging"
                                                            runat="server" Text=">" />
                                                        |
                                                        <asp:LinkButton ID="lnkGenLast" OnClick="lnkGenericLast_Click" runat="server" Text=">>"
                                                            CssClass="Paging" />
                                                    </div>
                                                    <asp:DataList ID="dlGenericImages" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
                                                        ItemStyle-VerticalAlign="Bottom" OnItemDataBound="dlGenericImages_ItemDataBound">
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="padding: 3px">
                                                                        <asp:ImageButton ID="ibtnGenThumbImages" runat="server" ImageUrl='<%# GetGenericImagePath(Eval("ThumbNailPath"))%>'
                                                                            AlternateText="Image" OnClick="ibtnGenericThumbImages_Click" />
                                                                        <asp:HiddenField ID="hdnDescription" runat="server" Value='<%# Eval("Description") %>' />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <%--Large image popup--%>
                                                    <asp:HiddenField ID="hfLargeGenImage" runat="server" Value='<%# Eval("ServerPath") %>' />
                                                    <ajax:ModalPopupExtender ID="MPEGenericImage" runat="server" TargetControlID="hfLargeGenImage"
                                                        PopupControlID="panOpenGen" BackgroundCssClass="modalBackground" CancelControlID="imgCancelGen"
                                                        PopupDragHandleControlID="panOpen" />
                                                    <asp:Panel ID="panOpenGen" runat="server" Height="600px" Width="700px" CssClass="ModalWindow">
                                                        <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
                                                            width: 662px;">
                                                            <img src="../Images/close_icon.gif" alt="close" id="imgCancelGen" runat="server" />
                                                        </div>
                                                        <iframe id="IframeGeneric" runat="server" scrolling="no" style="height: 600px; width: 700px;"
                                                            frameborder="0"></iframe>
                                                    </asp:Panel>
                                                    <%--Large image popup--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <%--------------------Generic Images Box End--------------------%>
            <%----------UCR Images Box Start----------%>
            <div id="divUCR" runat="server" style="width: 100%; float: left; margin-top: 5px;
                height: auto">
                <table id="UCRHeader" onclick="HideShow(this)" runat="server" border="0" cellpadding="0"
                    width="100%" style="border-collapse: collapse; cursor: pointer">
                    <tr>
                        <td class="LeftPanelTable" colspan="2">
                            <img id="imgUCR" runat="server" border="0" src="../images/UCR_Plus.gif" height="28"
                                alt="" style="width: 100%" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlUCR" runat="server">
                    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse;
                        height: auto">
                        <tr>
                            <td class="LeftPanelTable" style="padding: 10px" align="left">
                                <table border="0" width="210" cellpadding="0" style="border-collapse: collapse">
                                    <tr>
                                        <td align="left" class="OrangeText_LeftPanel">
                                            <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>--%>
                                            <asp:Label ID="lblUCRCount" runat="server" Style="font-weight: bold;" /><br />
                                            <asp:Label ID="lblUCRDescription" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                                            <div id="Div3" runat="server" style="text-align: center; font-weight: bold; padding-top: 10px;
                                                clear: both">
                                                <asp:LinkButton CssClass="Paging" OnClick="lnkUCRFirst_Click" ID="lnkUCRFirst" runat="server"
                                                    Text="<<" />
                                                |
                                                <asp:LinkButton ID="lnkUCRPre" OnClick="lnkUCRPrev_Click" CssClass="Paging" runat="server"
                                                    Text="<" />
                                                |
                                                <asp:Label CssClass="Paging" ID="lblUCRInfo" runat="server" />
                                                <asp:LinkButton ID="lnkUCRNext" OnClick="lnkUCRNext_Click" CssClass="Paging" runat="server"
                                                    Text=">" />
                                                |
                                                <asp:LinkButton ID="lnkUCRLast" OnClick="lnkUCRLast_Click" runat="server" Text=">>"
                                                    CssClass="Paging" />
                                            </div>
                                            <asp:DataList ID="dlUCRImages" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
                                                ItemStyle-VerticalAlign="Bottom" OnItemDataBound="dlUCRImages_ItemDataBound">
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="padding: 3px">
                                                                <asp:ImageButton ID="ibtnUCRThumbImages" runat="server" ImageUrl='<%# GetUCRImagePath(Eval("ThumbNailPath"))%>'
                                                                    AlternateText="Image" OnClick="ibtnUCRThumbImages_Click" />
                                                                <asp:HiddenField ID="hdnUCRDescription" runat="server" Value='<%# Eval("Description") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                            <%--Large image popup--%>
                                            <%-- <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("ServerPath") %>' />--%>
                                            <%--   <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfLargeGenImage"
                                                        PopupControlID="panOpenGen" BackgroundCssClass="modalBackground" CancelControlID="imgCancelGen"
                                                        PopupDragHandleControlID="panOpen" />--%>
                                            <%--  <asp:Panel ID="Panel2" runat="server" Height="600px" Width="700px" CssClass="ModalWindow">
                                                        <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
                                                            width: 662px;">
                                                            <img src="../Images/close_icon.gif" alt="close" id="img2" runat="server" />
                                                        </div>
                                                        <iframe id="Iframe1" runat="server" scrolling="no" style="height: 600px; width: 700px;"
                                                            frameborder="0"></iframe>
                                                    </asp:Panel>--%>
                                            <%--Large image popup--%>
                                            <%--  </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <%--------------------UCR Images Box End--------------------%>
            <ajaxtoolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                CollapseControlID="InventoryHeader" Collapsed="false" ExpandControlID="InventoryHeader"
                ImageControlID="imgInventory" CollapsedImage="../images/inventory.gif" ExpandedImage="../images/inventory-minus.gif"
                ExpandDirection="Vertical" TargetControlID="tblInvImages" BehaviorID="collapsibleBehavior"
                ScrollContents="false">
            </ajaxtoolkit:CollapsiblePanelExtender>
            <ajaxtoolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                CollapseControlID="PreHeader" Collapsed="false" ExpandControlID="PreHeader" ImageControlID="imgPreExpense"
                CollapsedImage="../images/expense.gif" ExpandedImage="../images/expense-minus.gif"
                ExpandDirection="Vertical" TargetControlID="tblExpImages" BehaviorID="collapsibleBehaviorExp"
                ScrollContents="false">
            </ajaxtoolkit:CollapsiblePanelExtender>
            <ajaxtoolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server"
                CollapseControlID="GenericHeader" Collapsed="true" ExpandControlID="GenericHeader"
                ImageControlID="imgGeneric" CollapsedImage="../images/Generic.gif" ExpandedImage="../images/generic-minus.gif"
                ExpandDirection="Vertical" TargetControlID="tblGenericImage" BehaviorID="collapsibleBehaviorGen"
                ScrollContents="false">
            </ajaxtoolkit:CollapsiblePanelExtender>
            <ajaxtoolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server"
                CollapseControlID="UCRHeader" Collapsed="true" ExpandControlID="UCRHeader" ImageControlID="imgUCR"
                CollapsedImage="../images/UCR_Plus.gif" ExpandedImage="../images/UCR_Minus.gif"
                ExpandDirection="Vertical" TargetControlID="pnlUCR" BehaviorID="collapsibleBehaviorUCR"
                ScrollContents="false">
            </ajaxtoolkit:CollapsiblePanelExtender>
            <%--------------------UCR BEGIN--------------------%>
            <%----------Image slideshow----------%>
            <asp:HiddenField ID="hfPopup" runat="server" />
            <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfPopup"
                PopupControlID="pnlAllImage" BackgroundCssClass="modalBackground" CancelControlID="img1"
                PopupDragHandleControlID="pnlAllImage" BehaviorID="ImageModelPopup" />
            <asp:Panel ID="pnlAllImage" runat="server" Height="920px" Width="750px">
                <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
                    width: 662px; display: none">
                    <img src="../Images/close_icon.gif" alt="close" id="img1" runat="server" />
                </div>
                <iframe id="ifrmSlideShow" runat="server" scrolling="no" style="height: 700px; width: 720px;"
                    frameborder="0"></iframe>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%----------Image slideshow----------%>
    <div id="dvAddUCR" class="modalPopup" style="display: none; min-width: 400px; min-height: 200px"
        runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="PopUpBoxHeading">
                        &nbsp;&nbsp;Add/Link UCR
                        <asp:Label ID="lblAddUCRHeader" runat="server"></asp:Label>
                    </td>
                    <td class="PopUpBoxHeading" align="right">
                        <img border="0" src="../Images/close.gif" id="imgCloseAddCR" alt="close" />
                    </td>
                </tr>
            </table>
            <div style="padding: 10px 0px; margin: 0px 10px" class="LeftPanelContentHeading">
                What do you want to do?</div>
            <div style="padding: 0px 5px; margin: 0px10px">
                <asp:RadioButtonList ID="rbtnListAddUCR" runat="server" RepeatDirection="Vertical"
                    OnSelectedIndexChanged="rbtnListAddUCR_SelectedIndexChanged" CellSpacing="5">
                    <asp:ListItem Text="Create CR" Value="1" class="LeftPanelContentHeading" Selected="True" />
                    <asp:ListItem Text="Link CR" Value="2" class="LeftPanelContentHeading" />
                </asp:RadioButtonList>
            </div>
            <div id="dvLinkAddCR" runat="server" style="padding: 0 0 5px 25px; margin: 0 0px 0px 10px;
                display: none; width: 100%">
                <div style="float: left;">
                    <asp:RadioButtonList ID="rbtnListAddUCRIdUrl" runat="server" RepeatDirection="Vertical"
                        OnSelectedIndexChanged="rbtnListAddUCRIdUrl_SelectedIndexChanged" CellSpacing="5">
                        <asp:ListItem Text="CR ID" Value="1" Selected="True" class="LeftPanelContentHeading" />
                        <asp:ListItem Text="CR URL" Value="2" class="LeftPanelContentHeading" />
                        <asp:ListItem Text="Find By VIN" Value="3" class="LeftPanelContentHeading" />
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="dvAddCRIdUrl" runat="server" style="padding: 5px; margin: 0 0px 0px 42px;
                display: none; clear: both;">
                <asp:TextBox ID="txtAddUCRIdUrl" runat="server" CssClass="txt1" />
            </div>
            <div style="padding: 5px 0px 0px 15; margin: 5px 0px 10px 30px">
                <asp:Button ID="btnAddUCRIdValidate" runat="server" Text="Validate" Style="display: none;"
                    CssClass="btn" OnClick="btnAddUCRIdValidate_Click" Width="80px" />
                <asp:Button ID="btnAddUCRUrlValidate" runat="server" Text="Validate" CssClass="btn"
                    Style="display: none;" OnClick="btnAddUCRUrlValidate_Click" Width="80px" />
                <asp:Button ID="btnAddUCRSearch" runat="server" Text="Search" CssClass="btn" Style="display: none;"
                    OnClick="btnAddUCRSearch_Click" Width="80px" />
                <asp:Button ID="btnAddUCROk" runat="server" Text="OK" OnClick="btnAddUCROk_Click"
                    OnClientClick="OpenCreateCRLink();" CssClass="btn" Width="80px" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAddUCRCancel" runat="server" Text="Cancel" CssClass="btn" Width="80px"
                    OnClick="btnAddUCRCancel_Click" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfCR" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajax:ModalPopupExtender ID="mpeAddCR" runat="server" BackgroundCssClass="modalBackground"
        BehaviorID="mpeAddcrbeh" TargetControlID="hfCR" PopupControlID="dvAddUCR" CancelControlID="imgCloseAddCR">
    </ajax:ModalPopupExtender>
    <%--------------------UCR END--------------------%>
    <%----------------------Change UCR Begin----------------------%>
    <div id="dvEditUCR" class="modalPopup" style="display: none; min-width: 400px; min-height: 200px"
        runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="PopUpBoxHeading">
                        &nbsp;&nbsp;Change UCR
                        <asp:Label ID="lblEditUCRHeader" runat="server"></asp:Label>
                    </td>
                    <td class="PopUpBoxHeading" align="right">
                        <img border="0" src="../Images/close.gif" id="imgCloseEditCR" alt="close" />
                    </td>
                </tr>
            </table>
            <div style="padding: 10px 0px; margin: 0px 10px" class="LeftPanelContentHeading">
                What do you want to do?</div>
            <div style="padding: 0px 5px; margin: 0px10px">
                <asp:RadioButtonList ID="rbtnlistEditUCR" runat="server" RepeatDirection="Vertical"
                    CellSpacing="5">
                    <asp:ListItem Text="Remove UCR" Value="1" class="LeftPanelContentHeading" Selected="True" />
                    <asp:ListItem Text="Relink UCR" Value="2" class="LeftPanelContentHeading" />
                </asp:RadioButtonList>
            </div>
            <div id="dvLinkEditCR" runat="server" style="padding: 0 0 5px 25px; margin: 0 0px 0px 10px;
                display: none; width: 100%">
                <div style="float: left;">
                    <asp:RadioButtonList ID="rbtnlistEditUCRIdUrl" runat="server" RepeatDirection="Vertical"
                        CellSpacing="5">
                        <asp:ListItem Text="CR ID" Value="1" Selected="True" class="LeftPanelContentHeading" />
                        <asp:ListItem Text="CR URL" Value="2" class="LeftPanelContentHeading" />
                        <asp:ListItem Text="Find By VIN" Value="3" class="LeftPanelContentHeading" />
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="dvEditCRIdUrl" runat="server" style="padding: 5px; margin: 0 0px 0px 42px;
                display: none; clear: both;">
                <asp:TextBox ID="txtEditUCRIdUrl" runat="server" CssClass="txt1" />
            </div>
            <div style="padding: 5px 0px 0px 15; margin: 5px 0px 10px 30px">
                <asp:Button ID="btnEditUCRIdValidate" runat="server" Text="Validate" CssClass="btn"
                    Width="80px" Style="display: none;" OnClick="btnEditUCRIdValidate_Click" />
                <asp:Button ID="btnEditUCRUrlValidate" runat="server" Text="Validate" CssClass="btn"
                    Width="80px" Style="display: none;" OnClick="btnEditUCRUrlValidate_Click" />
                <asp:Button ID="btnEditUCRSearch" runat="server" Text="Search" CssClass="btn" Style="display: none;"
                    Width="80px" OnClick="btnEditUCRSearch_Click" />
                <asp:Button ID="btnEditUCROk" runat="server" Text="OK" OnClick="btnEditUCROk_Click"
                    CssClass="btn" Width="80px" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnEditUCRCancel" runat="server" Text="Cancel" CssClass="btn" Width="80px"
                    OnClick="btnAddUCRCancel_Click" />&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hylnkViewReport" runat="server" Text="View Report" Target="_blank"
                    CssClass="btn" Width="100px" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfCR2" runat="server" />
    <ajax:ModalPopupExtender ID="mpeEditCR" runat="server" BackgroundCssClass="modalBackground"
        BehaviorID="mpeEditcrbeh" TargetControlID="hfCR2" PopupControlID="dvEditUCR"
        CancelControlID="imgCloseEditCR">
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
    <%--Location Detail Popup--%>
    <asp:UpdatePanel ID="updLocation" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="dvLocation" runat="server" class="modalPopup" style="display: none; width: 650px;
                min-height: 280px">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td class="PopUpBoxHeading">
                            &nbsp;&nbsp;Location
                        </td>
                        <td class="PopUpBoxHeading" align="right">
                            <img border="0" src="../Images/close.gif" id="imgCloseLocation" alt="close" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvAllLocation" runat="server" Width="100%" AutoGenerateColumns="false"
                    GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvAllLocation_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="Location" DataField="LocationCode" />
                        <asp:BoundField HeaderText="Added On" DataField="DateAdded" />
                        <asp:BoundField HeaderText="Added By" DataField="UserName" />
                        <asp:BoundField HeaderText="Latitude" DataField="Latitude" />
                        <asp:BoundField HeaderText="Longitude" DataField="Longitude" />
                        <asp:TemplateField HeaderText="Device">
                            <ItemTemplate>
                                <div title='<%#Eval("DeviceID") %>'>
                                    <%#Eval("DeviceName") %></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                    <HeaderStyle CssClass="PreTableBorderB" />
                    <HeaderStyle HorizontalAlign="Left" />
                    <RowStyle CssClass="gvRow" />
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <PagerStyle CssClass="PreFooterContentDetails" HorizontalAlign="Right" />
                </asp:GridView>
            </div>
            <asp:HiddenField ID="hfLoc" runat="server" />
            <ajax:ModalPopupExtender ID="mpeLocation" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="hfLoc" PopupControlID="dvLocation" CancelControlID="imgCloseLocation">
            </ajax:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Location Detail Popup--%>
    <%----------DOCUMENT BOX BEGIN----------%>
    <table border="0" id="tblDocument" runat="server" cellpadding="0" width="100%" style="border-collapse: collapse;
        margin-top: 5px">
        <tr>
            <td class="LeftPanelTable" colspan="2">
                <img border="0" src="../images/Documents.gif" width="100%" height="28" alt="" />
            </td>
        </tr>
        <tr>
            <td class="LeftPanelTable" style="padding: 0px" colspan="2" align="left">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                    <tr>
                        <td align="left" colspan="2" class="OrangeText_LeftPanel" style="vertical-align: top">
                            <asp:GridView ID="gvDocument" ShowHeader="false" runat="server" DataKeyNames="DocumentId"
                                AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvDocument_RowDataBound">
                                <%----%>
                                <Columns>
                                    <asp:TemplateField ItemStyle-CssClass="OrangeText_LeftPanel">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumentName" runat="server" Text='<%# Eval("DocumentName") %>'
                                                Style="cursor: pointer" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="DocumentName" HeaderText="" ItemStyle-CssClass="OrangeText_LeftPanel"
                                        ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Top" />--%>
                                    <asp:BoundField DataField="DisplayName" HeaderText="" ItemStyle-CssClass="OrangeText_LeftPanel"
                                        ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Top" />
                                    <%-- <asp:BoundField DataField="DateAdded" HeaderText="Date Added" ItemStyle-CssClass="OrangeText_LeftPanel"
                                        ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Top" />--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnFileName" runat="server" Value=' <%# Eval("FileName")%>' />
                                            <asp:ImageButton ID="btnImgDocument" runat="server" OnClick="btnImgDocument_Click"
                                                ImageUrl="../Images/view_edit.png" ToolTip="File,Image Display" />
                                            <asp:ImageButton ID="imgDownload" runat="server" ImageUrl="../Images/download.png"
                                                ToolTip="Download File" Style="padding-bottom: 4px" OnClick="imgDownload_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" style="padding: 5px 0px 0px 0px">
                <asp:Button ID="btnScan" runat="server" Text="Scan and Upload Document" CssClass="btn"
                    OnClick="btnScan_Click" />
                &nbsp;
                <%-- <asp:HyperLink ID="hlnkViewAllScan" runat="server" Target="_blank" CssClass="BlackTxt_Link"
                    NavigateUrl="~/UI/ViewAllDocument.aspx"></asp:HyperLink>--%>
            </td>
            <td style="width: auto; padding: 5px 0px 0px 0px; text-align: right">
                <asp:LinkButton ID="lnkDocuments" runat="server" ForeColor="Black" OnClick="lnkDocuments_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <%----------DOCUMENT BOX END----------%>
    <%----------Scan PopUp Begin----------%>
    <div id="divScan" class="modalPopup" style="display: none; min-width: 400px; min-height: 200px"
        runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="PopUpBoxHeading">
                        &nbsp;&nbsp;
                        <asp:Label ID="lblScan" runat="server"></asp:Label>
                    </td>
                    <td class="PopUpBoxHeading" align="right">
                        <img border="0" src="../Images/close.gif" id="imgCloseScanPopUp" alt="close" style="padding-right: 5px" />
                    </td>
                </tr>
            </table>
            <iframe id="frmScanner" runat="server" scrolling="yes" style="height: 680px; width: 850px;
                overflow: auto;" frameborder="0"></iframe>
        </div>
    </div>
    <asp:HiddenField ID="hfScan" runat="server" />
    <ajax:ModalPopupExtender ID="mpeScan" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="hfScan" PopupControlID="divScan" CancelControlID="imgCloseScanPopUp"
        BehaviorID="DocumentModelPopup11">
    </ajax:ModalPopupExtender>
    <div style="width: 100%; height: auto">
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="HiddenField2"
            PopupControlID="pnlDocument" BackgroundCssClass="modalBackground" CancelControlID="img2"
            PopupDragHandleControlID="pnlDocument" />
        <asp:Panel ID="pnlDocument" runat="server" Height="980px" Width="890px" CssClass="imagevideobox">
            <div style="position: absolute; text-align: right; padding-right: 10px; width: 885px;">
                <img src="../Images/close_icon.gif" alt="close" id="img2" runat="server" />
            </div>
            <iframe id="Iframe1" runat="server" scrolling="no" style="height: 680px; width: 890px;
                margin-top: 10px; background-color: White" frameborder="0"></iframe>
        </asp:Panel>
    </div>
    <%----------Scan PopUp End----------%>
</div>
<script type="text/javascript">

    var BrowserName = $.browser.name;

    function ChangeAddCSS(rbl) {
        var url = '<%=UCRlinkUrl %>';
        var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
        if (selectedvalue == "1") {
            $('#<%=txtAddUCRIdUrl.ClientID %>').attr('readonly', false);
            document.getElementById('<%=txtAddUCRIdUrl.ClientID %>').style.width = "150px";
            $('#<%=btnAddUCRIdValidate.ClientID %>').show();
            $('#<%=btnAddUCRUrlValidate.ClientID %>').hide();
            $('#<%=btnAddUCRSearch.ClientID %>').hide();
            $('#<%=txtAddUCRIdUrl.ClientID %>').val('');
        }
        else if (selectedvalue == "2") {
            $('#<%=txtAddUCRIdUrl.ClientID %>').attr('readonly', false);
            document.getElementById('<%=txtAddUCRIdUrl.ClientID %>').style.width = "300px";
            $('#<%=btnAddUCRIdValidate.ClientID %>').hide();
            $('#<%=btnAddUCRUrlValidate.ClientID %>').show();
            $('#<%=btnAddUCRSearch.ClientID %>').hide();
            //$('#<%=txtAddUCRIdUrl.ClientID %>').val('http://web.metaoptionllc.com:82/Report/');
            $('#<%=txtAddUCRIdUrl.ClientID %>').val(url);
        }
        else if (selectedvalue == "3") {
            var vin = $('#<%=hfvin.ClientID %>').val();
            document.getElementById('<%=txtAddUCRIdUrl.ClientID %>').style.width = "150px";
            $('#<%=btnAddUCRIdValidate.ClientID %>').hide();
            $('#<%=btnAddUCRUrlValidate.ClientID %>').hide();
            $('#<%=btnAddUCRSearch.ClientID %>').show();
            $('#<%=txtAddUCRIdUrl.ClientID %>').val(vin);
            $('#<%=txtAddUCRIdUrl.ClientID %>').attr('readonly', true);
        }
    }

    function ShowHideAddCR(rbtn) {
        var selvalue = $("#" + rbtn.id + " input:radio:checked").val();

        if (selvalue == "1") {
            $('#<%=dvLinkAddCR.ClientID %>').hide();
            $('#<%=dvAddCRIdUrl.ClientID %>').hide();
            $('#<%=btnAddUCROk.ClientID %>').show();
            $('#<%=btnAddUCRIdValidate.ClientID %>').hide();
            $('#<%=btnAddUCRUrlValidate.ClientID %>').hide();
            $('#<%=btnAddUCRSearch.ClientID %>').hide();
        }
        else if (selvalue == "2") {
            $('#<%=dvLinkAddCR.ClientID %>').show();
            $('#<%=dvAddCRIdUrl.ClientID %>').show();
            $('#<%=btnAddUCROk.ClientID %>').hide();
            $('#<%=rbtnListAddUCRIdUrl.ClientID %>').find("input[value='1']").attr("checked", "checked");
            $('#<%=txtAddUCRIdUrl.ClientID %>').val('');
            $('#<%=btnAddUCRIdValidate.ClientID %>').show();
            $('#<%=btnAddUCRUrlValidate.ClientID %>').hide();
            $('#<%=btnAddUCRSearch.ClientID %>').hide();
        }
    }

    function OpenCreateCRLink() {

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

    function ShowHideEditCR(rbtn) {
        var selvalue = $("#" + rbtn.id + " input:radio:checked").val();

        if (selvalue == "1") {
            $('#<%=dvLinkEditCR.ClientID %>').hide();
            $('#<%=dvEditCRIdUrl.ClientID %>').hide();
            $('#<%=btnEditUCROk.ClientID %>').show();
            $('#<%=btnEditUCRIdValidate.ClientID %>').hide();
            $('#<%=btnEditUCRUrlValidate.ClientID %>').hide();
            $('#<%=btnEditUCRSearch.ClientID %>').hide();
        }
        else if (selvalue == "2") {
            $('#<%=dvLinkEditCR.ClientID %>').show();
            $('#<%=dvEditCRIdUrl.ClientID %>').show();
            $('#<%=btnEditUCROk.ClientID %>').hide();
            $('#<%=rbtnlistEditUCRIdUrl.ClientID %>').find("input[value='1']").attr("checked", "checked");
            $('#<%=txtEditUCRIdUrl.ClientID %>').val('');
            $('#<%=btnEditUCRIdValidate.ClientID %>').show();
            $('#<%=btnEditUCRUrlValidate.ClientID %>').hide();
            $('#<%=btnEditUCRSearch.ClientID %>').hide();

        }
    }

    function ChangeEditCSS(rbl) {
        var url = '<%=UCRlinkUrl %>';
        var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
        if (selectedvalue == "1") {
            document.getElementById('<%=txtEditUCRIdUrl.ClientID %>').style.width = "150px";
            $('#<%=btnEditUCRIdValidate.ClientID %>').show();
            $('#<%=btnEditUCRUrlValidate.ClientID %>').hide();
            $('#<%=btnEditUCRSearch.ClientID %>').hide();
            $('#<%=txtEditUCRIdUrl.ClientID %>').val('');
            $('#<%=txtEditUCRIdUrl.ClientID %>').attr('readonly', false);
        }
        else if (selectedvalue == "2") {
            document.getElementById('<%=txtEditUCRIdUrl.ClientID %>').style.width = "300px";
            $('#<%=btnEditUCRIdValidate.ClientID %>').hide();
            $('#<%=btnEditUCRUrlValidate.ClientID %>').show();
            $('#<%=btnEditUCRSearch.ClientID %>').hide();
            //$('#<%=txtAddUCRIdUrl.ClientID %>').val('http://web.metaoptionllc.com:82/Report/');
            $('#<%=txtEditUCRIdUrl.ClientID %>').val(url);
            $('#<%=txtEditUCRIdUrl.ClientID %>').attr('readonly', false);
        }
        else if (selectedvalue == "3") {
            var vin = $('#<%=hfvin.ClientID %>').val();
            document.getElementById('<%=txtEditUCRIdUrl.ClientID %>').style.width = "150px";
            $('#<%=btnEditUCRIdValidate.ClientID %>').hide();
            $('#<%=btnEditUCRUrlValidate.ClientID %>').hide();
            $('#<%=btnEditUCRSearch.ClientID %>').show();
            $('#<%=txtEditUCRIdUrl.ClientID %>').val(vin);
            $('#<%=txtEditUCRIdUrl.ClientID %>').attr('readonly', true);
        }
    }

    if (BrowserName == "firefox") {
        $('#<%=rbtnListAddUCR.ClientID %>').live("change", function () { ShowHideAddCR(this); });
        $('#<%=rbtnListAddUCRIdUrl.ClientID %>').live("change", function () { ChangeAddCSS(this); });

        $('#<%=rbtnlistEditUCR.ClientID %>').live("change", function () { ShowHideEditCR(this); });
        $('#<%=rbtnlistEditUCRIdUrl.ClientID %>').live("change", function () { ChangeEditCSS(this); });
    }
    else if (BrowserName == "msie") {
        $('#<%=rbtnListAddUCR.ClientID %>').live("change", function () { ShowHideAddCR(this); });
        $('#<%=rbtnListAddUCRIdUrl.ClientID %>').live("change", function () { ChangeAddCSS(this); });

        $('#<%=rbtnlistEditUCR.ClientID %>').live("change", function () { ShowHideEditCR(this); });
        $('#<%=rbtnlistEditUCRIdUrl.ClientID %>').live("change", function () { ChangeEditCSS(this); });
    }
    else if (BrowserName == "chrome") {
        $('#<%=rbtnListAddUCR.ClientID %>').live("click", function () { ShowHideAddCR(this); });
        $('#<%=rbtnListAddUCRIdUrl.ClientID %>').live("click", function () { ChangeAddCSS(this); });

        $('#<%=rbtnlistEditUCR.ClientID %>').live("click", function () { ShowHideEditCR(this); });
        $('#<%=rbtnlistEditUCRIdUrl.ClientID %>').live("click", function () { ChangeEditCSS(this); });
    }

</script>
<script type="text/javascript" language="javascript">
    function HideModelpopup() {

        $find('ImageModelPopup').hide();


        return false;
    }
   
</script>
<script type="text/javascript" language="javascript">
    function HideShow(ctrl) {

        var ID = ctrl.id;
        if (endsWith(ID, 'InventoryHeader')) {
            //alert($find('collapsibleBehavior').get_Collapsed());
            if ($find('collapsibleBehavior').get_Collapsed() != true) {

                var collPre = $find('collapsibleBehavior');
                var collExp = $find('collapsibleBehaviorExp');
                var collGen = $find('collapsibleBehaviorGen');
                var collUCR = $find('collapsibleBehaviorUCR');

                if (collPre != null)
                    collPre.set_Collapsed(false);
                if (collGen != null)
                    collGen.set_Collapsed(false);
                if (collExp != null)
                    collExp.set_Collapsed(false);
                if (collUCR != null)
                    collUCR.set_Collapsed(false);
            }
            else {

                var collPre = $find('collapsibleBehavior');
                var collExp = $find('collapsibleBehaviorExp');
                var collGen = $find('collapsibleBehaviorGen');
                var collUCR = $find('collapsibleBehaviorUCR');
                if (collPre != null)
                    collPre.set_Collapsed(true);
                if (collGen != null)
                    collGen.set_Collapsed(true);
                if (collExp != null)
                    collExp.set_Collapsed(false);
                if (collUCR != null)
                    collUCR.set_Collapsed(false);
            }
        }
        else if (endsWith(ID, 'PreHeader')) {
            if ($find('collapsibleBehaviorExp').get_Collapsed() != true) {
                var collPre = $find('collapsibleBehavior');
                var collExp = $find('collapsibleBehaviorExp');
                var collGen = $find('collapsibleBehaviorGen');
                var collUCR = $find('collapsibleBehaviorUCR');
                if (collPre != null)
                    collPre.set_Collapsed(false);
                if (collExp != null)
                    collExp.set_Collapsed(false)
                if (collGen != null)
                    collGen.set_Collapsed(false);
                if (collUCR != null)
                    collUCR.set_Collapsed(false);
            }
            else {
                var collPre = $find('collapsibleBehavior');
                var collExp = $find('collapsibleBehaviorExp');
                var collGen = $find('collapsibleBehaviorGen');
                var collUCR = $find('collapsibleBehaviorUCR');
                if (collPre != null)
                    collPre.set_Collapsed(true);
                if (collExp != null)
                    collExp.set_Collapsed(true);
                if (collGen != null)
                    collGen.set_Collapsed(false);
                if (collUCR != null)
                    collUCR.set_Collapsed(false);
            }
        }
        else if (endsWith(ID, 'GenericHeader')) {
            if ($find('collapsibleBehaviorGen').get_Collapsed() != true) {

                var collPre = $find('collapsibleBehavior');
                var collGen = $find('collapsibleBehaviorGen');
                var collExp = $find('collapsibleBehaviorExp');
                var collUCR = $find('collapsibleBehaviorUCR');
                if (collGen != null)
                    collGen.set_Collapsed(false);
                if (collPre != null)
                    collPre.set_Collapsed(false);
                if (collExp != null)
                    collExp.set_Collapsed(false);
                if (collUCR != null)
                    collUCR.set_Collapsed(false);
            }
            else {

                var collPre = $find('collapsibleBehavior');
                var collGen = $find('collapsibleBehaviorGen');
                var collExp = $find('collapsibleBehaviorExp');
                var collUCR = $find('collapsibleBehaviorUCR');
                if (collGen != null)
                    collGen.set_Collapsed(true);
                if (collPre != null) {
                    collPre.set_Collapsed(true);
                }
                if (collExp != null)
                    collExp.set_Collapsed(false);
                if (collUCR != null)
                    collUCR.set_Collapsed(false);

            }
        }

        else if (endsWith(ID, 'UCRHeader')) {
            if ($find('collapsibleBehaviorUCR').get_Collapsed() != true) {

                var collPre = $find('collapsibleBehavior');
                var collGen = $find('collapsibleBehaviorGen');
                var collExp = $find('collapsibleBehaviorExp');
                var collUCR = $find('collapsibleBehaviorUCR');
                if (collGen != null)
                    collGen.set_Collapsed(false);
                if (collPre != null)
                    collPre.set_Collapsed(false);
                if (collExp != null)
                    collExp.set_Collapsed(false);
                if (collUCR != null)
                    collUCR.set_Collapsed(false);
            }
            else {

                var collPre = $find('collapsibleBehavior');
                var collGen = $find('collapsibleBehaviorGen');
                var collExp = $find('collapsibleBehaviorExp');
                var collUCR = $find('collapsibleBehaviorUCR');
                if (collGen != null)
                    collGen.set_Collapsed(false);
                if (collPre != null) {
                    collPre.set_Collapsed(true);
                }
                if (collExp != null)
                    collExp.set_Collapsed(false);
                if (collUCR != null)
                    collUCR.set_Collapsed(true);

            }
        }

        return false;
    }
    function endsWith(str, suffix) {
        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }

    function HideDocumentpopup() {
        var InventoryId = $('#<%=hfInventoryID.ClientID %>').val();
        $find('DocumentModelPopup11').hide();
        window.location.href = '../UI/InventoryDocuments.aspx?Code=' + InventoryId;
        return false;
    }
</script>

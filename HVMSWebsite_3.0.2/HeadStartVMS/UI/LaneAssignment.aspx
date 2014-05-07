<%@ Page Language="C#" AutoEventWireup="True" 
    EnableEventValidation="true" MaintainScrollPositionOnPostback="true" CodeBehind="LaneAssignment.aspx.cs"
    Inherits="METAOPTION.UI.LaneAssignment" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphViewLaneAssg" runat="server">
    <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
        <tr>
            <td align="left" valign="top">
                <table border="0" width="980px" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="left" valign="top" style="width: 30%">
                            <asp:UpdatePanel ID="upFilterCriteria" runat="server">
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" style="border-collapse: collapse; width: 100%;">
                                        <tr>
                                            <td class="ForLegend">
                                                Search Criteria
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                    <tr>
                                                        <td class="GridContent" style="width: 15%">
                                                            <b>Year</b>
                                                        </td>
                                                        <td class="GridContent" style="width: 8%">
                                                            <asp:DropDownList ID="ddlYear" runat="server" class="txt1" AutoPostBack="false" Width="60px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="GridContent" style="width: 10%">
                                                            <b>Make</b>
                                                        </td>
                                                        <td class="GridContent" style="width: 38%">
                                                            <asp:DropDownList ID="ddlMake" runat="server" class="txt1" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="GridContent" style="width: 15%; height: 10px">
                                                        </td>
                                                        <td class="GridContent" style="width: 8%; height: 10px">
                                                        </td>
                                                        <td class="GridContent" style="width: 10%">
                                                            <b></b>
                                                        </td>
                                                        <td class="GridContent" style="width: 38%; vertical-align: middle;">
                                                            <asp:DropDownList ID="ddlModel" runat="server" class="txt1" visible=false/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="GridContent" style="width: 30%" nowrap>
                                                            <b>Lane Type</b>
                                                        </td>
                                                        <td class="GridContent" style="width: 8%">
                                                            <asp:DropDownList ID="ddlLaneType" class="txt1" runat="server" Width="60px">
                                                                <asp:ListItem Value="-1">Choose</asp:ListItem>
                                                                <asp:ListItem Selected=True Value="1">Regular</asp:ListItem>
                                                                <asp:ListItem Value="2">Exotic</asp:ListItem>
                                                                <asp:ListItem Value="4">Online</asp:ListItem>
                                                                <asp:ListItem Value="3">Virtual</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="GridContent" style="width: 25%">
                                                            <b>Lane #</b>
                                                        </td>
                                                        <td class="GridContent" style="width: 10%">
                                                            <asp:TextBox ID="txtLaneNo" runat="server" class="txt1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; height: 28px" colspan="2" valign="middle">
                                                        </td>
                                                        <td style="text-align: right; padding-top: 3px" colspan="2">
                                                            <asp:Button ID="btnFilter" runat="server" Text="Apply Filter" class="Btn_Form" OnClick="btnFilter_Click"
                                                                CausesValidation="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" valign="top" style="width: 1%">
                            &nbsp;
                        </td>
                        <%--OnClientClick="return confirm('Are you sure you want to update exotic pattern?');"--%>
                        <td align="left" valign="top" style="width: 42%">
                            <%--this is the html code for History window--%>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                        <tr>
                                            <td class="ForLegend">
                                                Sort Criteria
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <table border="0" width="100%" height="110" cellpadding="0" style="border-collapse: collapse">
                                                    <tr>
                                                        <td class="GridContent">
                                                            <b>Sort 1</b>
                                                        </td>
                                                        <td class="GridContent">
                                                            <asp:DropDownList size="1" ID="ddlSort1" runat="server" class="txt1">
                                                                <asp:ListItem Value="vwLaneAssignments.DateAdded">Date Added</asp:ListItem>
                                                                <asp:ListItem Selected Value="vwLaneAssignments.RegularLane ">Regular#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.ExoticLane ">Exotic#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VirtualLane ">Virtual#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.OnlineLane ">Online#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VIN ">VIN#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.IsExotic ">Is Exotic</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.[Year] ">Year</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINDivisionName ">Make</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINModelName ">Model</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Body ">Body</asp:ListItem>
                                                                <asp:ListItem Value="CarCost ">Cost</asp:ListItem>
                                                                <asp:ListItem Value="MarketPrice">M.Price</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.MileageIn ">Mileage </asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.DealerName  ">Dealer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Designation  ">Desig.</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.BuyerName  ">Buyer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.TitlePresentNotes ">Title</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList size="1" ID="ddlSort11" runat="server" class="txt1" Width="50px">
                                                                <asp:ListItem Value="DESC">Desc</asp:ListItem>
                                                                <asp:ListItem Selected=True Value="ASC">Asc</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="GridContent">
                                                            <b>Sort 2</b>
                                                        </td>
                                                        <td class="GridContent">
                                                            <asp:DropDownList size="1" ID="ddlSort2" runat="server" class="txt1">
                                                                <asp:ListItem Value="vwLaneAssignments.DateAdded">Date Added</asp:ListItem>
                                                                <asp:ListItem Selected Value="vwLaneAssignments.RegularLane ">Regular#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.ExoticLane ">Exotic#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VirtualLane ">Virtual#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.OnlineLane ">Online#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VIN ">VIN#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.IsExotic ">Is Exotic</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.[Year] ">Year</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINDivisionName ">Make</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINModelName ">Model</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Body ">Body</asp:ListItem>
                                                                <asp:ListItem Value="CarCost ">Cost</asp:ListItem>
                                                                <asp:ListItem Value="MarketPrice">M.Price</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.MileageIn ">Mileage </asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.DealerName  ">Dealer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Designation  ">Desig.</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.BuyerName  ">Buyer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.TitlePresentNotes ">Title</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList size="1" ID="ddlSort22" runat="server" class="txt1" Width="50px">
                                                                <asp:ListItem Value="ASC">Asc</asp:ListItem>
                                                                <asp:ListItem Value="Desc">Desc</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="GridContent">
                                                            <b>Sort 3</b>
                                                        </td>
                                                        <td class="GridContent">
                                                            <asp:DropDownList size="1" ID="ddlSort3" runat="server" class="txt1">
                                                                <asp:ListItem Value="vwLaneAssignments.DateAdded">Date Added</asp:ListItem>
                                                                <asp:ListItem Selected Value="vwLaneAssignments.RegularLane ">Regular#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.ExoticLane ">Exotic#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VirtualLane ">Virtual#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.OnlineLane ">Online#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VIN ">VIN#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.IsExotic ">Is Exotic</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.[Year] ">Year</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINDivisionName ">Make</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINModelName ">Model</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Body ">Body</asp:ListItem>
                                                                <asp:ListItem Value="CarCost ">Cost</asp:ListItem>
                                                                <asp:ListItem Value="MarketPrice">M.Price</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.MileageIn ">Mileage </asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.DealerName  ">Dealer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Designation  ">Desig.</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.BuyerName  ">Buyer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.TitlePresentNotes ">Title</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList size="1" ID="ddlSort33" runat="server" class="txt1" Width="50px">
                                                                <asp:ListItem Value="ASC">Asc</asp:ListItem>
                                                                <asp:ListItem Value="Desc">Desc</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="GridContent">
                                                            <b>Sort 4</b>
                                                        </td>
                                                        <td class="GridContent">
                                                            <asp:DropDownList size="1" ID="ddlSort4" runat="server" class="txt1">
                                                                <asp:ListItem Value="vwLaneAssignments.DateAdded">Date Added</asp:ListItem>
                                                                <asp:ListItem Selected Value="vwLaneAssignments.RegularLane ">Regular#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.ExoticLane ">Exotic#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VirtualLane ">Virtual#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.OnlineLane ">Online#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VIN ">VIN#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.IsExotic ">Is Exotic</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.[Year] ">Year</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINDivisionName ">Make</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINModelName ">Model</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Body ">Body</asp:ListItem>
                                                                <asp:ListItem Value="CarCost ">Cost</asp:ListItem>
                                                                <asp:ListItem Value="MarketPrice">M.Price</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.MileageIn ">Mileage </asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.DealerName  ">Dealer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Designation  ">Desig.</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.BuyerName  ">Buyer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.TitlePresentNotes ">Title</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList size="1" ID="ddlSort44" runat="server" class="txt1" Width="50px">
                                                                <asp:ListItem Value="ASC">Asc</asp:ListItem>
                                                                <asp:ListItem Value="Desc">Desc</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="GridContent">
                                                            <b>Sort 5</b>
                                                        </td>
                                                        <td class="GridContent">
                                                            <asp:DropDownList size="1" ID="ddlSort5" runat="server" class="txt1">
                                                                <asp:ListItem Value="vwLaneAssignments.DateAdded">Date Added</asp:ListItem>
                                                                <asp:ListItem Selected Value="vwLaneAssignments.RegularLane ">Regular#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.ExoticLane ">Exotic#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VirtualLane ">Virtual#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.OnlineLane ">Online#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VIN ">VIN#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.IsExotic ">Is Exotic</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.[Year] ">Year</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINDivisionName ">Make</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINModelName ">Model</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Body ">Body</asp:ListItem>
                                                                <asp:ListItem Value="CarCost ">Cost</asp:ListItem>
                                                                <asp:ListItem Value="MarketPrice">M.Price</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.MileageIn ">Mileage </asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.DealerName  ">Dealer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Designation  ">Desig.</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.BuyerName  ">Buyer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.TitlePresentNotes ">Title</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList size="1" ID="ddlSort55" runat="server" class="txt1" Width="50px">
                                                                <asp:ListItem Value="ASC">Asc</asp:ListItem>
                                                                <asp:ListItem Value="Desc">Desc</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="GridContent">
                                                            <b>Sort 6</b>
                                                        </td>
                                                        <td class="GridContent">
                                                            <asp:DropDownList size="1" ID="ddlSort6" runat="server" class="txt1">
                                                               <asp:ListItem Value="vwLaneAssignments.DateAdded">Date Added</asp:ListItem>
                                                                <asp:ListItem Selected Value="vwLaneAssignments.RegularLane ">Regular#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.ExoticLane ">Exotic#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VirtualLane ">Virtual#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.OnlineLane ">Online#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VIN ">VIN#</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.IsExotic ">Is Exotic</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.[Year] ">Year</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINDivisionName ">Make</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.VINModelName ">Model</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Body ">Body</asp:ListItem>
                                                                <asp:ListItem Value="CarCost ">Cost</asp:ListItem>
                                                                <asp:ListItem Value="MarketPrice">M.Price</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.MileageIn ">Mileage </asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.DealerName  ">Dealer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.Designation  ">Desig.</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.BuyerName  ">Buyer</asp:ListItem>
                                                                <asp:ListItem Value="vwLaneAssignments.TitlePresentNotes ">Title</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList size="1" ID="ddlSort66" runat="server" class="txt1" Width="50px">
                                                                <asp:ListItem Value="ASC">Asc</asp:ListItem>
                                                                <asp:ListItem Value="Desc">Desc</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                        <td colspan="2" style="text-align: center; padding-right: 10px; padding-top: 4px">
                                                            <asp:Button ID="btnApplySorting" runat="server" Text="(S) Apply Sorting" class="btn" AccessKey="S"
                                                                CausesValidation="False" OnClick="btnApplySorting_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" valign="top" style="width: 1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="top" style="width: 29%">
                            <asp:UpdatePanel ID="upRecentChanges" runat="server">
                                <ContentTemplate>
                                    <%--OnClientClick="return confirm('Are you sure you want to update exotic pattern?');"--%>
                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                        class="arial-12">
                                        <tr>
                                            <td class="ForLegend">
                                                Recent Changes
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <table border="0" width="100%" height="110" cellpadding="0" style="border-collapse: collapse">
                                                            <tr>
                                                                <td class="GridContent">
                                                                    <asp:LinkButton ID="lnk20RecordsModified" runat="server" CssClass="GridContent_Link"
                                                                        OnClientClick="javascript:HideAnnouncement();" OnClick="lnk20RecordsModified_Click"
                                                                        CausesValidation="False">
                                                                        <asp:Label ID="lblMRCount" runat="server" Text="0"></asp:Label></asp:LinkButton>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="GridContent">
                                                                    <asp:LinkButton CssClass="GridContent_Link" ID="lnk10RecordsforEmailAnn" runat="server"
                                                                        OnClientClick="javascript:ShowAnnouncement(1,this);" OnClick="lnk10RecordsforEmailAnn_Click"
                                                                        CausesValidation="False"></asp:LinkButton>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="GridContent" nowrap>
                                                                    <asp:LinkButton CssClass="GridContent_Link" ID="lnk100RecordsForAnnounce_Email" OnClientClick="javascript:ShowAnnouncement(2,this);"
                                                                        runat="server" OnClick="lnk100RecordsForAnnounce_Email_Click" CausesValidation="False"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="GridContent">
                                                                    <asp:LinkButton CssClass="GridContent_Link" ID="lnlViewAllInventory" runat="server"
                                                                        OnClientClick="javascript:HideAnnouncement();" OnClick="lnkViewAllInventory_Click"
                                                                        CausesValidation="False">View All Inventory</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="" style="padding-top: 3px">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                  <%--  <tr>
                        <td class="GridContent" colspan="4">
                            <asp:LinkButton ID="lnkLaneReport"  runat="server" 
                                 ToolTip="Click here to view lane report." 
                                CommandName="lnkLaneReport_Click" OnClick="lnkLaneReport_Click" 
                                CausesValidation="False">View Lane Report</asp:LinkButton>
                       
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="ID" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="0" style="width: 978px; border-collapse: collapse">
                <tr>
                    <td align="center">
                        <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                            <tr>
                                <td colspan="8" align="right">
                                    &nbsp;
                                    <ajax:ModalPopupExtender ID="mdlpUpdate" runat="server" OkControlID="btnOK" CancelControlID="rghtclose"
                                        TargetControlID="btnOk" PopupControlID="pnlUpdate" PopupDragHandleControlID="PopupHeader"
                                        Drag="true" BackgroundCssClass="ModalPopupBG">
                                    </ajax:ModalPopupExtender>
                                    <asp:Panel ID="pnlUpdate" Style="display: none" runat="server" CssClass="popupConfirmation">
                                        <div class="popup_Container" style="height: 120px">
                                            <div class="popup_Titlebar" id="PopupHeader">
                                                <div class="TitlebarLeft">
                                                    Update Exotic Pattern!</div>
                                                <div id="rghtclose" class="TitlebarRight" onclick="$get('btnOK').click();">
                                                </div>
                                            </div>
                                            <div class="popup_Body">
                                                <div style="text-align: left">
                                                    Exotic Pattern updated!</div>
                                                <div>
                                                    <div style="text-align: center">
                                                        <p>
                                                            <asp:ImageButton ID="btnOK" runat="server" ImageUrl="~/Images/Cancel.gif" />
                                                        </p>
                                                    </div>
                                                </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <%-- <td id="rts" class="CheckBox" width="2%" >
                                                            <asp:CheckBox ID="chkRealTimeSorting" runat="server" AutoPostBack="True" 
                                                                 />
                                                        </td>
                                                        <td class="TableBorder" width="18%">
                                                            RealTime Sorting
                                                        </td>--%>
                                <td class="CheckBox" width="2%">
                                    <asp:CheckBox ID="chkShowExotic" runat="server" onclick="ShowHideGridColumns(3, this);" />
                                </td>
                                <td class="TableBorder" width="18%">
                                    Show Exotic
                                </td>
                                <td class="CheckBox" width="2%">
                                    <asp:CheckBox ID="chkShowOnline" runat="server" onclick="ShowHideGridColumns(4,this);ShowHideGridColumns(5,this);" />
                                </td>
                                <td class="TableBorder" width="18%">
                                    Show Online &amp; Virtual
                                </td>
                                <td align="center" class="CheckBox" width="6%">
                                    <asp:TextBox ID="txtExoticPattern" runat="server" Width="88px"></asp:TextBox>
                                </td>
                                <td class="TableBorder" width="18%">
                                    Exotic Pattern
                                </td>
                                <td class="TableBorder" width="10%">
                                    <asp:Button ID="btnUpdate" CssClass="Btn_Form" runat="server" Text="Update" CausesValidation="False"
                                        OnClientClick="javascript:changeExoticBackColor('ctl00_ContentPlaceHolder1_txtExoticPattern');"
                                        EnableViewState="False" OnClick="btnUpdate_Click" />
                                    <%--OnClientClick="return confirm('Are you sure you want to update exotic pattern?');"--%>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                            <ProgressTemplate>
                                <img alt="..." src="../Images/Wait.gif" />
                                Wait...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <table border="0" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" align="left">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td class="TableHeadingBg" colspan="2">
                            <table border="0" cellpadding="0" style="border-collapse: collapse; width: 980px">
                                <tr>
                                    <td class="TableHeading">
                                        Inventory List        </td>
                                    <td align="right" class="HeadingEditButton">
                                        <b>View Records</b>
                                    </td>
                                    <td align="right" width="30" class="HeadingEditButton">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlViewRecords" runat="server" CssClass="HeadingEditButton"
                                                    AutoPostBack="True" OnSelectedIndexChanged="btnApplySorting_Click">
                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                    <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="250">250</asp:ListItem>
                                                    <asp:ListItem Value="500">500</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" width="100%" id="tblListView" runat="server" cellpadding="0" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upnlInventoryList" runat="server" >
                                            <ContentTemplate>
                                            
                                               <asp:GridView ID="gvInventoryList" runat="server" DataKeyNames="InventoryId"
                                                    AutoGenerateColumns="False" HeaderStyle-CssClass="gvHeading" AllowPaging="True"
                                                    PageSize="50" OnPageIndexChanging="gvInventoryList_PageIndexChanging" OnRowEditing="gvInventoryList_RowEditing"
                                                    OnSelectedIndexChanging="gvInventoryList_SelectedIndexChanging" OnRowDataBound="gvInventoryList_RowDataBound"
                                                    PagerSettings-Mode="NumericFirstLast" 
                                                    PagerSettings-Position="TopAndBottom" 
                                                    onrowcreated="gvInventoryList_RowCreated" >
                                                    <Columns>
                                                        
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="40px">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hplViewCarPage" runat="server" NavigateUrl='<%# "InventoryDetail.aspx?Code=" +Eval("InventoryId")+"&ReturnURL=LaneAssignment.aspx" %>'
                                                                    ImageUrl="~/Images/edit-icon-lane.jpg" ToolTip="View car details"></asp:HyperLink>
                                                                <asp:ImageButton ID="btnHistory" runat="server" ImageUrl="~/Images/hist-icon-lane.jpg"
                                                                    ToolTip="View history" CausesValidation="false" CommandName="Select" AlternateText="History" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                            <ItemStyle CssClass="CellHeight" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Ex" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                
                                                                <%--<asp:CheckBox ID="gvchkIsExotic" CssClass="hideCol" runat="server" onclick="SwapExoticPattern(this);EditIsExotic(this, 'IsExotic', 0);" />--%>
                                                                <input id="gvchkIsExotic" type="checkbox" Class="hideCol" runat="server" onclick="SwapExoticPattern(this);EditIsExotic(this, 'IsExotic', 0);" />
                                                                <div id="gvdvIsExotic" runat="server" class="hideCol">
                                                                    <%--<asp:CheckBox ID="gvchkIsExotic" runat="server" onclick="SwapExoticPattern(this);" />--%>
                                                                    <div>
                                                                        <asp:ImageButton ID="gvbtnIsExotic" runat="server" ImageUrl="~/Images/confirm.gif"
                                                                            ToolTip="Save Changes" OnClientClick="EditIsExotic(this, 'IsExotic', 0);" />
                                                                    </div>
                                                                </div>
                                                                <asp:Label ID="gvlblIsExotic" runat="server" Width="4px" onclick="EditIsExotic(this, 'IsExotic', 1);"
                                                                    CssClass="lnktxt" Text='<%# Convert.ToString(Eval("IsExotic"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" CssClass="CellHeight" />
                                                            <HeaderStyle Width="20px" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Regular#">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="gvtxtRegularLane" Width="50px" runat="server" Text='<%# Bind("RegularLane") %>'
                                                                    MaxLength="7" CssClass="hideCol" onblur="EditLane(this, 'RegularLane', 0,1);" />
                                                                <asp:Label ID="gvlblRegularLane" Width="50px" runat="server" CssClass="lnktxt" Text='<%# Bind("RegularLane") %>'
                                                                    onclick="EditLane(this, 'RegularLane', 1,1);" ToolTip='<%# Bind("RegularNumberForeColor")%>'
                                                                    ForeColor='' />
                                                                <asp:HiddenField ID="gvhfRegularLane" runat="server" Value='<%#Eval("InventoryId") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="CellHeight"  />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Exotic#" HeaderStyle-CssClass="hideCol">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="gvtxtExoticLane" Width="50px" runat="server" CssClass="hideCol"
                                                                    MaxLength="7" Text='<%# Bind("ExoticLane") %>' onblur="EditLane(this, 'ExoticLane', 0,1);"></asp:TextBox>
                                                                <asp:Label ID="gvlblExoticLane" Width="50px" runat="server" CssClass="lnktxt" Text='<%# Bind("ExoticLane") %>'
                                                                    onclick="EditLane(this, 'ExoticLane', 1,1);" ForeColor="Orange"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="hideCol"  />
                                                            <ItemStyle CssClass="hideCol CellHeight" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Online#" HeaderStyle-CssClass="hideCol">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="gvtxtOnlineLane" Width="50px" runat="server" CssClass="hideCol"
                                                                    MaxLength="7" Text='<%# Bind("OnlineLane") %>' onblur="EditLane(this, 'OnlineLane', 0,1);"></asp:TextBox>
                                                                <asp:Label ID="gvlblOnlineLane" Width="50px" runat="server" CssClass="lnktxt" Text='<%# Bind("OnlineLane") %>'
                                                                    onclick="EditLane(this, 'OnlineLane', 1,1);"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="hideCol" />
                                                            <ItemStyle CssClass="hideCol CellHeight" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Virtual#" HeaderStyle-CssClass="hideCol" ItemStyle-CssClass="hideCol">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="gvtxtVirtualLane" Width="50px" runat="server" CssClass="hideCol"
                                                                    MaxLength="7" Text='<%# Bind("VirtualLane") %>' onblur="EditLane(this, 'VirtualLane', 0,1);"></asp:TextBox>
                                                                <asp:Label ID="gvlblVirtualLane" Width="50px" runat="server" CssClass="lnktxt" Text='<%# Bind("VirtualLane") %>'
                                                                    onclick="EditLane(this, 'VirtualLane', 1,1);"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="hideCol"  />
                                                            <ItemStyle CssClass="hideCol CellHeight" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:BoundField DataField="Year" HeaderText="Year">
                                                            <ItemStyle CssClass="CellHeight" Width="35px" HorizontalAlign="Center"/>
                                                        </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="VINDivisionName" HeaderText="Make">
                                                            <ItemStyle CssClass="CellHeight" Width="80px" />
                                                            <ItemStyle BackColor="" />
                                                        </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="VINModelName" HeaderText="Model">
                                                            <ItemStyle CssClass="CellHeight" Width="80px" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:TemplateField HeaderText="Mileage" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="gvtxtMileage" Width="45px" runat="server" CssClass="hideCol"
                                                                    Text='<%# Bind("MileageIn") %>' onblur="EditLane(this, 'Mileage', 0,0);"></asp:TextBox>
                                                                <asp:Label ID="gvlblMileage" Width="45px" runat="server" CssClass="lnktxt" Text='<%# Bind("MileageIn") %>'
                                                                    onclick="EditLane(this, 'Mileage', 1,0);"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle CssClass="GridContentRightLane CellHeight"  />
                                                        </asp:TemplateField>                                                        
                                                        
                                                         <asp:BoundField DataField="CarCost" HeaderText="Cost">
                                                            <ItemStyle CssClass="GridContentRightLane CellHeight" ForeColor="DarkGreen" />
                                                            <ItemStyle Width="45px" />
                                                        </asp:BoundField>
                                                      
                                                       <asp:BoundField DataField="ExtColor" HeaderText="Color">
                                                            <ItemStyle Height="8px" />
                                                            <ItemStyle Width="70px" />
                                                        </asp:BoundField>

                                                       <asp:BoundField DataField="PurchaseDate" HeaderText="DOP" DataFormatString="{0:d}">
                                                            <ItemStyle Height="8px" />
                                                            <ItemStyle Width="8px" />
                                                        </asp:BoundField>

                                                       <asp:BoundField DataField="Body" HeaderText="Body">
                                                            <ItemStyle Height="8px"  />
                                                            <ItemStyle Width="140px" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:TemplateField HeaderText="Mkt Price" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="gvtxtMarketPrice" Width="50px" runat="server" CssClass="hideCol"
                                                                    Text='<%# Bind("MarketPrice") %>' onblur="EditLane(this, 'MarketPrice', 0,0);"></asp:TextBox>
                                                                <asp:Label ID="gvlblMarketPrice" Width="50px" runat="server" CssClass="lnktxt" Text='<%# Bind("MarketPrice") %>'
                                                                    onclick="EditLane(this, 'MarketPrice', 1,0);"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle CssClass="GridContentRightLane CellHeight"  />
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="VIN">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="gvtxtVIN" Width="130px" runat="server" CssClass="hideCol" Text='<%# Bind("VIN") %>'
                                                                    onblur="EditLane(this, 'VIN', 0,0);"></asp:TextBox>
                                                                <asp:Label ID="gvlblVIN" Width="130px" runat="server" CssClass="lnktxt" Text='<%# Bind("VIN") %>'
                                                                    onclick="EditLane(this, 'VIN', 1,0);"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="CellHeight" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                       
                                                        <asp:BoundField DataField="TitlePresent" HeaderText="T" HeaderStyle-Wrap="false"
                                                            HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="CellHeight"  />
                                                            <HeaderStyle Width="20px" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="DealerName" HeaderText="Dealer">
                                                            <ItemStyle CssClass="CellHeight" Width="100px" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="Designation" HeaderText="Desig">
                                                            <ItemStyle CssClass="CellHeight" Width="40px" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="BuyerName" HeaderText="Buyer">
                                                            <ItemStyle CssClass="CellHeight" Width="40px" />
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Notes">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="gvtxtNotes" Width="220px" runat="server" CssClass="hideCol" TextMode="MultiLine"
                                                                    MaxLength="255" Rows="4" Text='<%# Bind("CarNote") %>' onblur="EditLane(this, 'Notes', 0,0);"></asp:TextBox>
                                                                <asp:Label ID="gvlblNotes" Width="200px" runat="server" CssClass="lnktxt" ToolTip='<%# Bind("CarNote") %>'
                                                                    Text='<%# Bind("Notes") %>' onclick="EditLane(this, 'Notes', 1,0);"></asp:Label>
                                                                <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("Notes") %>' ToolTip='<%# Bind("LaneNotes") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="CellHeight" />
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Groups" ItemStyle-Width="30px">
                                                            <ItemTemplate >
                                                                <div id="gvdvGRP" runat="server" class="hideCol">
                                                                    <table cellpadding="0" class="ForEditTable" width="240px">
                                                                        <tr>
                                                                            <td style="width: 20%" nowrap>
                                                                                <asp:Label ID="gvlblGRP1" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 20%" nowrap>
                                                                                <asp:Label ID="gvlblGRP2" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 20%" nowrap>
                                                                                <asp:Label ID="gvlblGRP3" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 20%" nowrap>
                                                                                <asp:Label ID="gvlblGRP4" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 20%" nowrap>
                                                                                <asp:Label ID="gvlblGRP5" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 20%; text-align: center">
                                                                                <asp:CheckBox ID="gvchkGRP1" runat="server" />
                                                                            </td>
                                                                            <td style="width: 20%; text-align: center">
                                                                                <asp:CheckBox ID="gvchkGRP2" runat="server" />
                                                                            </td>
                                                                            <td style="width: 20%; text-align: center">
                                                                                <asp:CheckBox ID="gvchkGRP3" runat="server" />
                                                                            </td>
                                                                            <td style="width: 20%; text-align: center">
                                                                                <asp:CheckBox ID="gvchkGRP4" runat="server" />
                                                                            </td>
                                                                            <td style="width: 20%; text-align: center">
                                                                                <asp:CheckBox ID="gvchkGRP5" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <div style="text-align: center; background-color: InfoBackground">
                                                                        <asp:ImageButton ID="gvbtnGRP" OnClientClick="EditGroup(this, 'GRP', 0);" runat="server"
                                                                            ToolTip="Save Changes" ImageUrl="~/Images/confirm.gif" /></div>
                                                                </div>
                                                                <asp:Label ID="gvlblGRP" runat="server" CssClass="lnktxt" Text='<%# Bind("GRP") %>'
                                                                    onclick="EditGroup(this, 'GRP', 1);"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="CellHeight" Wrap="false"  />
                                                        </asp:TemplateField>
                                                        <%--
                                                        <asp:TemplateField HeaderText="G3">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGRP3" runat="server" CssClass="lnktxt" Text='<%# Bind("GRP3") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="CellHeight" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="G4">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGRP4" runat="server" CssClass="lnktxt" Text='<%# Bind("GRP4") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="CellHeight" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="G5">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGRP5" runat="server" CssClass="lnktxt" Text='<%# Bind("GRP5") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="CellHeight" />
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <RowStyle CssClass="CellHeight" />
                                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                    <HeaderStyle CssClass="GridHeader" Height="16px"></HeaderStyle>
                                                </asp:GridView>
                                                
                                               <br />
                                                <div style="text-align:center">
                                                <asp:Button ID="Button1" runat="server" Text="(S) Apply Sorting" class="btn"
                                                                CausesValidation="False" OnClick="btnApplySorting_Click" /></div>
                                                
                                                <div id="divEditSection" runat="server" style="position: absolute; display: none">
                                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 700px; background-color: White;">
                                                        <tr>
                                                            <td colspan="6" class="PopUpBoxHeading" style="padding-left: 7px">
                                                                Edit Records
                                                            </td>
                                                            <td class="PopUpBoxHeading" align="right" style="padding-right: 3px">
                                                                <img id="btnCancel" src="../Images/close.gif" alt="Cancel" />
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                             <td class="lblb" align="right">
                                                &nbsp;
                                             </td>
                                             <td class="lblb" align="center">
                                                <asp:CheckBox ID="chkG1" runat="server" CausesValidation="false" />
                                             </td>
                                             <td class="lblb" align="center">
                                                <asp:CheckBox ID="chkG2" runat="server" CausesValidation="false" />
                                             </td>
                                             <td class="lblb" align="center">
                                                <asp:CheckBox ID="chkG3" runat="server" CausesValidation="false" />
                                             </td>
                                             <td class="lblb" align="center">
                                                <asp:CheckBox ID="chkG4" runat="server" />
                                             </td>
                                             <td class="lblb" align="center">
                                                <asp:CheckBox ID="chkG5" runat="server" CausesValidation="false" />
                                             </td>
                                          </tr>--%>
                                                        <tr>
                                                            <td class="lblb" align="right">
                                                                VIN#:
                                                            </td>
                                                            <td class="lbl">
                                                                <asp:TextBox CssClass="txt1" ID="txtVINNumber" runat="server" />
                                                            </td>
                                                            <td class="lblb" align="right">
                                                                Regular#:
                                                            </td>
                                                            <td class="lbl">
                                                                <asp:TextBox CssClass="txt1" ID="txtRegularNo" runat="server" />
                                                            </td>
                                                            <th class="lblb" align="right">
                                                                Exotic#:
                                                            </th>
                                                            <td class="lbl">
                                                                <asp:TextBox ID="txtExoticNo" CssClass="txt1" runat="server" />
                                                            </td>
                                                            <td class="lblb" rowspan="3">
                                                                <asp:CheckBoxList ID="chklstGroups" runat="server" />
                                                                <input type="hidden" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th class="lblb" align="right">
                                                                Online#:
                                                            </th>
                                                            <td class="lbl">
                                                                <asp:TextBox ID="txtOnlineNo" CssClass="txt1" runat="server" />
                                                            </td>
                                                            <td class="lblb" align="right">
                                                                Virtual#:
                                                            </td>
                                                            <td class="lblb">
                                                                <asp:TextBox ID="txtVirtualNo" CssClass="txt1" runat="server" />
                                                            </td>
                                                            <td class="lblb">
                                                                Market Price($):
                                                            </td>
                                                            <td class="lblb">
                                                                <asp:TextBox ID="txtMarketPrice" CssClass="txt1" runat="server" />
                                                            </td>
                                                            <asp:Label ID="lblInventoryId" runat="server" Visible="false" Text='<%# Bind("InventoryId") %>'></asp:Label>
                                                            <asp:TextBox ID="txtGRPName" runat="server" Text='<%# Bind("GRPName") %>' Visible="false" />
                                                            <asp:TextBox ID="txtGRPAB" runat="server" Text='<%# Bind("GRP") %>' Visible="false" />
                                                        </tr>
                                                        <tr>
                                                            <td class="lblb" align="right">
                                                                Is Exotic
                                                            </td>
                                                            <td class="lbl">
                                                                <asp:CheckBox ID="chkIsExotic" runat="server" CausesValidation="false" TextAlign="Left" />
                                                            </td>
                                                            <td class="lblb" align="right">
                                                                Note
                                                            </td>
                                                            <td colspan="2" class="lbl">
                                                                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" CssClass="txtMulti"
                                                                    Rows="4" Width="291px" />
                                                            </td>
                                                            <td class="lblb">
                                                                <asp:UpdateProgress ID="uprogSearch" runat="server" DisplayAfter="50">
                                                                    <ProgressTemplate>
                                                                        <img src="../Images/Wait.gif" alt="..." />
                                                                        Wait...
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="center">
                                                                <asp:Button ID="btnCancelUpdate" runat="server" CssClass="btn" Text="   Cancel   "
                                                                    CausesValidation="false" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnEditUpdate" runat="server" CssClass="btn" Text="    Save    "
                                                                    CausesValidation="false" OnClick="btnEditUpdate_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="display: none">
                                                    <asp:Button ID="btnHidentoOpenPopup" runat="server" Text="" />
                                                </div>
                                                <ajax:ModalPopupExtender ID="mpeEditLanes" BehaviorID="bhaveInventoryList" runat="server"
                                                    TargetControlID="btnHidentoOpenPopup" PopupControlID="divEditSection" CancelControlID="btnCancelUpdate"
                                                    BackgroundCssClass="modalBackground">
                                                </ajax:ModalPopupExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            &nbsp;
            </td>
        </tr>
        <tr>
            <td id="colAnnouncement" style="display: none">
                <asp:Panel ID="pnlNewAnnouncement" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                                <tr>
                                    <td class="TableHeadingBg TableHeading">
                                        Recent Changes Announcement / Emails
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                                            <tr>
                                                <td class="" width="11%">
                                                    <b>Title</b> <span style="color: #FF0000">*</span>
                                                </td>
                                                <td class="" width="35%" valign="middle">
                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="FormItem" Style="width: 300px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    
                                                </td>
                                                <td class="" width="2%">
                                                    <asp:CheckBox ID="chkSendEmail" runat="server" AutoPostBack="True" OnCheckedChanged="chkSendEmail_CheckedChanged"
                                                        onClick="javascript:ToGetInnerHTMLGrid();" />
                                                </td>
                                                <td class="" width="7%">
                                                    Send Email
                                                </td>
                                                <td class="" width="41%">
                                                    <asp:DropDownList ID="ddlSendEmailOption" runat="server" CssClass="FormItem" size="1">
                                                        <asp:ListItem Selected="True" Value="1">Send to all</asp:ListItem>
                                                        <asp:ListItem Value="2">Send to selected users</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="" valign="top" width="11%">
                                                    <b>Description</b><span style="color: #FF0000"> *</span>
                                                </td>
                                                <td class="" width="35%" valign="middle">
                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="FormItem" MaxLength="500"
                                                        Rows="20" Style="width: 300px; height: 67px" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                                                        Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="" colspan="3" valign="top">
                                                    <asp:ListBox ID="lstEmployeeList" runat="server" CssClass="FormItem" size="1" Width="386px">
                                                    </asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="" colspan="5" style="text-align: right">
                                                    <asp:Button class="Btn_Form" ID="btnCreateAnnouncement" runat="server" Text="Create Announcement"
                                                        OnClick="btnCreateAnnouncement_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <input id="hdnNoOfRowId" runat="server" type="hidden" />
                <input id="hdnExoticPattern" runat="server" type="hidden" />
                <input id="hdnTempExoticPattern" runat="server" type="hidden" />
                <input id="hdnMRS" runat="server" type="hidden" />
                <%# Eval("RegularLane") ?? "&nbsp;"%>
            </td>
        </tr>
    </table>
    
    <input id="hdnlvIventoryListInnerHtml" type="hidden" runat="server" />
    <asp:HiddenField ID="userId" runat="server" Value="-1" />
    <%--History modal poupup--%>
    <asp:Panel ID="pnlShowHistory" runat="server" CssClass="modalPopup" Width="99%" Style="display: none;">
        <asp:UpdatePanel ID="upShowHistory" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnShowHistory" runat="server" Style="display: none" />
                <ajax:ModalPopupExtender ID="mdpShowHistory" runat="server" TargetControlID="btnShowHistory"
                    PopupControlID="pnlShowHistory" CancelControlID="btnShowHistoryClose" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="PopUpBoxHeading" style="padding-left: 5px; width: 80%" nowrap>
                            Lane Assignments Histoty
                        </td>
                        <td align="right" class="PopUpBoxHeading" style="padding-right: 5px; width: 20%">
                            <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnShowHistoryClose.ClientID %>').click();" />
                        </td>
                    </tr>
                </table>
                <%-- <div class="ForLegend">--%>
                <div style="background-color: White; width: auto">
                    <div style="background-color: InfoBackground">
                        <b>Year :</b>
                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                        &nbsp;&gt;&gt; <b>Make :</b>
                        <asp:Label ID="lblMake" runat="server"> </asp:Label>
                        &nbsp;&gt;&gt; <b>Model :</b>
                        <asp:Label ID="lblModel" runat="server"></asp:Label></div>
                    <asp:GridView ID="gvLaneHistory" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="True" OnPageIndexChanging="gvLaneHistory_PageIndexChanging" GridLines="None"
                        PageSize="20" EmptyDataText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>History not available!</b>">
                        <RowStyle CssClass="GridContent" />
                        <Columns>
                            <asp:BoundField DataField="DateAdded" HeaderText="Date" DataFormatString="{0:mm/dd/yyyy HH:mm: ss}"
                                HtmlEncode="false">
                                <HeaderStyle CssClass="GridHeader" />
                                <ItemStyle CssClass="GridContent" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NewMarketPrice" HeaderText="N.Market Price">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="GridHeader" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="GridContent"
                                    Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OldMarketPrice" HeaderText="O.Market Price">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="GridHeader" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="GridContent" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NewRegular" HeaderText="N.Regular #">
                                <HeaderStyle Wrap="False" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridContent" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OldRegular" HeaderText="O.Regular #">
                                <HeaderStyle Wrap="False" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridContent" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NewOnline" HeaderText="N.Online #">
                                <HeaderStyle Wrap="False" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridContent" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Oldonline" HeaderText="O.Online #">
                                <HeaderStyle Wrap="False" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridContent" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NewExotic" HeaderText="N.Exotic #">
                                <HeaderStyle Wrap="False" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridContent" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OldExotic" HeaderText="O.Exotic #">
                                <HeaderStyle Wrap="False" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridContent" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NewVirtual" HeaderText="N.Virtual #">
                                <HeaderStyle Wrap="False" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridContent" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OldVirtual" HeaderText="O.Virtual #">
                                <HeaderStyle Wrap="False" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridContent" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle BorderStyle="Solid" CssClass="FooterContentDetails" />
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:GridView>
                    <div class="OrangeText_LeftPanel">
                        Hints: N= New , O = Old</div>
                </div>
                <%--</div>--%>
                <div class="TableHeadingBg">
                    <div style="text-align: right">
                        <asp:ImageButton ID="btnShowHistoryClose" runat="server" ImageUrl="~/Images/Delete.gif"
                            ToolTip="Cancel" />
                        &nbsp;
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    
    <%--kk--%>
    <%--<asp:Panel ID="pnlExoticPatternUpdated" runat="server" CssClass="modalPopup" Width="400px"
        Style="display: none;">
        <asp:UpdatePanel ID="upExoticPatternUpdated" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnExoticPatternUpdated" runat="server" Style="display: none" />
                <ajax:ModalPopupExtender ID="mdpupExoticPatternUpdated" runat="server" TargetControlID="btnExoticPatternUpdated"
                    PopupControlID="pnlExoticPatternUpdated" CancelControlID="btnExoticPatternUpdatedClose" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="PopUpBoxHeading" style="padding-left: 5px; width: 80%" nowrap>
                            Update!
                        </td>
                        <td align="right" class="PopUpBoxHeading" style="padding-right: 5px; width: 20%">
                            <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnExoticPatternUpdatedClose.ClientID %>').click();" />
                        </td>
                    </tr>
                </table>
                <div>
                    <div style="height: 8px">
                    </div>
                            Exotic Pattern Updated!
                    <div style="height: 8px">
                    </div>
                </div>
                <div>
                    <div style="text-align: center">
                        <div style="height: 10px">
                        </div>
                        <asp:Button ID="btnExoticPatternUpdatedClose" runat="server" Text="Cancel" CausesValidation="false"
                            CssClass="Btn_Form" />
                       
                    </div>
                </div>
                <div style="height: 8px">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>--%>
    <div id="dvhistory" runat="server" style="position: relative; display: none; top: 0px">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="PopUpBoxHeading" style="padding-left: 5px; width: 80%" nowrap>
                    Lane Assignments Histoty  <td align="right" class="PopUpBoxHeading" style="padding-right: 5px; width: 20%">
                    <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnShowHistoryClose.ClientID %>').click();" />
                </td>
            </tr>
        </table>
    </div>
    <input id="btnhOldVal" runat="server" type="hidden" />
    <input id="btnhOldValId" runat="server" type="hidden" />

    <script type="text/JavaScript">
    var regLane = (/^\d{2}-\d{4}$/);
    var alphanum=/^[0-9a-bA-B]+$/; //This contains A to Z , 0 to 9 and A to B
    var numericExpression = /^[0-9]+$/;//Only validate number
    var AlphaNum_Valid="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var num_valid="0123456789";
    var alph_valid="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"

    //var regLane = (/^\d{2}-\[^a-zA-Z0-9]{4}$/);
    var AlphaNumeric    =   false;
    var GroupOldValues = '';
    //Grid row High ligh Start
    var gridViewCtlId = '<%=gvInventoryList.ClientID%>';
    var gridViewCtl = null;
    var curSelRow = null;
    function getGridViewControl()
    {
//        var gridViewCtlId = '<%=gvInventoryList.ClientID%>';
        gridViewCtl = document.getElementById(gridViewCtlId);
        if (null == gridViewCtl)
        {
            gridViewCtl = document.getElementById(gridViewCtlId);
        }
    }
    
    function onGridViewRowSelected(rowIdx)
    {
        var selRow = getSelectedRow(rowIdx);
        if (curSelRow != null)
        {
            //selected row color will be RESET or restore
            curSelRow.style.backgroundColor = '#ffffff';
        }
        
        if (null != selRow)
        {
            curSelRow = selRow;
            curSelRow.style.backgroundColor = '#31F2ED';
        }
    }
    
    function getSelectedRow(rowIdx)
    {
        getGridViewControl();
        if (null != gridViewCtl)
        {
            return gridViewCtl.rows[rowIdx];
        }
        return null;
    }
///Grid High Light End Code
    
    function numeric(AlphaNum)
    {
        if(AlphaNum.match(numericExpression))
        {
            return true;
        }
        else
        {
        return false;
        }
    }
     
     function alphnumericValidate(elem)
     {
	      var bValid=false;
	      var inum=0;
	      var schr=0;
	      if(elem.length !=4)
	      {
	        return false;
	      }
	      for (var i=0; i<4; i++)
	       {
                if (AlphaNum_Valid.indexOf(elem.charAt(i)) < 0)
                {
                    return false;;
                }
           }
            return true;
	    }

    String.prototype.count=function(comma) 
    { 
	    return (this.length - this.replace(new RegExp(comma,"g"), '').length) / comma.length;
	  }

    var browser = navigator.appName;

    var browser_netscape = false;
    
    if (browser.indexOf("Netscape") >= 0)
      browser_netscape = true;
    else
      browser_netscape = false;  

    function popUp(URL)
    {
     try
     {
        day = new Date();
        id = day.getTime();
        eval("page" + id + " = window.open(URL, '" + id + "','toolbar=0,scrollbars=1,location=false,statusbar=0,menubar=0,resizable=0,width=950,height=500,left = 50,top = 100');");
     }
     catch(err)
     {
      alert(err);
     }
    }
    
   function ValidateLane(lane)
   {
      var OK = re.exec(lane.value);
      
      if (!OK)
      {
        window.alert("Incorrect Lane Number format! \n Use format: XX-XXXX where 'X' is numeric 0 through 9");
      }
   }
    
//   function OpenPopupWindow()
//   {
//      document.getElementById('<%=dvhistory.ClientID %>').style.display="block";
//      document.getElementById('<%=dvhistory.ClientID %>').style.position = "absolute";
//   } 
    
   function ShowHideGridColumns(CellIndex, chkBox)
   { 
      //var col_num = document.getElementById("column_numbder").value;
      rows = document.getElementById("ctl00_ContentPlaceHolder1_gvInventoryList").rows;

      if(chkBox.checked)
      {
        try
        {
          for(i=0; i < rows.length; i++) 
          {
            try
            {
            if (browser_netscape)
              rows[i].cells[CellIndex].style.display = "table-cell";
            else
              rows[i].cells[CellIndex].style.display = "block";
            //alert("rows[" + i + "].cells[" + CellIndex + "].style.display");
            }
            catch(err1)
            {
              //alert(err1)
            }
          }
        }
        catch(err)
        {
          // We need to ignore last row where we have pagination. Changed by Naushad on August 26, 2009
          //alert(err);
        }
      }
      else
      {
        try
        {
          for(i=0; i < rows.length; i++)
          {
            try { rows[i].cells[CellIndex].style.display="none"; }
            catch (err2) {}
           }
        }
        catch(err)
        {
          // We need to ignore last row where we have pagination. Changed by Naushad on August 26, 2009
          //alert(err);
        }
      } 
   } 
   
  function ShowAnnouncement(id, ctrl)
  {
    try
    {
      var av;

      // (1)Pending Announcement & email since Last login
      if(id==1)
      {
        av = document.getElementById("ctl00_ContentPlaceHolder1_lnk10RecordsforEmailAnn").innerHTML;
      }
      else
      {
        //(2)Only Pending Announcement & email
        av = document.getElementById("ctl00_ContentPlaceHolder1_lnk100RecordsForAnnounce_Email").innerHTML;
      }

      var a = av.indexOf('(');
      var b = av.indexOf(')');
      var iCount = av.substr(a+1,b-1)
         
      //Checking Either Pending Announcement & email since Last login OR Only Pending Announcement & email 
      //Show/Hide to create Announcement & send email
      if(iCount > 0)
      {
        try
        {
          var txtTitle =document.getElementById("ctl00_ContentPlaceHolder1_txtTitle");
          txtTitle.value=av;
          var AnnPnl = document.getElementById("colAnnouncement");
          AnnPnl.style.display = 'block';
        }
        catch(Er)
        {
          alert(Er);
        }
      }
      else
      {
        var AnnPnl = document.getElementById("colAnnouncement");
        AnnPnl.style.display = 'none';
      }
    }
    catch(err)
    {
      alert(err);
    }
  }

  //Hide Announcement
  function HideAnnouncement()
  {
    try
    {
      var AnnPnl = document.getElementById("colAnnouncement");
      AnnPnl.style.display = 'none';
    }
    catch(err)
    {
      alert(err);
    }
  }
   
   // This functions provides functionality to edit lane numbers
   // Condition = 1 {Change label to textbox}
   // Condition = 0 {Save textbox data and switch back to label}
   function EditLane(ctrl, IdName, Condition, ValidateCondition)
   {
      var initialControlValue = "";
   
      if(Condition==1)
      {
        try
        {
          // Get Lebel Id
          var txt;
          var id2;
          var objId= ctrl.id;
          // Get Text Box Control Id
          id2 = objId.replace("lbl" + IdName, "txt" + IdName);
          //Get Text Box Object
          txt = document.getElementById(id2);
          // Swap Label Value to text box

          if(IdName != "Notes")
          {
            txt.value = ctrl.innerHTML;
            initialControlValue = txt.value;
          }

          // Hide show
          txt.style.display="block";
          ctrl.style.display="none";
          // change Focus
          document.getElementById(id2).focus();
          document.getElementById(id2).select();
        }
        catch(err)
        {
          alert(err);
        }
      }
      else
      {  
            try
            {
                
                // Get Lebel Id
                 var objId = ctrl.id;
                  // Get Label Control Id
                 var id2   = objId.replace("txt" + IdName, "lbl" + IdName);
                 // Get Lebel Objet
                 var lbl   = document.getElementById(id2);
                 // Get Hidden Field Id of Inventory Id
                 var hfId  = objId.replace("txt" + IdName, "hfRegularLane");
                 
                 //Get Current Inventory Id
                 var inventoryId   = document.getElementById(hfId).value;
                 // Get Current logged in user Id
                 var UserId        = document.getElementById("ctl00_ContentPlaceHolder1_userId").value;
                 var change        = ctrl.value;
                 
                  // Change Back color 
                   if(IdName != "Notes")
                   {
                       if(lbl.innerHTML != ctrl.value)
                       {
                          changeBackColor(lbl);
                       }
                   }
                   else
                   {
                       if(lbl.title !=ctrl.value)
                       {
                            changeBackColor(lbl);
                            
                       }
                   }
                  // Swap the value to label 
                   if(IdName != "Notes")
                    {
                        //Keeping data to undo the records while gettining any ertror
                        var oldVal = document.getElementById("ctl00_ContentPlaceHolder1_btnhOldVal");
                        oldVal.value = lbl.innerHTML;
                        initialControlValue = oldVal.value;
                        
                        var OldValId = document.getElementById("ctl00_ContentPlaceHolder1_btnhOldValId");
                        OldValId.value = id2;
                        lbl.innerHTML = ctrl.value     
                    }
                    else
                    {
                        //Update Tooltips & Changes values for Note Only
                        if(lbl.title !=ctrl.value)
                        {
                            lbl.title =ctrl.value;
                            lbl.innerHTML=ctrl.value;
                        }
                    }
            }
            catch(err)
            {
              alert(err);
            }

        if(ctrl.value != '')
        {
            //ValidateCondition=0 mean not validate lane format(it could be price,VIN etc)
            //ValidateCondition=1 mean validate lane format
            var OK;
            if(ValidateCondition == 0)
            {
                 OK = false ;
            }
            else
            {
                //Lane Number format Validating start upto 6 & 7 letters 
                if(ctrl.value.length    ==  6)
                {
                    
                    
                    var temp            =   ctrl.value;
                    var twochar         =   temp.substring(0, 2);
                    var temp1           =   temp.substring(0, 2) + "-" + temp.substring(2, 6);
                    AlphaNumeric        =   alphnumericValidate(temp.substring(2, 6));
                    var firsttwochar    =   numeric(twochar);
                    if(AlphaNumeric == false || firsttwochar == false)//Validation=True
                    {
                        alert("Incorrect Lane Number format! \n Use format: XX-YYYY Where XX is numeric 0 through 9 and YYYY is alphanumeric");
                        ctrl.value = initialControlValue;
                        ctrl.focus();  
                        OK =   true;
                        return false;
                    }
                    else//Validation = false
                    {
                        lbl.innerHTML = temp1;
                         OK =   false;
                        change = ctrl.value;
                    }
                }
                else if(ctrl.value.length   ==  7)
                {
                    var twochar         =       ctrl.value.substring(0, 2);
                    var hyphon          =        ctrl.value.substring(2, 3);
                    var lastletter      =    ctrl.value.substring(3, 7);
                    AlphaNumeric        =      alphnumericValidate(lastletter);
                    var firsttwochar    =  numeric(twochar);
                    
                    if(AlphaNumeric == false || firsttwochar == false || hyphon != "-")//Validation=True
                    {
                        if("00-0000" != ctrl.value)
                        {
                            alert("Incorrect Lane Number format! \n Use format: XX-YYYY Where XX is numeric 0 through 9 and YYYY is alphanumeric");
                            ctrl.value = initialControlValue;
                            ctrl.focus();
                             OK =   true;
                            return false;
                        }
                        else
                        {
                            if( lbl.innerHTML == ctrl.value)
                             {
                                lbl.style.display = "block";
                                ctrl.style.display= "none";
                                return false;
                             }
                        }
                        
                    }
                    else//Validation = false
                    {
                        lbl.innerHTML = ctrl.value;
                        OK  =   false;
                        change = ctrl.value;
                    }
                }
                else //For General error
                {
                    alert("Incorrect Lane Number format! \n Use format: XX-YYYY Where XX is numeric 0 through 9 and YYYY is alphanumeric");
                    ctrl.value = initialControlValue;
                    ctrl.focus();
                    OK  =   true;
                    return false;
                }
                
            }

            ///////////Lane Number alpha numeric format validating
            if (OK == true)
            {
                alert("Incorrect Lane Number format! \n Use format: XX-YYYY Where XX is numeric 0 through 9 and YYYY is alphanumeric");
                    ctrl.value = initialControlValue;
                    ctrl.focus();
                    return false;
            }
           
                 //////////End Validating
            else
            {        
                 
                 
                 // Show/Hide
                 lbl.style.display = "block";
                 ctrl.style.display= "none";
                 //lbl.bgColor ='green'; 
                 // Call WebSevice method to save data.
                 
                 
                 switch(IdName)
                 {
                    case "RegularLane":
                       change = AddDashInLane(change);
                       METAOPTION.WS.LaneAssignment.UpdateRegularLane(inventoryId, change, UserId, wsSuccess, wsError);
                       break;
                    case "ExoticLane":
                       change = AddDashInLane(change);
                       METAOPTION.WS.LaneAssignment.UpdateExoticLane(inventoryId, change, UserId, wsSuccess, wsError);
                       break;
                    case "OnlineLane":
                       change = AddDashInLane(change);
                       METAOPTION.WS.LaneAssignment.UpdateOnlineLane(inventoryId, change, UserId, wsSuccess, wsError);
                       break;
                    case "VirtualLane":
                       change = AddDashInLane(change);
                       METAOPTION.WS.LaneAssignment.UpdateVirtualLane(inventoryId, change, UserId, wsSuccess, wsError);
                       break;
                    case "VIN":
                       METAOPTION.WS.LaneAssignment.UpdateVIN(inventoryId, change, UserId, wsSuccess, wsError);
                       break;
                    case "MarketPrice":
                        METAOPTION.WS.LaneAssignment.UpdateMarketprice(inventoryId, change, UserId, wsSuccess, wsError);
                       break;
                    case "Notes":
                        METAOPTION.WS.LaneAssignment.UpdateLaneNotes(inventoryId, change, UserId, wsSuccess, wsError);
                       break;
                    case "Mileage":
                        METAOPTION.WS.LaneAssignment.UpdateMileage(inventoryId, change, UserId, wsSuccess, wsError);
                       break;
                       
                 }
                 
                 //Get Counts
                 METAOPTION.WS.LaneAssignment.GetCount(UserId, wsCountSuccess, wsError);
              }
           }
           else
           {
             // Show/Hide      
                 lbl.style.display = "block";
                 ctrl.style.display= "none"; 
           } 
       }     
   }
   
   // Added by Naushad on 10/17/09
   function AddDashInLane(val)
   {
   
       // Anwarul introduced some issues, dashes are not going in. code added by Naushad on 10/17/09
       if (val.length == 6)
       {
          val = val.substring(0, 2) + "-" + val.substring(2, 6);
       }
       
       return val;
   }
   
   function findTotalCount(str)
   {
    try
    {
        var a=str.indexOf('(');
        var b=str.indexOf(')');
        var iCount = str.substr(a+1,b-1);
        return iCount; 
    }
    catch(err)
    {
      //TO DO
      alert(err);
    }
   }
   
   function ReplaceTotalCount(str,NewVal)
   {
       try
       {
            var sIndex = str.length;
            var b=str.indexOf(')');
            var myNewString = str.substr(b+1,sIndex-1);
            //var NewVal = parseInt(OldVal)+1;
            var newStr = '('+ String(NewVal) +')' + myNewString;
            return newStr; 
        }
        catch(err)
        {
        //TO DO
          alert(err);
        }
   }
   
    function wsSuccess(value)
	  {		   
	   
    }
    
    function wsCountSuccess(value)
	  {		   
        try
        {
          
            //Get count from Database for logged user
            var mySplitResult = value.split(",");
            var NewVal20 =mySplitResult[0];//For current user logged session display upto 20 modified records
            var NewVal10 =mySplitResult[1];//For logged user since last logged display upto 10 modified records
            var NewVal100 =mySplitResult[2];//For logged user since last logged display upto 100 modified records
            
            //Get Object of 20 records count link button
            var lnk20 = document.getElementById("ctl00_ContentPlaceHolder1_lnk20RecordsModified");
            //Get Object of 10 records count link button
            var lnk10 = document.getElementById("ctl00_ContentPlaceHolder1_lnk10RecordsforEmailAnn");
            //Get Object of 100 records count link button
            var lnk100 = document.getElementById("ctl00_ContentPlaceHolder1_lnk100RecordsForAnnounce_Email");
            var lnkVal20 =lnk20.innerHTML;//Get Inner HTML values from 20 count link button
            var lnkVal10 =lnk10.innerHTML;//Get Inner HTML values from 10 count link button
            var lnkVal00 =lnk100.innerHTML;//Get Inner HTML values from 100 count link button
            var oldVal  =   findTotalCount(lnkVal20);//Get the total count of Inner HTML values
            lnkVal20   =   ReplaceTotalCount(lnkVal20,NewVal20);//Find old count values fron Inner HTML
                if(oldVal<20)//Validating here total count not exceed 20
                {
                    lnk20.innerHTML=lnkVal20;//swap new count
                }
                oldVal  =   findTotalCount(lnkVal10);//Find old count values fron Inner HTML
                lnkVal10    =   ReplaceTotalCount(lnkVal10,NewVal10);//Add 1 value of total's count from Inner HTML values
                if(oldVal<10)//Validating here total count not exceed 10
                {
                    lnk10.innerHTML=lnkVal10;//swap new count
                }
                oldVal  =   findTotalCount(lnkVal00);//Find old count values fron Inner HTML
                lnkVal00    =   ReplaceTotalCount(lnkVal00,NewVal100);//Add 1 value of total's count from Inner HTML values
                if(oldVal<100)//Validating here total count not exceed 100
                {
                    lnk100.innerHTML=lnkVal00;//swap new count
                }
         }
         catch(err)
         {
         //TO DO
          alert(err);
         }
    }
    
    function wsError(value)
    {
        try
        {
            //Undo Changes
            //Geting Old Value
            var oldVal = document.getElementById("ctl00_ContentPlaceHolder1_btnhOldVal");
            //Getting Old Cotrol ID
            var OldValId = document.getElementById("ctl00_ContentPlaceHolder1_btnhOldValId");
            //Getting Cintrol objec
            var ondObj = document.getElementById(OldValId.value);
            //Undoing old values
            ondObj.innerHTML=oldVal.value;
            //Displaying error message
            alert("Error: " + value.get_message());
        }
        catch(err)
        {
          alert(err);
        }
    }
    
    function changeBackColor(ctrl)
    {
      //Get orignal Color
      //oldcolor = ctrl.currentStyle.background; 
      
      //Set new Color
      //ctrl.style.background = 'yellow';
      
      oldcolor = ctrl.style.backgroundColor;
      
      ctrl.style.backgroundColor = (ctrl.style.backgroundColor=='#ffff00'||ctrl.style.backgroundColor=='rgb(255, 255, 0)')?'yellow':'#ffff00';

      //Re-store orignal color after 2 seconds
      try
      {
        setTimeout(function(){ReStoreBackColor(ctrl,oldcolor);},2000);
      }
      catch(err)
      {
        alert(err);
      }
    }
    
  ///This function responsible to get Inner HTML of GridView binded data only
  function ToGetInnerHTMLGrid()
  {
    try
    {
      var tblListView = document.getElementById("ctl00_ContentPlaceHolder1_tblListView").innerHTML;
      
      //Calling server side method to insert into InnerHTML into temp table for emailing purpose
      METAOPTION.WS.LaneAssignment.InnserHTML(tblListView, wsSuccess, wsError);
    }
    catch(err)
    {
      alert(err);
    }
  }
  
    function ReStoreBackColor(obj,orignalColor)
    {
      try
      {
        obj.style.backgroundColor = orignalColor; 
      }
      catch(err)
      {
        alert(err);
      }
    } 
    
   function status(st)
   {
      return st;
   }
   
   function EditGroup(ctrl, IdName, Condition)  
   {
      if(Condition==1)
      {
        try
        {
             // Get Lebel Id
             var objId= ctrl.id;
             // Get DIV Control Id
             var id1 = objId.replace("lbl" + IdName, "dv" + IdName);
             //Get Div Object
             var dv = document.getElementById(id1);
             // Swap Label Value to check(checked/unchecked) box
             var GroupOldValues =ctrl.innerHTML;
            /// Group Check box id
            var chkg1Id = objId.replace("lbl" + IdName, "chkGRP1");
            var chkg2Id = objId.replace("lbl" + IdName, "chkGRP2");
            var chkg3Id = objId.replace("lbl" + IdName, "chkGRP3");
            var chkg4Id = objId.replace("lbl" + IdName, "chkGRP4");
            var chkg5Id = objId.replace("lbl" + IdName, "chkGRP5");
            //Show/Hide
            dv.style.display = "block";
            ctrl.style.display="none"; 
            //chk.focus();    
         }
         catch(err)
         {
          alert(err);
         }
        
      }
      else
      {  
        try
        {
            var objId= ctrl.id;
            var sep=',';
            var lblId   = objId.replace("btn" + IdName, "lbl" + IdName);
            // Get Button Id
             var objId = ctrl.id;
              // Get DIV Control Id
             var id2   = objId.replace("btn" + IdName, "dv" + IdName);
             // Get DIV Objet
             var dv   = document.getElementById(id2);
             //Get Label Object
             var lbl = document.getElementById(lblId);
             // Get Hidden Field Id of Inventory Id
             var hfId  = objId.replace("btn" + IdName, "hfRegularLane");
             
             //Get Current Inventory Id
             var inventoryId   = document.getElementById(hfId).value;
             // Get Current logged in user Id
             var UserId        = document.getElementById("ctl00_ContentPlaceHolder1_userId").value;  
             // Show/Hide      
             lbl.style.display = "block";
             dv.style.display= "none";
            ///**************To Get Group name*************************
            //For CheckBox
            //Check any single group has been checked out of five
            var chkg1 = false;
            var chkg2 = false;
            var chkg3 = false;
            var chkg4 = false;
            var chkg5 = false;
            // To Get Group Abb
            var abb='' ;
            
            /// Group Check box id
            var chkg1Id = objId.replace("btn" + IdName, "chkGRP1");
            var chkg2Id = objId.replace("btn" + IdName, "chkGRP2");
            var chkg3Id = objId.replace("btn" + IdName, "chkGRP3");
            var chkg4Id = objId.replace("btn" + IdName, "chkGRP4");
            var chkg5Id = objId.replace("btn" + IdName, "chkGRP5");
            ////
            var lblGN1Id = objId.replace("btn" + IdName, "lblGRP1");
            var lblGN2Id = objId.replace("btn" + IdName, "lblGRP2");
            var lblGN3Id = objId.replace("btn" + IdName, "lblGRP3");
            var lblGN4Id = objId.replace("btn" + IdName, "lblGRP4");
            var lblGN5Id = objId.replace("btn" + IdName, "lblGRP5");
            ///To Get Only Group Name
            var lblgname1 = GetGroupName(document.getElementById(lblGN1Id).innerHTML);
            var lblgname2 = GetGroupName(document.getElementById(lblGN2Id).innerHTML);
            var lblgname3 = GetGroupName(document.getElementById(lblGN3Id).innerHTML);
            var lblgname4 = GetGroupName(document.getElementById(lblGN4Id).innerHTML);
            var lblgname5 = GetGroupName(document.getElementById(lblGN5Id).innerHTML);
            /// checking check box either checked or not
            
            if(document.getElementById(chkg1Id).checked)
            {
                chkg1=true;
                abb = GetGroupAbb(document.getElementById(lblGN1Id).innerHTML);
            }
            if(document.getElementById(chkg2Id).checked)
            {
                chkg2=true;
                if(abb!='')
                {
                    abb=abb+sep;
                }
                abb = abb + GetGroupAbb(document.getElementById(lblGN2Id).innerHTML);
            }
            if(document.getElementById(chkg3Id).checked)
            {
                chkg3=true;
                if(abb!='')
                {
                    abb=abb+sep;
                }
                abb = abb +  GetGroupAbb(document.getElementById(lblGN3Id).innerHTML);
            }
            if(document.getElementById(chkg4Id).checked)
            {
                chkg4=true;
                if(abb!='')
                {
                    abb=abb+sep;
                }
                abb = abb + GetGroupAbb(document.getElementById(lblGN4Id).innerHTML);
            }
            if(document.getElementById(chkg5Id).checked)
            {
                chkg5=true;
                if(abb!='')
                {
                    abb=abb+sep;
                }
                abb = abb +  GetGroupAbb(document.getElementById(lblGN5Id).innerHTML);
            }
            //swap new changes data
            if(abb!='')
            {
                lbl.innerHTML = abb;
                //Change back color
                changeBackColor(lbl);
            }
     }
     catch(err)
     {
      alert(err);
     }
            
           
          //*************** Group Update part start****************************
          try
          {                 
            METAOPTION.WS.LaneAssignment.EditLaneGroups(inventoryId, chkg1,chkg2,chkg3,chkg4,chkg5,lblgname1,lblgname2,lblgname3,lblgname4,lblgname5,UserId , wsSuccess, wsError);        
          }
          catch(uerr)
          {
            alert(err);
          }
      }     
   } 
   
    //To get group name
    function GetGroupName(ctrl)
    {
      try
      {
        var LastIndex=ctrl.indexOf('(');
        var GRP = ctrl.substr(0,LastIndex - 1);
        return GRP;
      }
      catch(err)
      {
        alert(err);
      }
    }
    
    //To Get group Abb. to display instantly after any changes
    function GetGroupAbb(ctrl)
    {
      try
      {  
        var value=ctrl;
        var FirstIndex=value.indexOf('(');
        var LastIndex=value.indexOf(')');
        var GABB = value.substr(FirstIndex+1,FirstIndex+2);
        GABB = GABB.replace(")" , '');
        GABB = GABB.replace("(" , '');
        return GABB;
      }
      catch(err)
      {
        alert(err);
      }
    }
    
    function changeExoticBackColor(ctrlID)
    { 
       try
       {
          //Get control ID
          var ctrl = document.getElementById(ctrlID);
          //Get orignal Color
          //oldcolor = ctrl.currentStyle.background; 
          //Set new Color
          //ctrl.style.background = 'yellow';
          //Re-store orignal color after 5 seconds
            
          oldcolor = ctrl.style.backgroundColor;
          ctrl.style.backgroundColor = (ctrl.style.backgroundColor=='#ffff00'||ctrl.style.backgroundColor=='rgb(255, 255, 0)')?'yellow':'#ffff00';
            
        }
        catch(e)
        {
          alert(err);
        }
        try
        {
          setTimeout(function(){ReStoreBackColor(ctrl,oldcolor);},5000);
        }
        catch(err)
        {
          alert(err);
        }
    }
    
    ///This function used to get Exotic pattern
    //If Check Box is checked then Exotic pattern values would be swap into clicked row of grid Exotic # when Exotic # is either empty or 00-0000 format.
   function SwapExoticPattern(ctrl)  
   {
     // Get Check Box Id
     var objId= ctrl.id;
     // Get Check Box Control Id
     var id = objId.replace("gvchkIsExotic" , "gvlblExoticLane");
     //Get Exotic # Label Object
     var lbl = document.getElementById(id);
     var chk = document.getElementById(objId);
     if(chk.checked)
     {
        //Validating if existing pattern must be empty or 00-0000 format to replace exotic pattern
        if(lbl.innerHTML =='00-0000' || lbl.innerHTML =='')
        {
          var ExoticPattern = document.getElementById("ctl00_ContentPlaceHolder1_txtExoticPattern").value;  
          //Swaping New Values
          lbl.innerHTML=ExoticPattern;
          var txtId = objId.replace("gvchkIsExotic" , "gvtxtExoticLane");
          var txt = document.getElementById(txtId);
          txt.values=ExoticPattern;
           changeBackColor(lbl) ;
        }
     }
   }
   function test(ctrl)  
   {
    // Get Check Box Id
     var objId= ctrl.id;
     // Get Check Box Control Id
     var id = objId.replace("gvchkIsExotic1" , "gvlblExoticLane");
     //Get Exotic # Label Object
     var lbl = document.getElementById(id);
     var chk = document.getElementById(objId);
     if(chk.checked)
     {
        //Validating if existing pattern must be empty or 00-0000 format to replace exotic pattern
        if(lbl.innerHTML =='00-0000' || lbl.innerHTML =='')
        {
          var ExoticPattern = document.getElementById("ctl00_ContentPlaceHolder1_txtExoticPattern").value;  
          //Swaping New Values
          lbl.innerHTML=ExoticPattern;
          var txtId = objId.replace("gvchkIsExotic" , "gvtxtExoticLane");
          var txt = document.getElementById(txtId);
          txt.values=ExoticPattern;
          //Hide/Unhide control
                lbl.style.display = "block";
                ctrl.style.display="none"; 
           changeBackColor(lbl) ;
        }
     }
    
   }
   function EditIsExotic(ctrl, IdName, Condition)  
   {
      
      if(Condition==1)
      {
         try
         {
             // Get Lebel Id
             var objId= ctrl.id;
             // Get Check Box Control Id
             var id1 = objId.replace("lbl" + IdName, "chk" + IdName);
             //Get Div Object
             var chk = document.getElementById(id1);
             
             //Show/Hide
                //document.getElementById(id1).style.display = "block";
                chk.style.display = "block";
                ctrl.style.display="none"; 
         }
         catch(err)
         {
          alert(err);
         }
      }
      else
      {  
         try
         {
             var objId= ctrl.id;
             var chkChanges=false;
             var lblId   = objId.replace("chk" + IdName, "lbl" + IdName);
            // Get Button Id
             var objId = ctrl.id;
             //Get Label Object
             var lbl = document.getElementById(lblId);
             // Get Hidden Field Id of Inventory Id
             var hfId  = objId.replace("chk" + IdName, "hfRegularLane");
             
             //Get Current Inventory Id
             var inventoryId   = document.getElementById(hfId).value;
             // Get Current logged in user Id
             var UserId        = document.getElementById("ctl00_ContentPlaceHolder1_userId").value;  
             // Show/Hide 
             
             var chkId = objId.replace("chk" + IdName, "chk" + IdName);
             var chk = document.getElementById(chkId);
              if(chk.checked)
              {
                lbl.innerHTML='Y';
                chkChanges=true;
                var ExId = objId.replace("chk" +IdName , "lblExoticLane");
                //Get Exotic # Label Object
                var lblEx = document.getElementById(ExId);
                var ExoticPat = document.getElementById("ctl00_ContentPlaceHolder1_txtExoticPattern").value;
                if(ExoticPat ==lblEx.innerHTML)
                {
                 METAOPTION.WS.LaneAssignment.UpdateExoticLane(inventoryId, ExoticPat, UserId, wsSuccess, wsError);
                  //Get Counts
                 METAOPTION.WS.LaneAssignment.GetCount(UserId, wsCountSuccess, wsError);
                }
              }
              else
              {
                lbl.innerHTML='N';
                chkChanges=false;
              }
              
                  
             lbl.style.display = "block";
             ctrl.style.display= "none";
             //Save Changes in database
             METAOPTION.WS.LaneAssignment.EditLaneIsExotic(inventoryId, chkChanges, wsSuccess, wsError);
            //Back color change/restore
             changeBackColor(lbl) ;
         }
         catch(err)
         {
          alert(err);
         }
      }
   }
   
  
    </script>

    <script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(requestEndHandler );

// This function will handle the end request event
function requestEndHandler(sender, args)
 {
   if( args.get_error() ){
      document.getElementById("errorMessageLabel").innerText = 
         args.get_error().description;
      args.set_errorHandled(true);
 }
}


    </script>

</asp:Content>

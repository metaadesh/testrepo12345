<%@ Page Language="C#" MasterPageFile="~/UI/MasterPageNoLeftPanel.Master" AutoEventWireup="true" CodeBehind="ActivityStats.aspx.cs"
 Inherits="METAOPTION.UI.ActivityStats" Title="HeadStart VMS :: Activity Stats" %>

 <asp:Content ID="contActivityStats" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="padding:5px">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td id="tdtitle" runat="server" class="TableHeadingBg TableHeading">
                        ACTIVITY STATS
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="updActivityStats" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="padding:10px; width:100%; height:50px">
                    <div style="width:30%; float:left"><b>Filter result by:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlSortFilter" runat="server" CssClass="txt2" onchange="showHideDatePicker();">
                            <asp:ListItem Text="Today" Value="1" />
                            <asp:ListItem Text="This Week" Value="2" Selected="True" />
                            <asp:ListItem Text="Last One Week" Value="3" />
                            <asp:ListItem Text="This Month" Value="4" />
                            <asp:ListItem Text="Last One Month" Value="5" />
                            <asp:ListItem Text="This Year" Value="6" />
                            <asp:ListItem Text="Last One Year" Value="7" />
                            <asp:ListItem Text="Date Range" Value="8" />
                        </asp:DropDownList>
                    </div>
                    <div class="tdhideclass1" style="width:30%; float:left">
                        <b>From</b>
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="txt1" ReadOnly="true" />
                        <asp:ImageButton ID="imgDateFrom" runat="server" ImageUrl="~/Images/calender-icon.gif" />
                        <ajax:CalendarExtender ID="ceAddedOnFrom" runat="server" 
                            TargetControlID="txtDateFrom" PopupButtonID="imgDateFrom" />&nbsp;&nbsp;
                            <b>To</b>&nbsp;&nbsp;
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="txt1" ReadOnly="true" />
                        <asp:ImageButton ID="imgDateTo" runat="server" ImageUrl="~/Images/calender-icon.gif" />
                        <ajax:CalendarExtender ID="ceAddedOnTo" runat="server" 
                            TargetControlID="txtDateTo" PopupButtonID="imgDateTo" />
                    </div>
                    <div style="width:30%; float:left">
                        <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn" Width="80px" 
                            OnClick="btnFilter_Click" OnClientClick="showHideDatePicker()" />
                            <asp:UpdateProgress ID="uprgActivityStats" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="updActivityStats">
                            <ProgressTemplate>
                                <div id="dvProg" class="overlay">
                                    <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please wait...
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>                    
                </div>
                <div style="clear:both">
                <asp:Repeater ID="rptActivityStats" runat="server">
                    <ItemTemplate>
                        <div class="row-fluid">
                            <div class="span5 grid">
                                <table cellspacing="0" cellpadding="0"  width="100%">
                                    <tr class="grid-col">
                                        <td width="90%" class="lef-col">UCRs Added </td>
                                        <td width="10%" class="right-dic">
                                            <%--<%# String.Format("{0:#,###}", Convert.ToString(Eval("UCRAdded")))%>--%>
                                            <asp:HyperLink ID="hylUCRAdded" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=UCRA" Target="_blank" Enabled='<%# EnabledDisabledLink(Eval("UCRAdded")) %>'>
                                            <%# Convert.ToString(Eval("UCRAdded")) == "0" ? "0" : String.Format("{0:#,###}", Eval("UCRAdded")) %>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">UCRs Modified </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Convert.ToString(Eval("UCRModified")))%>--%>
                                           <asp:HyperLink ID="hylUCRModified" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=UCRM" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("UCRModified")) %>'>
                                           <%# Convert.ToString(Eval("UCRModified")) == "0" ? "0" : String.Format("{0:#,###}", Eval("UCRModified"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">Regular Lane Modified </td>
                                        <td class="right-dic">
                                            <%--<%# String.Format("{0:#,###}", Convert.ToString(Eval("RegularLaneModified")))%>--%>
                                            <asp:HyperLink ID="hylRegularLaneModified" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=RLM" Target="_blank" Enabled='<%# EnabledDisabledLink(Eval("RegularLaneModified")) %>'>
                                             <%# Convert.ToString(Eval("RegularLaneModified")) == "0" ? "0" : String.Format("{0:#,###}", Eval("RegularLaneModified"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">Exotic Lane Modified </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Convert.ToString(Eval("ExoticLaneModified")))%>--%>
                                            <asp:HyperLink ID="hylExoticLaneModified" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=ExLM" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("ExoticLaneModified")) %>'>
                                              <%# Convert.ToString(Eval("ExoticLaneModified")) == "0" ? "0" : String.Format("{0:#,###}", Eval("ExoticLaneModified"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">Inventory accessed </td>
                                        <td class="right-dic">
                                              <%--  <%# String.Format("{0:#,###}", Convert.ToString(Eval("InventoryAccessed")))%>--%>
                                              <asp:HyperLink ID="hylInventoryAccessed" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=InvAcc" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("InventoryAccessed")) %>'>
                                              <%# Convert.ToString(Eval("InventoryAccessed")) == "0" ? "0" : String.Format("{0:#,###}", Eval("InventoryAccessed"))%>
                                              </asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="span5 grid" style="width:65%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. Of times Title Present changed from 'Yes' to 'No'
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Convert.ToString(Eval("NoOfTimesTitlePresentChangedFromYesToNo")))%>--%>
                                            <asp:HyperLink ID="hylNoOfTimesTitlePresentChangedFromYesToNo" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TPYN" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfTimesTitlePresentChangedFromYesToNo")) %>'>
                                           <%# Convert.ToString(Eval("NoOfTimesTitlePresentChangedFromYesToNo")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfTimesTitlePresentChangedFromYesToNo"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. Of times Title Present changed from 'No' to 'Yes'
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%# String.Format("{0:#,###}", Convert.ToString(Eval("NoOfTimesTitlePresentChangedFromNoToYes")))%>--%>
                                           <asp:HyperLink ID="hylNoOfTimesTitlePresentChangedFromNoToYes" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TPNY" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfTimesTitlePresentChangedFromNoToYes")) %>'>
                                            <%# Convert.ToString(Eval("NoOfTimesTitlePresentChangedFromNoToYes")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfTimesTitlePresentChangedFromNoToYes"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. Of times Dup. Title changed from 'Yes' to 'No'
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Convert.ToString(Eval("NoOfTimesDupTitleChangedFromYesToNo")))%>--%>
                                            <asp:HyperLink ID="hylNoOfTimesDupTitleChangedFromYesToNo" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=DTYN" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfTimesDupTitleChangedFromYesToNo")) %>'>
                                             <%# Convert.ToString(Eval("NoOfTimesDupTitleChangedFromYesToNo")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfTimesDupTitleChangedFromYesToNo"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. Of times Dup. Title changed from 'No' to 'Yes'
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Convert.ToString(Eval("NoOfTimesDupTitleChangedFromNoToYes")))%>--%>
                                           <asp:HyperLink ID="hylNoOfTimesDupTitleChangedFromNoToYes" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=DTNY" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfTimesDupTitleChangedFromNoToYes")) %>'>
                                           <%# Convert.ToString(Eval("NoOfTimesDupTitleChangedFromNoToYes")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfTimesDupTitleChangedFromNoToYes"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. Of times Title Shipped changed from 'Yes' to 'No'
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Convert.ToString(Eval("NoOfTimesTitleShippedChangedFromYesToNo")))%>--%>
                                            <asp:HyperLink ID="hylNoOfTimesTitleShippedChangedFromYesToNo" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TSYN" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfTimesTitleShippedChangedFromYesToNo")) %>'>
                                            <%# Convert.ToString(Eval("NoOfTimesTitleShippedChangedFromYesToNo")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfTimesTitleShippedChangedFromYesToNo"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. Of times Title Shipped changed from 'No' to 'Yes'
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Convert.ToString(Eval("NoOfTimesTitleShippedChangedFromNoToYes")))%>--%>
                                           <asp:HyperLink ID="hylNoOfTimesTitleShippedChangedFromNoToYes" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TSNY" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfTimesTitleShippedChangedFromNoToYes")) %>'>
                                          <%# Convert.ToString(Eval("NoOfTimesTitleShippedChangedFromNoToYes")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfTimesTitleShippedChangedFromNoToYes"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 grid">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. Of cars added in the Inventory
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%#Eval("CarsAdded")%>--%>
                                           <asp:HyperLink ID="hylCarsAdded" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CAI" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("CarsAdded")) %>'>
                                          <%# Convert.ToString(Eval("CarsAdded")) == "0" ? "0" : String.Format("{0:#,###}", Eval("CarsAdded"))%>
                                            </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. Of cars deleted in the Inventory
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("CarsDeleted")%>--%>
                                            <asp:HyperLink ID="hylCarsDeleted" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CDI" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("CarsDeleted")) %>'>
                                              <%# Convert.ToString(Eval("CarsDeleted")) == "0" ? "0" : String.Format("{0:#,###}", Eval("CarsDeleted"))%>
                                            </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            Value Of cars added in the Inventory
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# Convert.ToString(Eval("TotalCarCost") == null ? "0" : String.Format("${0:#,###}", Eval("TotalCarCost")))%>--%>
                                           <asp:HyperLink ID="hylTCC" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TCC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("TotalCarCost")) %>'>
                                             <%# Convert.ToString(Eval("TotalCarCost")) == "" ? "0" : String.Format("${0:#,###}", Eval("TotalCarCost"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Value Of cars deleted in the Inventory
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# Convert.ToString(Eval("DeletedCarCost") == null ? "0" : String.Format("${0:#,###}", Eval("DeletedCarCost")))%>--%>
                                            <asp:HyperLink ID="hylDCC" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=DCC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("DeletedCarCost")) %>'>
                                               <%# Convert.ToString(Eval("DeletedCarCost")) == "" ? "0" : String.Format("${0:#,###}", Eval("DeletedCarCost"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. Of times changed to 'Here'
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("NoOfTimesChangedToHere")%>--%>
                                            <asp:HyperLink ID="hylNoOfTimesChangedToHere" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CH" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfTimesChangedToHere")) %>'>
                                                 <%# Convert.ToString(Eval("NoOfTimesChangedToHere")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfTimesChangedToHere"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. Of times changed to 'Not Here'
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("NoOfTimesChangedToNotHere")%>--%>
                                            <asp:HyperLink ID="hylNoOfTimesChangedToNotHere" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CNH" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfTimesChangedToNotHere")) %>'>
                                               <%# Convert.ToString(Eval("NoOfTimesChangedToNotHere")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfTimesChangedToNotHere"))%>
                                            </asp:HyperLink>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="span5 grid">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of Good CarFax
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("GoodCarFax")%>--%>
                                           <asp:HyperLink ID="hylGoodCarFax" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=GCF" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("GoodCarFax")) %>'>
                                                <%# Convert.ToString(Eval("GoodCarFax")) == "0" ? "0" : String.Format("{0:#,###}", Eval("GoodCarFax"))%>
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. of Bad CarFax
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("BadCarFax")%>--%>
                                           <asp:HyperLink ID="hylBadCarFax" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=BCF" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("BadCarFax")) %>'>
                                                  <%# Convert.ToString(Eval("BadCarFax")) == "0" ? "0" : String.Format("{0:#,###}", Eval("BadCarFax"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of Unknown CarFax
                                        </td>
                                        <td class="right-dic">
                                         <%--   <%#Eval("UnknownCarFax")%>--%>
                                         <asp:HyperLink ID="hylUnknownCarFax" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=UCF" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("UnknownCarFax")) %>'>
                                               <%# Convert.ToString(Eval("UnknownCarFax")) == "0" ? "0" : String.Format("{0:#,###}", Eval("UnknownCarFax"))%>
                                         </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. of Good AutoCheck
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("GoodAutoCheck")%>--%>

                                            <asp:HyperLink ID="hylGoodAutoCheck" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=GAC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("GoodAutoCheck")) %>'>
                                                <%# Convert.ToString(Eval("GoodAutoCheck")) == "0" ? "0" : String.Format("{0:#,###}", Eval("GoodAutoCheck"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of Bad AutoCheck
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%#Eval("BadAutoCheck")%>--%>
                                          <asp:HyperLink ID="hylBadAutoCheck" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=BAC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("BadAutoCheck")) %>'>
                                                <%# Convert.ToString(Eval("BadAutoCheck")) == "0" ? "0" : String.Format("{0:#,###}", Eval("BadAutoCheck"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. of Unknown AutoCheck
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("UnknownAutoCheck")%>--%>
                                             <asp:HyperLink ID="hylUnknownAutoCheck" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=UAC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("UnknownAutoCheck")) %>'>
                                               <%# Convert.ToString(Eval("UnknownAutoCheck")) == "0" ? "0" : String.Format("{0:#,###}", Eval("UnknownAutoCheck"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="span5 grid">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            Expenses Added
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("ExpenseAdded") %>--%>
                                            <asp:HyperLink ID="hylExpenseAdded" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=EA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("ExpenseAdded")) %>'>
                                               <%# Convert.ToString(Eval("ExpenseAdded")) == "0" ? "0" : String.Format("{0:#,###}", Eval("ExpenseAdded"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Expenses Deleted
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("ExpenseDeleted")%>--%>
                                           <asp:HyperLink ID="hylExpenseDeleted" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=ED" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("ExpenseDeleted")) %>'>
                                               <%# Convert.ToString(Eval("ExpenseDeleted")) == "0" ? "0" : String.Format("{0:#,###}", Eval("ExpenseDeleted"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            Value of Expenses Added
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# Convert.ToString(Eval("TotalExpenseAmount") == null ? "0" : String.Format("${0:#,###}", Eval("TotalExpenseAmount")))%>--%>
                                            <asp:HyperLink ID="hylVEA" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=VEA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("TotalExpenseAmount")) %>'>
                                               <%# Convert.ToString(Eval("TotalExpenseAmount") == "" ? "0" : String.Format("${0:#,###}", Eval("TotalExpenseAmount")))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Value of Expenses Deleted
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%# Convert.ToString(Eval("DeletedExpenseAmount") == null ? "0" : String.Format("${0:#,###}", Eval("DeletedExpenseAmount")))%>--%>
                                           <asp:HyperLink ID="hylVED" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=VED" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("DeletedExpenseAmount")) %>'>
                                             <%# Convert.ToString(Eval("DeletedExpenseAmount") == "" ? "0" : String.Format("${0:#,###}", Eval("DeletedExpenseAmount")))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 grid">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. Of cars came back to Inventory
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("NoOfComeBackCars")%>--%>
                                            <asp:HyperLink ID="hylNoOfComeBackCars" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CCBI" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfComeBackCars")) %>'>
                                                 <%# Convert.ToString(Eval("NoOfComeBackCars")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfComeBackCars"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. Of cars came back and deleted in the Inventory
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("NoOfDeletedComeBackCars")%>--%>
                                            <asp:HyperLink ID="hylNoOfDeletedComeBackCars" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CCBDI" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfDeletedComeBackCars")) %>'>
                                              <%# Convert.ToString(Eval("NoOfDeletedComeBackCars")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfDeletedComeBackCars"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            Total Value Of comeback cars
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# Convert.ToString(Eval("TotalValueOfComeBackCars") == null ? "0" : String.Format("${0:#,###}", Eval("TotalValueOfComeBackCars")))%>--%>
                                            <asp:HyperLink ID="hylTotalValueOfComeBackCars" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TVCC" Target="_blank" Enabled='<%# EnabledDisabledLink(Eval("TotalValueOfComeBackCars")) %>'>
                                              <%# Convert.ToString(Eval("TotalValueOfComeBackCars") == "" ? "0" : String.Format("${0:#,###}", Eval("TotalValueOfComeBackCars")))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Total Value Of deleted comeback cars
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# Convert.ToString(Eval("TotalValueOfDeletedComeBackCars") == null ? "0" : String.Format("${0:#,###}", Eval("TotalValueOfDeletedComeBackCars")))%>--%>
                                           <asp:HyperLink ID="hylTotalValueOfDeletedComeBackCars" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TVDCC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("TotalValueOfDeletedComeBackCars")) %>'>
                                               <%# Convert.ToString(Eval("TotalValueOfDeletedComeBackCars") == "" ? "0" : String.Format("${0:#,###}", Eval("TotalValueOfDeletedComeBackCars")))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="span5 grid">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            Deposits added
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("DepositAdded") %>--%>
                                            <asp:HyperLink ID="hylDepositAdded" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=DA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("DepositAdded")) %>'>
                                                <%# Convert.ToString(Eval("DepositAdded")) == "0" ? "0" : String.Format("{0:#,###}", Eval("DepositAdded"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Deposits deleted
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%#Eval("DepositDeleted")%>--%>
                                           <asp:HyperLink ID="hylDepositDeleted" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=DD" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("DepositDeleted")) %>'>
                                                <%# Convert.ToString(Eval("DepositDeleted")) == "0" ? "0" : String.Format("{0:#,###}", Eval("DepositDeleted"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            Total deposit amount
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# Convert.ToString(Eval("TotalDepositAmount") == null ? "0" : String.Format("${0:#,###}", Eval("TotalDepositAmount")))%>--%>
                                           <asp:HyperLink ID="hylTotalDepositAmount" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TDA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("TotalDepositAmount")) %>'>
                                              <%# Convert.ToString(Eval("TotalDepositAmount") == "" ? "0" : String.Format("${0:#,###}", Eval("TotalDepositAmount")))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Total deposit change***
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# Convert.ToString(Eval("TotalDepositAmount") == null ? "0" : String.Format("${0:#,###}", Eval("TotalDepositAmount")))%>--%>
                                           <asp:HyperLink ID="hylTotalDepositChange" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TDC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("TotalDepositAmount")) %>'>
                                              <%# Convert.ToString(Eval("TotalDepositAmount") == "" ? "0" : String.Format("${0:#,###}", Eval("TotalDepositAmount")))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="span5 grid">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of cars sold
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Eval("NoOfCarsSold"))%>--%>
                                            <asp:HyperLink ID="hylNoOfCarsSold" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CS" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfCarsSold")) %>'>
                                               <%# Convert.ToString(Eval("NoOfCarsSold")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfCarsSold"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. of cars not sold
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Eval("NoOfCarsNotSold"))%>--%>
                                           <asp:HyperLink ID="hylNoOfCarsNotSold" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CNS" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfCarsNotSold")) %>'>
                                             <%# Convert.ToString(Eval("NoOfCarsNotSold")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfCarsNotSold"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            Total amount of sold cars
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Eval("AmountOfCarsSold"))%>--%>
                                            <asp:HyperLink ID="hylAmountOfCarsSold" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TASC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("AmountOfCarsSold")) %>'>
                                              <%# Convert.ToString(Eval("AmountOfCarsSold")) == "" ? "0" : String.Format("${0:#,###}", Eval("AmountOfCarsSold"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Total amount of not sold cars
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Eval("AmountOfCarsNotSold"))%>--%>
                                           <asp:HyperLink ID="hylAmountOfCarsNotSold" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TANSC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("AmountOfCarsNotSold")) %>'>
                                           <%# Convert.ToString(Eval("AmountOfCarsNotSold")) == "" ? "0" : String.Format("${0:#,###}", Eval("AmountOfCarsNotSold"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 grid">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            Cars registered with MAA
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# Convert.ToString(Eval("MAA_CarsRegistered")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_CarsRegistered"))%>--%>
                                            <asp:HyperLink ID="hylMAA_CarsRegistered" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CRMAA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("MAA_CarsRegistered")) %>'>
                                           <%# Convert.ToString(Eval("MAA_CarsRegistered")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_CarsRegistered"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Total value of cars registerd with MAA
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%# Convert.ToString(Eval("MAA_TotalValueOfCarsReg")) == "" ? "0" : String.Format("${0:#,###}", Eval("MAA_TotalValueOfCarsReg"))%>--%>
                                          <asp:HyperLink ID="hylMAA_TotalValueOfCarsReg" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TVCRMAA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("MAA_TotalValueOfCarsReg")) %>'>
                                           <%# Convert.ToString(Eval("MAA_TotalValueOfCarsReg")) == "" ? "0" : String.Format("${0:#,###}", Eval("MAA_TotalValueOfCarsReg"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of cars acknowledged
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%# Convert.ToString(Eval("MAA_NoOfCarsAcknowledged")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsAcknowledged"))%>--%>
                                           <asp:HyperLink ID="hylMAA_NoOfCarsAcknowledged" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CAck" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("MAA_NoOfCarsAcknowledged")) %>'>
                                          <%# Convert.ToString(Eval("MAA_NoOfCarsAcknowledged")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsAcknowledged"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. of cars confirmed
                                        </td>
                                        <td class="right-dic">
                                         <%--   <%# Convert.ToString(Eval("MAA_NoOfCarsConfirmed")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsConfirmed"))%>--%>
                                          <asp:HyperLink ID="hylMAA_NoOfCarsConfirmed" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CCon" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("MAA_NoOfCarsConfirmed")) %>'>
                                          <%# Convert.ToString(Eval("MAA_NoOfCarsConfirmed")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsConfirmed"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of cars failed
                                        </td>
                                        <td class="right-dic">
                                         <%--   <%# Convert.ToString(Eval("MAA_NoOfCarsFailed")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsFailed"))%>--%>
                                         <asp:HyperLink ID="hylMAA_NoOfCarsFailed" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CFailed" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("MAA_NoOfCarsFailed")) %>'>
                                           <%# Convert.ToString(Eval("MAA_NoOfCarsFailed")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsFailed"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. of cars cancelled
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%# Convert.ToString(Eval("MAA_NoOfCarsCancelled")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsCancelled"))%>--%>
                                           <asp:HyperLink ID="hylMAA_NoOfCarsCancelled" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CCancel" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("MAA_NoOfCarsCancelled")) %>'>
                                            <%# Convert.ToString(Eval("MAA_NoOfCarsCancelled")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsCancelled"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of cars sold
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%# Convert.ToString(Eval("MAA_NoOfCarsSold")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsSold"))%>--%>
                                           <asp:HyperLink ID="hylMAA_NoOfCarsSold" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CSold" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("MAA_NoOfCarsSold")) %>'>
                                             <%# Convert.ToString(Eval("MAA_NoOfCarsSold")) == "0" ? "0" : String.Format("{0:#,###}", Eval("MAA_NoOfCarsSold"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="span5 grid">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            DocumentAdded
                                        </td>
                                        <td class="right-dic">
                                            <%--<%#Eval("DocumentAdded") %>--%>
                                            <asp:HyperLink ID="hylDocumentAdded" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=DocA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("DocumentAdded")) %>'>
                                              <%# Convert.ToString(Eval("DocumentAdded")) == "0" ? "0" : String.Format("{0:#,###}", Eval("DocumentAdded"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Document Deleted
                                        </td>
                                        <td class="right-dic">
                                            <%--<%#Eval("DocumentDeleted")%>--%>
                                             <asp:HyperLink ID="hylDocumentDeleted" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=DocD" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("DocumentDeleted")) %>'>
                                              <%# Convert.ToString(Eval("DocumentDeleted")) == "0" ? "0" : String.Format("{0:#,###}", Eval("DocumentDeleted"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            Commission Added
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("CommissionAdded") %>--%>
                                           <asp:HyperLink ID="hylCommissionAdded" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=ComA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("CommissionAdded")) %>'>
                                              <%# Convert.ToString(Eval("CommissionAdded")) == "0" ? "0" : String.Format("{0:#,###}", Eval("CommissionAdded"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Total Commission Amount
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%#Eval("TotalCommissionAmt")%>--%>
                                            <asp:HyperLink ID="hylTotalCommissionAmt" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TComA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("TotalCommissionAmt")) %>'>
                                              <%# Convert.ToString(Eval("TotalCommissionAmt")) == "" ? "0" : String.Format("${0:#,###}", Eval("TotalCommissionAmt"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of Late Title applied
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%# Convert.ToString(Eval("NoOfLateTitleApplied")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfLateTitleApplied"))%>--%>
                                         <asp:HyperLink ID="hylNoOfLateTitleApplied" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=LTA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("NoOfLateTitleApplied")) %>'>
                                               <%# Convert.ToString(Eval("NoOfLateTitleApplied")) == "0" ? "0" : String.Format("{0:#,###}", Eval("NoOfLateTitleApplied"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Total Late Title fee
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%# Convert.ToString(Eval("TotalLateTitleFee")) == "" ? "0" : String.Format("${0:#,###}", Eval("TotalLateTitleFee"))%>--%>
                                          <asp:HyperLink ID="hylTotalLateTitleFee" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=LTF" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("TotalLateTitleFee")) %>'>
                                              <%# Convert.ToString(Eval("TotalLateTitleFee")) == "" ? "0" : String.Format("${0:#,###}", Eval("TotalLateTitleFee"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="span5 grid">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of checks written
                                        </td>
                                        <td class="right-dic">
                                           <%-- <%# String.Format("{0:#,###}", Convert.ToString(Eval("CheckWritten")))%>--%>
                                           <asp:HyperLink ID="hylCheckWritten" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=CW" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("CheckWritten")) %>'>
                                            <%# Convert.ToString(Eval("CheckWritten")) == "0" ? "0" : String.Format("{0:#,###}", Eval("CheckWritten"))%>
                                           </asp:HyperLink>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            Total amount of checks
                                        </td>
                                        <td class="right-dic">
                                          <%--  <%# Convert.ToString(Eval("TotalCheckAmount") == null ? "0" : String.Format("${0:#,###}", Eval("TotalCheckAmount")))%>--%>
                                          <asp:HyperLink ID="hylTotalCheckAmount" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=TAC" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("TotalCheckAmount")) %>'>
                                            <%# Convert.ToString(Eval("TotalCheckAmount") == "" ? "0" : String.Format("${0:#,###}", Eval("TotalCheckAmount")))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="grid-col">
                                        <td class="lef-col">
                                            No. of VIN search per user
                                        </td>
                                        <td class="right-dic">
                                             <%-- <%# Convert.ToString(Eval("VINSearchPerUser")) == "0" ? "0" : String.Format("{0:#,###}", Eval("VINSearchPerUser"))%>--%>
                                             <asp:HyperLink ID="hylVINSearchPerUser" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=VINSPU" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("VINSearchPerUser")) %>'>
                                            <%# Convert.ToString(Eval("VINSearchPerUser")) == "0" ? "0" : String.Format("{0:#,###}", Eval("VINSearchPerUser"))%>
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lef-col">
                                            No. of Unique VIN search***
                                        </td>
                                        <td class="right-dic">
                                            000000
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                </div>        
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Repeater ID="rptLocationStats" runat="server">
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="location-box" width="100%">
                    <tr class="header-tag">
                        <td align="center">Unresolved Cars Stats</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0" class="sub-tab" style="border-left:1px solid #E0E3E7">
                                <tr>
                                    <td class="lef-col">No of cars unresolved</td>
                                    <td class="right-dic" style="padding-right:2px">
                                    <%-- <%#Eval("UnresolvedCars")%>--%>
                                     <asp:HyperLink ID="hylUnresolvedCars" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=UnresolvedCars" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("UnresolvedCars")) %>'>
                                      <%# Convert.ToString(Eval("UnresolvedCars")) == "0" ? "0" : String.Format("{0:#,###}", Eval("UnresolvedCars"))%>
                                     </asp:HyperLink>
                                    </td>
                                    <td class="lef-col">No of cars 3 years old</td>
                                    <td class="right-dic">
                                    <%-- <%#Eval("3YearsOld")%>--%>
                                     <asp:HyperLink ID="hyl3YearsOld" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=3YearsOld" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("3YearsOld")) %>'>
                                     <%# Convert.ToString(Eval("3YearsOld")) == "0" ? "0" : String.Format("{0:#,###}", Eval("3YearsOld"))%>
                                     </asp:HyperLink>
                                    </td>
                                    <td class="lef-col">No of cars 2.5 years old</td>
                                    <td class="right-dic">
                                      <%--<%#Eval("30MonthsOld")%>--%>
                                       <asp:HyperLink ID="hyl30MonthsOld" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=30MonthsOld" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("30MonthsOld")) %>'>
                                       <%# Convert.ToString(Eval("30MonthsOld")) == "0" ? "0" : String.Format("{0:#,###}", Eval("30MonthsOld"))%>
                                     </asp:HyperLink>

                                    </td>
                                </tr>
                                <tr class="grid-col">
                                    <td class="lef-col">No of cars 2 years old</td>
                                    <td class="right-dic">
                                     <%-- <%#Eval("2YearsOld")%>--%>
                                       <asp:HyperLink ID="hyl2YearsOld" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=2YearsOld" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("2YearsOld")) %>'>
                                       <%# Convert.ToString(Eval("2YearsOld")) == "0" ? "0" : String.Format("{0:#,###}", Eval("2YearsOld"))%>
                                     </asp:HyperLink>
                                    </td>
                                    <td class="lef-col">No of cars 1 year old</td>
                                    <td class="right-dic">
                                    <%--  <%#Eval("1YearOld")%>--%>
                                     <asp:HyperLink ID="hyl1YearOld" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=1YearOld" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("1YearOld")) %>'>
                                      <%# Convert.ToString(Eval("1YearOld")) == "0" ? "0" : String.Format("{0:#,###}", Eval("1YearOld"))%>
                                     </asp:HyperLink>

                                    </td>
                                    <td class="lef-col">No of cars 6 months old</td>
                                    <td class="right-dic">
                                     <%-- <%#Eval("6MonthsOld")%>--%>
                                     <asp:HyperLink ID="hyl6MonthsOld" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=6MonthsOld" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("6MonthsOld")) %>'>
                                       <%# Convert.ToString(Eval("6MonthsOld")) == "0" ? "0" : String.Format("{0:#,###}", Eval("6MonthsOld"))%>
                                     </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lef-col">No of cars 3 months old</td>
                                    <td class="right-dic">
                                     <%-- <%#Eval("3MonthsOld")%>--%>
                                      <asp:HyperLink ID="hyl3MonthsOld" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=3MonthsOld" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("3MonthsOld")) %>'>
                                       <%# Convert.ToString(Eval("3MonthsOld")) == "0" ? "0" : String.Format("{0:#,###}", Eval("3MonthsOld"))%>
                                     </asp:HyperLink>
                                    </td>
                                    <td colspan="6">&nbsp;</td>
                                </tr>
                            </table> 
                        </td>
                    </tr>
                </table>
                <br />
                <table border="0" cellpadding="0" cellspacing="0" class="location-box" width="100%">
                    <tr class="header-tag">
                        <td colspan="3" align="center"> Location Stats</td>
                    </tr>
                    <tr class="sub-line">
                        <td>Today</td>
                        <td>This week</td>
                        <td>All</td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0" class="sub-tab" style="border-left:1px solid #E0E3E7">
                                <tr>
                                    <td width="90%" class="lef-col">Active cars In-Transit</td>
                                    <td width="10%" class="right-dic">
                                      <%--<%#Eval("Today_ActiveInTransit")%>--%>
                                      <asp:HyperLink ID="hylToday_ActiveInTransit" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=Today_ACIT" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("Today_ActiveInTransit")) %>'>
                                       <%# Convert.ToString(Eval("Today_ActiveInTransit")) == "0" ? "0" : String.Format("{0:#,###}", Eval("Today_ActiveInTransit"))%>
                                      </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr class="grid-col">
                                    <td width="90%" class="lef-col">Car at office 1113</td>
                                    <td width="10%" class="right-dic">
                                      <%--<%#Eval("Today_Office1133")%>--%>
                                      <asp:HyperLink ID="hylToday_Office1133" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=Today_CO1133" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("Today_Office1133")) %>'>
                                        <%# Convert.ToString(Eval("Today_Office1133")) == "0" ? "0" : String.Format("{0:#,###}", Eval("Today_Office1133"))%>
                                      </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="90%" class="lef-col">Car at office 1373</td>
                                    <td width="10%" class="right-dic">
                                      <%--<%#Eval("Today_Office1373")%>--%>
                                       <asp:HyperLink ID="hylToday_Office1373" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=Today_CO1373" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("Today_Office1373")) %>'>
                                        <%# Convert.ToString(Eval("Today_Office1373")) == "0" ? "0" : String.Format("{0:#,###}", Eval("Today_Office1373"))%>
                                      </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr class="grid-col">
                                    <td width="90%" class="lef-col">Cars at MAA</td>
                                    <td width="10%" class="right-dic">
                                     <%--<%#Eval("Today_MAAToday")%>--%>
                                     <asp:HyperLink ID="hylToday_MAAToday" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=Today_CMAA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("Today_MAAToday")) %>'>
                                       <%# Convert.ToString(Eval("Today_MAAToday")) == "0" ? "0" : String.Format("{0:#,###}", Eval("Today_MAAToday"))%>
                                      </asp:HyperLink>

                                    </td>
                                </tr>
                                <tr>
                                    <td width="90%" class="lef-col">&nbsp;</td>
                                    <td width="10%" class="right-dic">
                                        <asp:HyperLink ID="hlnkMoreToday" runat="server" Text="more..." NavigateUrl="ManageLocation.aspx" Target="_blank" />
                                    </td>
                                </tr>
                        </table>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0" class="sub-tab">
                                <tr>
                                    <td width="90%" class="lef-col">Active cars In-Transit</td>
                                    <td width="10%" class="right-dic">
                                     <%--<%#Eval("Week_ActiveInTransit")%>--%>
                                      <asp:HyperLink ID="hylWeek_ActiveInTransit" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=Week_ACIT" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("Week_ActiveInTransit")) %>'>
                                     <%# Convert.ToString(Eval("Week_ActiveInTransit")) == "0" ? "0" : String.Format("{0:#,###}", Eval("Week_ActiveInTransit"))%>
                                      </asp:HyperLink>

                                    </td>
                                </tr>
                                <tr class="grid-col">
                                    <td width="90%" class="lef-col">Car at office 1113</td>
                                    <td width="10%" class="right-dic">
                                      <%-- <%#Eval("Week_Office1133")%>--%>
                                       <asp:HyperLink ID="hylWeek_Office1133" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=Week_CO1133" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("Week_Office1133")) %>'>
                                        <%# Convert.ToString(Eval("Week_Office1133")) == "0" ? "0" : String.Format("{0:#,###}", Eval("Week_Office1133"))%>
                                      </asp:HyperLink>

                                    </td>
                                </tr>
                                <tr>
                                    <td width="90%" class="lef-col">Car at office 1373</td>
                                    <td width="10%" class="right-dic">
                                   <%-- <%#Eval("Week_Office1373")%>--%>
                                    <asp:HyperLink ID="hylWeek_Office1373" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=Week_CO1373" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("Week_Office1373")) %>'>
                                       <%# Convert.ToString(Eval("Week_Office1373")) == "0" ? "0" : String.Format("{0:#,###}", Eval("Week_Office1373"))%>
                                     </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr class="grid-col">
                                    <td width="90%" class="lef-col">Cars at MAA</td>
                                    <td width="10%" class="right-dic">
                                    <%--<%#Eval("Week_MAA")%>--%>
                                    <asp:HyperLink ID="hylWeek_MAA" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=Week_CMAA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("Week_MAA")) %>'>
                                       <%# Convert.ToString(Eval("Week_MAA")) == "0" ? "0" : String.Format("{0:#,###}", Eval("Week_MAA"))%>
                                     </asp:HyperLink>

                                    </td>
                                </tr>
                                <tr>
                                    <td width="90%" class="lef-col">&nbsp;</td>
                                    <td width="10%" class="right-dic">
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text="more..." NavigateUrl="ManageLocation.aspx" Target="_blank" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0" class="sub-tab">
                                <tr>
                                    <td width="90%" class="lef-col">Active cars In-Transit</td>
                                    <td width="10%" class="right-dic">
                                    <%--<%#Eval("ALL_ActiveInTransit")%>--%>
                                    <asp:HyperLink ID="hylALL_ActiveInTransit" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=All_ACIT" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("ALL_ActiveInTransit")) %>'>
                                       <%# Convert.ToString(Eval("ALL_ActiveInTransit")) == "0" ? "0" : String.Format("{0:#,###}", Eval("ALL_ActiveInTransit"))%>
                                     </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr class="grid-col">
                                    <td width="90%" class="lef-col">Car at office 1113</td>
                                    <td width="10%" class="right-dic">
                                     <%-- <%#Eval("ALL_Office1133")%>--%>
                                     <asp:HyperLink ID="hylALL_Office1133" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=All_CO1133" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("ALL_Office1133")) %>'>
                                     <%# Convert.ToString(Eval("ALL_Office1133")) == "0" ? "0" : String.Format("{0:#,###}", Eval("ALL_Office1133"))%>
                                     </asp:HyperLink>

                                    </td>
                                </tr>
                                <tr>
                                    <td width="90%" class="lef-col">Car at office 1373</td>
                                    <td width="10%" class="right-dic">
                                    <%--<%#Eval("ALL_Office1373")%>--%>
                                     <asp:HyperLink ID="hylALL_Office1373" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=All_CO1373" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("ALL_Office1373")) %>'>
                                       <%# Convert.ToString(Eval("ALL_Office1373")) == "0" ? "0" : String.Format("{0:#,###}", Eval("ALL_Office1373"))%>
                                     </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr class="grid-col">
                                    <td width="90%" class="lef-col">Cars at MAA</td>
                                    <td width="10%" class="right-dic">
                                     <%-- <%#Eval("ALL_MAA")%>--%>
                                     <asp:HyperLink ID="hylALL_MAA" runat="server" NavigateUrl="ActivitystatsDetail.aspx?Code=All_CMAA" Target="_blank"  Enabled='<%# EnabledDisabledLink(Eval("ALL_MAA")) %>'>
                                        <%# Convert.ToString(Eval("ALL_MAA")) == "0" ? "0" : String.Format("{0:#,###}", Eval("ALL_MAA"))%>
                                     </asp:HyperLink>

                                    </td>
                                </tr>
                                <tr>
                                    <td width="90%" class="lef-col">&nbsp;</td>
                                    <td width="10%" class="right-dic">
                                        <asp:HyperLink ID="HyperLink2" runat="server" Text="more..." NavigateUrl="ManageLocation.aspx" Target="_blank" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <script type="text/javascript">
        function pageLoad() {
            showHideDatePicker();
        }
        
        function showHideDatePicker() {
            var sortFilter = $('#<%=ddlSortFilter.ClientID %>').val();                       
            if (sortFilter == "8")
                $('.tdhideclass1').show();
            else
                $('.tdhideclass1').hide();
        }


    </script>
 </asp:Content>
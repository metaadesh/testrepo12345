<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HeadStartVMS.UI.HomePage"
    Title="HeadstartVMS::Home Page" MasterPageFile="~/UI/MasterPage.Master" %>

<%@ MasterType VirtualPath="~/UI/MasterPage.Master" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphDefault" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td align="left" valign="top" class="myContainer">
                <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="center">
                            <asp:HyperLink NavigateUrl="ValidateVin.aspx" ID="hlnkAddInventoryimg" runat="server"
                                ToolTip="Click to add inventory">
                        <%--<img border="0" src="../images/Add-Inventory-Icon.gif" width="52" height="54" alt="" />--%>
                        <img border="0"  src="../Images/img_trans.gif" width="1" height="1" class="inventory-icon" alt="" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="ValidateVin.aspx" ID="hlnkAddInventoryHead" runat="server"
                                class="ContainerHaeding" Style="text-decoration: none;">
                        Add Inventory</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="90" valign="top">
                            <div class="DivTxt_Normal">
                                This section will provide you to add new car into your inventory. With type of car,
                                manufacturer details, facilities in the car as well as price details...</div>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="ValidateVin.aspx" ID="hlnkAddInventorytxt" runat="server"
                                class="OrangeText_Link">Click 
                     here to add a new Car</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="AddCustomMMB.aspx" ID="HyperLink1" runat="server"
                                class="OrangeText_Link">Click here to add a Custom MMB</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="24" align="left" valign="top">
                &nbsp;
            </td>
            <td align="left" valign="top" class="myContainer">
                <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="center">
                            <asp:HyperLink NavigateUrl="AccountsPayable.aspx" ID="hlnkAccountPayableimg" runat="server"
                                title="Click to view accounts payable">
                         <%--<img border="0" src="../images/Account-Payable.gif" width="52" height="54" alt="" />--%>
                         <img border="0"  src="../Images/img_trans.gif" width="1" height="1" class="account-icon" alt="" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="AccountsPayable.aspx" ID="hlnkAccountPayableHead" runat="server"
                                class="ContainerHaeding" Style="text-decoration: none;">
                     Account Payable</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="90" valign="top">
                            <div class="DivTxt_Normal">
                                This section allows you to print a new utility check or check for the commissions,
                                expenses, car costs etc.</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="MakeANewPayment.aspx" ID="hlnkNewPayment" runat="server"
                                class="OrangeText_Link">Click 
                     here to add new Payment</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="AccountsPayable.aspx" ID="hlnkAccountPayable" runat="server"
                                class="OrangeText_Link">Click 
                     here to view accounts payable</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="Payments.aspx" ID="lnkAllPayments" runat="server"
                                class="OrangeText_Link">Click 
                     here to view all Payment</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="24" align="left" valign="top">
                &nbsp;
            </td>
            <td align="left" valign="top" class="myContainer">
                <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="center">
                            <asp:HyperLink NavigateUrl="InventorySearch.aspx" runat="server" ID="hlnkInventorySearchimg"
                                title="Click to search inventory">
                         <%--<img border="0" src="../images/Search-Icon.gif" width="52" height="54" alt="" />--%>
                         <img border="0"  src="../Images/img_trans.gif" width="1" height="1" class="search-icon" alt="" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="InventorySearch.aspx" runat="server" ID="hlnkInventorySearchHead"
                                class="ContainerHaeding" Style="text-decoration: none;">
                        Search</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="90" valign="top">
                            <div class="DivTxt_Normal">
                                This section provides you functions to perform search on Inventory and Payments...</div>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;
                            <asp:HyperLink NavigateUrl="InventorySearch.aspx" runat="server" ID="hlnkInventorySearchtxt"
                                class="OrangeText_Link">Click 
                     here to search Inventory</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="SearchPayment.aspx" ID="hlnkSearchPaymenttxt" runat="server"
                                class="OrangeText_Link">Click 
                     here to search Payments</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" colspan="5" height="25">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" class="myContainer">
                <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="center">
                            <asp:HyperLink NavigateUrl="InventoryList.aspx" ID="hlnkManageInvenoryimg" runat="server"
                                title="Click to manage inventory">
                        <%--<img border="0" src="../images/Manage-Inventory-Icon.gif" width="52" height="54" alt="" />--%>
                        <img border="0"  src="../Images/img_trans.gif" width="1" height="1" class="manage-icon" alt="" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="InventoryList.aspx" ID="hlnkManageInvenoryHead" runat="server"
                                class="ContainerHaeding" Style="text-decoration: none;">
                        Manage Inventory </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="90" valign="top">
                            <div class="DivTxt_Normal">
                                This section allows you to view existing vehicles/inventory, locate an individual
                                vehicle, see details and ability to add expenses on Cars.</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="7">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="InventoryList.aspx" ID="hlnkManageInvenorytxt"
                                runat="server" class="OrangeText_Link">Click 
                     here to manage Inventory</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td style='white-space: nowrap;'>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="~/UI/InventoryList.aspx?CarStatus=3" ID="hlnkArchievedInventory"
                                runat="server" class="OrangeText_Link">Click 
                     here to manage Archived Inventory</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" valign="top">
                &nbsp;
            </td>
            <td align="left" valign="top" class="myContainer">
                <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="center">
                            <asp:HyperLink NavigateUrl="DealerList.aspx" ID="hlnkDealerListImg" runat="server"
                                title="Click to mange Purchased From/Sold To">
                        <%--<img border="0" src="../images/Manage-Dealer-Customer-Icon.gif" width="52" height="54" alt="Customer/Dealer" title="Click to manage Purchased From/Sold To" />--%>
                        <img border="0" src="../images/Manage-Dealer-Customer-Icon.gif" width="52" height="54"
                           alt="Customer/Dealer" title="Click to manage Purchased From/Sold To" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="DealerList.aspx" ID="hlnkDealerListHead" runat="server"
                                class="ContainerHaeding" Style="text-decoration: none;">Manage Purchased From/Sold To</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="90" valign="top">
                            <div class="DivTxt_Normal">
                                This section allows you to manage dealers &amp; customers in the system. You may
                                add a new Purchased From/Sold To or find and modify existing Purchased From/Sold
                                To information here.</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="AddCustomer.aspx" ID="hlnkAddCustomertxt" runat="server"
                                class="OrangeText_Link">Add A Purchased From/Sold To</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="DealerList.aspx" ID="hlnkDealerListtxt" runat="server"
                                class="OrangeText_Link">View Purchased From/Sold To</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" valign="top">
                &nbsp;
            </td>
            <td align="left" valign="top" class="myContainer">
                <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="center">
                            <asp:HyperLink Target="_blank" ID="hlnkLaneAssignmentImg" runat="server" title="Click to view lane inventory list">
                     <%--<img border="0" src="../images/Manage-Lane-Assignment-Icon.gif" width="52" height="54" alt="" />--%>
                     <img border="0"  src="../Images/img_trans.gif" width="1" height="1" class="lane-icon" alt="" /></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink ID="hlnkLaneAssignmentHead" runat="server" Target="_blank" class="ContainerHaeding"
                                Style="text-decoration: none;">Manage 
                     Lane Numbering</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="90" valign="top">
                            <div class="DivTxt_Normal">
                                This section provides you functions to modify Lane Numbers (Regular, Exotic, Online
                                and Virtual) and Market price quickly within a grid for all Cars not sold yet....</div>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink ID="hlnkLaneHistory" runat="server" NavigateUrl="~/UI/LaneLog.aspx"
                                class="OrangeText_Link" Text="View Lane History" />
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr id="trLaneAutomationSetting" runat="server">
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />&nbsp;
                            <asp:HyperLink ID="hlnkLaneAutomationSetting" runat="server" NavigateUrl="~/UI/LaneAutomationSetting.aspx"
                                class="OrangeText_Link" Text="Manage Lane Automation Setting" />
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr id="trApplyLaneAutomation" runat="server">
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />&nbsp;
                            <asp:HyperLink ID="hlnkApplyLaneAutomation" runat="server" NavigateUrl="~/UI/ApplyLaneAutomation.aspx"
                                class="OrangeText_Link" Text="Apply Lane Automation" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

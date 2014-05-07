<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Home.aspx.cs" Inherits="METAOPTION.UI.AdminHome"
    MasterPageFile="~/UI/Admin_MasterLeftPanel.Master" Title="Admin Panel :: Dashboard" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Admin_HomePage" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td align="left" valign="top" class="myContainer">
                <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td align="center">
                            <asp:HyperLink NavigateUrl="Admin_SearchOrganization.aspx" ID="hlnkOrganizationimg"
                                runat="server" ToolTip="Click to view Organization">
                      <img border="0" src="../images/DashBoard_Org.png" width="52" height="54" alt="" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="Admin_SearchOrganization.aspx" ID="hlnkOrganizationHead"
                                runat="server" class="ContainerHaeding" Style="text-decoration: none;">
                        Organization</asp:HyperLink>
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="Admin_AddOrganization.aspx" ID="hlnkOrganizationadd"
                                runat="server" class="OrangeText_Link">Click 
                     here to add new organization</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="Admin_SearchOrganization.aspx" ID="hlnkOrganistionsearch"
                                runat="server" class="OrangeText_Link">Click here to search Organization</asp:HyperLink>
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
                            <asp:HyperLink NavigateUrl="Admin_ManageUserSetting.aspx" ID="hlnkuserimg" runat="server"
                                title="Click to view Users">
                       <img border="0" src="../images/DashBoard_User.png" width="52" height="54" alt="" />
                         <%--  <img border="0"  src="../Images/Account-Payable.gif" width="1" height="1" class="account-icon" alt="" />--%>
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="Admin_ManageUserSetting.aspx" ID="hlnkuserHead" runat="server"
                                class="ContainerHaeding" Style="text-decoration: none;">
                     Users</asp:HyperLink>
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
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;
                            <asp:HyperLink NavigateUrl="Admin_User.aspx" ID="hlnknewuser" runat="server" class="OrangeText_Link">Click 
                     here to add new user</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;
                            <asp:HyperLink NavigateUrl="Admin_ManageUserSetting.aspx" ID="hlnkmanaguser" runat="server"
                                class="OrangeText_Link">Click 
                     here to manage user</asp:HyperLink>
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
                            <asp:HyperLink NavigateUrl="Admin_ViewAllGroups.aspx" runat="server" ID="hlnkgroup"
                                title="Click to view Group">
                        <img border="0" src="../images/DashBoard_Group.png" width="52" height="54" alt="" />
                        <%--  <img border="0"  src="../Images/Search-Icon.gif" width="1" height="1" class="search-icon" alt="" />--%>
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="Admin_ViewAllGroups.aspx" runat="server" ID="hlnkInventorySearchHead"
                                class="ContainerHaeding" Style="text-decoration: none;">
                        Group</asp:HyperLink>
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;
                            <asp:HyperLink NavigateUrl="Admin_AddNewGroup.aspx?mode=Ins" runat="server" ID="hlnkgrouptxt"
                                class="OrangeText_Link">Click 
                     here to add new group</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;
                            <asp:HyperLink NavigateUrl="Admin_ViewAllGroups.aspx" ID="hlnkmanagegroup" runat="server"
                                class="OrangeText_Link">Click 
                     here to manage group</asp:HyperLink>
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
                            <asp:HyperLink NavigateUrl="Admin_ManageSystem.aspx" ID="hlnkSystem" runat="server" title="Click to view System">
                      <img border="0" src="../images/DashBoard_System.png" width="52" height="54" alt="" />
                        <%--  <img border="0"  src="../Images/Manage-Inventory-Icon.gif" width="1" height="1" class="manage-icon" alt="" />--%>
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="Admin_ManageSystem.aspx" ID="hlnkManageInvenoryHead" runat="server" class="ContainerHaeding"
                                Style="text-decoration: none;">
                        System </asp:HyperLink>
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
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="Admin_AddSystem.aspx" ID="hlnkManageInvenorytxt" runat="server" class="OrangeText_Link">Click 
                     here to add new system</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style='white-space: nowrap;'>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="Admin_ManageSystem.aspx" ID="hlnkArchievedInventory" runat="server" class="OrangeText_Link">Click 
                     here to view system</asp:HyperLink>
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
                            <asp:HyperLink NavigateUrl="Admin_ManageIP.aspx" runat="server" ID="HyperLink1" title="Click to Manage IP">
                       <img border="0" src="../images/DashBoard_IP.png" width="52" height="54" alt="" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerHaeding" align="center">
                            <asp:HyperLink NavigateUrl="Admin_ManageIP.aspx" runat="server" ID="HyperLink2" class="ContainerHaeding"
                                Style="text-decoration: none;">
                        Manage IP</asp:HyperLink>
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
                        <td>
                            <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                alt="" />
                            &nbsp;<asp:HyperLink NavigateUrl="Admin_ManageIP.aspx" ID="hlnkAddCustomertxt" runat="server" class="OrangeText_Link">Click here to manage IP</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                </table>
            </td>
            <%-- <td align="left" valign="top">
                &nbsp;
            </td>
            <td align="left" valign="top" class="myContainer">
                <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
                   
                </table>
            </td>--%>
        </tr>
    </table>
</asp:Content>

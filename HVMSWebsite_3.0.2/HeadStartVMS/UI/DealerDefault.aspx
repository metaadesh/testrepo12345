<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealerDefault.aspx.cs"

 Inherits="METAOPTION.UI.DealerDefault" %>
 <%@ MasterType VirtualPath="~/UI/MasterPage.Master" %>
<asp:content contentplaceholderid="ContentPlaceHolder1" id="cphDefault" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0">
      <tr>
         <td align="left" valign="top" class="Container">
            <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
            
               <tr>
                  <td align="center" >
                     <asp:HyperLink NavigateUrl="CompanyContacts.aspx" ID="hlnkAddInventoryimg" runat="server"
                        ToolTip="Click to view Company Contacts">
                        <%--<img border="0" src="../images/Add-Inventory-Icon.gif" width="52" height="54" alt="" />--%>
                        <img border="0"  src="../Images/Manage-Dealer-Customer-Icon.gif" width="52" height="54" style=" padding-top: 10px; padding-bottom: 10px;"  alt="" />
                     </asp:HyperLink>
                  </td>
               </tr>               
               <tr>
                  <td class="ContainerHaeding" align="center">
                     <asp:HyperLink NavigateUrl="CompanyContacts.aspx" ID="hlnkAddInventoryHead" runat="server"
                        class="ContainerHaeding" Style="text-decoration: none;">
                        Contacts/Users</asp:HyperLink>
                  </td>
               </tr>
               <tr>
                  <td>
                     &nbsp;
                  </td>
               </tr>
               <tr>
                  <td height="20" valign="top">
                     <div class="DivTxt_Normal">
                        This section will provide you to view the Company Contacts and Users</div>
                  </td>
               </tr>
               <tr>
                  <td height="5">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;</td>
               </tr>
              
              <tr>
                  <td height="5">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
               </tr>
                <tr>
                  <td height="5">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
               </tr>

                 <tr>
                  <td>
                     <img border="0" src="../images/Buller-1.gif" width="9" height="9" alt="" />
                     &nbsp;<asp:HyperLink NavigateUrl="CompanyContacts.aspx" ID="HyperLink1" runat="server"
                        class="OrangeText_Link">Click here to view Company Contacts</asp:HyperLink>
                  </td>
               </tr>
                <tr>
                  <td>
                     <img border="0" src="../images/Buller-1.gif" width="9" height="9" alt="" />
                     &nbsp;<asp:HyperLink NavigateUrl="EntityUserList.aspx" ID="lnkUsers" runat="server"
                        class="OrangeText_Link">Click 
                     here to view Users</asp:HyperLink>
                  </td>
               </tr>
            </table>
         </td>
         <td width="24" align="left" valign="top">
            &nbsp;
         </td>
         <td align="left" valign="top" class="Container">
           <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
               <tr>
                  <td align="center">
                     <asp:HyperLink NavigateUrl="Payments.aspx" ID="hlnkAccountPayableimg" runat="server"
                        title="Click to view Account Payable">
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
                  <td height="20" valign="top">
                     <div class="DivTxt_Normal">
                        This section allows you to print a new utility check or check for the commissions,
                        expenses, car costs etc.</div>
                  </td>
               </tr>             
                <tr>
                  <td height="5">
                  &nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
               </tr>
                <tr>
                  <td height="5">
                  &nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
               </tr>
               <tr>
                  <td>
                     <img border="0" src="../images/Buller-1.gif" width="9" height="9" alt="" />
                     &nbsp;<asp:HyperLink NavigateUrl="Payments.aspx" ID="hlnkAccountPayable" runat="server"
                        class="OrangeText_Link">Click 
                     here to view Accounts Payable</asp:HyperLink>
                  </td>
               </tr>
               <tr>
                  <td height="5">
               
                  </td>
               </tr>
               <tr>
                  <td>
                     <img border="0" src="../images/Buller-1.gif" width="9" height="9" alt="" />
                     &nbsp;<asp:HyperLink NavigateUrl="ViewAllExpenses.aspx" ID="lnkAllPayments" runat="server"
                        class="OrangeText_Link">Click 
                     here to view all Expenses</asp:HyperLink>
                  </td>
               </tr>
            </table>
         </td>
         <td width="24" align="left" valign="top">
            &nbsp;
         </td>
          <td align="left" valign="top" class="Container">
                <table border="0" width="220" cellpadding="0" style="border-collapse: collapse" id="tableSearch">
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
                  <td id="tdtext" height="90" valign="top">
                     <div class="DivTxt_Normal">
                        This section provides you functions to perform search on Inventory ...</div>
                  </td>
               </tr>
               <tr><td height="15">&nbsp;</td></tr>             
                
               <tr>
                  <td>
                     <img border="0" src="../images/Buller-1.gif" width="9" height="9" alt="" />
                     &nbsp;<asp:HyperLink NavigateUrl="InventorySearch.aspx" runat="server" ID="hlnkInventorySearchtxt"
                        class="OrangeText_Link">Click 
                     here to search Inventory</asp:HyperLink>
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
         <td align="left" valign="top" class="Container">
             <table border="0" width="220" cellpadding="0" style="border-collapse: collapse">
               <tr>
                  <td align="center">
                     <asp:HyperLink NavigateUrl="PreExpense.aspx" ID="hlnkDealerListImg" runat="server"
                        title="Click to mange Purchased From/Sold To">
                        <%--<img border="0" src="../images/Manage-Dealer-Customer-Icon.gif" width="52" height="54" alt="Customer/Dealer" title="Click to manage Purchased From/Sold To" />--%>
                        <img border="0" src="../images/defmobile.png"  width="52" height="54" style="padding-bottom: 10px; padding-top: 10px;"
                           alt="Customer/Dealer" title="Click to view Pre-Expense/Location/Scan Log/......" />
                     </asp:HyperLink>
                  </td>
               </tr>
               <tr>
                  <td class="ContainerHaeding" align="center">
                     <asp:HyperLink NavigateUrl="PreExpense.aspx" ID="hlnkDealerListHead" runat="server"
                        class="ContainerHaeding" Style="text-decoration: none;">Mobile</asp:HyperLink>
                  </td>
               </tr>
               <tr>
                  <td>
                     &nbsp;
                  </td>
               </tr>
               <tr>
                  <td height="20" valign="top">
                     <div class="DivTxt_Normal">
                        This section allows you to view the Pre Inventory, Location, VIN scan log and Generic Images.. </div>
                  </td>
               </tr>
              
               <tr>
                  <td height="5">
                   &nbsp; &nbsp; &nbsp; &nbsp;
                  </td>
               </tr>
               <tr>
                  <td>
                     <img border="0" src="../images/Buller-1.gif" width="9" height="9" alt="" />
                     &nbsp;<asp:HyperLink NavigateUrl="PreInventory.aspx" ID="hlnkAddCustomertxt" runat="server"
                        class="OrangeText_Link">Click here to view Mobile-Inventory</asp:HyperLink>
                  </td>
               </tr>
             
               <tr>
                  <td>
                     <img border="0" src="../images/Buller-1.gif" width="9" height="9" alt="" />
                     &nbsp;<asp:HyperLink NavigateUrl="ViewLocation.aspx" ID="hlnkviewloctn" runat="server"
                        class="OrangeText_Link">Click here to view Location</asp:HyperLink>
                  </td>
               </tr>
               <tr>
                  <td>
                     <img border="0" src="../images/Buller-1.gif" width="9" height="9" alt="" />
                     &nbsp;<asp:HyperLink NavigateUrl="VinScanLog.aspx" ID="hlnkvinscanlogtxt" runat="server"
                        class="OrangeText_Link">Click here to view VIN Scan Log</asp:HyperLink>
                  </td>
               </tr>
                <tr>
                  <td>
                     <img border="0" src="../images/Buller-1.gif" width="9" height="9" alt="" />
                     &nbsp;<asp:HyperLink NavigateUrl="GenericImages.aspx" ID="hlnkgenerictxt" runat="server"
                        class="OrangeText_Link">Click here to view Generic Images</asp:HyperLink>
                  </td>
               </tr>

            </table>
         </td>
         <td align="left" valign="top">
            &nbsp;
         </td>
         <td align="left" valign="top" >
          
         </td>
         <td align="left" valign="top">
            &nbsp;
         </td>
         <td align="left" valign="top" >
        
         </td>
      </tr>
   </table>
</asp:content>

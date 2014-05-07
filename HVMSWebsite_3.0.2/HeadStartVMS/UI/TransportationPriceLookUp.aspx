<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransportationPriceLookUp.aspx.cs" 
Inherits="METAOPTION.UI.TransportationPriceLookUp" %>


<asp:content contentplaceholderid="ContentPlaceHolder1" id="cphViewVendor" runat="server">
  <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="RightPanel">   
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                        <td class="TableHeadingBg TableHeading">
                            Vendor Zone Rate
                            </td> 
                        </tr>
                        <tr>
                            <td class="TableBorder"> 
                <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                    <div style="width: 28%; float: left; padding: 5px; padding-left: 0px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder" >
                                    Vendor
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList runat="server" id="ddlVendor" CssClass="txt2"></asp:DropDownList>
                                </td>
                                </tr>
                                <tr>
                                <td class="TableBorder">
                                 Mileage
                                    
                                </td>
                                <td class="TableBorder">
                                       <asp:TextBox runat="server" id="txtMileage" Width="147px" CssClass="txt1" ></asp:TextBox>
                                </td>                                                            
                            </tr>
                            </table>
                            </div>
                             <div style="width: 25%; float: left; padding: 5px 5px 5px 5px"">
                             <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                              <tr>
                                <td class="TableBorder" style="width: 110px">
                                   Zone
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                <asp:DropDownList runat="server" id="ddlZone" CssClass="txt2"></asp:DropDownList>
                               
                                </td>
                                </tr>
                                <tr>
                                 <td>
                                   
                                </td>
                                <td style="text-align : right;" >
                                     <asp:Button ID="btnSearch" runat="server" Text="Search" Style=" width : 80px !important; margin-top : 5px;"
                                        CssClass="btn" onclick="btnSearch_Click" /> 
                                </td>                                
                            </tr>                          
                        </table>
                    </div>
                  
                </div>
                </td>
                </tr>
                <tr>
                <td class="TableBorder" >                
                         
                                <asp:GridView ID="grdPriceZoneDetails" runat="server" AutoGenerateColumns="False" Width="100%"  
                                      BorderWidth="0" DataKeyNames="Id" OnPageIndexChanging="grdPriceZoneDetails_PageIndexChanging"
                                 AllowPaging="true" PageSize="25" Mode="NumericFirstLast">
                                 <PagerSettings Mode="NumericFirstLast" Visible="true" />
                                 <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:BoundField DataField="VendorName" HeaderText="Vendor" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="MinMileage" HeaderText="Min. Mileage" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="MaxMileage" HeaderText="Max. Mileage" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="Price" HeaderText="Price" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                            <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                            <asp:BoundField DataField="State" HeaderText="State" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                            <asp:BoundField DataField="Zip" HeaderText="Zip" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />                                                                             
                                    </Columns>
                                  <EmptyDataTemplate>
                                     <asp:Label ID="lblDataMsg" runat="server"  CssClass="leftpanelcontentheading" Text="No Zone Rates found"></asp:Label>                                        
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle CssClass="empty" />
                                     <AlternatingRowStyle BackColor="#E4EDF4" />
                                </asp:GridView>
                        </td>
                    </tr> 
                   <tr>
                    <td class="FooterContentDetails">
                               <%-- <a id="AddNewContact" href='<%="AddNewContact.aspx?EntityId="+EntityId+"&type="+type+"&TB_iframe=true&height=220&width=800" %>'
                                    title="Add New Contact" style="font-size: 11px; font-weight: bold; color: #535152;
                                    font-family: Arial, Helvetica, sans-serif; text-decoration: underline;" class="thickbox">
                                    Add New Contact</a>
                                    <a id="AddContactdummy" style="display:none; cursor:pointer">Add New Contact</a>--%>
                            </td>
                        </tr>
                    </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <style type="text/css">
    .empty
    {
        border : none;
    }
    
    </style>
   
</asp:content>

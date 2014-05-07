<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPreference.ascx.cs"
    Inherits="METAOPTION.UserControls.EditPreference" %>

<script type="text/C#" runat="server">
    
    protected void Refresh_Click(object sender, EventArgs args)
    {
        //  update the grids contents
        this.frmviewPreference.DataBind();
    }
    
</script>

<script type="text/javascript">
        
            function updated() {
                //  close the popup
                tb_remove();
                
                //  refresh the update panel so we can view the changes  
                $('#<%= this.btnRefreshCustomers.ClientID %>').click();      
            }
            
            function pageLoad(sender, args) {
                if(args.get_isPartialLoad()){
                    //  reapply the thick box stuff
                    tb_init('a.thickbox');
                }
            }
        
</script>

<%--<asp:UpdatePanel ID="upPreference" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    </ContentTemplate>
</asp:UpdatePanel>--%>
<table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
    class="arial-12">
    <tr>
        <td class="TableHeadingBg">
            <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                <tr>
                    <td class="TableHeading">
                        Preferences
                    </td>
                    <td align="right" class="HeadingEditButton">
                        <a id="editpreprence" runat="server" href='<%# "EditPreference.aspx?EntityId="+EntityId+"&TB_iframe=true&height=500&width=800" %>'
                            title="Edit Preference" class="thickbox">
                            <img id="ImgEditPref" runat="server" alt="" src="../Images/Edit-Main-Icon.gif" border="0" /></a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnRefreshCustomers" runat="server" Style="display: none" OnClick="Refresh_Click" />
            <asp:FormView ID="frmviewPreference" runat="server" Width="100%" DataSourceID="objDealerPreference">
                <ItemTemplate>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                        <tr>
                            <td class="TableBorder" width="140">
                                <b>Preference Setting</b>
                            </td>
                            <td class="TableBorder" colspan="3">
                                <%# Eval("PreferenceSettings").ToString()!="True" ?"No":"Yes"  %>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder" colspan="4">
                                <asp:GridView ID="GridView1" BorderStyle="Solid" BorderWidth="0" AutoGenerateColumns="False"
                                    runat="server" Width="100%" DataSourceID="objPreference">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Enable" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEnable" runat="server" Text='<%# (Eval("IsEnabled").ToString()) !="True" ?"No": "Yes" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Year Range" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblYearRange" runat="server" Text='<%# Eval("yearsFrom")+"-"+ Eval("yearsTo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Make" HeaderText="Make" ItemStyle-CssClass="GridContent"
                                            HeaderStyle-CssClass="GridHeader">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="Model" ItemStyle-CssClass="GridContent"
                                            HeaderStyle-CssClass="GridHeader">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Mileage Range" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMileageRange" runat="server" Text='<%# Eval("MileageMin")+"-"+ Eval("MileageMax") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price Range" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriceRange" runat="server" Text='<%# Eval("PriceMin")+"-"+ Eval("PriceMax") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Preference Details
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="objPreference" runat="server" SelectMethod="GetDealerPreference"
                                    TypeName="METAOPTION.BAL.DealerCustomerBAL">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="DealerId" QueryStringField="EntityId" Type="Int64" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder" colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                <b>SMS Alerts</b>
                            </td>
                            <td class="TableBorder" width="203">
                                <%# (Eval("ReceiveSms").ToString()) !="True" ?"No":"Yes"%>
                            </td>
                            <td class="TableBorder">
                                <b>Email Alerts</b>
                            </td>
                            <td class="TableBorder" width="203">
                                <%# (Eval("ReceiveSms").ToString()) !="True" ?"No": "Yes" %>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder" colspan="2">
                                <asp:GridView ID="grdViewMobile" AutoGenerateColumns="False" BorderWidth="0" runat="server"
                                    Width="100%" BorderStyle="Solid" DataSourceID="objMobilePreference">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Enable" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEnable" runat="server" Text='<%# (Eval("IsEnable").ToString()) !="True" ?"No": "Yes" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="MobileNo" HeaderText="Mobile Number" ItemStyle-CssClass="GridContent"
                                                                        HeaderStyle-CssClass="GridHeader">
                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                        <ItemStyle CssClass="GridContent" />
                                                                    </asp:BoundField> --%>
                                        <asp:TemplateField HeaderText="Mobile Number" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                            <ItemTemplate>
                                                <span style="font-size: 11px">
                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("MobileNo")%>'>
                                                    </asp:Label>
                                                </span>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Mobile Details
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="objMobilePreference" runat="server" SelectMethod="GetDealerMobilePreference"
                                    TypeName="METAOPTION.BAL.DealerCustomerBAL">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="DealerId" QueryStringField="EntityId" Type="Int64" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                            <td class="TableBorder" colspan="2">
                                <asp:GridView ID="grdViewEmail" BorderWidth="0" AutoGenerateColumns="False" runat="server"
                                    Width="100%" BorderStyle="Solid" DataSourceID="objEmailPreference">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Enable" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEnable" runat="server" Text='<%# (Eval("IsEnable").ToString()) !="True" ?"No": "Yes" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="GridContent"
                                            HeaderStyle-CssClass="GridHeader">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Email Details
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="objEmailPreference" runat="server" SelectMethod="GetDealerEmailPreference"
                                    TypeName="METAOPTION.BAL.DealerCustomerBAL">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="DealerId" QueryStringField="EntityId" Type="Int64" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="objDealerPreference" runat="server" SelectMethod="GetDealerPreferenceSetting"
                TypeName="METAOPTION.BAL.DealerCustomerBAL">
                <SelectParameters>
                    <asp:QueryStringParameter Name="DealerId" QueryStringField="EntityId" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
</table>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contact.ascx.cs" Inherits="METAOPTION.UserControls.Contact" %>

<script type="text/C#" runat="server">
    
    protected void Refresh_Click(object sender, EventArgs args)
    {
        //  update the grids contents
        this.BindContactDetails();
        Response.Redirect("ViewDealer.aspx?EntityId=" + EntityId + "&type=" + type);
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

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    </ContentTemplate>
</asp:UpdatePanel>--%>
<table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
    class="arial-12">
    <tr>
        <td class="TableHeadingBg TableHeading">
            Contacts
        </td>
    </tr>
    <tr>
        <td class="TableBorder">
            <asp:ObjectDataSource ID="objContactType" runat="server" SelectMethod="GetContactType"
                TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="objJobTitle" runat="server" SelectMethod="GetJobTitle"
                TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
            <asp:Button ID="btnRefreshCustomers" runat="server" Style="display: none" OnClick="Refresh_Click" />

            <asp:GridView ID="grdContactDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                BorderStyle="Solid" BorderWidth="0" DataKeyNames="ContactId" OnRowDeleting="grdContactDetails_RowDeleting"
                OnRowDataBound="grdContactDetails_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="JobTitle" HeaderText="Job Title" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent" />
                    <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent" />
                    <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent" />
                    <asp:BoundField DataField="ContactType" HeaderText="Contact Type" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent" />
                    <asp:BoundField DataField="OfficePhone" HeaderText="Office Phone" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent" />
                    <asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent" />
                    <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                        HeaderStyle-Width="80px" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlkcontact" runat="server" ImageUrl="~/Images/ChangePassword.png" ToolTip="Add/Change Password"></asp:HyperLink>
                            <a href='<%# "EditContact.aspx?ID="+Eval("ContactId")+"&TB_iframe=true&height=220&width=800" %>'
                                title="Edit Details" class="thickbox">
                                <img alt="" src="../Images/newedit.gif" border="0" /></a>
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/DeleteButton.jpg"
                                OnClientClick="javascript:return confirm('Really do you want to delete?\n\nClick Ok to Confirm\nClick Cancel to ignore');" />

                            <asp:HiddenField ID="hfSecurityUserID" runat="server" Value='<%# Eval("SecurityUserID") %>' />
                            <asp:HiddenField ID="hfUserName" runat="server" Value='<%# Eval("UserName") %>' />
                            <asp:HiddenField ID="hfContactID" runat="server" Value='<%# Eval("ContactId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No Contact Details
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource ID="objContactDetails" runat="server" SelectMethod="GetEntityContactDetails"
                TypeName="METAOPTION.BAL.CommonBAL">
                <SelectParameters>
                    <asp:QueryStringParameter Name="EntityId" QueryStringField="EntityId" Type="Int64" />
                    <asp:QueryStringParameter DefaultValue="" Name="EntityTypeId" QueryStringField="type"
                        Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td class="FooterContentDetails">
            <a href='<%= "AddNewContact.aspx?EntityId="+EntityId+"&type="+type+"&TB_iframe=true&height=220&width=800" %>'
                title="Add New Contact" style="font-size: 11px; font-weight: bold; color: #535152;
                font-family: Arial, Helvetica, sans-serif; text-decoration: underline;" class="thickbox">
                Add New Contact</a>
        </td>
    </tr>
</table>

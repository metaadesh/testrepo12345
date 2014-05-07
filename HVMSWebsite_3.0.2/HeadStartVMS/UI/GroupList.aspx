<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="GroupList.aspx.cs" Inherits="METAOPTION.UI.GroupList" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphViewGrpList" runat="server">
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="RightPanel">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                     <tr>
                        <td class="AddHeading" style="padding-bottom:10px;">
                           Group List
                        </td>
                     </tr>
                     <tr>
                        <td>
                            <fieldset class="ForFieldset">
                            <legend class="ForLegend">Group List</legend><br />
                            <asp:GridView ID="GrdGroup" runat="server" 
                               Width="100%" AutoGenerateColumns="False" GridLines="None" 
                               AllowPaging="true" PageSize="20" 
                               OnPageIndexChanging="GrdGroup_PageIndexChanging"                   
                               EmptyDataText="No record found"
                               OnRowDeleting="GrdGroup_RowDeleting"
                               DataKeyNames="SecurityGroupId">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hylnkEdit" ToolTip="Edit" runat="server" ImageUrl="~/Images/edit-icon.jpg" NavigateUrl='<%# "AddGroup.aspx?Code="+Eval("SecurityGroupId")+"&Mode=edit"+ "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Width="20px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hylnkView" ToolTip="View" runat="server" ImageUrl="~/Images/Select.gif" NavigateUrl='<%# "AddGroup.aspx?Code="+Eval("SecurityGroupId")+"&Mode=view" + "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkDelete" 
                                               ToolTip="Delete" 
                                               runat="server" 
                                               ImageUrl="~/Images/DeleteButton.jpg" 
                                               CommandName="Delete"
                                               OnClientClick="javascript:return confirm('Do you want to delete this group?\n\nClick Ok to delete.\nYou won\'t be able to undo those changes.');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="20px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="GroupName" HeaderText="Group Name"  SortExpression="GroupName" />
                                    <asp:BoundField DataField="GroupDesc" HeaderText="Description"  SortExpression="GroupDesc" />
                                </Columns>            
                                <RowStyle CssClass="gvRow" />
                                <AlternatingRowStyle CssClass="gvAlternateRow" />
                                <HeaderStyle CssClass ="gvHeading" />
                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                <EmptyDataRowStyle CssClass="gvEmpty" />
                            </asp:GridView>
                            <div style="padding:7px">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="   New Group   " 
                                    CssClass="btn" width="120px" /> 
                            </div>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

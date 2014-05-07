<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Admin_MasterLeftPanel.Master"
    CodeBehind="Admin_ViewAllGroups.aspx.cs" Inherits="METAOPTION.UI.Admin_ViewAllGroups"
    Title="Admin Panel :: All Groups" %>

<asp:Content ID="contViewAllGroups" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
        .tdNoWrap
        {
            white-space: nowrap;
            text-align: center;
        }
    </style>
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <fieldset class="ForFieldSet">
                    <legend class="ForLegend" align="left">Manage Groups</legend>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                        <%--<tr class="h_title">
                        <td colspan="2" style="padding-bottom: 10px;">
                            Group List
                        </td>
                    </tr>--%>
                        <tr>
                            <td class="lblb" align="right">
                                <asp:Label ID="Label1" runat="server" Text="Organization" CssClass="TableBorderLabel"></asp:Label>
                            </td>
                            <td class="lbl">
                                <asp:DropDownList ID="ddlOrganization" runat="server" CssClass="txtMan2" Style="font-size: 11px;"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlOrganization_SelectedIndexChanged"
                                    Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <asp:GridView ID="GrdGroup" runat="server" Width="100%" AutoGenerateColumns="False"
                                    GridLines="None" AllowPaging="true" PageSize="20" OnPageIndexChanging="GrdGroup_PageIndexChanging"
                                    EmptyDataText="No Record Found" OnRowDeleting="GrdGroup_RowDeleting" DataKeyNames="SecurityGroupId">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="tdNoWrap"
                                            HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnkEdit" ToolTip="Edit" runat="server" ImageUrl="~/Images/edit-icon.jpg"
                                                    NavigateUrl='<%# "Admin_AddNewGroup.aspx?Code="+Eval("SecurityGroupId")+"&Mode=edit"+ "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'></asp:HyperLink>
                                                <asp:HyperLink ID="hylnkView" ToolTip="View" runat="server" ImageUrl="~/Images/Select.gif"
                                                    NavigateUrl='<%# "Admin_AddNewGroup.aspx?Code="+Eval("SecurityGroupId")+"&Mode=view" + "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'></asp:HyperLink>
                                                <asp:ImageButton ID="lnkDelete" ToolTip="Delete" runat="server" ImageUrl="~/Images/DeleteButton.jpg"
                                                    CommandName="Delete" OnClientClick="javascript:return confirm('Do you want to delete this group?\n\nClick Ok to delete.\nYou won\'t be able to undo those changes.');" />
                                            </ItemTemplate>
                                            <ItemStyle Width="75px"></ItemStyle>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px"></ItemStyle>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="GroupDesc" HeaderText="Description" SortExpression="GroupDesc"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="OrgCode" HeaderText="Organization" SortExpression="GroupDesc"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                    </Columns>
                                    <RowStyle CssClass="gvRow" />
                                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                                    <HeaderStyle CssClass="gvHeading" />
                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                    <EmptyDataRowStyle CssClass="gvEmpty" />
                                </asp:GridView>
                                <div style="padding: 7px">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="New Group"
                                        CssClass="btn" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnDefaultSetting" runat="server" Text="Assign Group" CssClass="btn"
                                        Width="120px" OnClientClick="clearPopupControls();" OnClick="btnDefaultSetting_Click" />
                                    <asp:Label ID="lblMsg" runat="server" Text="Please select organization" Style="font-size: 12px;
                                        font-family: Arial;" ForeColor="Red" Visible="false"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <%--------------------DEFAULT SETTING POPUP BEGIN--------------------%>
            <div id="dvDefaultSetting" runat="server" class="modalPopup" style="display: none;
                min-width: 400px; max-height: 550px">
                <div>
                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="PopUpBoxHeading" colspan="4" style="padding-left: 5px">
                                Group List
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img id="imgCloseDefSet" border="0" src="../Images/close.gif" alt="Close" style="padding-left: 5px"
                                    onclick="$find('behDefSet').hide();return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="lblb">
                                Group Name
                            </td>
                            <td class="lbl">
                                <asp:TextBox ID="txtGroupNamegrp" runat="server" CssClass="txt2" />
                            </td>
                            <td class="lblb">
                                Description
                            </td>
                            <td class="lbl">
                                <asp:TextBox ID="txtGroupDescgrp" runat="server" CssClass="txt2" />
                            </td>
                            <td class="lblb" align="center">
                                <asp:Button ID="btnSearchGroup" runat="server" Text="  Search  " CssClass="btn" OnClick="btnSearchGroup_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="5" style="padding: 10px">
                                <asp:GridView ID="gvGroups" runat="server" AutoGenerateColumns="False" Width="100%"
                                    GridLines="None" AllowPaging="True" OnPageIndexChanging="gvGroups_PageIndexChanging"
                                    PageSize="10" DataKeyNames="SecurityGroupId">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnAddGroup" CommandName="SelectEmp" CommandArgument='<%#Eval("SecurityGroupId") %>'
                                                    runat="server" ImageUrl="~/Images/confirm.gif" OnClick="ibtnAddGroup_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="GroupName" HeaderText="Name" />
                                        <asp:BoundField DataField="GroupDesc" HeaderText="Description" />
                                    </Columns>
                                    <RowStyle CssClass="gvRow" />
                                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                                    <HeaderStyle CssClass="gvHeading" />
                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="center">
                                <asp:Button ID="btnCanelGroup" runat="server" Text="Cancel" CssClass="btn" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:HiddenField ID="hfA" runat="server" />
            <ajax:ModalPopupExtender ID="mpeDefaultSetting" runat="server" BackgroundCssClass="modalBackground"
                BehaviorID="behDefSet" TargetControlID="hfA" PopupControlID="dvDefaultSetting"
                CancelControlID="btnCanelGroup">
            </ajax:ModalPopupExtender>
            <%--------------------DEFAULT SETTING POPUP END--------------------%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function clearPopupControls() {
            $('#<%=txtGroupNamegrp.ClientID %>').val('');
            $('#<%=txtGroupDescgrp.ClientID %>').val('');
        }
    </script>
</asp:Content>

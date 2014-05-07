<%@ Page Language="C#" MasterPageFile="~/UI/Admin_Master.Master" AutoEventWireup="true"
    Title="Admin Panel:: Search Organization" CodeBehind="Admin_SearchOrganization.aspx.cs"
    Inherits="METAOPTION.UI.Admin_SearchOrganization" %>

<asp:Content ID="contAddOrg" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
        .GridContent_alignCenter
        {
            border: #e2e2e2 1px solid;
            font-size: 11px;
            color: #535152;
            font-family: Arial, Helvetica, sans-serif;
            text-decoration: none;
            padding: 4px;
            text-align: center;
        }
    </style>
    <table width="99%" style="padding-left: 1%;">
        <tr>
            <td>
                <div style="min-height: 480px;">
                    <div>
                        <div style="height: 5px;">
                        </div>
                        <fieldset class="ForFieldSet">
                            <legend class="ForLegend">Search Organization</legend>
                            <div style="width: 100%">
                                <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                    <tr>
                                        <td class="TableBorder" nowrap="nowrap">
                                            Organization
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtOrganization" runat="server" CssClass="txt2" />
                                        </td>
                                        <td class="TableBorder">
                                            Website
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtwebsite" runat="server" CssClass="txt2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorder">
                                            Code
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtcode" runat="server" CssClass="txt2" Style="text-transform: uppercase" />
                                        </td>
                                        <td class="TableBorder">
                                            Phone
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtphone" runat="server" CssClass="txt2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorder">
                                            Email
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtemail" runat="server" CssClass="txt2"></asp:TextBox>
                                        </td>
                                        <td colspan="2" align="center" class="TableBorder">
                                            <asp:Button ID="btnSearchOrganization" runat="server" Text="Search" CssClass="btn"
                                                Width="100px" OnClick="btnSearchOrganization_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                    </div>
                    <br />
                    <div style="clear: both">
                        <asp:GridView ID="gv_org" runat="server" Width="100%" AutoGenerateColumns="false"
                            OnRowDataBound="gv_org_rowdatabound" AllowPaging="true" PageSize="20" AllowSorting="true"
                            OnSorting="gv_org_sort" DataKeyNames="OrganisationID" GridLines="None" EmptyDataText="No Record Found"
                            OnPageIndexChanging="pageindexchanging" HeaderStyle-BackColor="silver">
                            <Columns>
                                <asp:BoundField DataField="Organization" HeaderText="Organization" SortExpression="Organization"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="OrgCode" HeaderText="Code" SortExpression="OrgCode" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                <asp:TemplateField HeaderText="Website" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlink_website" runat="server" NavigateUrl='<%# Eval("Website") %>'
                                            Target="_blank"><%# Eval("Website") %></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Address" HeaderText="Address" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="Phone" HeaderText="Phone" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="Fax" HeaderText="Fax" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="DateAdded" HeaderText="DateAdded" SortExpression="DateAdded"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-Width="75px" HeaderStyle-CssClass="GridContent" />
                                <asp:TemplateField HeaderText="Lane" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="GridContent_alignCenter">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hAllowLaneAutomation" runat="server" Value='<%#Eval("AllowLaneAutomation") %>' />
                                        <asp:ImageButton ID="imgbtnLaneAutomation" runat="server" OnClick="imgbtnLaneAutomation_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="35px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MAA" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="GridContent_alignCenter">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hAllowMAA" runat="server" Value='<%#Eval("AllowMAA") %>' />
                                        <asp:ImageButton ID="imgbtnLaneMAA" runat="server" OnClick="imgbtnLaneMAA_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="35px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-CssClass="tdNoWrap">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="Org_Edit" runat="server" ImageUrl="../Images/edit-icon.jpg"
                                                        OnClick="Org_Edit_click" ToolTip="Click to edit" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgbtnSummary" runat="server" ImageUrl="../Images/summaryIcon.png"
                                                        OnClick="imgbtnSummary_Click" ToolTip="View Summary" Width="20px" Height="20px" />
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="hdnIsActive" runat="server" Value='<%#Eval("Active") %>' />
                                                    <asp:ImageButton ID="ImageBtn_active" runat="server" OnClick="ImageBtn_active_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageBtn_delete_" ToolTip="Click to delete" runat="server" ImageUrl="~/Images/DeleteButton.png"
                                                        OnClick="ImageBtn_delete_click" OnClientClick="javascript:return (confirm ('Do you want to delete this Organization Permanently?'));" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <HeaderStyle Width="25px"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="gvRow" />
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="gvHeading" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                            <EmptyDataRowStyle CssClass="gvEmpty" />
                        </asp:GridView>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

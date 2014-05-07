<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_SearchEntities.aspx.cs"
    Inherits="METAOPTION.UI.Admin_SearchEntities" MasterPageFile="~/UI/Admin_Master.Master"
    Title="Admin Panel :: Search Entity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="min-height: 480px;">
        <div style="height: 5px;">
        </div>
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Search Entity</legend>
            <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td class="TableBorder">
                        <asp:Label ID="Label1" runat="server" Text="Organization" CssClass="TableBorderLabel"></asp:Label>
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlOrganization" runat="server" Width="180px" AutoPostBack="false"
                            CssClass="txtMan2" Style="font-size: 11px;">
                        </asp:DropDownList>
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="Label94" runat="server" Text="Entity Type" CssClass="TableBorderLabel"></asp:Label>
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlEntityType" runat="server" Width="180px" AutoPostBack="true"
                            CssClass="txtMan2" Style="font-size: 11px;">
                        </asp:DropDownList>
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="Label3" runat="server" Text="Status" CssClass="TableBorderLabel"></asp:Label>
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txt2" Width="180px">
                            <asp:ListItem Text="ALL" Value="-1" Selected="True" />
                            <asp:ListItem Text="Active" Value="1" />
                            <asp:ListItem Text="Archive" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        <asp:Label ID="Label2" runat="server" Text="Name" CssClass="TableBorderLabel"></asp:Label>
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtName" runat="server" CssClass="txt2" Width="180px" />
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="Label4" runat="server" Text="City" CssClass="TableBorderLabel"></asp:Label>
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="txt2" Width="180px" />
                    </td>
                    <td class="TableBorder" style="white-space: nowrap;">
                        <asp:Label ID="Label5" runat="server" Text="Country" CssClass="TableBorderLabel"></asp:Label>
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txt2" AutoPostBack="True"
                            Width="180px" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        <asp:Label ID="Label6" runat="server" Text="State" CssClass="TableBorderLabel"></asp:Label>
                    </td>
                    <td class="TableBorder">
                        <asp:UpdatePanel ID="upState" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="txt2" Width="180px" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="Label7" runat="server" Text="Zip" CssClass="TableBorderLabel"></asp:Label>
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtZip" runat="server" CssClass="txt2" Width="180px" />
                    </td>
                    <td class="TableBorder" colspan="2" align="right">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:UpdateProgress AssociatedUpdatePanelID="upDealerList" ID="uprogSearch" runat="server">
                                        <ProgressTemplate>
                                            <img src="../Images/Wait.gif" alt="Please Wait..." />Please Wait...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" Width="100px"
                                        Style="margin-right: 40px;" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td class="TableBorder" colspan="6">
                    <asp:UpdatePanel ID="upDealerList" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvEntities" runat="server" AutoGenerateColumns="False" Width="100%"
                                EmptyDataText="No Record Found" AllowPaging="true" PageSize="20"
                                DataKeyNames="ID" AlternatingRowStyle-CssClass="gvAlternateRow" GridLines="None"
                                OnPageIndexChanging="gvViewBuyer_PageIndexChanging" OnRowDataBound="gvEntities_RowDataBound">
                                <Columns>
                                    <%-- <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                        HeaderStyle-Width="20px" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "ViewBuyerDetails.aspx?Mode=View&BuyerId="+Eval("BuyerId")+"&type=2" %>'
                                                runat="server" ImageUrl="~/Images/Select.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                        <ItemStyle Width="20px"></ItemStyle>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="Organisation" HeaderText="Organization" HeaderStyle-Wrap="false"
                                        HeaderStyle-CssClass="GridContent" ItemStyle-CssClass="GridContent" SortExpression="Organization">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EntityType" HeaderText="Entity Type" HeaderStyle-Wrap="false"
                                        HeaderStyle-CssClass="GridContent" ItemStyle-CssClass="GridContent" SortExpression="EntityType">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EntityName" HeaderText="Entity Name" HeaderStyle-Wrap="false"
                                        HeaderStyle-CssClass="GridContent" ItemStyle-CssClass="GridContent" SortExpression="EntityName">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="City" HeaderStyle-Wrap="false" ItemStyle-CssClass="GridContent"
                                        HeaderStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Country" HeaderStyle-Wrap="false" ItemStyle-CssClass="GridContent"
                                        HeaderStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("CountryName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State" HeaderStyle-Wrap="false" ItemStyle-CssClass="GridContent"
                                        HeaderStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" runat="server" Text='<%# Eval("State")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Zip" HeaderStyle-Wrap="false" ItemStyle-CssClass="GridContent"
                                        HeaderStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblZip" runat="server" Text='<%# Eval("Zip")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="35px" ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-CssClass="GridContent" HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hOrgID" runat="server" Value='<%#Eval("OrgID")%>' />
                                            <asp:HiddenField ID="hEntityTypeID" runat="server" Value='<%#Eval("EntityTypeID")%>' />
                                            <asp:HiddenField ID="hEntitiesActiveStatus" runat="server" Value='<%#Eval("IsActive")%>' />
                                            <asp:ImageButton ID="ibtnEntitiesStatus" runat="server" ImageUrl="../Images/H_active.png"
                                                Height="16" Width="16" Style="margin-left: 10px;" OnClick="ibtnEntityStatus_Click"
                                                OnClientClick="javascript:return confirm('Are you sure you want to change active status?');" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridContent" HorizontalAlign="Center" Width="35px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="gvRow" />
                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                <HeaderStyle CssClass="gvHeading"></HeaderStyle>
                                <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                                <EmptyDataRowStyle CssClass="gvEmpty" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="FooterContentDetails" colspan="6" width="100%" align="left">
                   <%-- <a href="Admin_ManageEntities.aspx" class="AddNewExpenseTxt">Add New Entity</a>--%>
                    <asp:LinkButton ID="lnk_newentity" runat="server" OnClick="lnk_newentity_click" CssClass="AddNewExpenseTxt" Text="">Add New Entity</asp:LinkButton>
                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

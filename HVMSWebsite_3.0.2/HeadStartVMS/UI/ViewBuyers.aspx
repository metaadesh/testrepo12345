<%@ Page Language="C#"  AutoEventWireup="true"
    CodeBehind="ViewBuyers.aspx.cs" Inherits="METAOPTION.UI.ViewBuyers" Title="HeadStartVMS::Buyer List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RightPanel">
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td class="TableHeadingBg TableHeading" colspan="2">
                                Buyer List
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder" colspan="2">
                                <asp:GridView ID="grdViewBuyer" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="gridView" CellPadding="4" GridLines="None" DataKeyNames="BuyerId" DataSourceID="objBuyerList"
                                    OnRowCreated="grdViewBuyer_RowCreated" OnRowDataBound="grdViewBuyer_RowDataBound"
                                    OnRowCommand="grdViewBuyer_RowCommand">
                                    <Columns>
                                        <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
                                        <%--  <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" HeaderStyle-Width="20px" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:HyperLink ID="hylnkEdit" ToolTip="Edit" runat="server" ImageUrl="~/Images/edit-icon.jpg" NavigateUrl='<%# "AddGroup.aspx?gid="+Eval("SecurityGroupId")+"&Mode=edit"+ "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'></asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    <ItemStyle Width="20px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" HeaderStyle-Width="20px" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:HyperLink ID="hylnkView" ToolTip="View" runat="server" ImageUrl="~/Images/View.jpg" NavigateUrl='<%# "AddGroup.aspx?gid="+Eval("SecurityGroupId")+"&Mode=view" + "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'></asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    <ItemStyle Width="20px"></ItemStyle>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" HeaderStyle-Width="20px" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "ViewBuyerDetails.aspx?BuyerId="+Eval("BuyerId") %>' runat="server" ImageUrl="~/Images/Select.gif"  />
                    </ItemTemplate>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    <ItemStyle Width="20px"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Title" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="FirstName">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="FirstName">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="MiddleName">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastName" HeaderText="Last Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TaxIdNumber" HeaderText="TaxId Number" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:BoundField DataField="AccountingCode" HeaderText="Accounting Code" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#E4EDF4" />
                                </asp:GridView>
                                <asp:ObjectDataSource ID="objBuyerList" runat="server" SelectMethod="GetBuyerList"
                                    TypeName="METAOPTION.BAL.ViewBuyerBAL"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="FooterContentDetails" width="100%" colspan="2">
                                <a href="#" class="AddNewExpenseTxt">Add New Buyer</a>
                           
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

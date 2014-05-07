<%@ Page Language="C#"  AutoEventWireup="true"
    CodeBehind="VendorList.aspx.cs" Inherits="METAOPTION.UI.VendorList" Title="HeadstartVMS::Vendor List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td class="TableHeadingBg TableHeading" colspan="6">
                Vendor List
            </td>
        </tr>
        <tr>
            <td class="TableBorder">
                Name
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtName" runat="server" CssClass="txt2" />
            </td>
            <td class="TableBorder">
                Category
            </td>
            <td class="TableBorder">
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="txt2" />
            </td>
            <td class="TableBorder" nowrap>
                Type
            </td>
            <td class="TableBorder">
                <asp:DropDownList ID="ddlType" runat="server" CssClass="txt2" />
            </td>
        </tr>
        <tr>
            <td class="TableBorder">
                City
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtCity" runat="server" CssClass="txt2" />
            </td>
            <td class="TableBorder">
                Country
            </td>
            <td class="TableBorder">
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txt2" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
            </td>
            <td class="TableBorder">
                State
            </td>
            <td class="TableBorder" nowrap>
                <asp:UpdatePanel ID="upState" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="txt2" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="TableBorder">
                Zip
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtZip" runat="server" CssClass="txt2" />
            </td>
            <td class="TableBorder">
                Status
            </td>
            <td class="TableBorder">
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txt2">
                    <asp:ListItem Text="ALL" Value="-1" Selected="True" />
                    <asp:ListItem Text="Active" Value="1"  />
                    <asp:ListItem Text="Archive" Value="2" />
                    <%--<asp:ListItem Text="Deleted" Value="0" />--%>
                </asp:DropDownList>
            </td>
            <td class="TableBorder" >
                <asp:UpdateProgress AssociatedUpdatePanelID="upDealerList" ID="uprogSearch" runat="server">
                    <ProgressTemplate>
                        <img src="../Images/Wait.gif" alt="Please Wait..." /><b style="vertical-align: middle">&nbsp;Pease
                            Wait...</b> </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
            <td class="TableBorder" colspan="2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td class="TableBorder" colspan="6">
                <asp:UpdatePanel ID="upDealerList" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvViewVendor" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="true" PageSize="20" GridLines="Both" EmptyDataText="No record found for selected criteria"
                            OnPageIndexChanging="gvViewVendor_PageIndexChanging" PagerSettings-Mode="NumericFirstLast">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "ViewVendor.aspx?Mode=View&EntityId="+Eval("VendorId")+"&type=3" %>'
                                            runat="server" ImageUrl="~/Images/Select.gif" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="Category" HeaderText="Vendor Category" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="Type" HeaderText="Vendor Type" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="CityStateZip" HeaderText="City/State/Zip" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="CountryName" HeaderText="Country Name" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" />
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
            <td class="FooterContentDetails" colspan="6" width="100%">
                <a href="AddVendor.aspx" class="AddNewExpenseTxt">Add New Vendor</a>
            </td>
        </tr>
    </table>
</asp:Content>

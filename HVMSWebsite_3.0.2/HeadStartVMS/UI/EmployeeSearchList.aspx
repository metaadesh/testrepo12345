<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSearchList.aspx.cs"
    Inherits="METAOPTION.UI.EmployeeSearchList" Title="HeadstartVMS::Employee List" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td class="TableHeadingBg TableHeading" colspan="6">
                Employee List
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
                Type
            </td>
            <td class="TableBorder">
                <asp:DropDownList ID="ddlType" runat="server" CssClass="txt2" />
            </td>
            <td class="TableBorder">
                City
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtCity" runat="server" CssClass="txt2" />
            </td>
        </tr>
        <tr>
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
            <td class="TableBorder">
                Zip
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtZip" runat="server" CssClass="txt2" />
            </td>
        </tr>
        <tr>
            <td class="TableBorder">
                Status
            </td>
            <td class="TableBorder" colspan="3">
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txt2">
                    <asp:ListItem Text="ALL" Value="-1" Selected="True" />
                    <asp:ListItem Text="Active" Value="1" />
                    <asp:ListItem Text="Archive" Value="2" />
                    <%--<asp:ListItem Text="Deleted" Value="0" />--%>
                </asp:DropDownList>
            </td>
            <td class="TableBorder" style="vertical-align: middle">
                <asp:UpdateProgress AssociatedUpdatePanelID="upDealerList" ID="uprogSearch" runat="server">
                    <ProgressTemplate>
                        <img src="../Images/Wait.gif" at="Please Wait..." />Pease Wait...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
            <td class="TableBorder" nowrap>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td class="TableBorder" colspan="6">
                <asp:UpdatePanel ID="upDealerList" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False" Width="100%"  EmptyDataText="No record found for selected criteria"
                            AllowPaging="true" PageSize="20" GridLines="None" OnPageIndexChanging="gvEmployee_PageIndexChanging"
                            PagerSettings-Mode="NumericFirstLast">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-Width="20px" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "ViewEmployee.aspx?Mode=View&EmployeeId="+Eval("EmployeeId")+"&type=5" %>'
                                            runat="server" ImageUrl="~/Images/Select.gif" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                    <ItemStyle Width="20px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="FirstName">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EmployeeType" HeaderText="Employee Type" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="City-State-Zip" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCityStateZip" runat="server" Text='<%# Eval("CityStateZip")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CountryCode" HeaderText="Country Name" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <%--<EmptyDataTemplate>
                                No Records Found
                            </EmptyDataTemplate>--%>
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
                <a href="AddEmployee.aspx" class="AddNewExpenseTxt">Add New Employee</a>
            </td>
        </tr>
    </table>
</asp:content>

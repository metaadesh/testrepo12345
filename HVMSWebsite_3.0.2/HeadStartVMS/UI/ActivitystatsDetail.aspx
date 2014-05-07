<%@ Page Language="C#" MasterPageFile="~/UI/MasterPageNoLeftPanel.Master" AutoEventWireup="true" CodeBehind="ActivitystatsDetail.aspx.cs" Inherits="METAOPTION.UI.ActivitystatsDetail" Title="HeadStart VMS :: Activity Stats Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnlUpper" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="dvTotal" runat="server" style="padding:2px; text-align:right" visible="false" class="TableHeadingBg TableHeading">
                <b>Grand Total:</b><asp:Label ID="lblTotal" runat="server" />
            </div>
            <asp:GridView ID="grddetail" runat="server" Width="100%" GridLines="Both" RowStyle-CssClass="gvRow" HeaderStyle-CssClass="gvHeading" CellPadding="5"
                    EmptyDataText="No record found for this search criteria." OnRowDataBound="grddetail_RowDataBound" OnPageIndexChanging="grddetail_PageIndexChanging">
        
                <HeaderStyle CssClass="gvHeading" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="gvAlternateRow" />
                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                <EmptyDataRowStyle HorizontalAlign="Center" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    
   <div style="clear: both;">
            <table id="table1" runat="server" border="0" style="width: 100%;" class="TableHeadingBg TableHeading">
                <tr>
                    <td style="width: 40%">
                        <asp:Label ID="lblCount" runat="server" BorderColor="Transparent" BackColor="Transparent"
                            ForeColor="#21618C" />
                    </td>
                    <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                        Page&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlPaging" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                            AutoPostBack="true" />
                        of
                        <%= gvAllActivity.PageCount%>
                    </td>
                    <td style="text-align: right; padding-right: 10px; color: #21618C">
                        Page size&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlPageSize1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="50" Value="50" Selected="True" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="250" Value="250" />
                        </asp:DropDownList>
                    </td>
                    <td style="white-space: nowrap; text-align: right">
                        <asp:Button ID="btnFirst" runat="server" Text="First" CssClass="btn" OnClick="btnFirst_Click" />
                        <asp:Button ID="btnPrev" runat="server" Text="Prev" CssClass="btn" OnClick="btnPrev_Click" />
                        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn" OnClick="btnNext_Click" />
                        <asp:Button ID="btnLast" runat="server" Text="Last" CssClass="btn" OnClick="btnLast_Click" />
                    </td>
                </tr>
            </table>
            <div>
                <asp:GridView ID="gvAllActivity" runat="server" Width="100%" CellPadding="5"
                    AutoGenerateColumns="True" EmptyDataText="No record found for this search criteria."
                    RowStyle-CssClass="gvRow" GridLines="Both"  HeaderStyle-CssClass="gvHeading" PagerSettings-Visible="false" AllowPaging="true"
                    PageSize="50" AllowSorting="true" CssClass="Grid" OnRowDataBound="gvAllActivity_RowDataBound" >
                                        
                    <RowStyle CssClass="gvRow" />
                    <HeaderStyle  CssClass="gvHeading" HorizontalAlign="Left"/>
                    <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right"/>
                    <EmptyDataRowStyle CssClass="gvEmpty" />
                </asp:GridView>
            </div>

            <table id="table2" runat ="server" border="0" style="width: 100%;" class="TableHeadingBg TableHeading">
                <tr>
                    <td style="width: 40%">
                        <asp:Label ID="lblCount1" runat="server" BorderColor="Transparent" BackColor="Transparent"
                            ForeColor="#21618C" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                        Page
                        <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                            AutoPostBack="true" />
                        of
                        <%= gvAllActivity.PageCount%>
                    </td>
                    <td style="text-align: right; padding-right: 10px; color: #21618C">
                        Page size&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlPageSize2" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="50" Value="50" Selected="True" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="250" Value="250" />
                        </asp:DropDownList>
                    </td>
                    <td style="white-space: nowrap; text-align: right">
                        <asp:Button ID="btnFirst1" runat="server" Text="First" CssClass="btn" OnClick="btnFirst_Click" />
                        <asp:Button ID="btnPrev1" runat="server" Text="Prev" CssClass="btn" OnClick="btnPrev_Click" />
                        <asp:Button ID="btnNext1" runat="server" Text="Next" CssClass="btn" OnClick="btnNext_Click" />
                        <asp:Button ID="btnLast1" runat="server" Text="Last" CssClass="btn" OnClick="btnLast_Click" />
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/UI/MasterPageNoLeftPanel.Master" AutoEventWireup="true"
    CodeBehind="LaneLog.aspx.cs" Inherits="METAOPTION.UI.LaneLog" Title="HeadStart VMS::Lane Log" %>

<asp:Content ID="contLaneLog" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="TableHeadingBg TableHeading" colspan="6">
                                SEARCH LANE HISTORY
                            </td>
                        </tr>
                    </table>
                    <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="TableBorderB">
                                    Inventory ID
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtInventoryID" runat="server" CssClass="txt2" />
                                </td>
                                <td class="TableBorderB">
                                    Field
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlField" runat="server" CssClass="txt2" />
                                </td>
                                <td class="TableBorderB">
                                    Added Between
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtAddedOnFrom" runat="server" CssClass="txt1" />
                                    <asp:ImageButton ID="imgAddedOnFrom" runat="server" ImageUrl="~/Images/calender-icon.gif" />
                                    <ajax:CalendarExtender ID="ceAddedOnFrom" runat="server" TargetControlID="txtAddedOnFrom"
                                        PopupButtonID="imgAddedOnFrom" />
                                    <span style="font-weight: bold; font-size: 12px; color: #535152; font-family: Arial, Helvetica, sans-serif;">
                                        And</span>
                                    <asp:TextBox ID="txtAddedOnTo" runat="server" CssClass="txt1" />
                                    <asp:ImageButton ID="imgAddedOnTo" runat="server" ImageUrl="~/Images/calender-icon.gif" />
                                    <ajax:CalendarExtender ID="ceAddedOnTo" runat="server" TargetControlID="txtAddedOnTo"
                                        PopupButtonID="imgAddedOnTo" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Added By
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlAddedBy" runat="server" CssClass="txt2" />
                                </td>
                                <td class="TableBorderB">
                                    Source
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSource" runat="server" CssClass="txt2" />
                                </td>
                                <td class="TableBorderB">
                                    Updated From
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtUpdatedFrom" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="right" style="padding: 5px 0px">
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn" Width="80px"
                                        OnClick="btnSearch_Click" />
                                </td>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="updMain">
                                        <ProgressTemplate>
                                            <div id="dvProg" class="overlay">
                                                <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                                wait...
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                            <tr>
                                <td style="text-align: right; padding-right: 10px; color: #21618C">
                                    Page size&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlPageSize1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="25" Value="25" />
                                        <asp:ListItem Text="50" Value="50" />
                                        <asp:ListItem Text="100" Value="100" />
                                        <asp:ListItem Text="250" Value="250" Selected="True" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvLaneLog" runat="server" AutoGenerateColumns="false" DataKeyNames="LaneHistoryId"
                            GridLines="None" Width="100%" RowStyle-CssClass="gvRow" CssClass="Grid" AlternatingRowStyle-CssClass="gvAlternateRow"
                            AllowPaging="true" EmptyDataText="No record found" PageSize="10" OnPageIndexChanging="gvLaneLog_PageIndexChanging"
                            AllowSorting="true" OnSorting="gvLaneLog_Sorting">
                            <Columns>
                                <asp:BoundField HeaderText="Inv ID" DataField="InventoryId" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridContent" SortExpression="InventoryId" />
                                <asp:BoundField HeaderText="Field" DataField="FieldName" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridContent" SortExpression="Field" />
                                <asp:BoundField HeaderText="Old Value" DataField="NewValue" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="New Value" DataField="CurrentValue" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridContent" />
                                <asp:TemplateField HeaderText="<div title='Is Announcement Created'>I.A.C</div>"
                                    ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <%# Convert.ToString(Eval("IsAnnouncementCreated")) == "1" ? "Yes" : "No"%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Added On" DataField="DateAdded" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridContent" SortExpression="DateAdded" ItemStyle-Width="120px" />
                                <asp:BoundField HeaderText="Added By" DataField="AddedBy" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridContent" SortExpression="AddedBy" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="Source" DataField="SourcePage" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridContent" SortExpression="Source" />
                                <asp:BoundField HeaderText="From" DataField="UpdateFrom" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridContent" SortExpression="UpdateFrom" />
                            </Columns>
                            <EmptyDataRowStyle CssClass="gvEmpty" />
                            <HeaderStyle CssClass="gvHeading" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function SetProgressPosition(e) {
            var posx = 0;
            var posy = 0;
            if (!e) var e = window.event;
            if (e.pageX || e.pageY) {
                posx = e.pageX;
                posy = e.pageY;
            }
            else if (e.clientX || e.clientY) {
                posx = e.clientX + document.documentElement.scrollLeft;
                posy = e.clientY + document.documentElement.scrollTop;
            }
            document.getElementById('dvProg').style.left = posx + 10 + "px";
            document.getElementById('dvProg').style.top = posy + "px";
        }
    </script>
</asp:Content>

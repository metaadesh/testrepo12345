<%@ Page Language="C#" MasterPageFile="~/UI/MasterPageNoLeftPanel.Master" AutoEventWireup="true" CodeBehind="ExportHistory.aspx.cs"
Inherits="METAOPTION.UI.ExportHistory" Title="HeadStart VMS::Export History" %>

<asp:Content ID="contExportHistory" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div onmousemove="SetProgressPosition(event)">
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="TableHeadingBg TableHeading" colspan="6">
                            SEARCH EXPORT HISTORY
                        </td>
                    </tr>
                </table>
                <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="TableBorderB">
                                File Name
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtFileName" runat="server" CssClass="txt3" />
                            </td>
                            <td class="TableBorderB">
                                Exported Between
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtAddedOnFrom" runat="server" CssClass="txt1" />
                                <asp:ImageButton ID="imgAddedOnFrom" runat="server" ImageUrl="~/Images/calender-icon.gif" />
                                <ajax:CalendarExtender ID="ceAddedOnFrom" runat="server" 
                                    TargetControlID="txtAddedOnFrom" PopupButtonID="imgAddedOnFrom" />
                                    <span style="font-weight:bold;font-size: 12px;color: #535152;font-family: Arial, Helvetica, sans-serif;">And</span>

                                <asp:TextBox ID="txtAddedOnTo" runat="server" CssClass="txt1" />
                                <asp:ImageButton ID="imgAddedOnTo" runat="server" ImageUrl="~/Images/calender-icon.gif" />
                                <ajax:CalendarExtender ID="ceAddedOnTo" runat="server" 
                                    TargetControlID="txtAddedOnTo" PopupButtonID="imgAddedOnTo" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Exported By
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlExportedBy" runat="server" CssClass="txt2" />
                            </td>
                            <td style="padding:5px 0px" align="center">
                                <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn" 
                                    Width="80px" OnClick="btnSearch_Click" />
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
                    <asp:GridView ID="gvExportHistory" runat="server" AutoGenerateColumns="false" DataKeyNames="ExportID"
                        GridLines="None" Width="100%" RowStyle-CssClass="gvRow" CssClass="Grid"
                        AlternatingRowStyle-CssClass="gvAlternateRow" AllowPaging="true" PageSize="20"
                        EmptyDataText="No record found"
                        OnPageIndexChanging="gvExportHistory_PageIndexChanging"
                        AllowSorting="true" OnSorting="gvExportHistory_Sorting" >
                        <Columns>
                            <asp:BoundField HeaderText="File Name" DataField="FileName" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" SortExpression="FileName" />
                            <asp:BoundField HeaderText="Record Count" DataField="RecordCount" ItemStyle-CssClass="GridContentNumbers" HeaderStyle-CssClass="GridContent" SortExpression="RecordCount" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Exported By" DataField="AddedBy" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" SortExpression="AddedBy" />
                            <asp:BoundField HeaderText="Exported On" DataField="DateAdded" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" SortExpression="DateAdded" ItemStyle-Width="150px" />
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
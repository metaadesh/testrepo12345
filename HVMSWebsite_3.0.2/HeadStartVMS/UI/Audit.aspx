<%@ Page Language="C#" MasterPageFile="~/UI/MasterPageNoLeftPanel.Master" AutoEventWireup="true" CodeBehind="Audit.aspx.cs"
Inherits="METAOPTION.UI.Audit" Title="HeadStart VMS::Audit" %>
<asp:Content ID="contAudit" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div onmousemove="SetProgressPosition(event)">
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="TableHeadingBg TableHeading" colspan="6">
                            SEARCH AUDIT
                        </td>
                    </tr>
                </table>
                <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="TableBorderB">
                                Table
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlTable" runat="server" CssClass="txt2" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTable_SelectedIndexChanged" />
                            </td>                            
                            <td class="TableBorderB">
                                Column
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlColumn" runat="server" CssClass="txt2" />
                            </td>
                            <td class="TableBorderB">
                                Row ID
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtRowID" runat="server" CssClass="txt2" />
                            </td>
                        </tr>
                        <tr>
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
                            <td class="TableBorderB">
                                Modified By
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlModifiedBy" runat="server" CssClass="txt2" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Added Between
                            </td>
                            <td class="TableBorder" colspan="2">
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
                            <td colspan="2" style="padding:5px 0px" align="right">
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
                    <asp:GridView ID="gvAudit" runat="server" AutoGenerateColumns="false" DataKeyNames="HistoryID"
                        GridLines="None" Width="100%" RowStyle-CssClass="gvRow" CssClass="Grid"
                        AlternatingRowStyle-CssClass="gvAlternateRow" AllowPaging="true" PageSize="20"
                        EmptyDataText="No record found"
                        OnPageIndexChanging="gvAudit_PageIndexChanging"
                        AllowSorting="true" OnSorting="gvAudit_Sorting" >
                        <Columns>
                            <asp:BoundField HeaderText="Table" DataField="TableName" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" SortExpression="TableName" />
                            <asp:BoundField HeaderText="HistoryID" DataField="HistoryID" ItemStyle-CssClass="GridContentNumbers" HeaderStyle-CssClass="GridContentNumbers" />
                            <asp:BoundField HeaderText="RowID" DataField="RowID" ItemStyle-CssClass="GridContentNumbers" HeaderStyle-CssClass="GridContentNumbers" SortExpression="RowID" />
                            <asp:BoundField HeaderText="Column" DataField="ColumnName" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" SortExpression="ColumnName" />
                            <asp:BoundField HeaderText="Old Value" DataField="OldValue" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                            <asp:BoundField HeaderText="New Value" DataField="NewValue" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                            <asp:BoundField HeaderText="Source" DataField="Source" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" SortExpression="Source" />
                            <asp:BoundField HeaderText="From" DataField="UpdateFrom" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" SortExpression="UpdateFrom" />
                            <asp:TemplateField HeaderText="Modified On" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" ItemStyle-Width="120px" SortExpression="ModifiedDate">
                                <ItemTemplate>
                                    <%#Eval("ModifiedDate")%> <br /> <%#Eval("ModifiedBy")%>
                                </ItemTemplate>
                            </asp:TemplateField>
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

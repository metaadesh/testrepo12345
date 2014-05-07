<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExpenseDetails.ascx.cs"
    Inherits="METAOPTION.UserControls.ExpenseDetails" %>
<table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
    <tr>
        <td>
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                class="arial-12">
                <tr>
                    <td class="TableHeadingBg TableHeading">
                        Expenses Types
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        <asp:GridView ID="grvexpensetypes" runat="server" AutoGenerateColumns="False" Width="100%"
                            DataKeyNames="EntityExpenseID" CssClass="gridView" CellPadding="4" GridLines="None"
                            AllowPaging="True" PageSize="50" PagerSettings-Mode="NumericFirstLast">
                            <Columns>
                                <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="10px" ItemStyle-Width="10px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DateAdded" HeaderText="Added On" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="10px" ItemStyle-Width="10px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MinCount" HeaderText="Min Count" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContentRight" SortExpression="" HeaderStyle-Width="10px"
                                    ItemStyle-Width="10px" ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaxCount" HeaderText="Max Count" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="10px" ItemStyle-Width="10px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DefaultPrice" HeaderText="Default Price($)" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="10px" ItemStyle-Width="10px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight"
                                    HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <a id="imgExpenseEdit" href='<%# "EditExpenses.aspx?ID="+Eval("EntityExpenseID")+"&TB_iframe=true&height=220&width=800" %>'
                                            title="Edit Details" class="thickbox">
                                            <img alt="" src="../Images/newedit.gif" border="0" /></a>
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Images/DeleteButton.png"
                                            OnClientClick="javascript:return confirm('Really do you want to delete?\n\nClick Ok to Confirm\nClick Cancel to ignore');"
                                            OnClick="ibtnDelete_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Expense found"></asp:Label>
                            </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="#E4EDF4" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        var EntityType = "<%= Session["LoginEntityTypeID"]%>";
       
        if (EntityType == "3") {
           $("table[id$='grvexpensetypes']").find('a[id="imgExpenseEdit"]').hide();
        }
    });
</script>

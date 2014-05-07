<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="ManageEntityExpenses.aspx.cs" Inherits="METAOPTION.UI.ManageEntityExpenses" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upExpense" runat="server">
        <ContentTemplate>
          <div class="RightPanel">
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td class="AddHeading">
                    Add Expense
                </td>
            </tr>
            <tr>
                <td align="left">
                    <fieldset class="ForFieldSet">
                        <legend class="ForLegend">Expense Details</legend>
                        <br>
                        <asp:GridView ID="gvExpenseList" runat="server" DataKeyNames="ExpenseTypeId" Width="100%"
                AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                PageSize="100" AllowSorting="true" CssClass="Grid" 
                            onrowdatabound="gvExpenseList_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true" HeaderStyle-Width="1%"
                        ItemStyle-Width="1%">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkallappr" runat="server" onclick="javascript:SelectAllCheckboxes1(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkappr" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" ItemStyle-CssClass="GridContent"
                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="5%" ItemStyle-Width="5%" />

                    <asp:TemplateField HeaderText="Min Count" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true" HeaderStyle-Width="1%"
                        ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtmincount" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Max Count" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true" HeaderStyle-Width="1%"
                        ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtmaxcount" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Default Price($)" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true"
                        HeaderStyle-Width="1%" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtprice" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
                <RowStyle CssClass="gvRow" />
                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                <EmptyDataRowStyle CssClass="gvEmpty" />
            </asp:GridView>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel"
                        Width="71px" CssClass="Btn_Form" OnClick="btnCancel_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Width="71px"
                        CssClass="Btn_Form" />
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>


            

            <script type="text/javascript">
                function ShowMsg(Message)
                { alert(Message); }

                function SelectAllCheckboxes1(chk) {
                    $('#<%=gvExpenseList.ClientID%>').find("input:checkbox").each(function () {
                        if (this != chk) { this.checked = chk.checked; }
                    });
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPageNoLeftPanel.Master"
    CodeBehind="AccountsPayable.aspx.cs" Inherits="METAOPTION.UI.AccountsPayable"
    Title="HeadStart VMS :: Accounts Payable" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        
        <tr>
            <td valign="top" align="left">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse" class="arial-12">
                    <tr>
                        <td class="TableHeadingBg TableHeading" colspan="2">
                            Accounts Payable
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" colspan="2" id="accountPayableTD">
                                                             
                            <mo:ExtGridView ID="grvUnpaidCarExpense" runat="server" DataKeyNames="ExpenseId" AutoGenerateColumns="false"
                                ShowHeader="true"  AllowPaging="true" PageSize="10"  AllowSorting="true"  Width="100%" OnPageIndexChanging="grvUnpaidCarExpense_PageIndexChanging"
                                OnSorting="grvUnpaidCarExpense_OnSorting" OrderBy="DealerName" EmptyDataText="No record found."  EmptyDataRowStyle-CssClass="GridEmptyRow" CssClass="Grid" >
                                <PagerSettings Mode="NumericFirstLast"  Visible="true" />
                                <Columns>  
                                    <asp:BoundField DataField="PurchaseDate" SortExpression="PurchaseDate" HeaderText="Purchase Date"
                                        DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Width="90px" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                    </asp:BoundField> 
                                    <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="GridHeader" HeaderText="Pay" ItemStyle-CssClass="GridContent">
                                        <ItemTemplate> 
                                            <asp:HiddenField ID="hdnEntityTypeId" Value='<%# Eval("EntityTypeId")%>' runat="server" />
                                            <asp:HiddenField ID="hdnEntityId" Value='<%# Eval("EntityId")%>' runat="server" />
                                            <asp:HiddenField ID="hdnSysID" Value='<%# Eval("SystemID")%>' runat="server" />
                                            <asp:LinkButton ID="lnkPay" CommandArgument='<%# Eval("ExpenseId")%>' OnClick="lnkPay_Click" runat="server" Text="Pay"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ExpenseAmount" SortExpression="ExpenseAmount" HeaderText="Expense" DataFormatString="{0:C}"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="68px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CarCost" SortExpression="CarCost" HeaderText="Car Cost" DataFormatString="{0:C}"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="68px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DealerName" SortExpression="DealerName" HeaderText="Dealer"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="200px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField> 
                                     <asp:BoundField DataField="Designation" SortExpression="Designation" HeaderText="Desig"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField> 
                                      <asp:BoundField DataField="TitlePresent" SortExpression="TitlePresent" HeaderText="Title"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField> 
                                    <asp:TemplateField SortExpression="VIN" HeaderStyle-Width="120px" HeaderStyle-CssClass="GridHeader" HeaderText="VIN" ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnInventoryId" Value='<%# Eval("InventoryId")%>' runat="server" /> 
                                            <asp:LinkButton ID="lnkVIN" CommandArgument='<%# Eval("ExpenseId")%>' OnClick="lnkVIN_Click" runat="server" Text='<%# Eval("VIN")%>' ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:BoundField DataField="Year" SortExpression="Year" HeaderText="Year"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Model" SortExpression="Model" HeaderText="Model"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="80px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="CarNote" SortExpression="CarNote" HeaderText="Car Note"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="200px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField> 
                                </Columns>                  
                                <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                            </mo:ExtGridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="FooterContentDetails" width="50%">
                        </td>
                        <td class="FooterContentDetails" width="50%" align="right">
                            <asp:DataPager ID="pager" runat="server" PageSize="200" PagedControlID="grvUnpaidCarExpense" >
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                        NextPreviousButtonCssClass="command"  />
                                    <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </td>
        </tr> 
    </table>
</asp:Content>

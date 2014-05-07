<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="DuplicateExpense.aspx.cs" 
    Inherits="METAOPTION.UI.DuplicateExpense" Title="Duplicate Expense" %>

<asp:Content ID="contDuplicateExpense" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <asp:GridView ID="gvDuplicateExpense" runat="server" Width="100%" AutoGenerateColumns="false" 
            AllowPaging="true" PageSize="250" OnPageIndexChanging="gvDuplicateExpense_PageIndexChanging"
            PagerStyle-CssClass="FooterContentDetails" PagerStyle-HorizontalAlign="Right">
            <Columns>
                <asp:BoundField HeaderText="Expense Date" DataField="ExpenseDate" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" DataFormatString="{0:MM/dd/yyyy}" />
                <asp:BoundField HeaderText="Name" DataField="EntityName" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                <asp:BoundField HeaderText="Amount($)" DataField="ExpenseAmount" DataFormatString="{0:#,###}" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentNumbers" />
                <asp:BoundField HeaderText="Expense Type" DataField="ExpenseType" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                <asp:BoundField HeaderText="Comments" DataField="Comments" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
            </Columns>    
        </asp:GridView>
    </div>
</asp:Content>
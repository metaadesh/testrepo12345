<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="ExpenseVsDepositReport.aspx.cs" 
Inherits="METAOPTION.UI.ExpenseVsDepositReport" Title="HeadStart VMS :: Accounting Report - Expense vs. Deposit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="updRptPanel" runat="server">
 <ContentTemplate>
    <fieldset class="ForFieldSet">
        <legend class="ForLegend">Accounting Report - Expense vs. Deposit</legend>
        <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
            <tr>
                <td class="TableBorderB" style="width:60px;">From Date</td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtStartDate" runat="server" onkeydown="return false;" CssClass="date" />
                    <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />                    
                    <ajax:CalendarExtender ID="Calendarextender1" runat="server" PopupButtonID="imgStartDate"
                                TargetControlID="txtStartDate" />
                </td>
                <td class="TableBorderB" style="width:60px;">To Date</td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtEndDate" runat="server" onkeydown="return false;" CssClass="date" />
                    <asp:Image ID="imgEndDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />                   
                            <ajax:CalendarExtender 
                                ID="txtEndDate_CalendarExtender" 
                                runat="server" 
                                PopupButtonID="imgEndDate"
                                TargetControlID="txtEndDate" />
                </td>
                <td class="TableBorderB" style="width:50px;">Filter By</td>
                <td class="TableBorder"><asp:DropDownList ID="ddlfilter" runat="server">
                    <asp:ListItem Value="E">Expense</asp:ListItem>
                    <asp:ListItem Value="D">Deposits</asp:ListItem>
                    <asp:ListItem Selected="True" Value="B">Both</asp:ListItem>
                    </asp:DropDownList>
                 </td>
                 <td class="TableBorder">
                     <asp:Button ID="btnView"
                     runat="server" 
                     CssClass="btn" 
                     Text="View Report" onclick="btnView_Click"/></td>
            </tr>
      </table>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
        <tr>
        <td>
            <asp:GridView ID="gvExpensebyMonthly_Daily" 
            runat="server" 
            GridLines="None"
            ShowFooter="true"
            AutoGenerateColumns="false" 
            ShowHeader="true"  
            AllowPaging="true" 
            PageSize="100" 
            DataKeyNames="ID" 
            AllowSorting="true"  
            Width="100%"
            PagerSettings-Mode="NumericFirstLast" 
            PagerSettings-Position="TopAndBottom"
            EmptyDataText="No record found for this search criteria!"  
            EmptyDataRowStyle-CssClass="GridEmptyRow" 
            HeaderStyle-CssClass="GridHeader"
            FooterStyle-CssClass="FooterContentDetails"
            PagerStyle-CssClass="FooterContentDetails"
            ItemStyle-CssClass="GridContent"
            OnRowDataBound="gvExpensebyMonthly_Daily_RowDataBound" 
            onpageindexchanging="gvExpensebyMonthly_Daily_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Date" HeaderText="Date"
                DataFormatString="{0:dd, MMM yyyy}" HeaderStyle-Width="90px" ItemStyle-CssClass="GridContent" ></asp:BoundField>
                <asp:TemplateField HeaderText="Description" HeaderStyle-Width="300px" 
                 ItemStyle-CssClass="GridContent" >
                    <ItemTemplate>
                        <asp:Label ID="lbldesc" Text='<%# Eval("Comments") %>'  runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblfdesc" Text="TOTAL" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expense" HeaderStyle-Width="90px" 
                 ItemStyle-CssClass="GridContent NumericGridContent" FooterStyle-CssClass="NumericGridContent" HeaderStyle-CssClass="NumericGridHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblexpense" Text='<%# String.Format("{0:C}",Eval("ExpenseAmount")) %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblexptotal" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deposit" HeaderStyle-Width="90px"  
                 ItemStyle-CssClass="GridContent NumericGridContent" FooterStyle-CssClass="NumericGridContent" HeaderStyle-CssClass="NumericGridHeader">
                    <ItemTemplate>
                        <asp:Label ID="lbldeposit" Text='<%# String.Format("{0:C}",Eval("DepositAmount")) %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lbldeposittotal" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RunningBalance" HeaderText="Running Balance" DataFormatString="{0:C}" ItemStyle-CssClass="GridContent NumericGridContent" HeaderStyle-CssClass="NumericGridHeader"></asp:BoundField>
            </Columns>
            <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
            <PagerSettings Visible="true"/>
            <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
        </td>
        </tr>
        </table>
    </fieldset>
 </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

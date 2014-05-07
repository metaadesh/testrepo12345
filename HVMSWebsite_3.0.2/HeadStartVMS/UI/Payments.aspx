<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="METAOPTION.UI.Payments"
    Title="HeadStart VMS :: Payments" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"> 
        <tr>
            <td valign="top" align="left">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                    class="arial-12">
                    <tr>
                        <td  class="TableHeadingBg TableHeading">
                            <asp:Label ID="lblResultTitle" Text="View All Payments" runat="server"></asp:Label>
                        </td>
                        <td align="right"  class="TableHeadingBg">
                        <asp:LinkButton ID="lnkViewAllPayment" Text="View  Payment Report" 
                                ToolTip="Click here to open view all payment report" runat="server" 
                                onclick="lnkViewAllPayment_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" colspan="2">
                                                             
                            <mo:ExtGridView ID="grvAllPayments" runat="server" DataKeyNames="PaymentId" AutoGenerateColumns="false"
                                ShowHeader="true"  AllowPaging="true" PageSize="10"  AllowSorting="true"  
                                Width="100%" OnPageIndexChanging="grvAllPayments_PageIndexChanging"
                                OnSorting="grvAllPayments_OnSorting" OrderBy="PaymentId" 
                                EmptyDataText="No record found."  EmptyDataRowStyle-CssClass="GridEmptyRow" 
                                CssClass="Grid" onrowdatabound="grvAllPayments_RowDataBound" >
                                <PagerSettings Mode="NumericFirstLast" Visible="true" />
                                <PagerSettings Position="TopAndBottom" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="35px" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:Image ID="imgNoprint" runat="server" ImageUrl="~/Images/no-print.gif" ToolTip="Already Printed"
                                                Visible='<%# (Convert.ToString(Eval("IsPrinted")).ToLower() == "true" && Convert.ToString(Eval("IsVoided")).ToLower() == "0") ? true : false %>' />
                                            <%--<asp:CheckBox ID="chkSelect" runat="server" Visible='<%# (Convert.ToString(Eval("IsPrinted")).ToLower() == "false" || Convert.ToString(Eval("IsPrinted")).ToLower() == "" ) ? true : false %>' />--%>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible="false" />
                                            <asp:Label ID="lblVoidText" runat="server" Text=""></asp:Label>
                                             <asp:HiddenField ID="hdIsCheckVoided"  Value='<%#Eval("IsVoided")%>' runat="server" />
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                   
                                    <asp:BoundField DataField="InvoiceNumber" SortExpression="InvoiceNumber" HeaderText="Invoice No."
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="100px" ItemStyle-CssClass="GridContent" ItemStyle-Wrap="true">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CheckDate" SortExpression="CheckDate" HeaderText="Check Date"
                                        DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Width="65px" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                    </asp:BoundField> 
                                    <asp:TemplateField SortExpression="CheckNumber" HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader" HeaderText="Check No."
                                        ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCheckNumber" CommandArgument='<%# Eval("PaymentId")%>' OnClick="lnkCheckNumber_OnClick"
                                                runat="server"><%# Eval("CheckNumber")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Amount" SortExpression="Amount" HeaderText="Amount" DataFormatString="{0:C}"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="68px" ItemStyle-CssClass="GridContentNumbers" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Right">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="recipienttype" SortExpression="recipienttype" HeaderText="Recipient Type"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="90px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="recipientname" SortExpression="recipientname" HeaderText="Recipient Name"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="150px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PeechtreeRefNumber" SortExpression="PeechtreeRefNumber" HeaderText="Pt Ref. No."
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="82px" ItemStyle-CssClass="GridContent" ItemStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DisplayName" SortExpression="DisplayName" HeaderText="Added By"
                                        HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="110px" ItemStyle-CssClass="GridContent">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DateAdded" SortExpression="DateAdded" HeaderText="Date Added"
                                        DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="70px"
                                        ItemStyle-CssClass="GridContent">
                                    </asp:BoundField> 
                                   <%-- <asp:TemplateField>
                                    <ItemTemplate>
                                    <asp:HiddenField ID="hdIsCheckVoided"  Value='<%#Eval("IsVoided")%>' runat="server" />
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                                                
                            </mo:ExtGridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="FooterContentDetails" width="50%">
                            <asp:LinkButton ID="lnkViewAllPayments" runat="server" class="AddNewExpenseTxt" OnClick="lnkViewAllPayments_Click">View All Payments Made</asp:LinkButton>
                        </td>
                        <td class="FooterContentDetails" width="50%" align="right">
                            <asp:DataPager ID="pager" runat="server" PageSize="50" PagedControlID="grvAllPayments" >
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
</asp:content>

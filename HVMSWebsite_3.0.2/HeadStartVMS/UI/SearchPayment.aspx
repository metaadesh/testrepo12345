<%@ Page Language="C#"  AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="SearchPayment.aspx.cs" Inherits="METAOPTION.UI.SearchPayment"
    Title="HeadStart VMS :: Search Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                <tr>
                    <td align="left">
                        <fieldset class="ForFieldSet">
                            <legend class="ForLegend">Search Payment</legend>
                            <asp:ValidationSummary ID="vsSearchPayment" runat="server" ShowSummary="true" />
                            <br />
                            <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse">
                                <tr>
                                    <td width="18%" class="GridContent_padding5">
                                        <b>Recipient Type</b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:DropDownList ID="ddlRecipientType" class="FormItem" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlRecipientType_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="Select One " Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="18%" class="GridContent_padding5">
                                        <b>Select Recipient</b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:HiddenField ID="hdnSelectedRecipientID" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnAccountingCode" runat="server" />
                                        <asp:Label ID="lblSelectedRecipientName" runat="server" Text=""></asp:Label>
                                        <asp:LinkButton ID="lnkSelect" runat="server" class="GridContent_Link" >Select</asp:LinkButton>
                                        <ajax:ModalPopupExtender ID="mpeSelectRecipient" runat="server" TargetControlID="lnkSelect"
                                            PopupControlID="pnlSelectRecipient" BackgroundCssClass="modalBackground" DropShadow="False"
                                            CancelControlID="btnCancel" PopupDragHandleControlID="pnlHeader">
                                        </ajax:ModalPopupExtender>
                                        <asp:Panel runat="server" ID="pnlSelectRecipient" Style="display: none; background: #f0f0f0;">
                                            <table border="0" width="800" cellpadding="0" class="PopUpBox">
                                                <tr>
                                                    <td class="PopUpBoxHeading">
                                                        <asp:Label ID="lblRecipientType" runat="server" Text="Recipient(s)"></asp:Label>
                                                    </td>
                                                    <td class="PopUpBoxHeading" align="right">
                                                        <asp:ImageButton ID="btnClosePopup" runat="server" CausesValidation="false" ImageUrl="~/Images/close.gif"
                                                            OnClick="btnClosePopup_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2" style="padding: 10px">
                                                        <table class="arial-12" cellpadding="0" border="0" width="100%" style="border-collapse: collapse;
                                                            margin-bottom: 20px;">
                                                            <tr>
                                                                <td class="GridContent_padding5">
                                                                    <div style="padding-left: 10px;">
                                                                        <b>Search: </b>
                                                                        <asp:DropDownList ID="ddlSearchField" runat="server" class="FormItem">
                                                                            <asp:ListItem Value="recipientname" Text="Name" Selected="True"> </asp:ListItem>
                                                                            <asp:ListItem Value="AccountingCode" Text="Accounting Code"> </asp:ListItem>
                                                                            <asp:ListItem Value="Street" Text="Street"> </asp:ListItem>
                                                                            <asp:ListItem Value="City" Text="City"> </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:DropDownList ID="ddlSearchOperator" runat="server" class="FormItem">
                                                                            <asp:ListItem Value="begins with">Begins With</asp:ListItem>
                                                                            <asp:ListItem Value="ends with">Ends With</asp:ListItem>
                                                                            <asp:ListItem Value="contains">Contains</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="txtSearchString" runat="server" class="FormItem"></asp:TextBox>
                                                                        <asp:Button ID="btnFilter" runat="server" Text="Filter" class="Btn_Form filterbtn"
                                                                            CausesValidation="false" OnClick="btnFilter_Click" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:GridView ID="grvRecipient" runat="server" DataKeyNames="recipientid" AutoGenerateColumns="false"
                                                            ShowHeader="true" AllowPaging="true" AllowSorting="true" PageSize="10" Width="100%" EmptyDataText="No record found."  EmptyDataRowStyle-CssClass="GridEmptyRow" CssClass="Grid"
                                                            OnPageIndexChanging="grvRecipient_PageIndexChanging" OnSorting="grvRecipient_OnSorting" >
                                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-Width="25px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                                    <ItemTemplate>
                                                                        <mo:GroupRadioButton ID="selectedRadioButton" runat="server" GroupName="recipients" />
                                                                        <asp:HiddenField ID="hdnAccCode" runat="server" Value='<%# Eval("AccountingCode")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="recipienttype" HeaderText="Recipient Type" Visible="false"
                                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="AccountingCode" SortExpression="AccountingCode" HeaderText="Acc. Code"
                                                                    HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="recipientname" SortExpression="recipientname" HeaderText="Name"
                                                                    HeaderStyle-Width="200px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Street" SortExpression="Street" HeaderText="Street" HeaderStyle-CssClass="GridHeader"
                                                                    ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="City" SortExpression="City" HeaderText="City" HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader"
                                                                    ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="State" SortExpression="State" HeaderText="State" HeaderStyle-Width="80px"
                                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="CountryCode" SortExpression="CountryCode" HeaderText="Country"
                                                                    HeaderStyle-Width="50px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="padding: 10px" align="center">
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="Btn_Form" CausesValidation="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnSelectRecipient" runat="server" Text="Select" CausesValidation="false"
                                                            class="Btn_Form" OnClick="btnSelectRecipient_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="18%" class="GridContent_padding5">
                                        <b>
                                            <asp:Label ID="lblCheckNumber" runat="server" Text="Check Number" AssociatedControlID="txtCheckNumber"></asp:Label>
                                        </b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:TextBox ID="txtCheckNumber" runat="server" class="FormItem" MaxLength="12"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="ftbCheckNumber" runat="server" TargetControlID="txtCheckNumber"
                                            FilterType="Numbers" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5" valign="top">
                                        <b>Check Date</b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="padding: 0px 5px 0px 0px">
                                                    Min.
                                                </td>
                                                <td style="padding: 0px 5px 0px 0px">
                                                    <ajax:CalendarExtender ID="calMinCheckDate" runat="server" TargetControlID="txtMinCheckDate"
                                                        Format="MM/dd/yyyy" PopupButtonID="btnMinCheckDate">
                                                    </ajax:CalendarExtender>
                                                    <asp:TextBox ID="txtMinCheckDate" runat="server" class="FormItem" Style="width: 60px;"></asp:TextBox>
                                                </td>
                                                <td style="padding: 0px 30px 0px 0px">
                                                    <asp:ImageButton ID="btnMinCheckDate" runat="server" ImageUrl="../images/calender-icon.gif"
                                                        CausesValidation="false" />
                                                </td>
                                                <td style="padding: 0px 5px 0px 0px">
                                                    Max
                                                </td>
                                                <td style="padding: 0px 5px 0px 0px">
                                                    <ajax:CalendarExtender ID="Calendarextender1" runat="server" TargetControlID="txtMaxCheckDate"
                                                        Format="MM/dd/yyyy" PopupButtonID="btnMaxCheckDate">
                                                    </ajax:CalendarExtender>
                                                    <asp:TextBox ID="txtMaxCheckDate" runat="server" class="FormItem" Style="width: 60px;"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnMaxCheckDate" runat="server" ImageUrl="../images/calender-icon.gif"
                                                        CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5" valign="top">
                                        <b>Amount</b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="padding: 0px 5px 0px 0px">
                                                    Min.
                                                </td>
                                                <td style="padding: 0px 32px 0px 0px">
                                                    <asp:TextBox ID="txtMinAmount" runat="server" class="FormItem" Style="width: 80px;"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID="ftbMinAmount" runat="server" TargetControlID="txtMinAmount"
                                                        FilterType="Custom,Numbers" ValidChars="." />
                                                    <asp:RegularExpressionValidator ID="revMinAmount" Display="None" runat="server" ControlToValidate="txtMinAmount"
                                                        ValidationGroup="vg" ErrorMessage="Enter a valid amount with max. upto two decimal point."
                                                        ValidationExpression="^\d+(\.\d{0,2})?$" />
                                                </td>
                                                <td style="padding: 0px 5px 0px 0px">
                                                    Max
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMaxAmount" runat="server" class="FormItem" Style="width: 80px;"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID="ftbMaxAmount" runat="server" TargetControlID="txtMaxAmount"
                                                        FilterType="Custom,Numbers" ValidChars="." />
                                                    <asp:RegularExpressionValidator ID="revMaxAmount" runat="server" Display="None" ControlToValidate="txtMaxAmount"
                                                        ErrorMessage="Enter a valid amount with max. upto two decimal point." ValidationExpression="^\d+(\.\d{0,2})?$" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5">
                                        <b>
                                            <asp:Label ID="lblInvoiceNumber" runat="server" Text="Invoice Number" AssociatedControlID="txtInvoiceNumber"></asp:Label></b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:TextBox ID="txtInvoiceNumber" runat="server" class="FormItem" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5">
                                        <b>
                                            <asp:Label ID="lblAccountNumber" runat="server" Text="Account Number" AssociatedControlID="ddlAccountNumber"></asp:Label></b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:DropDownList ID="ddlAccountNumber" runat="server" class="FormItem" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Select One "></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
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
                        <asp:Button ID="btnSearchPayments" runat="server" Text="Search Payments" class="Btn_Form"
                            PostBackUrl="~/UI/Payments.aspx" />
                    </td>
                </tr>
                <tr>
                    <td class="height30">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

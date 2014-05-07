<%@ Page Language="C#" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ViewAllPayments.aspx.cs" Inherits="METAOPTION.Reports.ViewAllPayments"
    Title="HeadStart VMS :: View All Payments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="updPnl" runat="server">
                    <ContentTemplate>
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td align="left" colspan="2">
                <fieldset class="ForFieldSet">
                    <legend class="ForLegend">Search Payment</legend>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td class="GridContent_padding5" valign="top">
                <b>Payment Date</b>
            </td>
            <td class="GridContent_padding5">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            Start Date
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true" class="FormItem" Style="width: 60px;"></asp:TextBox>
                            <ajax:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" TargetControlID="txtStartDate"
                                PopupButtonID="btnMinCheckDate1">
                            </ajax:CalendarExtender>
                        </td>
                        <td> &nbsp;
                            <asp:ImageButton ID="btnMinCheckDate1" runat="server" CausesValidation="false" ImageUrl="../images/calender-icon.gif" />
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            End Date
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true" class="FormItem" Style="width: 60px;"></asp:TextBox>
                            <ajax:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" TargetControlID="txtEndDate"
                                PopupButtonID="btnMaxCheckDate2">
                            </ajax:CalendarExtender>
                        </td>
                        <td> &nbsp;
                            <asp:ImageButton ID="btnMaxCheckDate2" runat="server" CausesValidation="false" ImageUrl="../images/calender-icon.gif" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="GridContent_padding5" width="18%">
                <b>Recipient Type</b>&nbsp;
            </td>
            <td class="GridContent_padding5">
                        <asp:DropDownList ID="ddlRecipientType" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="true" class="FormItem" OnSelectedIndexChanged="ddlRecipientType_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Text="Select One" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="rfvRecipientType" runat="server"  ControlToValidate="ddlRecipientType"
                           InitialValue="0"  ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="GridContent_padding5" width="18%">
                <b>Select Recipient</b>
            </td>
            <td class="GridContent_padding5">
                <asp:HiddenField ID="hdnSelectedRecipientID" runat="server" Value="0" />
                <asp:HiddenField ID="hdnAccountingCode" runat="server" />
                <asp:TextBox ID="lblSelectedRecipientName" CssClass="FormItem" ReadOnly="true" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvRecipeientName" runat="server" SetFocusOnError="true"  ControlToValidate="lblSelectedRecipientName"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:LinkButton ID="lnkSelect" runat="server" class="GridContent_Link" OnClick="lnkSelect_Click">Select</asp:LinkButton>
                <ajax:ModalPopupExtender ID="mpeSelectRecipient" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="btnCancel" DropShadow="False" PopupControlID="pnlSelectRecipient"
                    PopupDragHandleControlID="pnlHeader" TargetControlID="lnkSelect">
                </ajax:ModalPopupExtender>
                <asp:Panel ID="pnlSelectRecipient" runat="server" Style="display: none; background: #f0f0f0;">
                    <table border="0" cellpadding="0" class="PopUpBox" width="800">
                           <tr>
                            <td class="PopUpBoxHeading">
                                <asp:Label ID="lblRecipientType" runat="server" Text="Recipient(s)"></asp:Label>
                            </td>
                            <td align="right" class="PopUpBoxHeading">
                                <asp:ImageButton ID="btnClosePopup" runat="server" CausesValidation="false" ImageUrl="~/Images/close.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="padding: 10px">
                                <table border="0" cellpadding="0" class="arial-12" style="border-collapse: collapse;
                                    margin-bottom: 20px;" width="100%">
                                    <tr>
                                        <td class="GridContent_padding5">
                                            <div style="padding-left: 10px;">
                                                <b>Search: </b>
                                                <asp:DropDownList ID="ddlSearchField" runat="server" class="FormItem">
                                                    <asp:ListItem Value="begins with">Begins With</asp:ListItem>
                                                    <asp:ListItem Value="ends with">Ends With</asp:ListItem>
                                                    <asp:ListItem Value="contains">Contains</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSearchOperator" runat="server" class="FormItem">
                                                    <asp:ListItem Selected="True" Text="Name" Value="recipientname">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="Accounting Code" Value="AccountingCode">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="Street" Value="Street">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="City" Value="City">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtSearchString" runat="server" class="FormItem"></asp:TextBox>
                                                <asp:Button ID="btnFilter" runat="server" CausesValidation="false" class="Btn_Form filterbtn"
                                                    OnClick="btnFilter_Click" Text="Filter" />
                                            </div>
                                            
                                        </td>
                                    </tr>
                                </table>
                                 <asp:GridView ID="grvRecipient" runat="server" AllowPaging="true" 
                    AllowSorting="true" AutoGenerateColumns="false" CssClass="Grid" 
                    DataKeyNames="recipientid" EmptyDataRowStyle-CssClass="GridEmptyRow" 
                    EmptyDataText="No record found." 
                    OnPageIndexChanging="grvRecipient_PageIndexChanging" 
                    OnSorting="grvRecipient_OnSorting" PageSize="10" ShowHeader="true" Width="100%">
                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="25px" 
                            ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <mo:GroupRadioButton ID="selectedRadioButton" runat="server" 
                                    GroupName="recipients" />
                                <asp:HiddenField ID="hdnAccCode" runat="server" 
                                    Value='<%# Eval("AccountingCode")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="recipienttype" HeaderStyle-CssClass="GridHeader" 
                            HeaderText="Recipient Type" ItemStyle-CssClass="GridContent" Visible="false" />
                        <asp:BoundField DataField="AccountingCode" HeaderStyle-CssClass="GridHeader" 
                            HeaderStyle-Width="80px" HeaderText="Acc. Code" 
                            ItemStyle-CssClass="GridContent" SortExpression="AccountingCode" />
                        <asp:BoundField DataField="recipientname" HeaderStyle-CssClass="GridHeader" 
                            HeaderStyle-Width="200px" HeaderText="Name" ItemStyle-CssClass="GridContent" 
                            SortExpression="recipientname" />
                        <asp:BoundField DataField="Street" HeaderStyle-CssClass="GridHeader" 
                            HeaderText="Street" ItemStyle-CssClass="GridContent" SortExpression="Street" />
                        <asp:BoundField DataField="City" HeaderStyle-CssClass="GridHeader" 
                            HeaderStyle-Width="80px" HeaderText="City" ItemStyle-CssClass="GridContent" 
                            SortExpression="City" />
                        <asp:BoundField DataField="State" HeaderStyle-CssClass="GridHeader" 
                            HeaderStyle-Width="80px" HeaderText="State" ItemStyle-CssClass="GridContent" 
                            SortExpression="State" />
                        <asp:BoundField DataField="CountryCode" HeaderStyle-CssClass="GridHeader" 
                            HeaderStyle-Width="50px" HeaderText="Country" ItemStyle-CssClass="GridContent" 
                            SortExpression="CountryCode" />
                    </Columns>
                </asp:GridView>
                            </td>
                        </tr>
                     
                        <tr>
                            <td align="center" colspan="2" style="padding: 10px">
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" class="Btn_Form"
                                    Text="Cancel" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSelectRecipient" runat="server" CausesValidation="false" class="Btn_Form"
                                    OnClick="btnSelectRecipient_Click" Text="Select" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="GridContent_padding5" width="18%">
                <b>
                    <asp:Label ID="lblCheckNumber" runat="server" AssociatedControlID="txtCheckNumber"
                        Text="Check Number"></asp:Label>
                </b>
            </td>
            <td class="GridContent_padding5">
                <asp:TextBox ID="txtCheckNumber" runat="server" class="FormItem" MaxLength="12"></asp:TextBox>
                <ajax:FilteredTextBoxExtender ID="ftbCheckNumber" runat="server" FilterType="Numbers"
                    TargetControlID="txtCheckNumber" />
            </td>
        </tr>
        <tr>
            <td class="GridContent_padding5" valign="top">
                <b>Check Date</b>
            </td>
            <td class="GridContent_padding5">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            Min.
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtCheckStartDate" runat="server" ReadOnly="true" class="FormItem" Style="width: 60px;"></asp:TextBox>
                           
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckStartDate"
                                PopupButtonID="imgCheckStartDate">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgCheckStartDate" runat="server" CausesValidation="false" ImageUrl="../images/calender-icon.gif" />
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            Max
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtCheckEndDate" runat="server" ReadOnly="true" class="FormItem" Style="width: 60px;"></asp:TextBox>
                            
                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckEndDate"
                                PopupButtonID="imgCheckEndDate">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgCheckEndDate" runat="server" CausesValidation="false" ImageUrl="../images/calender-icon.gif" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="GridContent_padding5" valign="top">
                <b>Amount</b>
            </td>
            <td class="GridContent_padding5">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            Min.
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtMinAmount" runat="server" class="FormItem" Style="width: 60px;"></asp:TextBox>
                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <ajax:FilteredTextBoxExtender ID="ftbMinAmount" runat="server" FilterType="Custom,Numbers"
                                TargetControlID="txtMinAmount" ValidChars="." />
                            <asp:RegularExpressionValidator ID="revMinAmount" runat="server" ControlToValidate="txtMinAmount"
                                Display="None" ErrorMessage="Enter a valid amount with max. upto two decimal point."
                                ValidationExpression="^\d+(\.\d{0,2})?$" ValidationGroup="vg" />
                        </td>
                        <td>
                            Max
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtMaxAmount" runat="server" class="FormItem" Style="width: 60px;"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="ftbMaxAmount" runat="server" FilterType="Custom,Numbers"
                                TargetControlID="txtMaxAmount" ValidChars="." />
                            <asp:RegularExpressionValidator ID="revMaxAmount" runat="server" ControlToValidate="txtMaxAmount"
                                Display="None" ErrorMessage="Enter a valid amount with max. upto two decimal point."
                                ValidationExpression="^\d+(\.\d{0,2})?$" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="GridContent_padding5">
                <b>
                    <asp:Label ID="lblInvoiceNumber" runat="server" AssociatedControlID="txtInvoiceNumber"
                        Text="Invoice Number"></asp:Label>
                </b>
            </td>
            <td class="GridContent_padding5">
                <asp:TextBox ID="txtInvoiceNumber" runat="server" class="FormItem" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="GridContent_padding5">
                <b>
                    <asp:Label ID="lblAccountNumber" runat="server" AssociatedControlID="ddlAccountNumber"
                        Text="Account Number"></asp:Label>
                </b>
            </td>
            <td class="GridContent_padding5">
                <asp:DropDownList ID="ddlAccountNumber" runat="server" AppendDataBoundItems="true"
                    class="FormItem">
                    <asp:ListItem Text="Select One " Value="0"></asp:ListItem>
                </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="rfvAccountNumber" runat="server"  ControlToValidate="ddlAccountNumber"
                           InitialValue="0"  ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="center">
            <td class="GridContent_padding5" colspan="2" align="center">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnViewAllPayments" runat="server" class="Btn_Form" 
                    Text="View All Payments" onclick="btnViewAllPayments_Click" />
               
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

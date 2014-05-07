<%@ Page Language="C#"  AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="MakeANewPayment.aspx.cs" Inherits="METAOPTION.UI.MakeANewPayment"
    Title="Make A New Payment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upNewPayment" runat="server">
        <ContentTemplate>
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                <tr>
                    <td align="left">
                        <fieldset class="ForFieldSet">
                            <legend class="ForLegend">Make a New Payment</legend>
                            <br />
                            <asp:Label ID="lblError" runat="server" Text="" style="color:Red; font-weight:bold;"></asp:Label>
                            <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse">
                                <tr>
                                    <td class="GridContent_padding5">
                                        <b>Recipient Type</b>
                                    </td>
                                    <td class="GridContent_padding5">
                                        <asp:DropDownList ID="ddlRecipientType" class="FormItem" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlRecipientType_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="Select One " Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRecipientType" runat="server" ControlToValidate="ddlRecipientType"
                                            ErrorMessage="Select valid recipient type." Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
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
                                        <asp:LinkButton ID="lnkSelect" runat="server"  
                                            class="GridContent_Link" OnClick="lnkSelect_Click" >Select</asp:LinkButton>
                                             
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
                                                        <asp:ImageButton ID="btnClosePopup" runat="server" CausesValidation="false" ImageUrl="~/Images/close.gif" />
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
                                                                        <asp:Button ID="btnFilter" runat="server" Text="Filter" class="Btn_Form filterbtn" CausesValidation="false" OnClick="btnFilter_Click" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:GridView ID="grvRecipient" runat="server" DataKeyNames="recipientid" AutoGenerateColumns="false"
                                                            ShowHeader="true" AllowPaging="true" AllowSorting="true" PageSize="15" Width="100%" EmptyDataText="No record found."  EmptyDataRowStyle-CssClass="GridEmptyRow" CssClass="TableBorder" 
                                                            OnPageIndexChanging="grvRecipient_PageIndexChanging"
                                                            OnSorting="grvRecipient_OnSorting" PagerSettings-Mode="NumericFirstLast">
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
                                                                <asp:BoundField DataField="AccountingCode" SortExpression="AccountingCode" HeaderText="Acc. Code" HeaderStyle-Width="80px"
                                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="recipientname" SortExpression="recipientname" HeaderText="Name" HeaderStyle-Width="200px"
                                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="Street" SortExpression="Street" HeaderText="Street" HeaderStyle-CssClass="GridHeader"
                                                                    ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="City" SortExpression="City" HeaderText="City" HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader"
                                                                    ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="State"  SortExpression="State" HeaderText="State" HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader"
                                                                    ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="CountryCode"  SortExpression="CountryCode"  HeaderText="Country" HeaderStyle-Width="50px"
                                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"></asp:BoundField>
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
                                        <asp:TextBox ID="txtCheckNumber" runat="server" class="FormItem" MaxLength="12" onblur="DuplicateCheck(this)"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="ftbCheckNumber" runat="server" TargetControlID="txtCheckNumber"
                                            FilterType="Numbers" />
                                        <span id="spnDupCheck" style="display:inline-block">
                                           <!--To add HDML dynamically-->
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5">
                                        <b>
                                            <asp:Label ID="lblAmount" runat="server" Text="Amount" AssociatedControlID="txtAmount"></asp:Label></b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:TextBox ID="txtAmount" runat="server" class="FormItem" MaxLength="18"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="ftbAmount" runat="server" TargetControlID="txtAmount"
                                            FilterType="Custom,Numbers" ValidChars="+-." />
                                        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                            ErrorMessage="Enter a valid amount." Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revAmount" runat="server" ControlToValidate="txtAmount"
                                            ErrorMessage="Enter a valid amount with max. upto two decimal point." 
                                            ValidationExpression="^[-+]?[0-9]\d{0,9}(\.\d{1,2})?%?$" /> 
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5">
                                        <b>
                                            <asp:Label ID="lblCheckDate" runat="server" Text="Check Date" AssociatedControlID="txtCheckDate" ></asp:Label></b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <ajax:CalendarExtender ID="calCheckDate" runat="server" TargetControlID="txtCheckDate"
                                            Format="MM/dd/yyyy" PopupButtonID="btnCalender">
                                        </ajax:CalendarExtender>
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="padding: 0px 5px 0px 0px"> 
                                                    <asp:TextBox ID="txtCheckDate" runat="server" class="FormItem" Style="width: 80px;"  ></asp:TextBox>
                                                </td>
                                                <td style="padding: 0px 5px 0px 0px">
                                                    <asp:ImageButton ID="btnCalender" runat="server" CausesValidation="false" ImageUrl="../images/calender-icon.gif" />
                                                    <asp:RequiredFieldValidator ID="rfvCheckDate" runat="server" ControlToValidate="txtCheckDate"
                                                        ErrorMessage="Select a valid date." Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <%--<tr>
                                    <td class="GridContent_padding5">
                                        <b><asp:Label ID="lblBankName" runat="server" Text="Bank Name" AssociatedControlID="ddlBankName"></asp:Label></b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:DropDownList ID="ddlBankName" runat="server" class="FormItem" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Select One "></asp:ListItem>
                                        </asp:DropDownList> 
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="GridContent_padding5">
                                        <b>
                                            <asp:Label ID="lblAccountNumber" runat="server" Text="Account Number" AssociatedControlID="ddlAccountNumber"></asp:Label></b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:DropDownList ID="ddlAccountNumber" runat="server" class="FormItem" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Select One "></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAccountNumber" runat="server" ControlToValidate="ddlAccountNumber"
                                            ErrorMessage="Select valid account number." Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5" valign="top">
                                        <b>
                                            <asp:Label ID="lblComments" runat="server" Text="Comments" AssociatedControlID="txtComments"></asp:Label></b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:TextBox ID="txtComments" runat="server" class="FormItem" TextMode="MultiLine"
                                            Rows="4" Columns="29" MaxLength="45"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5" valign="top" colspan="2">
                                        <b><u>Note:</u>- </b>if this payment is for Vendor, Buyer or Dealer you need to
                                        select expenses against this payment on next screen.
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
                        <asp:Button ID="btnAddPayment" runat="server" Text="Add Payment" class="Btn_Form"
                              PostBackUrl="~/UI/ApplyPayments.aspx" />
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

    <script type="text/javascript">
        function DuplicateCheck(txt) {
            METAOPTION.WS.AutoFillNames.DuplicateCheckPayment(txt.value, onSuccess);
        }

        function onSuccess(val) 
        {
            var html = '';
            if (val.length > 0) 
            {
                html = '<div id="duplicateCheck" class="parent">Duplicate Check <br /><div id="duplicateCheckDetail" class="child"><table border="0" cellpadding="0" cellspacing="0">';
                html += "<tr><th>Check#</th><th>Amount($)</th><th>Added By</th><th>Check Date</th><th>Date Added</th><th>Recipient</th></tr>";
            }
            
            for (var i = 0; i < val.length; i++) 
            {
                html += i % 2 == 0 ? '<tr class="gvRow">' : '<tr class="gvAlternateRow">';
                html += '<td>' + val[i].CheckNo;
                html += '</td><td style="text-align:right">' + val[i].Amount;
                html += '</td><td>' + val[i].AddedBy;
                html += '</td><td>' + val[i].CheckDate;
                html += '</td><td>' + val[i].DateAdded;
                html += '</td><td>' + val[i].RecipientName + '(' + val[i].RecipientType + ')</td></tr>'

            }
            var dupCheck = '<%=DuplicateCheckMessage %>';
            if (val.length > 0)
                html += '</table></div></div>';

            document.getElementById("spnDupCheck").innerHTML = html;
            
            // Add confirmation message for duplicate entry
            if (val.length > 0) {
                $("#<%=btnAddPayment.ClientID %>").live("click", function () {
                    var answer = confirm(dupCheck);
                    if (answer)
                        return true;
                    else
                        return false;
                });
            }

            else {
                $("#<%=btnAddPayment.ClientID %>").die("click");
            }
        }

    </script>
    <style type="text/css">
        #duplicateCheck { color:Red; }
        #duplicateCheckDetail { color:#21618C; background:#fff; padding:0px; display:none;position:absolute; border:1px solid #123;}
        #duplicateCheck:hover #duplicateCheckDetail,#duplicateCheckDetail:hover{ display:block; }
        #duplicateCheck td{padding:4px; border:1px solid #f1f1f1}
        #duplicateCheck th { background-color:#E2F6FD;padding:4px; }
    </style>
</asp:Content>

<%@ Page Language="C#"  AutoEventWireup="true"
    CodeBehind="PaymentStatus.aspx.cs" Inherits="METAOPTION.UI.PaymentStatus" Title="HeadStart VMS :: Payment Status"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin:0px 20px 150px 20px ;">
        <asp:Label ID="lblMessage" runat="server" Text="" style="font-weight:bold; font-size:14px;"></asp:Label>
        <br />
        <asp:Label ID="lblPeachtreeUpdateStatus" runat="server" Text="" style="font-weight:bold; font-size:14px;"></asp:Label>
    </div>
    <div style="text-align:center">
        <asp:Button ID="btnExpenseAgainstPayment" runat="server" Text="Check this Payment" ToolTip="Click here to see or do some more action with this payment." class="Btn_Form" OnClick="btnExpenseAgainstPayment_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAddNewPayment" runat="server" Text="Add New Payment" ToolTip="Click here to make a new payment." class="Btn_Form" OnClick="btnAddNewPayment_Click" />
    </div>
</asp:Content>

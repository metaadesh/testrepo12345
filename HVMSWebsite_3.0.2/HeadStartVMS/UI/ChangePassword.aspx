<%@ Page Language="C#" AutoEventWireup="true" 
   CodeBehind="ChangePassword.aspx.cs" Inherits="HeadStartVMS.UI.ChangePassword" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphChangePassword" runat="server">
   <div class="AddHeading">
       Change Password
   </div>
   <div class="RightPanel">
      <fieldset class="ForFieldSet">
         <legend class="ForLegend">Change Password</legend>&nbsp;<div style="text-align: center">
         </div>
         <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
            width="100%">
            <tr>
               <td class="lblb" style="width: 25%">
                  <b>Old Password</b><span style="color: Red"> *</span>
               </td>
               <td class="lbl">
                  <asp:TextBox ID="txtOldPassword" runat="server" CssClass="txtMan2" TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword"
                     Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
               </td>
            </tr>
            <tr>
               <td class="lblb">
                  <b>New Password</b><span style="color: Red"> *</span>
               </td>
               <td class="lbl">
                  <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txtMan2" TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassword"
                     Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
               </td>
            </tr>
            <tr>
               <td class="lblb">
                  <b>Confirm Password</b><span style="color: Red"> *</span>
               </td>
               <td class="lbl">
                  <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txtMan2" TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword"
                     Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                     ControlToValidate="txtConfirmPassword" Display="Dynamic" ErrorMessage="Confirm password not matched with new Password"></asp:CompareValidator>
               </td>
            </tr>
            <tr>
               <td>
                   &nbsp;
               </td>
               <td style="color:Green">
                   &nbsp;
                  <asp:Label ID="lblStatus" runat="server"  />
               </td>
            </tr>
            <tr>
               <td>
                   &nbsp;
               </td>
               <td style="padding-top: 10px;">
                  <asp:Button ID="btnCancel" class="Btn_Form" runat="server" Text="Cancel" 
                       CausesValidation="False" PostBackUrl="~/UI/Default.aspx"  />
                   &nbsp;&nbsp;
                  <asp:Button ID="btnSave" class="Btn_Form" runat="server" Text="Update" OnClick="btnSave_Click" />
               </td>
            </tr>
         </table>
      </fieldset>
   </div>
</asp:Content>

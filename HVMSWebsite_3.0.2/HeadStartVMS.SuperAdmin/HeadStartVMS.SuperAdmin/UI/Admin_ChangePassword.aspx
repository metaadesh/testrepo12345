<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_ChangePassword.aspx.cs"
    Title="Admin Panel:: Change Password" Inherits="METAOPTION.UI.Admin_ChangePassword"
    MasterPageFile="~/UI/Admin_MasterLeftPanel.Master" %>

<asp:Content ID="changePaswor" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Change Password</legend>
            <div style="min-height: 400px;">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                    <tr style="background-color: White">
                        <td style="height: 15px;">
                            <asp:Label ID="ShowMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <b>Organization</b>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:DropDownList ID="ddl_Organization" runat="server" CssClass="txt3" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_Organization_selectedindexchange">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <b>Role</b>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:DropDownList ID="ddl_role" runat="server" CssClass="txt2" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_role_selectedindexchange">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <b>User Name</b>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:DropDownList ID="ddl_username" runat="server" CssClass="txt3" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_username_selectedindexchange">
                            </asp:DropDownList>
                            <asp:Label ID="lbl_usererrormsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%-- <tr>
                    <td class="GridContent_padding5">
                        <b>Old Password</b><b style="color: Red">*</b>
                    </td>
                    <td class="GridContent_padding5">
                        <asp:TextBox ID="txt_oldpasswd" runat="server" CssClass="txt2" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_oldpasswd"
                            ErrorMessage="Enter Old Password." ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>--%>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <b>New Password</b>&nbsp;<b style="color: Red">*</b>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:TextBox ID="txt_newpasswd" runat="server" CssClass="txt2" MaxLength="30" TextMode="Password"></asp:TextBox>
                            <asp:Label ID="lbl_newpassword" runat="server" ForeColor="Red" Text=""></asp:Label>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_newpasswd"
                            ErrorMessage="Enter New Password." ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <b>Confirm Password</b>&nbsp;<b style="color: Red">*</b>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:TextBox ID="txt_confirmpasswd" runat="server" CssClass="txt2" TextMode="Password"></asp:TextBox>
                            <asp:Label ID="lbl_confirmpassword" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_newpasswd"
                            ControlToValidate="txt_confirmpasswd" ErrorMessage="Passwords Do Not Match."
                            ForeColor="Red"></asp:CompareValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 40px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <%--colspan="2"--%>
                            <asp:Button ID="btn_cancel" runat="server" OnClick="btn_cancel_click" Text="<< Back"
                                Width="80px" class="Btn_Form" Style="margin-left: 20px;" />
                        </td>
                        <td style="white-space: nowrap;" aling="center">
                            <asp:Button ID="btn_update" runat="server" Text="Update" OnClick="btn_update_click"
                                Width="80px" class="Btn_Form" />
                        </td>
                    </tr>
                    <tr>
                        <td class="height30">
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
